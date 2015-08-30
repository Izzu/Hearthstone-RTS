using UnityEngine;
using System.Collections;

public class CardScript : MonoBehaviour {

	public int myHandIndex;

	public Lerper myPosition = new Lerper();

	private Lerper mySize = new Lerper(new Vector3(.5f, .75f, .01f));

	public Slerper myRotation = new Slerper();

	public HandScript myHandScript
	{
		get 
		{ 
			return transform.parent.GetComponent<HandScript>(); 
		}

		set
		{
			transform.parent = value.transform;
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

	public delegate int MethodEffect(Message inMessage);

	public MethodEffect myPlayEffect;
	public MethodEffect myDrawEffect;
	public MethodEffect myDiscardEffect;

	public static int ManaEffect (Message input)
	{
		input.mySubject.myPlayerScript.myMana++;

		return 0;
	}

	void Start()
	{
		myPlayEffect = ManaEffect;
	}

	// Update is called once per frame
	void Update () {

		transform.localRotation = myRotation.Slerp();
		
		transform.localPosition = myPosition.Lerp();

		transform.localScale = mySize.Lerp();
	}
	
	void OnMouseDrag ()
	{
		if(null != myHandScript && myHandScript.myOwningPlayer == GlobalScript.ourGlobalScript.myMainPlayerScript)
		{
			transform.position = Camera.main.ScreenToWorldPoint(GlobalScript.ourCursorScript.Cursor());

			myPosition.Reset(transform.localPosition);
		}
	}

	void OnMouseEnter()
	{
		if (GlobalScript.ourCursorScript.myCardScript != this)
		{
			myRotation.Animate(Quaternion.Euler(0f, 0f, 0f), .2f);

			myPosition.Animate(myHandScript.CardPosition(this) + new Vector3(0f, .5f, -.1f), .2f);

			mySize.Animate(new Vector3(1f, 1.5f, .01f), .2f);
		}
	}

	void OnMouseExit()
	{
		if (GlobalScript.ourCursorScript.myCardScript != this)
		{
			myRotation.Animate(myHandScript.CardRotation(this), .2f);

			myPosition.Animate(myHandScript.CardPosition(this), .2f);

			mySize.Animate(new Vector3(.5f, .75f, .01f), .2f);
		}
	}

	void OnMouseDown()
	{
		GlobalScript.ourCursorScript.myCardScript = this;

		if (null != myHandScript && myHandScript.myOwningPlayer == GlobalScript.ourGlobalScript.myMainPlayerScript)
		{

			GlobalScript.ourCursorScript.CursorDown(transform);

			mySize.Animate(new Vector3(.25f, .375f, .01f), .2f);

			myRotation.Animate(Quaternion.Euler(0f, 0f, 0f), 0.2f);
		}
	}
 
	void OnMouseUp ()
	{
		GlobalScript.ourCursorScript.myCardScript = null;

		if (null != myHandScript && myHandScript.myOwningPlayer == GlobalScript.ourGlobalScript.myMainPlayerScript)
		{
			Debug.Log(Input.mousePosition.y / Screen.height);
			if (Input.mousePosition.y / Screen.height > .2f)
			{
				int manaCost = CostMana;
				if(manaCost > myHandScript.myOwningPlayer.myMana)
				{
					//myPlayEffect(new Message(new Message.Term(), new Message.Term()));
				}
			}

			myPosition.Reset(transform.localPosition).Animate(myHandScript.CardPosition(this), .2f);

			myRotation.Reset(transform.localRotation).Animate(myHandScript.CardRotation(this), .2f);
		}
	}

	private int myCostMana = 0;

	public int CostMana
	{
		get 
		{ 
			return myCostMana; 
		}
		set 
		{ 
			myCostMana = value; 
		}
	}

	void OnGUI ()
	{
		Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

		Vector2 GUIposition = new Vector2(screenPosition.x, Screen.height - screenPosition.y);

		GUI.Box(new Rect(GUIposition, new Vector2(100f, 20f)), transform.name);
	}

}
