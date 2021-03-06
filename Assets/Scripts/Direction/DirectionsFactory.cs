namespace Mapbox.Unity.MeshGeneration.Factories
{
	using UnityEngine;
	using Mapbox.Directions;
	using System.Collections.Generic;
	using System.Linq;
	using Mapbox.Unity.Map;
	using Data;
	using Modifiers;
	using Mapbox.Utils;
	using Mapbox.Unity.Utilities;
	using System.Collections;

	public class DirectionsFactory : MonoBehaviour
	{
		[SerializeField]
		AbstractMap _map;

		[SerializeField]
		MeshModifier[] MeshModifiers;
		[SerializeField]
		Material _material;

		[SerializeField]
		List<Transform> _waypoints;
		private List<Vector3> _cachedWaypoints;

		[SerializeField]
		[Range(1,10)]
		private float UpdateFrequency = 2;



		private Directions _directions;
		private int _counter;

		GameObject _directionsGO;
		private bool _recalculateNext;

		protected virtual void Awake()
		{
			if (_map == null)
			{
				_map = FindObjectOfType<AbstractMap>();
			}
			_directions = MapboxAccess.Instance.Directions;
			//_map.OnInitialized += Query;
			_map.OnUpdated += Query;
		}

        //Initilization of the DirectionFacotry
        public void Initialize()
        {
			foreach (Transform child in transform)
			{
				_waypoints.Add(child);
			}
			_cachedWaypoints = new List<Vector3>(_waypoints.Count);
			foreach (var item in _waypoints)
			{
				_cachedWaypoints.Add(item.position);
			}
			_recalculateNext =	false;

			foreach (var modifier in MeshModifiers)
			{
				modifier.Initialize();
			}

			StartCoroutine(QueryTimer());
		}

        protected virtual void OnDestroy()
		{
			_map.OnInitialized -= Query;
			_map.OnUpdated -= Query;
		}

		//Mapbox Direction Query to get the itinirary path
		void Query()
		{
			var count = _waypoints.Count;
			var wp = new Vector2d[count];
			for (int i = 0; i < count; i++)
			{
				wp[i] = _waypoints[i].GetGeoPosition(_map.CenterMercator, _map.WorldRelativeScale);
			}
			var _directionResource = new DirectionResource(wp, RoutingProfile.Walking);
			_directionResource.Steps = true;
			_directions.Query(_directionResource, HandleDirectionsResponse);
		}

        //Real time update with a custom frequency
		public IEnumerator QueryTimer()
		{
			while (true)
			{
                //Path Update frequency
				yield return new WaitForSeconds(UpdateFrequency);

				_waypoints = new List<Transform>();

                //Get all the waypoints from the scene and add them to the list (Spawned from SpawnOnMap)
                foreach (Transform child in transform)
				{
					_waypoints.Add(child);
				}

				for (int i = 0; i < _waypoints.Count; i++)
				{
					if (_waypoints[i].position != _cachedWaypoints[i])
					{
						_recalculateNext = true;
						_cachedWaypoints[i] = _waypoints[i].position;
					}
				}

				if (_recalculateNext && _waypoints.Count>1)
				{
					Query();
					_recalculateNext = false;
				}

				if (_waypoints.Count <= 1)
				{
					Destroy(_directionsGO);
				}
			}
		}

        //Handle the response from the Mapbox Direction Query
        void HandleDirectionsResponse(DirectionsResponse response)
		{
			if (response == null || null == response.Routes || response.Routes.Count < 1)
			{
				return;
			}

			var meshData = new MeshData();
			var dat = new List<Vector3>();
			foreach (var point in response.Routes[0].Geometry)
			{
				dat.Add(Conversions.GeoToWorldPosition(point.x, point.y, _map.CenterMercator, _map.WorldRelativeScale).ToVector3xz());
			}

			var feat = new VectorFeatureUnity();
			feat.Points.Add(dat);

			foreach (MeshModifier mod in MeshModifiers.Where(x => x.Active))
			{
				mod.Run(feat, meshData, _map.WorldRelativeScale);
			}

            //Create the GameObject for the path
            CreateGameObject(meshData);
		}

        //Mesh creation for the path
		GameObject CreateGameObject(MeshData data)
		{
			if (_directionsGO != null)
			{
				_directionsGO.Destroy();
			}
			_directionsGO = new GameObject("direction waypoint " + " entity");
			var mesh = _directionsGO.AddComponent<MeshFilter>().mesh;
			mesh.subMeshCount = data.Triangles.Count;

			mesh.SetVertices(data.Vertices);
			_counter = data.Triangles.Count;
			for (int i = 0; i < _counter; i++)
			{
				var triangle = data.Triangles[i];
				mesh.SetTriangles(triangle, i);
			}

			_counter = data.UV.Count;
			for (int i = 0; i < _counter; i++)
			{
				var uv = data.UV[i];
				mesh.SetUVs(i, uv);
			}

			mesh.RecalculateNormals();
			_directionsGO.AddComponent<MeshRenderer>().material = _material;
			return _directionsGO;
		}
	}

}
