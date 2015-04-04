using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class PlayerScoreList : MonoBehaviour {
	public GameObject playerScoreEntryPrefab;
	List<GameUser> playerList;
	GameUser lastPlayer = null;

	// Initialization
	void Start () {
		// Load current score list from save file
		GameUsersContainer userCont = GameUsersContainer.ReadXMLData (_GLOBAL.saveFile);

		// Check if save file is not empty
		if (userCont.gameUserList.Count == 0)
			return;
		//and get player list
		else
			playerList = userCont.gameUserList;

		// Remember last player
		lastPlayer = playerList[playerList.Count - 1];

		// Sort the player list by score value
		playerList.Sort ();

		DrawScoreboard ();
	}

	// Draw the Scoreboard
	void DrawScoreboard () {
		int rowNumber = 10;	// Number of Scoreboard rows
		int counter = 1;	// Scoreboard row counter

		// User list is empty -> nothing to draw
		if (playerList == null)
			return;

		// Cleanup objects that we created
		while (this.transform.childCount > 0) {
			Transform child = this.transform.GetChild(0);
			child.SetParent(null);
			Destroy (child.gameObject);
		}

		// Score list is shorter then 10 entries
		if (playerList.Count < rowNumber)
			rowNumber = playerList.Count;

		// Go through the user list
		foreach (GameUser user in playerList) {
			// draw 10 rows and an 11th if the last score is not in those 10
			if (counter <= rowNumber || user.Equals (lastPlayer)) {
				// Create row and add it to the vertical layout group
				GameObject go = (GameObject) Instantiate (playerScoreEntryPrefab);
				go.transform.SetParent (this.transform, false);

				// Find matching user and highlight his entry green
				if (user.Equals (lastPlayer))
					go.transform.GetComponent<Image>().color = new Color(0.59f, 1.0f, 0.55f, 0.59f);

				// Fill the values of the row
				go.transform.Find("Rank").GetComponent<Text>().text = counter.ToString();
				go.transform.Find("Name").GetComponent<Text>().text = user.Name;
				go.transform.Find("Surname").GetComponent<Text>().text = user.Surname;
				go.transform.Find("Score").GetComponent<Text>().text = user.score.ToString();
			}

			counter++;
		}
	}
}
