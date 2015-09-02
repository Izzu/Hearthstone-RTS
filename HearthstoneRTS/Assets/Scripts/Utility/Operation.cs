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
	[SerializeField]
	private Enum myEnum = Enum.Null;

	[SerializeField]
	private float myValue = 1f;

	public Operation(Enum input = Enum.Null, float value = 1f)
	{
		myEnum = input;
		myValue = value;
	}

	// Delegate is what all operations call
	// Messages and a float are input into them
	private delegate int Method(Message message, float value = 1f);

	//enums for operations
	//when making enums: YOU MUST ADD THEM TO THE END
	public enum Enum
	{
		Null,//Null operations can be recycled
		Value,
		AddMana,
		AddManaCap,
		AddGold,
		ForceBolt,
		Damage,
		Attack,
		NegHandSize,
		Blizzard,
		Frost,
		Summon,
		Draw,
		Calldown
	}//NEW OPS APPEND TO THE END

	//Operation table, index with Enums
	private static Method[] ourMethods = 
	{
		Value,//Null has a dummy method, and are replaces in Push()
		Value,
		AddMana,
		AddManaCap,
		AddGold,
		ForceBolt,
		Damage,
		Attack,
		NegHandSize,
		Blizzard,
		Frost,
		Summon,
		Draw,
		Calldown
	};//NEW OPS APPEND TO THE END
	
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

	static int Summon(Message message, float value = 1f)
	{
		Object prefab = Resources.Load("Prefabs/Units/Unit");

		Vector3 position = message.myObject.myPosition;

		GameObject gameObject = Transform.Instantiate(prefab, position, Quaternion.Euler(Vector3.up)) as GameObject;

		UnitScript unitScript = gameObject.GetComponent<UnitScript>();

		unitScript.myOwningPlayer = message.mySubject.myPlayerScript;

		return 0;
	}

	static int Calldown(Message message, float value = 1f)
	{
		Object prefab = Resources.Load("Prefabs/Units/Building");

		Vector3 position = message.myObject.myPosition;

		GameObject gameObject = Transform.Instantiate(prefab, position + Vector3.up * 30, Quaternion.Euler(Vector3.up)) as GameObject;

		UnitScript unitScript = gameObject.GetComponent<UnitScript>();

		unitScript.myOwningPlayer = message.mySubject.myPlayerScript;

		return 0;
	}



















	/*************************************
	 * 
	 *		Finished Functions
	 * 
	 * ***********************************/

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
		Object prefab = Resources.Load("Prefabs/Projectiles/ForceBolt");

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

		gameObject.GetComponent<Renderer>().material.color = new Color(200f / 255f, 122f / 255f, 85f / 255f);

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

	static int AddManaCap(Message message, float value = 1f)
	{
		PlayerScript playerScript = message.mySubject.myPlayerScript;

		int power = (int)value;

		if (null != playerScript)
		{
			playerScript.AddManaCap(power);
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

	static int Draw(Message message, float value = 1f)
	{
		PlayerScript playerScript = message.mySubject.myPlayerScript;

		int power = (int)value;

		if (null != playerScript)
		{
			for (int i = 0; i < (int)value; i++)
			{
				playerScript.Draw();
			} 
		}

		return 0;
	}

}
