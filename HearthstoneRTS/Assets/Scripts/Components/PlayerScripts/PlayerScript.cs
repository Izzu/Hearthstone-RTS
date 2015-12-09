using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {

	public int myMana, myOverload, myManaCap, myGold, myDebt, mySupply, myDemand;

	public HandScript myHandScript;

	public PanningScript myPanningScript;

	public SelectionScript mySelectionScript;

	public UnitScript myHeroUnitScript;

	public DeckScript myDeckScript;

	public Color myColor;

	public HashSet<ProductionScript> myProduction;

	/*[System.Serializable]
	public class FrameData
	{
		public int mySpellsCast;
		public int myCardsPlayed;
		public int myItemsUsed;
		public int myUnitsMade;
		public int myAttacks;
		public int myHeals;
		public int myDeaths;
		public int myDamages;
		public int myDraws;
	}

	[System.Serializable]
	public class TurnData
	{
		public FrameData myFrameData = new FrameData();
	}

	[System.Serializable]
	public class MatchData
	{
		public TurnData myTurnData = new TurnData();
	}

	public MatchData myMatchData;
	public TurnData myTurnData;
	public FrameData myFrameData;*/

	void Awake ()
	{
		/*MatchData myMatchData = new MatchData();

		TurnData myTurnData = new TurnData();

		FrameData myFrameData = new FrameData();*/

		myProduction = new HashSet<ProductionScript>();
	}

	void Start ()
	{

	}
	
	void Update ()
	{
		
	}

	public void TurnBegin ()
	{
		Draw();

		ManaFill();
	}

	public void TurnEnd ()
	{

	}

	//registering units with a player costs the player supply
	public bool Register(OwnerScript owned, int demand = 0, int supply = 0)
	{

		//if player hasn't enough supply
		if(demand > mySupply - myDemand)
		{
			owned.Own(null, demand, supply);

			Debug.Log(this + " can't afford " + demand + " supply.");
			
			return false;
		}

		//otherwise, proceed
		owned.Own(this, demand, supply);

		myDemand += demand;
		mySupply += supply;

		return true;
	}

	public void Release(OwnerScript owned)
	{
		if (this == owned.owner)
		{
			mySupply -= owned.supply;
			myDemand -= owned.demand;
		}
	}

	public Message.Term ToTerm()
	{
		Message.Term term = null == myHeroUnitScript ? new Message.Term(this, null, null, transform.position) : myHeroUnitScript.ToTerm();

		term.myPlayerScript = this;

		return term;
	}

	public Message ToMessage ()
	//	Generates a Message from:
	//		the player,
	//		his hero,
	//		the unit his hero is attacking,
	//		that unit's owner,
	//		or some random player if nothing is found
	
	{
		//no hero
		if(null == myHeroUnitScript)
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

		//player has a hero
		else
		{
			return myHeroUnitScript.ToMessage();
		}
	}

	public PlayerScript Draw(bool destroyOnFailure = true)
	//	If no space in hand, 
	//		trigger removal effect
	//		destroy card if specified
	{
		CardScript drawCardScript = myDeckScript.RemoveCard();

		if (null != drawCardScript)
		{
			/*myFrameData.myDraws++;
			myTurnData.myFrameData.myDraws++;
			myMatchData.myTurnData.myFrameData.myDraws++;

			GlobalScript.ourPlayerFrameData.myDraws++;
			GlobalScript.ourPlayerTurnData.myFrameData.myDraws++;
			GlobalScript.ourPlayerMatchData.myTurnData.myFrameData.myDraws++;*/

			//	If false, then hand was over capacity,
			//	But the removal effect for the card should go off
			if(false == myHandScript.InsertCard(drawCardScript))
			{
				EffectScript.AffectsList(drawCardScript.myRemoveEffects, ToMessage());
				
				//failed to draw card so destroy if specified
				if (destroyOnFailure)
				{
					Destroy(drawCardScript.gameObject);
				}
			}
		}

		return this;
	}

	//	Activates a card
	public void ActivateCard(CardScript cardScript, Message message)
	{
		EffectScript.AffectsList(cardScript.myPlayEffects, message);
		
		/*myFrameData.myCardsPlayed++;
		myTurnData.myFrameData.myCardsPlayed++;
		myMatchData.myTurnData.myFrameData.myCardsPlayed++;

		GlobalScript.ourPlayerFrameData.myCardsPlayed++;
		GlobalScript.ourPlayerTurnData.myFrameData.myCardsPlayed++;
		GlobalScript.ourPlayerMatchData.myTurnData.myFrameData.myCardsPlayed++;*/

		switch(cardScript.myType)
		{
			case CardScript.Type.Spell:

				/*myFrameData.mySpellsCast++;
				myTurnData.myFrameData.mySpellsCast++;
				myMatchData.myTurnData.myFrameData.mySpellsCast++;

				GlobalScript.ourPlayerFrameData.mySpellsCast++;
				GlobalScript.ourPlayerTurnData.myFrameData.mySpellsCast++;
				GlobalScript.ourPlayerMatchData.myTurnData.myFrameData.mySpellsCast++;*/
				break;

			case CardScript.Type.Item:

				/*myFrameData.myItemsUsed++;
				myTurnData.myFrameData.myItemsUsed++;
				myMatchData.myTurnData.myFrameData.myItemsUsed++;

				GlobalScript.ourPlayerFrameData.myItemsUsed++;
				GlobalScript.ourPlayerTurnData.myFrameData.myItemsUsed++;
				GlobalScript.ourPlayerMatchData.myTurnData.myFrameData.myItemsUsed++;*/
				break;

			case CardScript.Type.Unit:

				/*myFrameData.myUnitsMade++;
				myTurnData.myFrameData.myUnitsMade++;
				myMatchData.myTurnData.myFrameData.myUnitsMade++;

				GlobalScript.ourPlayerFrameData.myUnitsMade++;
				GlobalScript.ourPlayerTurnData.myFrameData.myUnitsMade++;
				GlobalScript.ourPlayerMatchData.myTurnData.myFrameData.myUnitsMade++;*/
				break;

		}

	}










	public PlayerScript AddMana(int input)
	{
		myMana += input;

		return this;
	}

	public PlayerScript AddOverload (int input)
	{
		myOverload += input;

		return this;
	}

	public void ManaFill()
	{
		myMana = myManaCap - myOverload;

		myOverload = 0;

		myMana = myMana < 0 ? 0 : myMana;
	}

	public PlayerScript AddGold(int input)
	{
		if(myDebt > 0)
		{
			if(myDebt > input)
			{
				myDebt -= input;
			}
			else
			{
				myDebt = 0;
				myGold += input - myDebt;
			}
		}
		else
		{
			myGold += input;
		}

		return this;
	}

	public PlayerScript AddManaCap(int input)
	{
		myManaCap += input;

		return this;
	}

	public PlayerScript SubGold(int input)
	{
		if(myGold > input)
		{
			myGold -= input;
		}
		else
		{
			myGold = 0;
		}
		return this;
	}

	public PlayerScript AddDebt(int input)
	{
		myDebt += input;

		return this;
	}

	public PlayerScript SubMana(int input)
	{
		myMana -= input;

		return this;
	}

}
