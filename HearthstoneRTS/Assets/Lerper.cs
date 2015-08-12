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
		Debug.Log(IsActive() ? "true" : "false");
		return IsActive() ? Vector3.Lerp(myLocation, myDestination, Percent()) : myDestination;
	}

	public bool Animate (Vector3 inPOSITION, float inTIME)
	{
		myLocation = Lerp();

		if(0.0f > inTIME)
		{
            Stop();
            Debug.Log("0.0f > inTIME\n");
			
			return false;
			
		} else {
			
			myDestination = inPOSITION;

            Set(inTIME);
            Debug.Log("Animating.\n");
			
			return true;
			
		}
	}
	
	public bool Reanimate (Vector3 inPOSITION, float inTIME) {
		
		if (myDestination == inPOSITION) {
			
            Debug.Log("myDestination == input\n");
			
			return false;
			
			
		} else {
			
			return Animate(inPOSITION, inTIME);
			
		}
		
	}

};
