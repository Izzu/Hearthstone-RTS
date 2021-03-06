﻿using UnityEngine;
using System.Collections;

public class BugAnimationScript : MonoBehaviour 
{

	[SerializeField]
	Animator myAnimator;

	void AttackAnimation()
	{
		if (myAnimator)
			myAnimator.SetInteger("state", 1);

		myWait = 1f;
	}

    void DeathAnimation()
	{
		if (myAnimator)
			myAnimator.SetInteger("state", 5);

		myWait = 2f;
    }

	void Update()
	{
		Vector3 position = transform.position;

		Vector3 diff = position - myLastPosition;

		myLastPosition = position;

		if(myWait > 0f)
		{
			myWait -= Time.deltaTime;
		}
		else
		{
			if ((diff).magnitude > 0f)
			{
				Debug.Log(myWait);

				if (myAnimator)
					myAnimator.SetInteger("state", 7);
			}
			else
			{
				if (myAnimator)
					myAnimator.SetInteger("state", 0);
			}
		}
	}

	private float myWait = 0f;
	private Vector3 myLastPosition;
}
