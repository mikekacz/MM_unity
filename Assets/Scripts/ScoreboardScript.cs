using UnityEngine;
using System.Collections;

public class ScoreboardScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		// F1 Key pressed
		if (Input.GetKeyDown(KeyCode.F1)) {
			this.gameObject.SetActive(false);
			GameObject.Find("/Canvas").transform.GetChild(0).gameObject.SetActive(true);
		}
	}
}
