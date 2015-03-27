﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScoreList : MonoBehaviour {
	public GameObject playerScoreEntryPrefab;
	GameUsersContainer userList;

	// Use this for initialization
	void Start () {
		userList = GameUsersContainer.ReadXMLData (".\\ScoreList.xml");
	}
	
	// Update is called once per frame
	void Update () {
		if (userList == null)
			return;

		while (this.transform.childCount > 0) {
			Transform child = this.transform.GetChild(0);
			child.SetParent(null);
			Destroy (child.gameObject);
		}

		foreach (GameUser user in (System.Collections.Generic.IEnumerable<GameUser>) userList.gameUserList) {
			GameObject go = (GameObject) Instantiate (playerScoreEntryPrefab);
			go.transform.SetParent (this.transform, false);
			go.transform.Find("Name").GetComponent<Text>().text = user.Name;
			go.transform.Find("Surname").GetComponent<Text>().text = user.Surname;
			go.transform.Find("Score").GetComponent<Text>().text = user.score.ToString();
		}
	}
}
