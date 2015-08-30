﻿using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public string myName;

	public int myMana, myManaCap, myGold;

	public HandScript myHandScript;

	public PanningScript myPanningScript;

	public SelectionScript mySelectionScript;

	public UnitScript myHeroUnitScript;

	public DeckScript myDeckScript;

	public Color myColor;

	void Start () {

	}
	
	void Update () {

	}

	public Message.Term ToTerm ()
	{
		Message.Term term = null == myHeroUnitScript ? new Message.Term(this, null, null, transform.position) : myHeroUnitScript.ToTerm();

		term.myPlayerScript = this;
		
		return term;
	}

	public void TurnBegin ()
	{
		Draw();

		AddMana(++myManaCap - myMana);
	}

	public void TurnEnd ()
	{

	}
	
	public Message ToMessage ()
	{
		//no hero
		if(myHeroUnitScript)
		{
			return myHeroUnitScript.ToMessage();
		}
		//player has a hero
		else
		{
			//finding another player
			foreach (PlayerScript playerScript in GlobalScript.ourPlayerScripts)
			{
				if (playerScript != this)
				{
					return new Message(ToTerm(), playerScript.ToTerm());
				}
			}

			//no player found, be null
			return new Message(ToTerm(), new Message.Term(null, null, null, Vector3.zero));
		}
	}

	public PlayerScript Draw(CardScript drawCardScript = null)
	{
		drawCardScript = myDeckScript.RemoveCard();

		if (drawCardScript)
		{
			myHandScript.InsertCard(drawCardScript);
		}

		return this;
	}

	public PlayerScript AddMana (int input)
	{
		myMana += input;

		return this;
	}

	public PlayerScript SubMana(int input)
	{
		myMana -= input;

		return this;
	}

}
