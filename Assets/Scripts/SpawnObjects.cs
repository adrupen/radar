using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Marker{
	public GameObject prefab;
	public Vector2 direction;
	public float height;
	public float speed;
	public float spawn_time;
	public Vector2 initial_position;
	public bool is2D;
	public bool isMostDangerous;
};

[RequireComponent(typeof(EventChain))]
public class SpawnObjects : MonoBehaviour {

	public List<Marker> objects;
	public float radar_range;
    public float labelOffset = 50f;

	private List<GameObject> active_objects;
	private List<Marker> to_be_removed;

    private GameObject labelsParent;
    private Text[] jetLabels;
    private RectTransform[] jetLabelsTransforms;
    private Camera labelCamera;

	private GameObject marker_space;
	private float clockOffset = 0f;
	private bool gamePaused = true;
    private float reactionTime = 0;

    // override, force markers to behave as 2d markers
    private bool is2D = false;

void Start(){
		marker_space = new GameObject ("Markers");
		active_objects = new List<GameObject> ();
		to_be_removed = new List<Marker> ();
        labelsParent = GameObject.Find("JetLabels");

        jetLabels = labelsParent.transform.GetComponentsInChildren<Text>();
        jetLabelsTransforms = new RectTransform[3];
        for (int i=0; i < jetLabels.Length; i++) {
            jetLabels[i].text = (i + 1).ToString();
            jetLabelsTransforms[i] = jetLabels[i].gameObject.GetComponent<RectTransform>();
        }
    }
	
	// Spawning prefabs
	void Update () {
		if (!gamePaused) {
			foreach (Marker m in objects) {
				if ((Time.time - clockOffset) > m.spawn_time) {
					Vector3 worldPosition = new Vector3 (m.initial_position.x, 0, m.initial_position.y);
					GameObject g = Instantiate (m.prefab, worldPosition, Quaternion.LookRotation (new Vector3 (m.direction.x * -1, 0, m.direction.y * -1)), marker_space.transform);
					active_objects.Add (g);
					g.GetComponent<MarkerBehaviour> ().height = m.height;
					g.GetComponent<MarkerBehaviour> ().direction = m.direction;
					g.GetComponent<MarkerBehaviour> ().speed = m.speed;
                    if(is2D)
                        g.GetComponent<MarkerBehaviour>().is2D = true;
                    else
                        g.GetComponent<MarkerBehaviour>().is2D = m.is2D;
                    to_be_removed.Add (m);
				}
			}
			foreach (Marker m in to_be_removed) {
				objects.Remove (m);
			}
			to_be_removed.Clear ();

			for (int i = active_objects.Count - 1; i > -1; i--) {
				if (active_objects [i].transform.position.magnitude > radar_range) {
					Destroy (active_objects [i]);
					active_objects.RemoveAt (i);
				}
			}

            trackLabels();
		}
	}

    public void setLabelCamera(Camera newCamera) { labelCamera = newCamera; }

    void trackLabels() {
        if (labelCamera == null)
            return;

        for(int i=0; i < jetLabelsTransforms.Length; i++) {
            Vector3 labelPos = labelCamera.WorldToScreenPoint(active_objects[i].transform.position);
            labelPos.y -= labelOffset;
            jetLabelsTransforms[i].anchoredPosition = labelPos;
        }
    }

	public void populateMarkers(List<Marker> markers){
		foreach(Marker m in markers){
			objects.Add (m);
		}
	}

	public void clearMarkers(){
		for (int i = active_objects.Count - 1; i > -1; i--) {
			Destroy (active_objects [i]);
			active_objects.RemoveAt (i);
		}
		objects.Clear ();
	}

	public void startGame(){
		clockOffset = Time.time;
		gamePaused = false;
	}

	public void pauseGame(){
        reactionTime = Time.time - clockOffset;
		gamePaused = true;
	}

    public float getReactionTime() { return reactionTime; }

    public void setIs2d(bool _is2D) { is2D = _is2D; }
}
