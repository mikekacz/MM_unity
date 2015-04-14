﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EventHandler : MonoBehaviour {
	private Vector2 touchPosPrev;
	private bool couldBeSwipe;
	private int errorMargin = 10;
	private int minSwipeDist = 10;

	// Use this for initialization
	void Start() {
		// Load previous gamers from file
		_GLOBAL.users = GameUsersContainer.ReadXMLData(_GLOBAL.saveFile);
	}

	// Switch between login and scoreboard screen
	void switchScreen () {
		GameObject canvas = GameObject.Find("/Canvas");
		GameObject loginScreen = canvas.transform.GetChild(0).gameObject;
		GameObject scoreBoard = canvas.transform.GetChild(2).gameObject;

		if (loginScreen.activeInHierarchy) {
			loginScreen.SetActive (false);
			scoreBoard.SetActive (true);
			return;
		}

		if (scoreBoard.activeInHierarchy) {
			scoreBoard.SetActive (false);
			loginScreen.SetActive (true);
			return;
		}
	}

	// Update is called once per frame
	void Update () {
		// F1 Key pressed
		if (Input.GetKeyDown(KeyCode.F1)) {
			switchScreen ();
		}
		// http://forum.unity3d.com/threads/swipe-help-please.48601/#post-323387
		else if ((Input.touchCount == 1)) {
			//get only touch info
			var touch = Input.GetTouch(0);
			
			switch (touch.phase) {
			case TouchPhase.Began:
				couldBeSwipe = true;
				touchPosPrev = touch.position;
				break;
			case TouchPhase.Moved:
				break;
			case TouchPhase.Stationary:
				couldBeSwipe = false;
				break;
			case TouchPhase.Ended:
				var swipeVector = touch.position - touchPosPrev;
				if (couldBeSwipe && (swipeVector.magnitude > minSwipeDist) && ((swipeVector.x / swipeVector.y > 4) || (swipeVector.x / swipeVector.y < -4) )) {
					switchScreen();
				}
				break;
			case TouchPhase.Canceled:
				couldBeSwipe = false;
				break;
			}
		}
	}

	// Add a new gamer to the list
	public void AddGamer() {
		// Get user information from input field on thhr login screen
		string name = GameObject.Find("Q1InputField").GetComponent<InputField>().text;
		string surname = GameObject.Find("Q2InputField").GetComponent<InputField>().text;
		string email = GameObject.Find("Q3InputField").GetComponent<InputField>().text;
		
		// Add a new user to the user list
		_GLOBAL.users.list.Add(new GameUser(name, surname, email, 0));
		
		// Store user list in the save file
		_GLOBAL.users.SaveXMLData(_GLOBAL.saveFile);
	}
}
