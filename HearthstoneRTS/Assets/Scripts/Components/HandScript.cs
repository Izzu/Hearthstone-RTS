using UnityEngine;
using System.Collections;

public class HandScript : MonoBehaviour {

    public PlayerScript myOwningPlayer;

	//public QuadScript myQuadScript;

    public int myCardCapacity = 10;

	public float myOffset = 2.0f;

	public CardScript[] myCards
	{
		//	one day this getter
		//	will be glorius
		//	once children are computed once per update
		get
		{
			return transform.GetComponentsInChildren<CardScript>();
		}
	}

	void Awake()
	{
		//myCards = transform.GetComponentsInChildren<CardScript>();
	}

	void Update()
	{
		//myCards = transform.GetComponentsInChildren<CardScript>();
	}

    public int CountCards()
    {
        return myCards.Length;
    }

	public CardScript GetCard(int index)
	{
		return index < myCards.Length ? myCards[index] : null;
	}

	//	Inserts a card into the hand,
	//		returns false if not successful
    public bool InsertCard(CardScript input, bool discard = true)
    {
		if (null != input)
		{
			EffectScript.AffectsList(input.myInsertEffects, myOwningPlayer.ToMessage());
			
			int cardCount = CountCards();

			if (cardCount < myCardCapacity)
			{
				input.myHandScript = this;

				Reanimate();

				return true;
			}
			else if (discard)
			{
				EffectScript.AffectsList(input.myRemoveEffects, myOwningPlayer.ToMessage());
			}
		}
		return false;
    }

	public CardScript RemoveCard(CardScript input, bool discard = false)
    {
        if (input.transform.IsChildOf(transform))
        {
			input.myHandScript = null;

			Reanimate();

			if (discard)
			{
				EffectScript.AffectsList(input.myRemoveEffects, myOwningPlayer.ToMessage());
			}

            return input;
        }
        return null;
    }

	public void Reanimate ()
	{
		CardScript[] handCardScripts = myCards;

		for (int i = 0, n = handCardScripts.Length; i < n; i++)
		{
			handCardScripts[i].myPosition.Reanimate(CardPosition(i, n), 1f);

			handCardScripts[i].myRotation.Reanimate(CardRotation(i, n), .5f);
		}
	}

	public Quaternion CardRotation(int cardIndex, int cardCount)
	{
		return Quaternion.Euler(0f, 0f, cardCount > 5 ? 
			Lerper.Erp(
				60f,
				-60f,
				(cardIndex + 1f) / (cardCount + 1f)):
			0f);
	}

	int Find (CardScript cardScript)
	{
		for(int i = 0; i < myCards.Length; i++)
		{
			if(myCards[i] == cardScript)
			{
				return i;
			}
		}
		return -1;
	}

	public Quaternion CardRotation(CardScript input)
	{
		return CardRotation(Find(input), CountCards());
	}

	public Vector3 CardPosition(int cardIndex, int cardCount)
	{
		Vector3 offset = new Vector3(myOffset, 0f, -.1f);
		return Vector3.Lerp(-offset, offset, (1.0f + cardIndex) / (1.0f + cardCount));
	}

	public Vector3 CardPosition(CardScript input)
	{
		return CardPosition(Find(input), CountCards());
	}

}
