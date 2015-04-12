using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class GameUser : IComparable<GameUser> {
	[XmlAttribute("Name")]
	public string Name;
	[XmlAttribute("Surname")]
	public string Surname;
	[XmlAttribute("email")]
	public string email;
	[XmlAttribute("score")]
	public int score;

	// Default constructor
	public GameUser() : this("", "", "", 0) {}

	// Constructor
	public GameUser(string newName, string newSurname, string newEmail, int newScore) {
		Name = newName;
		Surname = newSurname;
		email = newEmail;
		score = newScore;
	}

	// Produces descending comaprison
	public int CompareTo(GameUser otherUser) {
		if (otherUser == null) {
			return 1;
		}

		if (otherUser.Name == Name && otherUser.Surname == Surname && otherUser.email == email && otherUser.score == score) {
			return 0;
		}

		return otherUser.score - score;	
	}
}

// http://wiki.unity3d.com/index.php?title=Saving_and_Loading_Data:_XmlSerializer
[XmlRoot("GameUsersContainer")]
public class GameUsersContainer {
	[XmlArray("GameUsers")]
	[XmlArrayItem("GameUser")]
	public List<GameUser> list = new List<GameUser>();

	public static GameUsersContainer ReadXMLData(string _path) {
		if (!File.Exists(_path)) {
			return new GameUsersContainer();
		}

		var serializer = new XmlSerializer(typeof(GameUsersContainer));
		var stream = new FileStream(_path, FileMode.Open);
		var container = serializer.Deserialize(stream) as GameUsersContainer;
		stream.Close();

		return container;
	}

	public void SaveXMLData(string _path) {
		var serializer = new XmlSerializer(typeof(GameUsersContainer));
		var stream = new FileStream(_path, FileMode.Create);
		serializer.Serialize(stream, this);
		stream.Close();
	}
}

public class UserList : MonoBehaviour {
	// Use this for initialization
	void Start() {
		// Load previous gamers from file
		_GLOBAL.users = GameUsersContainer.ReadXMLData(_GLOBAL.saveFile);
	}

	// Update is called once per frame
	void Update () {
		// F1 Key pressed
		if (Input.GetKeyDown(KeyCode.F1)) {
			this.gameObject.SetActive(false);
			GameObject.Find("/Canvas").transform.GetChild(2).gameObject.SetActive(true);
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
