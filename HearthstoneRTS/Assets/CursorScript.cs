using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CursorScript : MonoBehaviour
{

	private Vector3 myTransformScreenPoint;

	//private Vector3 myScreenToTransformOffset;

	public CardScript myCardScript = null;

	public UnitScript myUnitScript = null;

	public HashSet<UnitScript> myOnScreenUnits = new HashSet<UnitScript>();

	public PlayerScript myPlayerScript;

	public float myRayLength = 100f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		
		if(myCardScript)
		{
			myCardScript.transform.position = Camera.main.ScreenToWorldPoint(Cursor());
		}

		if (myUnitScript == null && myCardScript == null)
		{
			myPlayerScript.mySelectionScript.Selection();
		}

		if(myUnitScript)
		{
			myPlayerScript.mySelectionScript.myIsDragging = false;

			if(Input.GetMouseButton(0))
			{ 
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit, myRayLength))
				{
					if (hit.transform != myUnitScript.transform)
					{
						myUnitScript.GetComponent<NavMeshAgent>().destination = hit.point;
					}
				}
			}
		}

	}

	//screen point
	public Vector3 CursorDown (Transform intransform)
	{
		myTransformScreenPoint = Camera.main.WorldToScreenPoint(intransform.position);

		//myScreenToTransformOffset = intransform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, myTransformScreenPoint.z));

		return myTransformScreenPoint;
	}

	//screen point
	public Vector3 Cursor ()
	{
		return new Vector3(Input.mousePosition.x, Input.mousePosition.y, myTransformScreenPoint.z);
	}


}
