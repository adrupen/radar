using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerBehaviour : MonoBehaviour {

	[Header("General marker variables")]
	public float height;
	public float speed;
	public Vector2 direction;
	public bool is2D = false;

	[Header("2D marker variables")]
	public float shadowScaleFactor = 2;
	public float heightTintFactor = 5;

	private GameObject jet;
	private GameObject line;
	private GameObject shadow;
	private GameObject donut;

	// Use this for initialization
	void Start () {
		jet = transform.GetChild (0).gameObject;
		line = transform.GetChild (1).gameObject;
		shadow = transform.GetChild(2).gameObject;
		donut = transform.GetChild(3).gameObject;

		if (is2D)
			setup2Dmarker ();
		else
			setup3Dmarker ();

		direction.Normalize ();
	}

	void setup3Dmarker(){
		line.transform.LookAt (new Vector3 (0, 0, 1));

		donut.SetActive (false);
	}

	void setup2Dmarker(){

		line.SetActive (false);

		// Put shadow on a plane y = -10, to awoid occluding jets below y = 0
		shadow.transform.position += Vector3.down * 10;

		Color clr = shadow.GetComponent<Renderer>().material.color;
		// Darken it a bit, so jet and shadow don't meld together
		clr -= new Color (0.1f, 0.1f, 0.1f, 0f);

		if (height < 0)
			//deal with negative height
		{
			shadow.SetActive(false);
			donut.SetActive(true);
			float absHeight = Mathf.Abs(height);
			donut.transform.localScale = new Vector3(absHeight / shadowScaleFactor, 1.0f, absHeight/shadowScaleFactor);
			donut.GetComponent<Renderer>().material.color = Color.Lerp(clr, Color.black, (height / heightTintFactor));
		}

		else
		{
			donut.SetActive(false);
			shadow.SetActive(true);
			shadow.transform.localScale = new Vector3((height / shadowScaleFactor), 1.0f, (height / shadowScaleFactor));
			shadow.GetComponent<Renderer>().material.color = Color.Lerp(clr, Color.black, (height / heightTintFactor));
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (is2D)
			markerTranslate2D ();
		else
			markerTranslate3D();

	}

	void markerTranslate3D(){
		transform.position += new Vector3(direction.x * speed * Time.deltaTime, 0, direction.y * speed * Time.deltaTime);
		jet.transform.position += new Vector3(0, height - jet.transform.localPosition.y, 0);
		line.transform.localScale = new Vector3 (1.0f, height, 1.0f);
	}

	void markerTranslate2D(){
		transform.position += new Vector3(direction.x * speed * Time.deltaTime, 0, direction.y * speed * Time.deltaTime);
	}
}
