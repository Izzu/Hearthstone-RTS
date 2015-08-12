using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {

	public int myHandIndex;
	
	public Lerper myPosition = new Lerper();
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position = myPosition.Lerp();
		
	}
	
	Vector3 Position (int playerHandSize) {
		return Vector3.Lerp(new Vector3(-1.75f, -1.0f, 3), new Vector3(+1.75f, -1.0f, 3), myHandIndex / (1.0f + playerHandSize));
	}
}
