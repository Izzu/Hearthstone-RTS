using UnityEngine;
using System.Collections;

[System.Serializable]
public class Slerper : Clocker {

	[SerializeField]
	private Quaternion myBegin, myEnd;

	public Slerper(Quaternion inPOSITION = new Quaternion())
	{
		myBegin = inPOSITION;
		
		myEnd = inPOSITION;
	}

	public Quaternion Slerp ()
	{
		return isActive ? Quaternion.Slerp(myBegin, myEnd, percent) : myEnd;
	}

	public Slerper Reset(Quaternion input)
	{
		myBegin = input;
		myEnd = input;

		Stop();

		return this;
	}

	public bool Animate (Quaternion inPOSITION, float inTIME)
	{
		myBegin = Slerp();

		if(0.0f > inTIME)
		{
            Stop();
			
			return false;
			
		} else {
			
			myEnd = inPOSITION;

            Set(inTIME);
			
			return true;
			
		}
	}
	
	public bool Reanimate (Quaternion inPOSITION, float inTIME)
	{	
		if (myEnd == inPOSITION) {
			
			return false;
			
		} else {
			
			return Animate(inPOSITION, inTIME);

		}
	}

}
