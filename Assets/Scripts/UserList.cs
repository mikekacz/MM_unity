using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameUser : IComparable<GameUser>
{
	public string Name;
	public string Surname;
	public string email;
	public int score;

	public GameUser (string newName, string newSurname, string newEmail, int newScore){
		Name = newName;
		Surname = newSurname;
		email = newEmail;
		score = newScore;
	}

	public int CompareTo(GameUser otherUser){
	//produces descending comaprison
		if (otherUser == null) {
			return 1;
		}
		return otherUser.score - score;	
	}
}

public class UserList : MonoBehaviour {
	public List<GameUser> gameUserList = new List<GameUser>();

	void Start (){
		//load previous gamers from file


		gameUserList.Add(new GameUser("Michal","Kaczmarek","mikekacz@tlen.pl",1999));
		gameUserList.Add(new GameUser("Michal","Kaczmarek","mikekacz@tlen.pl",9999));
		gameUserList.Add(new GameUser("Michal","Kaczmarek","mikekacz@tlen.pl",2999));

		gameUserList.Sort ();

		foreach (GameUser gameUser in gameUserList) {
			Debug.Log (gameUser.Name + " " + gameUser.Surname + " " + gameUser.score); 
		}
	}
}
