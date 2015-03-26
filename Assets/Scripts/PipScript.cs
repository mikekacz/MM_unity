using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PipScript : MonoBehaviour {
	// colors
	public Material[] colorset;
	public bool active = true;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private int currentcolorindex = -1;
	
	void OnMouseDown ()
    {
		//catch if clicking through UI
		if (GameObject.Find ("/Canvas").transform.GetChild (0).gameObject.activeInHierarchy) {
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
