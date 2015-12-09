using UnityEngine;
using System.Collections;

public class ProductionScript : MonoBehaviour {

	public Vector3 myRallyPoint;

	public UnitScript myUnit;

	void Awake()
	{
		myUnit = GetComponent<UnitScript>();

		if(null == myUnit)
		{

		}
		else
		{
			Debug.Log("Production.myOwner: " + myUnit.myOwner.owner);
			myUnit.myOwner.owner.myProduction.Add(this);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
