using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;

public class GameUser : IComparable<GameUser>
{
	[XmlAttribute("Name")]
	public string Name;
	[XmlAttribute("Surname")]
	public string Surname;
	[XmlAttribute("email")]
	public string email;
	[XmlAttribute("score")]
	public int score;

	public GameUser (string newName, string newSurname, string newEmail, int newScore){
		Name = newName;
		Surname = newSurname;
		email = newEmail;
		score = newScore;
	}

	public GameUser (){
		Name = "";
		Surname = "";
		email = "";
		score = 0;
	}

	public int CompareTo(GameUser otherUser){
	//produces descending comaprison
		if (otherUser == null) {
			return 1;
		}
		if (otherUser.Name == this.Name && otherUser.Surname == this.Surname && otherUser.email == this.email && otherUser.score == this.score) {
			return 0;
		}
		return otherUser.score - score;	
	}
}


// http://wiki.unity3d.com/index.php?title=Saving_and_Loading_Data:_XmlSerializer
[XmlRoot("GameUsersContainer")]
public class GameUsersContainer
{
	[XmlArray("GameUsers")]
	[XmlArrayItem("GameUser")]
	public List<GameUser> gameUserList = new List<GameUser> ();

	public static GameUsersContainer ReadXMLData (string _path)
	{
		if (!File.Exists (_path)) {
			return new GameUsersContainer ();
		}
		var serializer = new XmlSerializer (typeof(GameUsersContainer));
		var stream = new FileStream (_path, FileMode.Open);
		var container = serializer.Deserialize (stream) as GameUsersContainer;
		stream.Close ();

		return container;
	}

	public void SaveXMLData(string _path)
	{
		var serializer = new XmlSerializer (typeof(GameUsersContainer));
		var stream = new FileStream (_path, FileMode.Create);
		serializer.Serialize (stream, this);
		stream.Close ();
	}
}

public class UserList : MonoBehaviour {
	public GameUsersContainer GUList;

	// Initialization
	void Start () {
		//load previous gamers from file
		GUList = GameUsersContainer.ReadXMLData (_GLOBAL.saveFile);
	}

	public void AddGamer () {
		// Get user information from input field on thhr login screen
		string name = GameObject.Find ("Q1InputField").GetComponent<InputField>().text;
		string surname = GameObject.Find ("Q2InputField").GetComponent<InputField>().text;
		string email = GameObject.Find ("Q3InputField").GetComponent<InputField>().text;

		// Add a new user to the user list
		GUList.gameUserList.Add (new GameUser(name, surname, email, 0));

		// Store user list in the save file
		GUList.SaveXMLData (_GLOBAL.saveFile);
	}
}
