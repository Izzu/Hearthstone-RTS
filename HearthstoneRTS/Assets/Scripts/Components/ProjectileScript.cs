using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

	public Operation[] myHitUnit;

	public Message myMessage;

	public bool myDeleteOnUnit = true;
	public Clocker myClocker;
	// Use this for initialization
	// Awake happens before start
	// Don't have interdependent starts and awakes
	void Start () 
	{
		if(null == myClocker) myClocker = new Clocker(21f);
	}
	
	// Update is called once per frame
	void Update()
	{
		if(!myClocker.IsActive())
		{
			Destroy(gameObject);
		}
	}

	//hits something
	void OnCollisionEnter(Collision collision)
	{
		UnitScript unitScript = collision.gameObject.GetComponent<UnitScript>();

		myMessage = new Message(new Message.Term(), new Message.Term());

		bool deleting = false;

		if (null != unitScript)
		{
			myMessage.myObject = unitScript.ToTerm();

			Operation.ActivateList(myHitUnit, myMessage);

			if (myDeleteOnUnit)
			{
				deleting = true;
			}
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
