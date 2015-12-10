using UnityEngine;
using System.Collections;

public class BugAnimationScript : MonoBehaviour 
{

	[SerializeField]
	Animation myAnimation;

	[SerializeField]
	Messenger_Script myAttackMessenger;

    [SerializeField]
    Messenger_Script myDeathMessenger;

	void Start ()
	{
		myAttackMessenger.Subscribe(new Messenger_Script.Subscription(gameObject, "Attack"));
        myDeathMessenger.Subscribe(new Messenger_Script.Subscription(gameObject, "DeathAnimation"));
	}

	void Attack()
	{
        Debug.Log("playing attk animation");
		myAnimation.Play("Attack");
		myAttacking = 1f;
	}

    void DeathAnimation()
    {
        myAnimation.Play("Die_1");
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
