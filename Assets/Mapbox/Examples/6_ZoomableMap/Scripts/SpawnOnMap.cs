namespace Mapbox.Examples
{
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
		string[] _locationStrings;
		Vector2d[] _locations;

		[SerializeField]
		GameObject _user;

		[SerializeField]
		float _spawnScale = 100f;

		[SerializeField]
		GameObject _markerPrefab;

		List<GameObject> _spawnedObjects;

		void Awake()
		{
			_locations = new Vector2d[_locationStrings.Length];
			_spawnedObjects = new List<GameObject>();
			var user_instance = Instantiate(_markerPrefab, transform);
			//user_instance.transform.localPosition = _user.transform.position;
			user_instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
			

			for (int i = 0; i < _locationStrings.Length; i++)
			{
				var locationString = _locationStrings[i];
				_locations[i] = Conversions.StringToLatLon(locationString);
				var instance = Instantiate(_markerPrefab,transform);
				instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
				instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
				_spawnedObjects.Add(instance);
			}
			_spawnedObjects.Add(user_instance);
		}

		private void Update()
		{
			
			Vector3 user_pos= _user.transform.position;
			Debug.Log(user_pos);
			int count = _spawnedObjects.Count;
			var spawnedObject = _spawnedObjects[count - 1];
			spawnedObject.transform.position = user_pos;
			Debug.Log(spawnedObject.transform.position);
			spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);


			for (int i = 0; i < count-1; i++)
			{
				spawnedObject = _spawnedObjects[i];
				var location = _locations[i];
				spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
				spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
			}
		}
	}
}