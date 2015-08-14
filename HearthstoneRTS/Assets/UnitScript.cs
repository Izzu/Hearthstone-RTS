using UnityEngine;
using System.Collections;

public class UnitScript : MonoBehaviour {

	private string myName;
	
	private int myHealth, myDamage, myOffense, myDefense;
	
	void Start () {
	
	}
	
	public void Damage (int input) {
		myDamage += input;
		
		Checkup();
	}
	
	public void Checkup () {
		
	}
	
	//void Update () {}
	
	
}
