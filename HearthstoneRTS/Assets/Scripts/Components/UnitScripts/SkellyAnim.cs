using UnityEngine;
using System.Collections;

public class SkellyAnim : MonoBehaviour 
{

	[SerializeField]
	Animation myAnimation;

	void AttackAnimation()
	{
		myAnimation.Play("attack01");
		myAttacking = 1f;
	}

	void DeathAnimation()
	{
		myAnimation.Play("death");
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
				if (Vector3.Dot(diff, transform.forward) > 0)
				{
					myAnimation.Play("walk01");
				}
			}
			else
			{
				myAnimation.Play("idle01");
			}
		}

		myLastPosition = position;
	}

	private float myAttacking;
	private Vector3 myLastPosition;
}
