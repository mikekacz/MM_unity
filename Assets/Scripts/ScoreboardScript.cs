using UnityEngine;
using System.Collections;

public class ScoreboardScript : MonoBehaviour {

	private Vector2 touchPosPrev;
	private bool couldBeSwipe;
	private int errorMargin = 10;
	private int minSwipeDist =10;

	void switchScreen (){
		this.gameObject.SetActive(false);
		GameObject.Find("/Canvas").transform.GetChild(0).gameObject.SetActive(true);
	}
		
	// Update is called once per frame
	void Update () {
		// F1 Key pressed
		if (Input.GetKeyDown(KeyCode.F1)) {
			switchScreen ();
		}
		// http://forum.unity3d.com/threads/swipe-help-please.48601/#post-323387
		else if ((Input.touchCount == 1)){
			//get only touch info
			var touch = Input.GetTouch(0);
			
			switch (touch.phase){
			case TouchPhase.Began :
				couldBeSwipe = true;
				touchPosPrev = touch.position;
				break;
			case TouchPhase.Moved :
				break;
				
			case TouchPhase.Stationary:
				couldBeSwipe = false;
				break;
			case TouchPhase.Ended:
				var swipeVector = touch.position - touchPosPrev;
				if (couldBeSwipe && (swipeVector.magnitude>minSwipeDist) && ((swipeVector.x / swipeVector.y > 4) || (swipeVector.x / swipeVector.y < -4) )){
					switchScreen();
				}
				break;
			case TouchPhase.Canceled:
				couldBeSwipe =false;
				break;
			}
			
		}
		
	}


}
