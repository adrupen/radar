using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour {

	[System.Serializable]
	public struct Marker{
		public GameObject prefab;
		public Vector2 direction;
		public float height;
		public float speed;
		public float spawn_time;
		public Vector2 initial_position;
	};

	public List<Marker> objects;
	public float radar_range;

	private List<GameObject> active_objects;

	private GameObject marker_space;

	void Start(){
		marker_space = new GameObject ("Markers");
		active_objects = new List<GameObject> ();
	}
	
	// Spawning prefabs
	void Update () {
		foreach(Marker m in objects){
			if (Time.time > m.spawn_time) {
				GameObject g  = Instantiate (m.prefab, m.initial_position, Quaternion.identity, marker_space.transform);
				active_objects.Add (g);
				g.GetComponent<MarkerBehaviour> ().height = m.height;
				g.GetComponent<MarkerBehaviour> ().direction = m.direction;
				g.GetComponent<MarkerBehaviour> ().speed = m.speed;
				objects.Remove (m);
			}
		}
	}
}
