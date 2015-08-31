using UnityEngine;
using System.Collections;

public class Message {
	
	public class Term {

		public PlayerScript myPlayerScript;
		public UnitScript myUnitScript;
		public CardScript myCardScript;
		public Vector3 myPosition;

		public Term (
			PlayerScript inPlayerScript,
			CardScript inCardScript,
			UnitScript inUnitScript,
			Vector3 inposition)
		{
			myCardScript = inCardScript;
			myUnitScript = inUnitScript;
			myPlayerScript = inPlayerScript;
			myPosition = inposition;
		}

		public Term ()
		{
			myCardScript = null;
			myUnitScript = null;
			myPlayerScript = null;
			myPosition = Vector3.zero;
		}
	}

	public Term mySubject, myObject;
	public float myPower;

	public Message(Term inSubject, Term inObject, float input = 1f)
	{
		mySubject = inSubject;
		myObject = inObject;
		myPower = 1f;
	}

}
