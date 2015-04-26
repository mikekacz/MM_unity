using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public static class Global {
	// "false" if game just started and some setting were not initialized.
	public static bool gameInitialized = false;

	// "false" if at the beginning of the game platform specific scenes were not loaded.
	public static bool scenesSwaped = false;

	// True if a game just ended
	public static bool newGame = false;

	// Path to the save file
	public static string saveFile = Application.persistentDataPath + Path.DirectorySeparatorChar + "ScoreList.xml";

	// Number of scoreboard rows
	public static int scoreRows = 10;

	// Game user list
	public static GameUsersContainer users = null;

	// Current user
	public static GameUser currentUser = null;

	// List of toggles to save the choices which input fields need to be remembered
	public static bool[] toggles = new bool[3];
}
