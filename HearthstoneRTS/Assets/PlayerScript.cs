using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public string myName;

	public float myPanningFactor = 1.0f;
	
	public int myMana;

	private Vector3 myMouseLastPosition;

	void Start () {
	
	}
	
	void Update () {

		Panning (Input.mousePosition - myMouseLastPosition);

		myMouseLastPosition = Input.mousePosition;

	}

	//selecting units needs to happen on all of the player's units in a boxed area
		//q

	//camera panning needs to happen on four directions, north, south, east west,
		//on the four screen edges,
		//but only when the mouse is moving towards that screen edge
		//and only when it is on that screen edge
	void Panning (Vector3 mouseMovement) {
		//mousePosition
			//botteom left position is mouse 0,0
			//top right of screen is screen.width, screen.height

		//Input.mouseScrollDeta is the mouse wheel
			//hopefully is mouse movement
			//don't know which way is up, down, left, right

		Vector2 screenPosition = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);
		
		if (screenPosition.x > 0.95f) {
			//moving EAST
			
			transform.Translate(myPanningFactor, 0.0f, 0.0f);


		} else if(screenPosition.x < 0.05f) {
			//moving WEST
			
			transform.Translate(-myPanningFactor, 0.0f, 0.0f);

		}
		
		if (screenPosition.y > 0.95f) {
			//moving NORTH
			
			transform.Translate(0.0f, 0.0f, myPanningFactor);


		} else if(screenPosition.y < 0.05f) {
			//moving SOUTH
			
			transform.Translate(0.0f, 0.0f, -myPanningFactor);


		}

	}
	
}
