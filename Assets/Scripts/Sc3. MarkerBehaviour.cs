using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerBehaviour : MonoBehaviour {

	public float height;
	public float speed;
	public Vector2 direction;

	private GameObject jet;
	private GameObject shadow;
    private GameObject donut;

    // Use this for initialization
    void Start()
    {
        jet = this.transform.GetChild(0).gameObject;
        shadow = this.transform.GetChild(2).gameObject;
        donut = this.transform.GetChild(3).gameObject;


        shadow.transform.position += Vector3.down * 10;

        //Change saturation of shadow depending on height
        //shadow.GetComponent<Renderer>().material.color = new Color(height/15, height/15, height/15);


        Color clr = shadow.GetComponent<Renderer>().material.color;
        shadow.GetComponent<Renderer>().material.color = Color.Lerp(clr, Color.black, (height / 5));

        if (height < 0)
        //deal with negative height
        {
            shadow.GetComponent<Renderer>().enabled = false;
            donut.GetComponent<Renderer>().enabled = true;
        }

        else
        {
            donut.GetComponent<Renderer>().enabled = false;
            shadow.GetComponent<Renderer>().enabled = true;
        }

        

        direction.Normalize();

    }

    // Update is called once per frame
    void Update() {
        this.transform.position += new Vector3(direction.x * speed * Time.deltaTime, 0, direction.y * speed * Time.deltaTime);
        jet.transform.position += new Vector3(0, height - jet.transform.localPosition.y, 0);
        
        if (height < 0)
            //deal with negative height
        {
            float absHeight = Mathf.Abs(height);
            Debug.Log(absHeight);
            donut.transform.localScale = new Vector3(absHeight / 2, 1.0f, absHeight/2);
        }

        else
        { 
            shadow.transform.localScale = new Vector3((height / 2), 1.0f, (height / 2));
        }

    }

}
