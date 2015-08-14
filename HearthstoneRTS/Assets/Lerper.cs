﻿using UnityEngine;
using System.Collections;

//moves a vector 3 through time
public class Lerper : Clocker {

	private Vector3 myBegin, myEnd;
	
	public Lerper(Vector3 inPOSITION = new Vector3()) {
		
		myBegin = inPOSITION;
		
		myEnd = inPOSITION;
		
	}

	public Vector3 Lerp ()
	{
		return IsActive() ? Vector3.Lerp(myBegin, myEnd, Percent()) : myEnd;
	}

	public Lerper Set (Vector3 input)
	{
		myBegin = input;
		myEnd = input;

		Stop();

		return this;
	}

	public bool Animate (Vector3 inPOSITION, float inTIME)
	{
		myBegin = Lerp();

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
	
	public bool Reanimate (Vector3 inPOSITION, float inTIME) {
		
		if (myEnd == inPOSITION) {
			
			return false;
			
		} else {
			
			return Animate(inPOSITION, inTIME);
			
		}
		
	}

};
