using UnityEngine;
using System.IO;

public static class Global {
	// "false" if game just started and some setting were not initialized.
	public static bool gameInitialized = false;

	// "false" if at the beginning of the game platform specific scenes were not loaded.
	public static bool scenesSwaped = false;

	// Path to the save file
	public static string saveFile = Application.persistentDataPath + Path.DirectorySeparatorChar + "ScoreList.xml";

	// Game user list
	public static GameUsersContainer users = null;

	// Number of scoreboard rows
	public static int scoreRows = 10;
}
