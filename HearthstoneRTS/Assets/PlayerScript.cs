using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public string myName;

	public float myPanningFactor = 1.0f;
	
	public int myMana;

	private Vector3 myMouseLastPosition;

	public HandScript myHandScript;

	void Start () {

	}
	
	void Update () {

		//Panning (Input.mousePosition - myMouseLastPosition);

		//GameObject card = Instantiate(Resources.Load("Card")) as GameObject;
		//GiftCard(card.GetComponent<CardScript>());

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

	bool GiftCard (CardScript input)
	{
		if(myHandScript)
		{
			return myHandScript.InsertCard(input);
		}
		return false;
	}

	CardScript BurnCard (int index)
	{
		if (myHandScript)
		{
			return myHandScript.RemoveCard(myHandScript.GetCard(index));
		}
		return null;
	}
	
}
