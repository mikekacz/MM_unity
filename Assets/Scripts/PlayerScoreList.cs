using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class PlayerScoreList : MonoBehaviour {
	public GameObject playerScoreEntryPrefab;
	private List<GameUser> playerList = null;
	private GameUser lastPlayer = null;

	// Initialization
	void Start() {
		// If user list is empty load it from save file
		if ( Global.users == null)
			Global.users = GameUsersContainer.ReadXMLData(Global.saveFile);

		// Check if save file is not empty
		if (Global.users.list.Count == 0)
			return;

		// Copy users list
		playerList = new List<GameUser>(Global.users.list);

		// Remember last player
		lastPlayer = playerList[playerList.Count - 1];

		// Sort the player list by score value
		playerList.Sort();

		DrawScoreboard();
	}

	// Draw the Scoreboard
	void DrawScoreboard() {
		int rowNumber = Global.scoreRows;
		bool search = true;

		// User list is empty -> nothing to draw
		if (playerList == null || playerList.Count == 0)
			return;

		// Cleanup objects that we created
		while (this.transform.childCount > 0) {
			Transform child = this.transform.GetChild(0);
			child.SetParent(null);
			Destroy(child.gameObject);
		}

		// Score list is shorter then default setting
		if (playerList.Count < rowNumber)
			rowNumber = playerList.Count;

		// Go through the user list
		for (int counter = 0; (counter < rowNumber || search); counter++) {
			// draw 10 rows and an 11th if the last score is not in those 10
			if (counter < rowNumber || playerList[counter].Equals(lastPlayer)) {
				// Create row and add it to the vertical layout group
				GameObject go = (GameObject) Instantiate(playerScoreEntryPrefab);
				go.transform.SetParent(this.transform, false);

				// Find matching user and highlight his entry green
				if (playerList[counter].Equals(lastPlayer)) {
					go.transform.GetComponent<Image>().color = new Color(0.59f, 1.0f, 0.55f, 0.59f);
					search = false;
				}

				// Fill the values of the row
				go.transform.Find("Rank").GetComponent<Text>().text = (counter + 1).ToString();
				go.transform.Find("Name").GetComponent<Text>().text = playerList[counter].Name;
				go.transform.Find("Surname").GetComponent<Text>().text = playerList[counter].Surname;
				go.transform.Find("Score").GetComponent<Text>().text = playerList[counter].score.ToString();
			}
		}
	}
}
