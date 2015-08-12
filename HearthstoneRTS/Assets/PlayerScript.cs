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

	void Panning (Vector3 mouseMovement) {

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
