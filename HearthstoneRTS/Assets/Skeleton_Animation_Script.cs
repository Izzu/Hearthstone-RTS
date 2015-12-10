using UnityEngine;
using System.Collections;

public class Skeleton_Animation_Script : MonoBehaviour 
{

	[SerializeField]
	Animator myAnimator;

	void AttackAnimation()
	{
		if (myAnimator)
			myAnimator.SetInteger("state", 2);

		myWait = 1f;
	}

	void DeathAnimation()
	{
		if (myAnimator)
			myAnimator.SetInteger("state", 3);

		myWait = 2f;
	}

	void Update()
	{
		Vector3 position = transform.position;

		Vector3 diff = position - myLastPosition;

		if (myWait > 0f)
		{
			myWait -= Time.deltaTime;
		}
		else
		{
			if ((diff).magnitude > 0f)
			{
				if (myAnimator)
					myAnimator.SetInteger("state", 1);
			}
			else
			{
				Debug.Log("idle");

				if (myAnimator)
					myAnimator.SetInteger("state", 0);
			}
		}

		myLastPosition = position;
	}

	private float myWait = 0f;
	private Vector3 myLastPosition;
}
