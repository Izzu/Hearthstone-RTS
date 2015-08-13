using UnityEngine;
using System.Collections;

public class HandScript : MonoBehaviour {

    public PlayerScript myOwningPlayer;

    public int myCardCapacity = 10;

    public int CountCards()
    {
        return transform.GetComponentsInChildren<CardScript>().Length;
    }

    public bool InsertCard(CardScript input)
    {
        int cardCount = CountCards();
        if (cardCount < myCardCapacity)
        {
            input.myHandIndex = CountCards();

            input.transform.parent = transform;

            input.myHandScript = this;
            return true;
        }
        return false;
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

}
