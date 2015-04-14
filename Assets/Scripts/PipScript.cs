using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PipScript : MonoBehaviour {
	// colors
	public Material[] colorset;
	public bool active = true;
	private float rotSpeed;
	
	// Use this for initialization
	void Start () {
		rotSpeed = Random.Range (0, 20);
	}
	
	// Update is called once per frame
	void Update () {
		if (active) {
			
			if (this.GetComponentInParent<Pips> () != null) {
				//rotate ball
				transform.Rotate (0, Time.deltaTime * rotSpeed, 0);


			}
		}
	}

	private int currentcolorindex = -1;
	
	void OnMouseDown ()
    {
		//catch if clicking through UI
		if (GameObject.Find ("/Canvas").transform.GetChild (0).gameObject.activeInHierarchy) {
			Debug.Log ("ui active");
			return;
		}
		if (GameObject.Find ("/Canvas").transform.GetChild (2).gameObject.activeInHierarchy) {
			Debug.Log ("ui active");
			return;
		} 
			//catch if row is active
			//catch if clicking on secret row
			if (active) {

				if (this.GetComponentInParent<Pips> () != null) {

					if (currentcolorindex == -1) {
						GetComponent<Renderer> ().material = colorset [0];
						currentcolorindex = 0;
					} else {
						currentcolorindex ++;
						if (currentcolorindex == colorset.Length)
							currentcolorindex = 0;
						GetComponent<Renderer> ().material = colorset [currentcolorindex];
					}
				}
			}

    }


}
