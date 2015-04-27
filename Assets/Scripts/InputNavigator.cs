using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//for text validation
using System.Text.RegularExpressions;

public class InputNavigator : MonoBehaviour {
	EventSystem system;

	public InputField[] inputs;
	public Button button;
	
	public Color error_clr;
	public Color normal_clr;

	// Use this for initialization
	void Start() {
		system = EventSystem.current;

		inputs[0].ActivateInputField();
		ValidateText(0);
		ValidateText(1);
		ValidateText(2);
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.Tab)) {
			Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
			
			if (next != null) {
				
				InputField inputfield = next.GetComponent<InputField>();
				if (inputfield != null)
					inputfield.OnPointerClick(new PointerEventData(system)); //if it's an input field, also set the text caret
				
				system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
			}
		}
	}
	
	// Validate input fields
	public void ValidateText(int id) {
		// Prepare regex
		Regex regex;
			
		switch (id) {
		case 0:
			//http://stackoverflow.com/questions/275160/regex-for-names
			regex = new Regex("^[A-Z][a-zA-Z '&-]*[A-Za-z]$");
			break;
		case 1:
			regex = new Regex("^[A-Z][a-zA-Z '&-]*[A-Za-z]$");
			break;
		case 2:
			regex = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
			break;
		default:
			regex = new Regex("");
			break;
		}

		//check if matching regex
		Match match = regex.Match(inputs[id].text);
		if (match.Success) {
			Global.validInput[id] = true;

			ColorBlock cb = inputs[id].colors;
			cb.normalColor = normal_clr;
			cb.highlightedColor = normal_clr;

			inputs[id].colors = cb;
		} else {
			Global.validInput[id] = false;

			ColorBlock cb = inputs[id].colors;
			cb.normalColor = error_clr;
			cb.highlightedColor = error_clr;

			inputs[id].colors = cb;
		}
		button.interactable = Global.validInput[0] & Global.validInput[1] & Global.validInput[2];
	}
}
