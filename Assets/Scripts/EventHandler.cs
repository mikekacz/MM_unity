using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EventHandler : MonoBehaviour {
	private Vector2 touchPosPrev;
	private bool couldBeSwipe;
	private int minSwipeDist = 10;

	public GameObject loginScreen;
	public GameObject scoreScreen;

	// Use this for initialization
	void Start() {
		// Load previous gamers from file
		Global.users = GameUsersContainer.ReadXMLData(Global.saveFile);

		// Restore player details on login screen if necessary
		if (Global.newGame) {
			RestoreLogin();
			Global.newGame = false;
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
				if (couldBeSwipe && (swipeVector.magnitude > minSwipeDist) && ((swipeVector.x / swipeVector.y > 1) || (swipeVector.x / swipeVector.y < -1) )) {
					switchScreen();
				}
				break;
			case TouchPhase.Canceled:
				couldBeSwipe = false;
				break;
			}
		}
	}

	// "Start Game" button was clicked.
	public void StartButtonClick() {
		SaveToggles();
		AddGamer();
		Game1.ResetGame();
		Global.newGame = true;
	}

	// Sets the correct toggle
	public void SetToggle(int id) {
		switch (id) {
		case 0:
			Global.toggles[id] = loginScreen.transform.GetChild(3).GetComponent<Toggle>().isOn;
			break;
		case 1:
			Global.toggles[id] = loginScreen.transform.GetChild(6).GetComponent<Toggle>().isOn;
			break;
		case 2:
			Global.toggles[id] = loginScreen.transform.GetChild(9).GetComponent<Toggle>().isOn;
			break;
		default:
			Debug.Log("Toggle with id = " + id + " not known.");
			break;
		}
	}

	// Switch between login and scoreboard screen
	private void switchScreen () {
		if (loginScreen.activeInHierarchy) {
			loginScreen.SetActive (false);
			scoreScreen.SetActive (true);
			return;
		}
		
		if (scoreScreen.activeInHierarchy) {
			scoreScreen.SetActive (false);
			loginScreen.SetActive (true);
			return;
		}
	}

	// Remember toggle choices
	private void SaveToggles() {
		Global.toggles[0] = loginScreen.transform.GetChild(3).GetComponent<Toggle>().isOn;
		Global.toggles[1] = loginScreen.transform.GetChild(6).GetComponent<Toggle>().isOn;
		Global.toggles[2] = loginScreen.transform.GetChild(9).GetComponent<Toggle>().isOn;
	}

	// Add a new gamer to the list
	private void AddGamer() {
		// Get user information from input field on thhr login screen
		string name = GameObject.Find("Q1InputField").GetComponent<InputField>().text;
		string surname = GameObject.Find("Q2InputField").GetComponent<InputField>().text;
		string email = GameObject.Find("Q3InputField").GetComponent<InputField>().text;
		
		// Add a new user to the user list
		Global.currentUser = new GameUser (name, surname, email, 0);
		Global.users.list.Add(Global.currentUser);

		// Store user list in the save file
		Global.users.SaveXMLData(Global.saveFile);
	}

	// Fill fields on login screen
	private void RestoreLogin() {
		if (Global.currentUser == null)
			return;

		loginScreen.transform.GetChild(3).GetComponent<Toggle>().isOn = Global.toggles[0];
		if (Global.toggles[0]) {
			loginScreen.transform.GetChild(2).GetComponent<InputField>().text = Global.currentUser.Name;
		} else {
			loginScreen.transform.GetChild(2).GetComponent<InputField>().text = "";
			Global.currentUser.Name = "";
		}

		loginScreen.transform.GetChild(6).GetComponent<Toggle>().isOn = Global.toggles[1];
		if (Global.toggles[1]) {
			loginScreen.transform.GetChild(5).GetComponent<InputField>().text = Global.currentUser.Surname;
		} else {
			loginScreen.transform.GetChild(5).GetComponent<InputField>().text = "";
			Global.currentUser.Surname = "";
		}

		loginScreen.transform.GetChild(9).GetComponent<Toggle>().isOn = Global.toggles[2];
		if (Global.toggles[2]) {
			loginScreen.transform.GetChild(8).GetComponent<InputField>().text = Global.currentUser.email;
		} else {
			loginScreen.transform.GetChild(8).GetComponent<InputField>().text = "";
			Global.currentUser.email = "";
		}
	}
}
