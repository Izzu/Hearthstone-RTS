using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public string myName;

	public int myMana, myOverload, myManaCap, myGold, myDebt, mySupply, myDemand;

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
	
	//	Generates a Message from:
	//		the player, 
	//		his hero,
	//		the unit his hero is attacking,
	//		that unit's owner,
	//		or some random player if nothing is found
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

	//	If no space in hand, 
	//		trigger removal effect
	//		destroy card if specified
	public PlayerScript Draw(CardScript drawCardScript = null, bool destroyOnFailure = true)
	{
		drawCardScript = myDeckScript.RemoveCard();

		if (drawCardScript)
		{
			//	If false, then hand was over capacity,
			//	But the removal effect for the card should go off
			if(false == myHandScript.InsertCard(drawCardScript))
			{
				drawCardScript.myRemoveEffect.Activate(ToMessage());

				//failed to draw card so destroy if specified
				if (destroyOnFailure)
				{
					Debug.Log("DIE");
					Destroy(drawCardScript.gameObject);
				}
			}
		}

		return this;
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
