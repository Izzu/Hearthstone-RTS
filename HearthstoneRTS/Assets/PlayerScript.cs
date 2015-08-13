using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public string myName;

	public int myMana;

	public HandScript myHandScript;

	public CursorScript myCurserScript;

	void Start () {

	}
	
	void Update () {

		if (Input.GetKey("down"))
		{
			GameObject card = Instantiate(Resources.Load("Card")) as GameObject;
			GiftCard(card.GetComponent<CardScript>());
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
