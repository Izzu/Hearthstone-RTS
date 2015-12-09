using UnityEngine;
using System.Collections;

public class Card_Script : Effect_Script {

	public Effect_Script[] myPlayEffects, myManaEffects;
	public int myManaValue;

	public int mana;

	// Use this for initialization
	void Start () {
	
	}

	public Card_Holder_Script myCardHolder
	{
		get
		{
			return GetComponentInParent<Card_Holder_Script>();
		}
	}

	public int Mana
	{
		get
		{
			return Effect_Script.AffectsList(myManaEffects, myManaValue, gameObject, gameObject);
		}
	}

	// Update is called once per frame
	void Update () 
	{
		mana = Mana;
	}

	public override int Affect(int input, GameObject inSubject, GameObject inObject)
	{
		Player_Script playerScript = myCardHolder.myOwner;
			
		if(playerScript)
		{
			//get cost of card
			int manaCost = Mana;
			int playerMana = playerScript.Mana;

			Debug.Log("PlayerMana: " + playerMana);

			//if player has enough
			if (manaCost <= playerMana)
			{
				Term_Effect_Script subtractionTerm = gameObject.AddComponent<Term_Effect_Script>();

				subtractionTerm.myTerm = -manaCost;

				//pay cost
				playerScript.myManaEffects = Effect_Script.Append(playerScript.myManaEffects, subtractionTerm);

				if(playerScript.myGraveyard)
				{
					transform.parent = playerScript.myGraveyard.transform;
					Debug.Log("Went into graveyard");
				}
				else
				{
					Debug.Log("No graveyard for used card");
				}

				return Effect_Script.AffectsList(myPlayEffects, 0, inSubject, inObject);
			}
			else
			{
				Debug.Log("Card: " + gameObject.name + " uses " + manaCost + " mana.");
			}
		}
		return 0;

	}

}
