using UnityEngine;
using System.Collections;

public class Slerper : Clocker {

	private Quaternion myBegin, myEnd;

	public Slerper(Quaternion inPOSITION = new Quaternion())
	{
		
		myBegin = inPOSITION;
		
		myEnd = inPOSITION;
		
	}

	public Quaternion Slerp ()
	{
		return IsActive() ? Quaternion.Slerp(myBegin, myEnd, Percent()) : myEnd;
	}

	public Slerper Set(Quaternion input)
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
	
	public bool Reanimate (Quaternion inPOSITION, float inTIME) {
		
		if (myEnd == inPOSITION) {
			
			return false;
			
		} else {
			
			return Animate(inPOSITION, inTIME);
			
		}
		
	}

}
