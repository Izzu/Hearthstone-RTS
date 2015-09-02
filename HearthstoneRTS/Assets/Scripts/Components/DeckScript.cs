using UnityEngine;
using System.Collections;

public class DeckScript : MonoBehaviour {

	public PlayerScript myPlayerScript;

	public int myCardCapacity = 52;

	public Lerper3 mySize;

	public Slerper myRotation;

	private Quaternion myIdleRotation;

	public float myInspectAngle;

	void Awake ()
	{
		mySize = new Lerper3();

		myRotation = new Slerper();
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
	}

	public int CountCards()
	{
		return transform.GetComponentsInChildren<CardScript>().Length;
	}

	//	When over capacity:
	//		returns false
	//		doesn't put the card in deck
	public bool InsertCard(CardScript input)
	{
		if (null != input && CountCards() < myCardCapacity)
		{
			input.transform.parent = transform;

			input.myDeckScript = this;

			input.GetComponent<Renderer>().enabled = false;

			input.GetComponent<BoxCollider>().enabled = false;

			input.myPosition.Animate(Vector3.zero, 2);

			mySize.Animate(DeckSize(), .2f);

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

			cardScript.myDeckScript = null;

			cardScript.GetComponent<Renderer>().enabled = true;

			cardScript.GetComponent<BoxCollider>().enabled = true;

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
		int count = CountCards();

		return count > 0 ? 
			new Vector3(
				.5f,
				(null != GlobalScript.ourCursorScript && GlobalScript.ourCursorScript.myDeckScript == this ? 2f : .75f)
				, CountCards() * .005f
			) : 
			Vector3.zero;
	}

	/*public Quaternion DeckRotation ()
	{
		return Quaternion.Euler(305.3767f, 93.77283f, 310.0072f);
	}*/

}
