using UnityEngine;
using System.Collections;

public class InteractionScript : MonoBehaviour {

	private UnitScript myUnit;

	public UnitScript myTarget;

	//clocker waits
	private float myWait;
	[SerializeField]
	private float myDuration;

	//private int myLastPhase;

	private bool isOn = false;

	[SerializeField]
	public bool isLoop = false;

	//[System.Serializable]
	//public class Data
	//{
		//public bool isBusy, canMove;

		public EffectScript myEffect;

		public float myValue;

		public float myRange;

	//	public Messenger_Script myMessenger;
	//}




	//[System.Serializable]
	//private class Wrap : Phaser<Data> {}

	//[SerializeField]
	//private Wrap[] myPhases;


	public void Stop ()
	{
		isOn = false;
	}

	public UnitScript target
	{
		set
		{
			myTarget = value;
		}
	}

	public UnitScript self
	{
		set
		{
			myUnit = value;
		}
	}

	//public Data data 
	//{
	//	get
	//	{
	//		if(isActive)
	//		{
	//			return myPhases[Phaser<Data>.Phase(begin, myPhases)].myData;
	//		}
	//		return null;
	//	}
	//	set
	//	{
	//		myPhases[Phaser<Data>.Phase(begin, myPhases)].myData = value;
	//	}
	//}

	void Awake()
	{
		myUnit = GetComponent<UnitScript>();

		isOn = true;
	}

	void Update()
	{
		Activate();
	}

	//public int count
	//{
	//	get
	//	{
	//		return myPhases.Length;
	//	}
	//}

	public void Activate ()
	{
		if (myWait <= 0f)
		{
			myWait = myDuration;

			HealthScript health = myUnit.GetComponent<HealthScript>();

			if(health.isAlive)
			{
				if (myTarget)
				{
					myDistance = (myTarget.transform.position - myUnit.transform.position).magnitude;
					if (myDistance < myRange)
					{
						myEffect.Affect(new Message(myUnit.ToTerm(), myTarget.ToTerm()), 0f);
					}
				}
			}
		}
		else
		{
			myWait -= Time.deltaTime;
		}
	}

	float myDistance;
}
