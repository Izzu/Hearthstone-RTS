using UnityEngine;
using System.Collections;

public class HandScript : MonoBehaviour {

    public Player myOwningPlayer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    public int CountCards()
    {
        return transform.childCount;
    }
}
