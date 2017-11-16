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
	private List<Marker> to_be_removed;

	private GameObject marker_space;

	void Start(){
		marker_space = new GameObject ("Markers");
		active_objects = new List<GameObject> ();
		to_be_removed = new List<Marker> ();
	}
	
	// Spawning prefabs
	void Update () {

		foreach(Marker m in objects){
			if (Time.time > m.spawn_time) {
				
				GameObject g  = Instantiate (m.prefab, m.initial_position, Quaternion.LookRotation(new Vector3(m.direction.x * -1, 0, m.direction.y * -1)), marker_space.transform);
				active_objects.Add (g);
				g.GetComponent<MarkerBehaviour> ().height = m.height;
				g.GetComponent<MarkerBehaviour> ().direction = m.direction;
				g.GetComponent<MarkerBehaviour> ().speed = m.speed;
				to_be_removed.Add (m);
			}
		}
		foreach (Marker m in to_be_removed) {
			objects.Remove (m);
		}
		to_be_removed.Clear ();

		for (int i = active_objects.Count - 1; i > -1; i--) {
			if (active_objects[i].transform.position.magnitude > radar_range) {
				Destroy (active_objects [i]);
				active_objects.RemoveAt (i);
			}
		}
	}
}
