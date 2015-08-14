using UnityEngine;
using System.Collections;


//moves a float through time
public class Erper : Clocker {
	
	private float myBegin, myEnd;

	public Erper(float inPOSITION = 0.0f)
	{
		
		myBegin = inPOSITION;
		
		myEnd = inPOSITION;
		
	}

	public static float Erp(float a, float b, float t)
	{
		return a + t * (b - a);
	}

	public float Erp ()
	{
		return IsActive() ? Erp(myBegin, myEnd, Percent()) : myEnd;
	}

	public Erper Set (float input)
	{
		myBegin = input;
		myEnd = input;

		Stop();

		return this;
	}

	public bool Animate (float inPOSITION, float inTIME)
	{
		myBegin = Erp();

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
	
	public bool Reanimate (float inPOSITION, float inTIME) {
		
		if (myEnd == inPOSITION) {
			
			return false;
			
		} else {
			
			return Animate(inPOSITION, inTIME);
			
		}
		
	}
}
