using UnityEngine;
using System.Collections;

public class InteractionScript : MonoBehaviour {

	private UnitScript myUnit;

	private UnitScript myTarget;

	private Clocker myClocker;
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

	public bool isActive
	{
		get
		{
			return isOn && myClocker.isActive;
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

	public float begin
	{
		set
		{
			myClocker.myBeginTime = value;
			myClocker.myEndTime = myClocker.myBeginTime + myDuration;

			isOn = true;
		}

		get
		{
			return myClocker.myBeginTime;
		}
	}

	void Awake()
	{
		myUnit = GetComponent<UnitScript>();

		isOn = false;
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
		if (isOn && myClocker.isActive)
		{
			Debug.Log("Active");
			if((myTarget.transform.position - myUnit.transform.position).magnitude < myRange)
			myEffect.Affect(
				new Message(myUnit.ToTerm(), null == myTarget ? new Message.Term() : myTarget.ToTerm()),
				0f);
		}
	}
}
