using UnityEngine;
using System.Collections;

//keeps track of two times and the time lapse between them
public class Clocker {

	private float myBeginTime;

	private float myEndTime;

	public Clocker (float a, float b) {

		myBeginTime = a;
		myEndTime = b;
		
	}
	
	public Clocker (float duration = 0f) 
	{
		myBeginTime = Time.time;

		myEndTime = Time.time + duration;
	}

	public Clocker Extend (float input) 
	{
		myEndTime += input;
		
		return this;
	}

	public Clocker Set (float input) {
		myBeginTime = Time.time;
		myEndTime = Time.time + input;
		
		return this;
	}

	public Clocker Stop () {
		myEndTime = myBeginTime;
		
		return this;
	}

	public float percent
	{
		get
		{
			return (Time.time - myBeginTime) / (myEndTime - myBeginTime);
		}
	}

	public bool isActive
	{
		get
		{
			return (myBeginTime < myEndTime) & (Time.time < myEndTime);
		}
	}

}
