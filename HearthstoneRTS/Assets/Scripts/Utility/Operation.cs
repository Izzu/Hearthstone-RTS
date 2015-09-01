using UnityEngine;
using System.Collections;

//	Operations pair delegates with a float
//		<Damage, 4f>
//		<Summon, 3.14f>
//		^ this is the basic idea

//This is a pairing of a delegate and a float value
//Default constructor puts value = 1f
[System.Serializable]
public class Operation
{
	public Enum myEnum = Enum.Null;
	public float myValue = 1f;

	public Operation(Enum input = Enum.Null, float value = 1f)
	{
		myEnum = input;
		myValue = value;
	}

	// Delegate is what all operations call
	// Messages and a float are input into them
	private delegate int Method(Message message, float value = 1f);

	//enums for operations
	public enum Enum
	{
		Null,//Null operations can be recycled
		Value,
		AddMana,
		AddGold,
		ForceBolt,
		Damage,
		Attack,
		NegHandSize,
		Blizzard,
		Frost
	}

	//Operation table, index with Enums
	private static Method[] ourMethods = 
	{
		Value,//Null has a dummy method, and are replaces in Push()
		Value,
		AddMana,
		AddGold,
		ForceBolt,
		Damage,
		Attack,
		NegHandSize,
		Blizzard,
		Frost
	};
	
	// Uses the delegate indexed by enum with the float and a message
	public int Activate(Message message)
	{
		return ourMethods[(int)myEnum](message, myValue);
	}

	//	Inserts an operation to an operation array,
	//	If too small [int inch] determines how much to grow by
	//	Handles list = <null> 
	static public Operation[] Push (Operation[] list, Operation input, int inch = 2)
	{
		//	Handle idiots inching wrong
		inch = inch < 1 ? 1 : inch;

		//	Handle null list, i.e. 
		//		"Make me a new list and put this on it"
		if (null == list)
		{
			list = new Operation[2];
			list[0] = input;
			return list;
		}
		else
		{
			//	Find a null indeces to recycle
			for (int i = 0; i < list.Length; i++)
			{
				if (list[i].myEnum == Enum.Null)
				{
					list[i] = input;
					return list;
				}
			}

			//	No vacancies, so make a new list
			Operation[] newList = new Operation[list.Length + 2];

			//	Put old values into new list
			for (int i = 0; i < list.Length; i++)
			{
				newList[i] = list[i];
			}
			
			//	Put input on new list
			newList[list.Length] = input;

			//return new list
			return newList;
		}
	}

	//	For those too lazy to if(null != list) then foreach op in list, op.activate
	//	Handles null list
	static public void ActivateList (Operation[] list, Message input)
	{
		if (null == list)
		{
			return;
		}
		for(int i = 0; i < list.Length; i++)
		{
			list[i].Activate(input);
		}
	}























	/************************************************
	 *												*
	 *												*
	 *			Defined Methods for ops				*
	 *												*
	 *												*
	 ************************************************/

	static int Attack (Message message, float value = 1f)
	{
		/****************************************
		 *
		 *		Eventually this will get done
		 *
		 *
		 *
		 *
		 ****************************************/

		return 0;
	}

	static int Frost (Message message, float value = 1f)
	{
		UnitScript unitScript = message.myObject.myUnitScript;

		int power = (int)value;

		if (null != unitScript)
		{
			unitScript.Damage(power);
		}

		return 0;
	}

	static int Blizzard (Message message, float value = 1f)
	{
		Object prefab = Resources.Load("Prefabs/Projectiles/Blizzard");

		Vector3 position = message.myObject.myPosition;

		GameObject gameObject = Transform.Instantiate(prefab, position, Quaternion.Euler(Vector3.up)) as GameObject;

		RainScript rainScript = gameObject.GetComponent<RainScript>();

		rainScript.myWaves = (int)value;

		rainScript.myMessage = message;

		return 0;
	}

	static int Value (Message message, float value = 1f)
	{
		return (int)value;
	}

	static int ForceBolt(Message message, float value = 1f)
	{
		Object prefab = Resources.Load("Prefabs/Projectiles/Projectile");

		Vector3 position = message.mySubject.myPosition;

		Vector3 destination = message.myObject.myPosition;

		Vector3 difference = destination - position;

		Quaternion rotation = Quaternion.LookRotation(difference);

		GameObject gameObject = Transform.Instantiate(prefab, Vector3.Lerp(position, destination, .8f / difference.magnitude), rotation) as GameObject;

		ProjectileScript projectileScript = gameObject.GetComponent<ProjectileScript>();

		projectileScript.myMessage = message;

		projectileScript.myDeleteOnUnit = true;

		projectileScript.myClocker = new Clocker(1f);

		projectileScript.myHitUnit = new Operation[1];

		projectileScript.myHitUnit[0] = new Operation(Enum.Damage, value);

		gameObject.GetComponent<Renderer>().material.color = new Color(160f / 255f, 82f / 255f, 45f / 255f);

		gameObject.transform.localScale = new Vector3(.3f, .3f, .3f);

		gameObject.GetComponent<Rigidbody>().AddForce(1000f * difference.normalized);

		gameObject.name = prefab.name;

		return 0;
	}

	static int Damage(Message message, float value = 1f)
	{
		UnitScript unitScript = message.myObject.myUnitScript;

		int power = (int)value;

		if (null != unitScript)
		{
			unitScript.Damage(power);
		}

		return 0;
	}

	static int NegHandSize (Message message, float value = 1f)
	{
		PlayerScript playerScript = message.mySubject.myPlayerScript;

		if (null != playerScript)
		{
			return - playerScript.myHandScript.CountCards();
		}

		return 0;
	}

	static int AddMana(Message message, float value = 1f)
	{
		PlayerScript playerScript = message.mySubject.myPlayerScript;

		int power = (int)value;

		if (null != playerScript)
		{
			playerScript.AddMana(power);
		}
		return 0;
	}

	static int AddGold(Message message, float value = 1f)
	{
		PlayerScript playerScript = message.mySubject.myPlayerScript;

		int power = (int)value;

		if (null != playerScript)
		{
			playerScript.AddGold(power);
		}

		return 0;
	}

}
