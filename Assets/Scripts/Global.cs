using UnityEngine;
using System.IO;

public static class Global {
	private static bool initialized = true;

	// Path to the save file
	public static string saveFile = "ScoreList.xml";

	// Game user list
	public static GameUsersContainer users = null;

	// Number of scoreboard rows
	public static int scoreRows = 10;

	// Use this for initialization
	public static void Initialize() {
		if (initialized) {
			GameObject.Find ("/Canvas").transform.GetChild (1).gameObject.SetActive (false);
			GameObject.Find ("/Canvas").transform.GetChild (0).gameObject.SetActive (true);
			initialized = false;
		}
	}
}
