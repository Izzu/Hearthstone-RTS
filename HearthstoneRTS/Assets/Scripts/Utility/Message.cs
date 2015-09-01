﻿using UnityEngine;
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

	public Message(Term inSubject, Term inObject)
	{
		mySubject = inSubject;
		myObject = inObject;
	}

}
