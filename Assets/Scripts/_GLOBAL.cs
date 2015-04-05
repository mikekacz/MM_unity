using UnityEngine;
using System.IO;

public class _GLOBAL {
	// Path to the save file
	public static string saveFile = Application.persistentDataPath + Path.DirectorySeparatorChar + "ScoreList.xml";

	// Game user list
	public static GameUsersContainer users = null;

	// Number of scoreboard rows
	public static int scoreRows = 10;

	// Login panel
	public static GameObject loginPanel = GameObject.Find("/Canvas").transform.GetChild(0).gameObject;

	// Scoreboard panel
	public static GameObject scorePanel = GameObject.Find("/Canvas").transform.GetChild(2).gameObject;
}
