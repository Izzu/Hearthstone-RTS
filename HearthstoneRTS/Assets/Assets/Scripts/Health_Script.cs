using UnityEngine;
using System.Collections;

public class Health_Script : Effect_Script {

	[SerializeField]
	private int myHealthValue, myDamageValue;

	[SerializeField]
	private Effect_Script[] myHealthEffects, myDamageEffects;
	
	public int health
	{
		get
		{
			return Effect_Script.AffectsList(myDamageEffects, myDamageValue, gameObject, gameObject);
		}
	}

	public int damage
	{
		get
		{
			return Effect_Script.AffectsList(myHealthEffects, myHealthValue, gameObject, gameObject);
		}
	}

	public bool isDamaged
	{
		get
		{
			return damage > 0;
		}
	}

	public override int Affect(int input, GameObject inSubject, GameObject inObject)
	{
		return health - damage;
	}

}
