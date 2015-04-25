using UnityEngine;
using System.IO;

public static class Global {
	// "false" if game just started.
	public static bool initialized = false;

	public static bool sceneSwitched = false;

	// Path to the save file
	public static string saveFile = Application.persistentDataPath + Path.DirectorySeparatorChar + "ScoreList.xml";

	// Game user list
	public static GameUsersContainer users = null;

	// Number of scoreboard rows
	public static int scoreRows = 10;
}
