using UnityEngine;
using System.Collections;

public class InteractionScript : MonoBehaviour {

	private UnitScript myUnit;

	private UnitScript myTarget;

	private float myBegin;

	private int myLastPhase;

	private bool isOn = false;




	[System.Serializable]
	public class Data
	{
		//public bool isBusy, canMove;

		public EffectScript myEffect;

		public float myValue;

		public float myRange;
	}




	[System.Serializable]
	private class Wrap : Phaser<Data> {}

	[SerializeField]
	private Wrap[] myPhases;


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
			return isOn && Phaser<Data>.IsActive(myBegin, myPhases);
		}
	}

	public Data data 
	{
		get
		{
			if(isActive)
			{
				return myPhases[Phaser<Data>.Phase(begin, myPhases)].myData;
			}
			return null;
		}
		set
		{
			myPhases[Phaser<Data>.Phase(begin, myPhases)].myData = value;
		}
	}

	public float begin
	{
		set
		{
			myBegin = value;

			if(null != myPhases)
			{
				Phaser<Data>.Syncronize(myBegin, myPhases);
			}

			isOn = true;

			myLastPhase = -1;
		}

		get
		{
			return myBegin;
		}
	}



	void Awake()
	{
		myUnit = GetComponent<UnitScript>();

		begin = myBegin;

		isOn = false;
	}

	void Update()
	{
		Activate();
	}

	public void Activate ()
	{
		if (isOn && null != myPhases && Phaser<Data>.IsActive(myBegin, myPhases))
		{
			for (int n = Phaser<Data>.Phase(myBegin, myPhases); myLastPhase < n; myLastPhase++)
			{
				Data phaseData = data;

				if (null != phaseData && null != phaseData.myEffect)
				{
					phaseData.myEffect.Affect(
						new Message(myUnit.ToTerm(),
							null == myTarget ? new Message.Term() : myTarget.ToTerm()), 
						null == phaseData ? 0f : phaseData.myRange);
				}
			}
		}
	}
}

[System.Serializable]
public class Phaser<Type>
{
	[SerializeField]
	public float myDuration;

	[SerializeField]
	public Type myData;

	private float myClock;

	public static void Syncronize(float begin, Phaser<Type>[] phases)
	{
		for(int i = 0; i < phases.Length; i++)
		{
			begin += phases[i].myDuration;

			phases[i].myClock = begin;
		}
	}

	public static bool IsActive(float begin, Phaser<Type>[] phases)
	{
		if(null == phases | phases.Length == 0)
		{
			return false;
		}

		float end = phases[phases.Length - 1].myClock;

		return (begin < end) & (Time.time < end);
	}

	public static int Phase(float begin, Phaser<Type>[] phases)
	{
		if(null == phases)
		{
			return -1;
		}

		float time = Time.time;

		float end = phases[phases.Length - 1].myClock;

		if (time < begin)
		{
			return -1;
		}
		else if (time > end)
		{
			return phases.Length;
		}

		int hi = phases.Length - 1;

		int lo = 0;

		int i = (hi + lo) >> 1;

		for (; 1 < hi - lo; i = (hi + lo) >> 1)
		{
			if (time < phases[i].myClock)
			{
				hi = i;
			}
			else
			{
				lo = i;
			}
		}
		if(time < phases[lo].myClock)
		{
			return lo;
		}
		return hi;
	}

}
