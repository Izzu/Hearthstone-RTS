using UnityEngine;
using System.Collections;

public class FactoryEffect : EffectScript {

	public GameObject myGameObject;

	public float myHeight;

	public override float Affect(Message message, float input)
	{
		if (null == myGameObject)
		{
			return input;
		}

		GameObject gameObject = Instantiate(myGameObject, message.myObject.myPosition + Vector3.up * myHeight, Quaternion.identity) as GameObject;

		gameObject.name = myGameObject.name;

		return input;
	}

}
