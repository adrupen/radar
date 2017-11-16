using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerBehaviour : MonoBehaviour {

	public float height;
	public float speed;
	public Vector2 direction;

	private GameObject jet;
	private GameObject line;

	// Use this for initialization
	void Start () {
		jet = this.transform.GetChild (0).gameObject;
		line = this.transform.GetChild (1).gameObject;
		line.transform.LookAt (new Vector3 (0, 0, 1));
		//line.transform.rotation = Quaternion.FromToRotation(transform.rotation.eulerAngles, new Vector3(1, 0, 0));

		direction.Normalize ();
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += new Vector3(direction.x * speed * Time.deltaTime, 0, direction.y * speed * Time.deltaTime);
		jet.transform.position += new Vector3(0, height - jet.transform.localPosition.y, 0);
		line.transform.localScale = new Vector3 (1.0f, height, 1.0f);
	}
}
