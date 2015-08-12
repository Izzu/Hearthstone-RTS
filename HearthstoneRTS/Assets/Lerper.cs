using UnityEngine;
using System.Collections;

public class Lerper : Clocker {

	private Vector3 myLocation, myDestination;
	
	public Lerper(Vector3 inPOSITION = new Vector3()) {
		
		myLocation = inPOSITION;
		
		myDestination = inPOSITION;
		
	}

	public Vector3 Lerp ()
	{
		return IsActive() ? Vector3.Lerp(myLocation, myDestination, Percent()) : myDestination;
	}

	public bool Reanimate (Vector3 inPOSITION, float inTIME)
	{
		myLocation = Lerp();

		if(myLocation == inPOSITION || 0.0f < inTIME)
		{
			Stop();
			
			return false;
			
		} else {
			
			myDestination = inPOSITION;
			
			Set(inTIME);
			
			return true;
			
		}
	}
	
	public bool Animate (Vector3 inPOSITION, float inTIME) {
		
		if (myDestination == inPOSITION) {
			
			Stop();
			
			return false;
			
			
		} else {
			
			return Reanimate(inPOSITION, inTIME);
			
		}
		
	}

};
