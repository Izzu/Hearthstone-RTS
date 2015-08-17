using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectionScript : MonoBehaviour {
	
	public HashSet<UnitScript> mySelectedUnits = new HashSet<UnitScript>();

	public PlayerScript myPlayerScript;

	public bool myIsDragging = false;
	private Vector3 mySelectionBegin, mySelectionEnd;
	private Rect mySelectionRect;

	void OnGUI()
	{
		if (myIsDragging)
		{
			GUI.Box(mySelectionRect, "");
		}
	}

	void Update()
	{

		if (mySelectedUnits.Count > 0)
		{
			if (Input.GetMouseButton(1))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit, myPlayerScript.myCurserScript.myRayLength))
				{
					foreach (UnitScript unit in mySelectedUnits)
					{
						unit.GetComponent<NavMeshAgent>().destination = hit.point;
					}
				}
			}
		}

	}

	public void Selection ()
	{

		if (Input.GetMouseButtonDown(0))
		{
			mySelectionBegin = Input.mousePosition;
			myIsDragging = false;

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
				if (unit.myOwningPlayer == myPlayerScript && unit.ScreenCheck(selectionRect))
				{
					mySelectedUnits.Add(unit);
				}
			}

			myIsDragging = false;

			mySelectionBegin = Vector3.zero;
			mySelectionEnd = Vector3.zero;

		}

	}
}
