﻿using UnityEngine;
using System.Collections;

public class CardScript : MonoBehaviour {

	public int myHandIndex;

	public Lerper myPosition = new Lerper();

	private Lerper mySize = new Lerper(new Vector3(.5f, .75f, .01f));

	public Slerper myRotation = new Slerper();

    public HandScript myHandScript;

	// Update is called once per frame
	void Update () {

		transform.localRotation = myRotation.Slerp();
		
		transform.localPosition = myPosition.Lerp();

		transform.localScale = mySize.Lerp();
	}
	
	void OnMouseDown ()
	{
		myHandScript.myOwningPlayer.myCurserScript.myCardScript = this;

		myHandScript.myOwningPlayer.myCurserScript.CursorDown(transform);

		mySize.Animate(new Vector3(.5f, .75f, .01f), .2f);

		myRotation.Animate(Quaternion.Euler(30f, 0f, 0f), 0.2f);
	}
 
	void OnMouseDrag ()
	{
		transform.position = Camera.main.ScreenToWorldPoint(myHandScript.myOwningPlayer.myCurserScript.Cursor());

		myPosition.Set(transform.localPosition);
	}

	void OnMouseEnter()
	{
		myRotation.Animate(Quaternion.Euler(0f, 0f, 0f), .2f);

		myPosition.Animate(myHandScript.CardPosition(this) + new Vector3(0f, .5f, -.1f), .2f);

		mySize.Animate(new Vector3(1f, 1.5f, .01f), .2f);
	}

	void OnMouseExit()
	{
		myRotation.Animate(myHandScript.CardRotation(this), .2f);

		myPosition.Animate(myHandScript.CardPosition(this), .2f);

		mySize.Animate(new Vector3(.5f, .75f, .01f), .2f);
	}

	void OnMouseUp ()
	{
		myHandScript.myOwningPlayer.myCurserScript.myCardScript = null;

		myPosition.Set(transform.localPosition).Animate(myHandScript.CardPosition(this), .2f);

		myRotation.Set(transform.localRotation).Animate(myHandScript.CardRotation(this), .2f);
	}

}
