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

	private float myLastClickTime = -1f;
	
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

		if(Input.GetMouseButtonDown(0))
		{
			if(DoubleClick())
			{
				if (Input.GetKey("left shift"))
				{
					myPlayerScript.mySelectionScript.mySelectedUnits.UnionWith(myOnScreenUnits);
				} else {
					myPlayerScript.mySelectionScript.mySelectedUnits = new HashSet<UnitScript>(myOnScreenUnits);
				}				
			}

			myLastClickTime = Time.time;
		}

	}

	//screen point
	public Vector3 CursorDown (Transform intransform)
	{
		myTransformScreenPoint = Camera.main.WorldToScreenPoint(intransform.position);

		return myTransformScreenPoint;
	}

	//screen point
	public Vector3 Cursor ()
	{
		return new Vector3(Input.mousePosition.x, Input.mousePosition.y, myTransformScreenPoint.z);
	}

	public bool DoubleClick()
	{
		return Time.time - myLastClickTime < 1f;
	}

}
