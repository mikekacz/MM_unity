using UnityEngine;
using System.Collections;

public class Secret : MonoBehaviour {
	public Material[] colorset;
	public GameObject[] pips;
	public Material[] secret;
	public Material mat_start;

	// Use this for initialization
	void Start () {
		//GenerateSecret ();

	}

	void Awake () {
		GenerateSecret ();
		HideSecret ();
	}

	void GenerateSecret () {
		secret = new Material[4];

		for (int i = 0 ; i < secret.Length ; i++){
			int rand = Random.Range (0, colorset.Length);
			secret[i] = colorset[rand];
		}
	}

	void HideSecret (){
		foreach (GameObject pip in pips){
			pip.GetComponent<Renderer>().material = mat_start;
		}

	}

	public void ShowSecret(){
		// apply materials - to be user in win/loose routine
		for (int i = 0 ; i < secret.Length ; i++){
			pips[i].GetComponent<Renderer>().material = secret[i];
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
