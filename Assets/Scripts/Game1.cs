using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game1 : MonoBehaviour {
	public float countdown = 5*60;
	public float starttime ;
	public float stoptime ;
	public float restseconds = 5*60;
	public GameObject secret;
	public int currentrow ;
	public GameObject[] Rows;
	public float bestguess;

	public int Score;
	
	public Material colorMatch;
	public Material colorPartialMatch;

	public bool alreadyStopped = false;

	// Use this for initialization
	void Start () {
		if (Global.initialized) {
			StartClock ();
		} else {
			GameObject.Find("/Canvas").transform.GetChild(1).gameObject.SetActive(false);
			GameObject.Find("/Canvas").transform.GetChild(0).gameObject.SetActive(true);
			Global.initialized = true;
		}
	}

	public void StartClock (){
		starttime = Time.time;
		stoptime = starttime + countdown;
		currentrow = 0;
	}

	public void ResetGame() {
		Application.LoadLevel("StartGame");
	}

	public void CheckGuess(){
		//check if UI is active
		if (GameObject.Find ("/Canvas").transform.GetChild (0).gameObject.activeInHierarchy) {
			Debug.Log ("ui active");
			return;
		} 
		//calculate guess
		float guess = 0.0f;
		// array containing secret colors
		System.Collections.Generic.List<string> colors_secret = new List<string>();
		// array conatining selected colors
		System.Collections.Generic.List<string> colors_selected = new List<string>();
	
		GameObject[] list;
		//list = GameObject.Find ("ScoreBoard").GetComponent<Game1> ().Rows [currentrow].transform.GetChild (2).gameObject.GetComponent<Pips> ().pip_list;
		list = GameObject.Find ("Rows/Row " + currentrow + "/Pips").GetComponent<Pips> ().pip_list;


		//fill arrays
		for (int i=0; i <4; i++) {
			colors_secret.Add(secret.GetComponent<Secret>().secret[i].name.Replace(" (Instance)",""));
			colors_selected.Add(list[i].GetComponent<Renderer>().material.name.Replace(" (Instance)",""));
		}


		int colors_correct = 0;
		int colors_semicorrect = 0;
		//collection of items to be removed
		System.Collections.Generic.List<string> colors_correct_list = new List<string>();

		//loop for color matches
		for (int i=0; i <4; i++) {
			if (colors_secret[i] == colors_selected[i]){
				colors_correct++;
				colors_correct_list.Add(colors_secret[i]);
			}

		}

		//loop removing correct elements
		for (int i=0; i < colors_correct_list.Count; i++) {
			colors_secret.Remove(colors_correct_list[i]);
			colors_selected.Remove(colors_correct_list[i]);
		}

		for (int i=0; i < colors_selected.Count; i++) {
			for (int k=0; k < colors_secret.Count; k++){
				if (colors_selected[i] == colors_secret[k]) {
					colors_semicorrect ++;
					colors_secret.Remove(colors_secret[k]);
					colors_selected.Remove(colors_selected[i]);
					i--;
					break;
				}
			}
		}




		guess = (colors_correct * 25f ) + (colors_semicorrect *12.5f);
		if (guess > bestguess)
			bestguess = guess;



		//disable clicks on old row
		GameObject[] oldPips = GameObject.Find ("Rows/Row " + currentrow + "/Pips").GetComponent<Pips> ().pip_list;
		foreach (GameObject pip in oldPips){
			pip.GetComponent<PipScript>().active = false;
		}

		//hide button
		GameObject.Find ("Rows/Row " + currentrow + "/Canvas/Button").SetActive (false);
		//show feedback
		GameObject.Find ("Rows/Row " + currentrow + "/Ans").SetActive (true);
		//fill feedback
		GameObject ans = GameObject.Find ("Rows/Row " + currentrow + "/Ans");
		for (int i= 0; i<(colors_correct + colors_semicorrect); i++) {
			if (i > colors_correct-1){
				ans.transform.GetChild(i).GetComponent<Renderer>().material = colorPartialMatch;
			}
			else {
				ans.transform.GetChild(i).GetComponent<Renderer>().material = colorMatch;
			}
			ans.transform.GetChild(i).gameObject.SetActive(true);
		}

		//validate Win
		if (bestguess == 100)
			EvaluateWin ();
		else if (currentrow == 9) {
			//last row
			EvaluateWin();
		}
		else {
			//activate new row
			currentrow++;
			GameObject newRow = GameObject.Find ("Rows/Row " + currentrow);
			newRow.transform.GetChild(0).gameObject.SetActive(true);
			newRow.transform.GetChild(2).gameObject.SetActive(true);
		}
	}
	// Calculate Score
	void prepareScore(){

		if (!alreadyStopped) {
			float guiTime = (Time.time < stoptime) ? Time.time - starttime : stoptime - starttime;  //warning hidden IF statement
			restseconds = countdown - (guiTime);
		} 
		//else 
		//	restseconds = 0;

		float ratio_guess = bestguess / 100f;
		float ratio_tries = (((10f - currentrow) * 2.5f / 10f) + 7.5f) / 10f; // 0row -> 1.0 ; 10row -> .75
		float ratio_time = ((restseconds / countdown) * 0.5f) +0.5f; // 0sec -> 1.0 ; 600s -> .5
		
		//Debug.Log(ratio_guess);
		//Debug.Log(ratio_tries);
		//Debug.Log(ratio_time);
		
		Score = Mathf.CeilToInt(10000f * ratio_guess * ratio_tries * ratio_time);
	}


	// Update is called once per frame
	void OnGUI (){


		prepareScore ();

		int roundedRestSeconds = Mathf.CeilToInt(restseconds);
		int displaySeconds = roundedRestSeconds % 60;
		int displayMinutes = roundedRestSeconds / 60; 
		string textleft = string.Format ("{0:00}:{1:00}", displayMinutes, displaySeconds); 

		GameObject.Find("/Canvas/Panel 1/Time").GetComponent<UnityEngine.UI.Text>().text = "Time Left: "+textleft;
		//GetComponent<UnityEngine.UI.Text> ().text = "Time Left: "+textleft;
		//GEtComponent<UnityEngine.UI.Text> ().text += "\n\rCurrent Row:\n\r "+(currentrow+1);
		//GetComponent<UnityEngine.UI.Text> ().text += "\n\rBest Guess:\n\r "+bestguess;
		GetComponent<UnityEngine.UI.Text> ().text = "Score: "+Score;
	}

	public GameUser lastentry ;


	void EvaluateWin(){
		//do not run if Stopped already
		if (alreadyStopped)
			return;

		alreadyStopped = true;

		// Debug.Break();
		// stop script routines
		stoptime = Time.time;
		// calculate score
		prepareScore ();

		//show secret
		GameObject.Find ("Secret").GetComponent<Secret> ().ShowSecret ();
		GameObject.Find ("/Canvas").transform.GetChild(2).gameObject.SetActive (true);

		//add score
		lastentry = Global.users.list[Global.users.list.Count - 1];

		lastentry.score = Score;
		//save score to file
		Global.users.SaveXMLData (Global.saveFile);


	}
	        

	void Update () {
		if (restseconds <= 0) {
			EvaluateWin (); // stop and evaluate win
		}

		//GetComponent<UnityEngine.UI.Text> ().text = "Time Left:\n\r "+(Time.time - gametime);
	}
}
