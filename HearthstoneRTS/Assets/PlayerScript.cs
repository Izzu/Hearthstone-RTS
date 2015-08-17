using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public string myName;

	public int myMana;

	public HandScript myHandScript;

	public PanningScript myPanningScript;

	public CursorScript myCurserScript;

	public SelectionScript mySelectionScript;

	public UnitScript myHeroUnitScript;

	void Start () {

	}
	
	void Update () {

		

	}

	public void TurnBegin()
	{

	}

	public void TurnEnd()
	{

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
