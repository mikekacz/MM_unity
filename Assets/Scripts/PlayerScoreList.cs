using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScoreList : MonoBehaviour {
	public GameObject playerScoreEntryPrefab;
	GameUsersContainer userList;

	// Use this for initialization
	void Start () {
		//userList = GameUsersContainer.ReadXMLData (".\\ScoreList.xml");
		userList = GameUsersContainer.ReadXMLData (GameObject.Find("/Canvas").transform.GetChild(0).GetComponent<UserList>().path);

		userList.gameUserList.Sort ();

		DrawScoreboard ();
	}

	void DrawScoreboard () {
		int RowNumber = 10;

		if (userList == null)
			return;

		while (this.transform.childCount > 0) {
			Transform child = this.transform.GetChild(0);
			child.SetParent(null);
			Destroy (child.gameObject);
		}

		RowNumber = (userList.gameUserList.Count < RowNumber) ? userList.gameUserList.Count : RowNumber; //warning hidden IF statement

		//mark my score
		GameUser _lastentry = GameObject.Find ("/Canvas/Panel 1/Scoreboard").GetComponent<Game1> ().lastentry;
		//Debug.Log(userList.gameUserList.FindIndex (_lastentry));


		for (int i=0; i<RowNumber; i++){
			GameUser user = userList.gameUserList[i];
			GameObject go = (GameObject) Instantiate (playerScoreEntryPrefab);
			go.transform.SetParent (this.transform, false);
			go.transform.Find("Name").GetComponent<Text>().text = user.Name;
			go.transform.Find("Surname").GetComponent<Text>().text = user.Surname;
			go.transform.Find("Score").GetComponent<Text>().text = user.score.ToString();
		}
	}
}
