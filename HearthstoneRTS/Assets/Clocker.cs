using UnityEngine;
using System.Collections;

//keeps track of two times and the time lapse between them
public class Clocker {

	private float myBegin;

	private float myEnd;

	public Clocker (float a, float b) {
	
		myBegin = a;
		myEnd = b;
		
	}
	
	public Clocker () 
	{	
		myBegin = Time.time;
		
		myEnd = Time.time;
	}

	public Clocker Extend (float input) {
		myEnd += input;
		
		return this;
	}

	public Clocker Set (float input) {
		myBegin = Time.time;
		myEnd = Time.time + input;
		
		return this;
	}

	public Clocker Stop () {
		myEnd = myBegin;
		
		return this;
	}

	public float Percent ()
	{
		return (Time.time - myBegin) / (myEnd - myBegin);
	}

	public bool IsActive ()
	{
		return (myBegin < myEnd) & (Time.time < myEnd);
	}


}
