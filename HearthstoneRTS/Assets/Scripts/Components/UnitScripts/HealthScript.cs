using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour
{
	[SerializeField]
	private int myStrength, myDamage;

	[SerializeField]
	private EffectScript[] myDamageEffects, myHealEffects;
	//needs a way to append efects

	private UnitScript myUnit;

	public int strength
	{
		get
		{
			return myStrength;
		}
	}

	public int health
	{
		get
		{
			return myStrength - myDamage;
		}
	}

	public int damage
	{
		get
		{
			return myDamage;
		}
	}

	public bool isDamaged
	{
		get
		{
			return damage > 0;
		}
	}

	public bool isAlive
	{
		get
		{
			return health > 0;
		}
	}

	void Awake ()
	{
		myUnit = GetComponent<UnitScript>();
	}

	public void Damage(int input)
	{
		if(input <= 0)
		{
			return;
		}

		if(null != myDamageEffects)
		{
			input = (int)EffectScript.AffectsList(
				myDamageEffects, 
				null == myUnit ? 
					new Message(new Message.Term(), new Message.Term()) : 
					myUnit.ToMessage(), 
				(float)input);
		}

		if(input <= 0)
		{
			return;
		}

		/*myOwningPlayer.myFrameData.myDamages++;
		myOwningPlayer.myTurnData.myFrameData.myDamages++;
		myOwningPlayer.myMatchData.myTurnData.myFrameData.myDamages++;

		GlobalScript.ourPlayerFrameData.myDamages++;
		GlobalScript.ourPlayerTurnData.myFrameData.myDamages++;
		GlobalScript.ourPlayerMatchData.myTurnData.myFrameData.myDamages++;*/

		myDamage += input;

		Checkup();
	}

	public void Checkup()
	{
		if (myDamage >= myStrength && null != myUnit)
		{
			myUnit.Death();
		}
	}

	public void Heal(int input)
	{
		if (input <= 0)
		{
			return;
		}

		if (null != myHealEffects)
		{
			input = (int)EffectScript.AffectsList(
				myHealEffects,
				null == myUnit ?
					new Message(new Message.Term(), new Message.Term()) :
					myUnit.ToMessage(),
				(float)input);
		}

		if (input == 0)
		{
			return;
		} 
		else if(input < 0)
		{
			Damage(-input);
			return;
		}

		/*myOwningPlayer.myFrameData.myHeals++;
		myOwningPlayer.myTurnData.myFrameData.myHeals++;
		myOwningPlayer.myMatchData.myTurnData.myFrameData.myHeals++;

		GlobalScript.ourPlayerFrameData.myHeals++;
		GlobalScript.ourPlayerTurnData.myFrameData.myHeals++;
		GlobalScript.ourPlayerMatchData.myTurnData.myFrameData.myHeals++;*/

		myDamage = input > myDamage ? 0 : myDamage - input;

		Checkup();
	}
}
