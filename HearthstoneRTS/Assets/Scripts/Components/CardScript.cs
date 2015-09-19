using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardScript : MonoBehaviour {

	public enum Type
	{
		Null,
		Item,
		Spell,
		Unit
	}

	public Lerper3 myPosition;

	private Lerper3 mySize;

	public Slerper myRotation;

	public EffectScript[] myPlayEffects;
	public EffectScript[] myInsertEffects;
	public EffectScript[] myRemoveEffects;

	public Type myType = Type.Null;

	void Awake ()
	{
		myPosition = new Lerper3();

		mySize = new Lerper3(new Vector3(.5f, .75f, .01f));

		myRotation = new Slerper();

		myRenderer = transform.GetComponent<Renderer>();

		myBoxCollider = transform.GetComponent<BoxCollider>();
	}

	public HandScript myHandScript
	{
		get 
		{
			return null != transform.parent ? transform.parent.GetComponent<HandScript>() : null; 
		}

		set
		{
			HandScript handScript = value;

			transform.parent = null != handScript ? handScript.transform : null;
		}
	}

	public DeckScript myDeckScript
	{
		get
		{
			return transform.parent.GetComponent<DeckScript>();
		}

		set
		{
			DeckScript deckScript = value;

			transform.parent = null;
			
			if (null != deckScript)
			{
				transform.parent = deckScript.transform;
			}
		}
	}

	// Update is called once per frame
	void Update () {

		transform.localRotation = myRotation.Slerp();
		
		transform.localPosition = myPosition.Lerp();

		transform.localScale = mySize.Lerp();
	}
	
	void OnMouseDrag ()
	{
		HandScript handScript = myHandScript;

		if (null != handScript && handScript.myOwningPlayer == GlobalScript.ourGlobalScript.myMainPlayerScript)
		{
			transform.position = Camera.main.ScreenToWorldPoint(GlobalScript.ourCursorScript.Cursor());

			myPosition.Reset(transform.localPosition);
		}
	}

	void OnMouseEnter()
	{
		if (GlobalScript.ourCursorScript.myCardScript != this && null != myHandScript)
		{
			myRotation.Animate(Quaternion.Euler(0f, 0f, 0f), .2f);

			myPosition.Animate(myHandScript.CardPosition(this) + new Vector3(0f, .5f, -.1f), .2f);

			mySize.Animate(new Vector3(.5f, .75f, .01f) * 1.5f, .2f);
		}
	}

	void OnMouseExit()
	{
		HandScript handScript = myHandScript;

		if (handScript && GlobalScript.ourCursorScript.myCardScript != this)
		{
			myRotation.Animate(handScript.CardRotation(this), .2f);

			myPosition.Animate(handScript.CardPosition(this), .2f);

			mySize.Animate(new Vector3(.5f, .75f, .01f), .2f);
		}
	}

	void OnMouseDown()
	{
		GlobalScript.ourCursorScript.myCardScript = this;

		HandScript handScript = myHandScript;

		if (null != handScript && handScript.myOwningPlayer == GlobalScript.ourGlobalScript.myMainPlayerScript)
		{
			GlobalScript.ourCursorScript.CursorDown(transform);

			mySize.Animate(new Vector3(.25f, .375f, .01f), .2f);

			myRotation.Animate(Quaternion.Euler(0f, 0f, 0f), 0.2f);
		}
	}
 
	void OnMouseUp ()
	{
		GlobalScript.ourCursorScript.myCardScript = null;
		
		//store handscript because it's not free
		HandScript handScript = myHandScript;

		PlayerScript playerScript = handScript.myOwningPlayer;

		//check if card is main player's
		if (null != handScript && playerScript == GlobalScript.ourGlobalScript.myMainPlayerScript)
		{

			//card must be over 20% of the screen to be used
			//Debug.Log(Input.mousePosition.y / Screen.height);
			if (Input.mousePosition.y / Screen.height > .2f)
			{
				Message message = playerScript.ToMessage();

				//raycast terrain
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit, GlobalScript.ourCursorScript.myRayLength, 1 << 8))
				{
					message.myObject.myPosition = hit.point;
				}

				//raycast units
				if (Physics.Raycast(ray, out hit, GlobalScript.ourCursorScript.myRayLength, 1 << 9))
				{
					UnitScript unitScript = hit.transform.GetComponent<UnitScript>();

					if (unitScript)
					{
						message.myObject = unitScript.ToTerm();
					}
				}

				//get cost of card
				int manaCost = CostMana(message);
				int goldCost = CostGold(message);

				//if player has enough
				if (manaCost <= playerScript.myMana)
				{
					if (goldCost <= playerScript.myGold)
					{
						//pay cost
						playerScript
							.SubMana(manaCost)
							.SubGold(goldCost)
							.AddDebt(CostDebt(message))
							.AddOverload(CostOverload(message));

						handScript.RemoveCard(this, false);

						playerScript.ActivateCard(this, message);

						Destroy(gameObject);
					}
					else
					{
						Debug.Log("Card: " + gameObject.name + " uses " + goldCost + " gold.");
					}
				}
				else
				{
					Debug.Log("Card: " + gameObject.name + " uses " + manaCost + " mana.");
				}

				return;
			}

			myPosition.Reset(transform.localPosition).Animate(myHandScript.CardPosition(this), .2f);

			myRotation.Reset(transform.localRotation).Animate(myHandScript.CardRotation(this), .2f);
		}
	}

	//delete this when cards have names
	void OnGUI()
	{
		HandScript handScript = myHandScript;

		if (null != handScript && handScript.myOwningPlayer == GlobalScript.ourGlobalScript.myMainPlayerScript)
		{
			Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

			Vector2 GUIposition = new Vector2(screenPosition.x - 75f * .5f, Screen.height - screenPosition.y + 10f);

			GUI.Box(new Rect(GUIposition, new Vector2(75f, 20f)), transform.name);
		}
	}
	












	/********************************
	 * 
	 *			Resources
	 * 
	 * *****************************/


	

	public Resource myMana, myGold, mySupply, myOverload, myDebt;

	public int CostMana(Message message)
	{
		return myMana.Cost(message);
	}

	public int CostSupply(Message message)
	{
		return mySupply.Cost(message);
	}

	public int CostGold(Message message)
	{
		return myGold.Cost(message);
	}

	public int CostOverload(Message message)
	{
		return myOverload.Cost(message);
	}

	public int CostDebt(Message message)
	{
		return myDebt.Cost(message);
	}

	public BoxCollider myBoxCollider;
	public Renderer myRenderer;

}
