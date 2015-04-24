using UnityEngine;
using System.Collections;

public class StartButtonClick : MonoBehaviour {

	public GameObject Panel0;
	public GameObject Scoreboard;

	public void Click(){
		//Panel0.EventHandler.AddGamer
		Debug.Log ("Panel0.EventHandler.AddGamer");
		Panel0.GetComponent<EventHandler> ().AddGamer ();
		//Scoreboard.Game1.Resetgame
		Debug.Log ("Scoreboard.Game1.Resetgame");
		Scoreboard.GetComponent<Game1> ().ResetGame ();

	}
}
