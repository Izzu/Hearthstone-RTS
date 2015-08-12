using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {

	public int myHandIndex;
	
	public Lerper myPosition;

    public HandScript myHandScript;
	
	// Use this for initialization
	void Start () {
		
        myPosition = new Lerper();

	}
	
	// Update is called once per frame
	void Update () {

        myPosition.Reanimate(Position(myHandScript ? myHandScript.CountCards() : 1), 2.0f);

		transform.localPosition = myPosition.Lerp();
		
	}
	
	Vector3 Position (int playerHandSize) {
		return Vector3.Lerp(new Vector3(-1.75f, -1.0f, 3), new Vector3(+1.75f, -1.0f, 3), (1.0f + myHandIndex) / (1.0f + playerHandSize));
	}
}
