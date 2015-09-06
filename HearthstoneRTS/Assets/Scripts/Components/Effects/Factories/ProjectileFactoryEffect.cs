using UnityEngine;
using System.Collections;

public class ProjectileFactoryEffect : EffectScript {

	public GameObject myGameObject;

	public float myHeight;

	public float myForce;

	public override float Affect(Message message, float input)
	{
		if (null == myGameObject)
		{
			return input;
		}

		Vector3 position = message.mySubject.myPosition;

		Vector3 destination = message.myObject.myPosition;

		Vector3 difference = destination - position;

		GameObject gameObject = Instantiate(
			myGameObject,
			Vector3.Lerp(position, destination, .8f / difference.magnitude) + Vector3.up * myHeight, 
			Quaternion.LookRotation(difference)) as GameObject;

		gameObject.name = myGameObject.name;

		//creating a new projectile
		ProjectileScript projectileScript = gameObject.GetComponent<ProjectileScript>();

		projectileScript.myMessage = message;

		//add forse
		gameObject.GetComponent<Rigidbody>().AddForce(myForce * difference.normalized);

		return input;
	}
}
