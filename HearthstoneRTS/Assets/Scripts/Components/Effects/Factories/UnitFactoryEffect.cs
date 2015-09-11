using UnityEngine;
using System.Collections;

public class UnitFactoryEffect : EffectScript
{

	public GameObject myGameObject;

	public float myHeight;

	public bool myObject;

	public override float Affect(Message message, float input)
	{
		if (null == myGameObject)
		{
			return input;
		}

		GameObject gameObject = Instantiate(myGameObject, message.myObject.myPosition + Vector3.up * myHeight, Quaternion.identity) as GameObject;

		gameObject.name = myGameObject.name;

		//set owner
		UnitScript unit = gameObject.GetComponent<UnitScript>();
		if (null == unit)
		{

		}
		else
		{
			PlayerScript player = null == message ? null : message.Player(false);

			if(null != player)
			{
				if(null != unit.myOwner)
				{
					player.Register(unit.myOwner, unit.myOwner.demand, unit.myOwner.supply);
				}
			}
			//what should happen first?
			//	1. unit registers himself with player
			//	2. player registers unit, but this makes no sense
			//when should it happen?
			//	1. unit awake
			//	2. unit start

			//player.Register(owned)	<- player is acted upon
			//	-owned.Own(this)		<- owned is acted upon by player.Register

			//owned.Own()				<- owned is acted upon by player.Register
			//	-player.Release(this)	<- player resources are freed

			//things to consider:
			//beginning matches with set unit ownerships
			//game creating units at runtime
			//unity creating units at anytime
			
		}

		return input;
	}

}
