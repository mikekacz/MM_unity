using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//for text validation
using System.Text.RegularExpressions;

public class InputNavigator : MonoBehaviour
{
	EventSystem system;
	
	void Start()
	{
		system = EventSystem.current;// EventSystemManager.currentSystem;

		inputs [0].ActivateInputField ();
		ValidateTexts ();

	}
	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
			
			if (next != null)
			{
				
				InputField inputfield = next.GetComponent<InputField>();
				if (inputfield != null)
					inputfield.OnPointerClick(new PointerEventData(system));  //if it's an input field, also set the text caret
				
				system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
			}
			//else Debug.Log("next nagivation element not found");
			
		}
	}

	public InputField[] inputs;
	public Button button;

	public Color error_clr;
	public Color normal_clr;

	public void ValidateTexts ()
	{
		bool buttonState = true;

		//loop through texts
		Regex regex;
		for (int i=0; i< inputs.Length; i++) {
			//prepare regex 
			switch (i)
			{
				case 0: 
				regex = new Regex("^[A-Z][a-zA-Z '&-]*[A-Za-z]$"); //http://stackoverflow.com/questions/275160/regex-for-names
				break;
				case 1: 
				regex = new Regex("^[A-Z][a-zA-Z '&-]*[A-Za-z]$");
				break;
				case 2: 
				regex = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
				break;
				default:regex = new Regex("");
				break;
			}
			//check if matching regex

			Match match = regex.Match(inputs[i].text);
			if (match.Success)
			{
				ColorBlock cb = inputs[i].colors;
				cb.normalColor = normal_clr;
				cb.highlightedColor = normal_clr;

				inputs[i].colors = cb;
			}
			else {
				buttonState = false;

				ColorBlock cb = inputs[i].colors;
				cb.normalColor = error_clr;
				cb.highlightedColor = error_clr;
				
				inputs[i].colors = cb;
			}
		}
		button.interactable = buttonState;
	}
}
