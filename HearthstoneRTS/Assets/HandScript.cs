using UnityEngine;
using System.Collections;

public class HandScript : MonoBehaviour {

    public PlayerScript myOwningPlayer;

    public int myCardCapacity = 10;

	static private Vector3 ourDifference = new Vector3(1.8f, 0.0f, 0.0f);

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

    public bool InsertCard(CardScript input)
    {
        int cardCount = CountCards();
        if (cardCount < myCardCapacity)
        {
            input.myHandIndex = CountCards();

            input.transform.parent = transform;

            input.myHandScript = this;

			foreach (CardScript cardScript in transform.GetComponentsInChildren<CardScript>())
			{
				cardScript.myPosition.Reanimate(
					CardPosition(cardScript.myHandIndex, cardCount),
					0.2f);
			}

            return true;
        }
        return false;
    }

    //returns the success of the removal
    public CardScript RemoveCard(CardScript input)
    {
        if (input.transform.IsChildOf(transform))
        {
            foreach (CardScript cardScript in transform.GetComponentsInChildren<CardScript>())
            {
                if (cardScript.myHandIndex > input.myHandIndex)
                {
                    cardScript.myHandIndex--;
                }
            }
            input.transform.parent = null;

            return input;
        }
        return null;
    }

	public Vector3 CardPosition(int cardIndex, int cardCount)
	{
		return Vector3.Lerp(-ourDifference, ourDifference, (1.0f + cardIndex) / (1.0f + cardCount));
	}

	public Vector3 CardPosition(CardScript input)
	{
		return CardPosition(input.myHandIndex, CountCards());
	}

}
