
	using UnityEngine;
	using Mapbox.Utils;
	using Mapbox.Unity.Map;
	using Mapbox.Unity.MeshGeneration.Factories;
	using Mapbox.Unity.Utilities;
	using System.Collections.Generic;

	public class SpawnOnMap : MonoBehaviour
	{
		[SerializeField]
		AbstractMap _map;

		[SerializeField]
		[Geocode]
		List<string> _locationStrings;
        
		List<Vector2d> _locations;

		[SerializeField]
		GameObject _user;

		[SerializeField]
		float _spawnScale = 100f;

		[SerializeField]
		GameObject _markerPrefab;

		[SerializeField]
		float visitedWaypointDistance = 20;

		List<GameObject> _spawnedObjects;


		void Start()
		{

		}

		public void Initialize(List<Vector2d> _locations)
		{
			this._locations = _locations;
			_spawnedObjects = new List<GameObject>();

			//user's position
			var user_instance = Instantiate(_markerPrefab, transform);
			user_instance.transform.localPosition = _user.transform.position;
			user_instance.GetComponent<MeshRenderer>().enabled = false;

			//waypoint's position
			for (int i = 0; i < _locations.Count; i++)
			{
				/*
				var locationString = _locationStrings[i];
				_locations.Add(Conversions.StringToLatLon(locationString));*/
				var instance = Instantiate(_markerPrefab, transform);
				instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
				instance.transform.localPosition += new Vector3(0, 8, 0);
				instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
				_spawnedObjects.Add(instance);
			}
			_spawnedObjects.Add(user_instance);

			this.GetComponent<DirectionsFactory>().Initialize();        
		}

		private void LateUpdate()
		{

			int count = _spawnedObjects.Count;

			//Recalculate user's position
			_spawnedObjects[count - 1].transform.position = _user.transform.position;

			//Recalculate waypoint's position
			for (int i = 0; i < count - 1; i++)
			{
				var spawnedObject = _spawnedObjects[i];
				var location = _locations[i];
				spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
				spawnedObject.transform.localPosition += new Vector3(0, 8, 0);
				spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
			}

			if (count > 1)
			{
				//Distance between ther user and the next waypoint
				float nextWaypointDist = (float)Vector3.Distance(_spawnedObjects[0].transform.position, _spawnedObjects[count - 1].transform.position);
				//If the user is near the next waypoint, delete it			
				if (nextWaypointDist <= visitedWaypointDistance)
				{
					Destroy(_spawnedObjects[0]);
					_spawnedObjects.RemoveAt(0);
					_locations.RemoveAt(0);
				}
			}

		}
	}
