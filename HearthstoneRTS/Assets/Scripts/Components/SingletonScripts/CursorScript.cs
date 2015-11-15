using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CursorScript : MonoBehaviour
{

	private Vector3 myTransformScreenPoint;

	public CardScript myCardScript = null;

	public UnitScript myUnitScript = null;

	public DeckScript myDeckScript = null;

	public HashSet<UnitScript> myOnScreenOwnedUnitScripts = new HashSet<UnitScript>();

	public HashSet<UnitScript> myOnScreenUnownedUnitScripts = new HashSet<UnitScript>();

	public float myRayLength = 100f;

	private float myLastClickTime = -1f;

	private float myLastDoubleClickTime = -1f;

	public float LastDoubleClickTime
	{
		get { return myLastDoubleClickTime;  }
	}

	public float myDoubleClickWait = .5f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		PlayerScript mainPlayerScript = GlobalScript.ourGlobalScript.myMainPlayerScript;
		
		//move cursor's card
		if(myCardScript)
		{
			myCardScript.transform.position = Camera.main.ScreenToWorldPoint(Cursor());
		}

		//deck uninspection
		if(null != myDeckScript)
		{
			if(Input.GetMouseButtonUp(0))
			{
				DeckScript deckScript = myDeckScript;

				myDeckScript = null;

				deckScript.myRotation.Animate(Quaternion.Euler(305.3767f, 93.77283f, 310.0072f), .2f);

				deckScript.mySize.Animate(deckScript.DeckSize(), .2f);
			}
		}
	}

	//screen point
	public Vector3 CursorDown (Transform intransform)
	{
		myTransformScreenPoint = Camera.main.WorldToScreenPoint(intransform.position);

		return myTransformScreenPoint;
	}

	//screen point
	public Vector3 Cursor ()
	{
		return new Vector3(Input.mousePosition.x, Input.mousePosition.y, myTransformScreenPoint.z);
	}

	public bool DoubleClick()
	{
		return Time.time - myLastClickTime < myDoubleClickWait;
	}

}
