﻿using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {

	public int myHandIndex;
	
	public Lerper myPosition;
	
	// Use this for initialization
	void Start () {
		myPosition = new Lerper();
	}
	
	// Update is called once per frame
	void Update () {

        myPosition.Reanimate(Position(1), 1.0f);

		transform.localPosition = myPosition.Lerp();
		
	}
	
	Vector3 Position (int playerHandSize) {
		return Vector3.Lerp(new Vector3(-1.75f, -1.0f, 3), new Vector3(+1.75f, -1.0f, 3), myHandIndex / (1.0f + playerHandSize));
	}
}
