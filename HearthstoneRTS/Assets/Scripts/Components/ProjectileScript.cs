﻿using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

	public Message myMessage;

	public bool myDeleteOnUnit = false;

	public bool mySelfTrigger = false;

	public bool myAllyTrigger = false;

	public EffectScript[] myHitUnit;

	public Clocker myClocker;

	public float myLifetime;

	void Awake()
	{
		if (myLifetime > 0f)
		{
			myClocker = new Clocker();
		}
	}

	// Update is called once per frame
	void Update()
	{
		if(null != myClocker && !myClocker.isActive)
		{
			Destroy(gameObject);
		}
	}

	//hits something
	void OnCollisionEnter(Collision collision)
	{
		UnitScript unitScript = collision.gameObject.GetComponent<UnitScript>();

		if (null == unitScript)
		{
			return;
		}

		bool deleting = false;

		if(null == myMessage.mySubject)
		{
			
		}
		else if(false == mySelfTrigger && unitScript == myMessage.mySubject.myUnitScript)
		{
			return;
		}

		if(false == myAllyTrigger && unitScript.myOwner == myMessage.mySubject.myPlayerScript)
		{
			return;
		}

		myMessage.myObject = unitScript.ToTerm();

		EffectScript.AffectsList(myHitUnit, myMessage);

		if (myDeleteOnUnit)
		{
			deleting = true;
		}

		if(deleting)
		{
			Destroy(gameObject);
		}
	}

	//exit
	/*void OnDisable ()
	{
		if (gameObject.activeSelf)
		{
			Debug.Log("Inactive: " + gameObject.name);
		}
		else
		{
			Debug.Log("Destroyed: " + gameObject.name);
			EffectMethods.Affect(myEnd, myMessage);
		}
	}*/

}
