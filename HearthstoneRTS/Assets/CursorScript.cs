using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CursorScript : MonoBehaviour
{

	private Vector3 myMouseLastPosition;

	private Vector3 myTransformScreenPoint;

	//private Vector3 myScreenToTransformOffset;

	public CardScript myCardScript = null;

	public UnitScript myUnitScript = null;

	public HashSet<UnitScript> mySelectedUnits = new HashSet<UnitScript>();
	public HashSet<UnitScript> myOnScreenUnits = new HashSet<UnitScript>();

	public float myPanningFactor = 1.0f;

	public PlayerScript myPlayerScript;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (false == Input.GetMouseButton(0))
		{
			Panning (Input.mousePosition - myMouseLastPosition);
		}

		myMouseLastPosition = Input.mousePosition;

		if(myCardScript)
		{
			myCardScript.transform.position = Camera.main.ScreenToWorldPoint(Cursor());
		}

		if(myUnitScript)
		{
			myIsDragging = false;

			if(Input.GetMouseButton(0))
			{ 
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit, 10f))
				{
					if (hit.transform != myUnitScript.transform)
					{
						myUnitScript.GetComponent<NavMeshAgent>().destination = hit.point;
					}
				}
			}
		}

		if(myUnitScript == null && myCardScript == null)
		{
			Selecting();
		}

		if(mySelectedUnits.Count > 0)
		{
			if(Input.GetMouseButton(1))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit, 10f))
				{
					if (true)
					{
						foreach(UnitScript unit in mySelectedUnits)
						{
							unit.GetComponent<NavMeshAgent>().destination = hit.point;
						}
					}
				}
			}
		}

	}

	void Panning(Vector3 mouseMovement)
	{

		Vector2 screenPosition = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);

		if (screenPosition.x > 0.975f)
		{
			//moving EAST
			transform.Translate(myPanningFactor, 0.0f, 0.0f);

		}
		else if (screenPosition.x < 0.025f)
		{
			//moving WEST
			transform.Translate(-myPanningFactor, 0.0f, 0.0f);

		}

		if (screenPosition.y > 0.975f)
		{
			//moving NORTH
			transform.Translate(0.0f, 0.0f, myPanningFactor);

		}
		else if (screenPosition.y < 0.025f)
		{
			//moving SOUTH
			transform.Translate(0.0f, 0.0f, -myPanningFactor);

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

	private bool myIsDragging = false;
	private Vector3 mySelectionBegin, mySelectionEnd;
	private Rect mySelectionRect;

	void Selecting () 
	{

		if (Input.GetMouseButtonDown(0))
		{
			mySelectionBegin = Input.mousePosition;
			myIsDragging = true;

			if (Input.GetKey("left shift") == false)
			{
				mySelectedUnits.Clear();
			}

		}

		if (Input.GetMouseButton(0))
		{
			mySelectionEnd = Input.mousePosition;
			myIsDragging = true;
		}

		float left = Mathf.Min(mySelectionBegin.x, mySelectionEnd.x);
		float guitop = Mathf.Min(Screen.height - mySelectionBegin.y, Screen.height - mySelectionEnd.y);

		mySelectionRect = new Rect(
			left,
			guitop,
			Mathf.Max(mySelectionBegin.x, mySelectionEnd.x) - left,
			Mathf.Max(Screen.height - mySelectionBegin.y, Screen.height - mySelectionEnd.y) - guitop);
		
		if (Input.GetMouseButtonUp(0))
		{
			float screentop = Mathf.Min(mySelectionBegin.y, mySelectionEnd.y);

			Rect selectionRect = new Rect(
				left,
				screentop,
				Mathf.Max(mySelectionBegin.x, mySelectionEnd.x) - left,
				Mathf.Max(mySelectionBegin.y, mySelectionEnd.y) - screentop);

			foreach (UnitScript unit in FindObjectsOfType<UnitScript>())
			{
				if (unit.ScreenCheck(selectionRect))
				{
					mySelectedUnits.Add(unit);
				}
			}

			myIsDragging = false;

		}

	}

	void OnGUI()
	{
		if(myIsDragging)
		{
			GUI.Box(mySelectionRect, "");
		}
	}

}
