using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public string myName;

	public int myMana;

	public HandScript myHandScript;

	public PanningScript myPanningScript;

	public CursorScript myCurserScript;

	public SelectionScript mySelectionScript = null;

	void Start () {

	}
	
	void Update () {

		if (Input.GetKey("down"))
		{
			GameObject card = Instantiate(Resources.Load("Card")) as GameObject;
			if(false == GiftCard(card.GetComponent<CardScript>()))
			{
				Object.Destroy(card);
			}
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
