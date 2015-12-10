using UnityEngine;
using System.Collections;

public class Bug2 : MonoBehaviour {
	[SerializeField]
	Animation myAnimation;

	void AttackAnimation()
	{
		myAnimation.Play("Attack");
		myAttacking = 1f;
	}

	void DeathAnimation()
	{
		myAnimation.Play("Die_Left");
		myAttacking = 100f;
	}

	void Update()
	{
		Vector3 position = transform.position;

		Vector3 diff = position - myLastPosition;

		if (myAttacking > 0f)
		{
			myAttacking -= Time.deltaTime;
		}
		else
		{
			myAttacking = 0f;
			if ((diff).magnitude > 0)
			{
				if (Vector3.Dot(diff, transform.forward) < 0)
				{
					myAnimation.Play("Walk");
				}
			}
			else
			{
				myAnimation.Play("Idle");
			}
		}

		myLastPosition = position;
	}

	private float myAttacking;
	private Vector3 myLastPosition;
}