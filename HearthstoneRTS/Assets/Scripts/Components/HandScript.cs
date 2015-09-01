using UnityEngine;
using System.Collections;

public class HandScript : MonoBehaviour {

    public PlayerScript myOwningPlayer;

	//public QuadScript myQuadScript;

    public int myCardCapacity = 10;

	public float myOffset = 2.0f;

    public int CountCards()
    {
        return transform.GetComponentsInChildren<CardScript>().Length;
    }

	public CardScript GetCard(int index)
	{
		foreach (CardScript cardScript in transform.GetComponentsInChildren<CardScript>())
		{
			if (cardScript.myHandIndex == index)
			{
				return cardScript;
			}
		}
		return null;
	}

	//	Inserts a card into the hand,
	//		returns false if not successful
    public bool InsertCard(CardScript input)
    {
		if (null != input)
		{
			input.myInsertEffect.Activate(myOwningPlayer.ToMessage());

			int cardCount = CountCards();

			if (cardCount < myCardCapacity)
			{
				input.myHandIndex = CountCards();

				input.myHandScript = this;

				foreach (CardScript cardScript in transform.GetComponentsInChildren<CardScript>())
				{
					cardScript.myPosition.Reanimate(CardPosition(cardScript.myHandIndex, cardCount + 1), 1f);

					cardScript.myRotation.Reanimate(CardRotation(cardScript.myHandIndex, cardCount + 1), .5f);
				}

				return true;
			}
			else
			{
				input.myRemoveEffect.Activate(myOwningPlayer.ToMessage());
			}
		}
		return false;
    }

    public CardScript RemoveCard(CardScript input, bool discarded = false)
    {
        if (input.transform.IsChildOf(transform))
        {
			int cardCount = CountCards();

            foreach (CardScript cardScript in transform.GetComponentsInChildren<CardScript>())
            {
                if (cardScript.myHandIndex > input.myHandIndex)
                {
                    cardScript.myHandIndex--;
                }

				cardScript.myPosition.Reanimate(CardPosition(cardScript.myHandIndex, cardCount - 1), 1f);

				cardScript.myRotation.Reanimate(CardRotation(cardScript.myHandIndex, cardCount - 1), .5f);
            }

			if (discarded)
			{
				input.myRemoveEffect.Activate(myOwningPlayer.ToMessage());
			}

			input.myHandScript = null;

            return input;
        }
        return null;
    }

	public Quaternion CardRotation(int cardIndex, int cardCount)
	{
		return Quaternion.Euler(0f, 0f, cardCount > 5 ? 
			Erper.Erp(
				60f,
				-60f,
				(cardIndex + 1f) / (cardCount + 1f)):
			0f);
	}

	public Quaternion CardRotation(CardScript input)
	{
		return CardRotation(input.myHandIndex, CountCards());
	}

	public Vector3 CardPosition(int cardIndex, int cardCount)
	{
		Vector3 offset = new Vector3(myOffset, 0f, -.1f);
		return Vector3.Lerp(-offset, offset, (1.0f + cardIndex) / (1.0f + cardCount));
	}

	public Vector3 CardPosition(CardScript input)
	{
		return CardPosition(input.myHandIndex, CountCards());
	}

}
