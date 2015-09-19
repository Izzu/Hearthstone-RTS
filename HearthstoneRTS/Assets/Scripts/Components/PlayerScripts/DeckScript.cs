using UnityEngine;
using System.Collections;

public class DeckScript : MonoBehaviour {

	public PlayerScript myPlayerScript;

	public int myCardCapacity = 52;

	public Lerper3 mySize;

	public Slerper myRotation;

	private Quaternion myIdleRotation;

	public float myInspectAngle;

	public CardScript[] myCards
	{
		get 
		{ 
			return transform.GetComponentsInChildren<CardScript>(); 
		}
	}

	public int Length 
	{ 
		get
		{
			return myCards.Length;
		}
	}

	void Awake ()
	{
		mySize = new Lerper3();

		myRotation = new Slerper();

		//myCards = transform.GetComponentsInChildren<CardScript>();
	}

	// Use this for initialization
	void Start () 
	{
		mySize.Animate(DeckSize(), 1f);

		myIdleRotation = transform.localRotation;

		myRotation.Animate(myIdleRotation, 1f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.localRotation = myRotation.Slerp();

		transform.localScale = mySize.Lerp();

		//myCards = transform.GetComponentsInChildren<CardScript>();
	}

	//	When over capacity:
	//		returns false
	//		doesn't put the card in deck
	public bool InsertCard(CardScript input)
	{
		if (null != input && Length < myCardCapacity)
		{
			input.transform.parent = transform;

			input.myDeckScript = this;

			input.myRenderer.enabled = false;

			input.myBoxCollider.enabled = false;

			input.myPosition.Animate(Vector3.zero, 2);

			mySize.Animate(DeckSize(), .2f);

			return true;
		}
		return false;
	}

	public CardScript RemoveCard()
	{
		CardScript[] deckCardScript = myCards;
		if (deckCardScript.Length > 0)
		{
			CardScript cardScript = deckCardScript[0];

			cardScript.myDeckScript = null;

			cardScript.myRenderer.enabled = true;

			cardScript.myBoxCollider.enabled = true;

			cardScript.transform.position = transform.position;

			cardScript.myPosition.Reset(cardScript.transform.localPosition);

			cardScript.transform.parent = null;
			

			mySize.Animate(DeckSize(), .2f);

			return cardScript;
		}

		return null;
	}

	void OnMouseEnter()
	{
		GlobalScript.ourCursorScript.myDeckScript = this;

		myRotation.Animate(Quaternion.Euler(0f, myInspectAngle, 0f), .2f);

		mySize.Animate(DeckSize(), .2f);
	}

	void OnMouseExit()
	{
		if(false == Input.GetMouseButton(0))
		{
			GlobalScript.ourCursorScript.myDeckScript = null;

			myRotation.Animate(myIdleRotation, .2f);

			mySize.Animate(DeckSize(), .2f);
		}
	}

	public Vector3 DeckSize()
	{
		int count = Length;

		return count > 0 ? 
			new Vector3(
				.5f,
				(null != GlobalScript.ourCursorScript && GlobalScript.ourCursorScript.myDeckScript == this ? 2f : .75f)
				, count * .005f
			) : 
			Vector3.zero;
	}

	/*public Quaternion DeckRotation ()
	{
		return Quaternion.Euler(305.3767f, 93.77283f, 310.0072f);
	}*/

}
