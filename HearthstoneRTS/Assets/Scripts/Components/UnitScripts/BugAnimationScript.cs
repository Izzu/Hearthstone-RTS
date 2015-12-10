using UnityEngine;
using System.Collections;

public class BugAnimationScript : MonoBehaviour 
{

	[SerializeField]
	Animator myAnim;

	void AttackAnimation()
	{
		myAnim.SetInteger("state", 1);
		myWait = 1f;
	}

    void DeathAnimation()
	{
		myAnim.SetInteger("state", 5);
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
				Debug.Log((diff).magnitude);

				if (myWait < 0f)
				{
					myAnim.SetInteger("state", 7);
					myWait = 3f;
				}
			}
			else
			{
				//myAnim.SetInteger("state", 0);
			}
		}
	}

	private float myWait;
	private Vector3 myLastPosition;
}
