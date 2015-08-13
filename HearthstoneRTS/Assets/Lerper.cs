﻿using UnityEngine;
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

	public Lerper Reposition (Vector3 input)
	{
		myLocation = input;
		myDestination = input;

		Stop();

		return this;
	}

	public bool Animate (Vector3 inPOSITION, float inTIME)
	{
		myLocation = Lerp();

		if(0.0f > inTIME)
		{
            Stop();
			
			return false;
			
		} else {
			
			myDestination = inPOSITION;

            Set(inTIME);
			
			return true;
			
		}
	}
	
	public bool Reanimate (Vector3 inPOSITION, float inTIME) {
		
		if (myDestination == inPOSITION) {
			
			return false;
			
		} else {
			
			return Animate(inPOSITION, inTIME);
			
		}
		
	}

};
