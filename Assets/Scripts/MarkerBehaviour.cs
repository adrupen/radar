using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerBehaviour : MonoBehaviour {

	public float height;

	private GameObject jet;
	private GameObject line;

	// Use this for initialization
	void Start () {
		jet = this.transform.GetChild (0).gameObject;
		line = this.transform.GetChild (1).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos;
		pos.x = this.transform.position.x;
		pos.y = height;
		pos.z = this.transform.position.z;
		jet.transform.position = pos;
		line.transform.localScale = new Vector3 (1.0f, height, 1.0f);
	}
}
