using UnityEngine;
using System.Collections;

public class DeckScript : MonoBehaviour {

	public PlayerScript myPlayerScript;

	public int myCardCapacity = 52;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public int CountCards()
	{
		return transform.GetComponentsInChildren<CardScript>().Length;
	}

	public bool InsertCard(CardScript input)
	{
		if (CountCards() < myCardCapacity)
		{
			input.transform.parent = transform;

			input.myDeckScript = this;

			input.GetComponent<Renderer>().enabled = false;

			input.myPosition.Animate(Vector3.zero, 2);

			return true;
		}
		return false;
	}

	public CardScript RemoveCard()
	{
		CardScript[] deckCardScript = transform.GetComponentsInChildren<CardScript>();
		if (deckCardScript.Length > 0)
		{
			CardScript cardScript = deckCardScript[0];

			cardScript.transform.parent = null;
			cardScript.myDeckScript = null;
			cardScript.GetComponent<Renderer>().enabled = true;

			return cardScript;
		}
		return null;
	}

}
