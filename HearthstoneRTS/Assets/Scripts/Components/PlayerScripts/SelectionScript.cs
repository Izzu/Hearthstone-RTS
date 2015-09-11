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
		//controlling selected units
		if (mySelectedUnits.Count > 0)
		{
			//if mouse.right is down
			if (Input.GetMouseButton(1))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit, GlobalScript.ourCursorScript.myRayLength))
				{
					//the ray hit a "unit" gameObject
					UnitScript target = hit.transform.gameObject.GetComponent<UnitScript>();
					if (null != target && target.gameObject.layer == 9)
						//we don't care whether we're targetting friend or foe anymore,
						//commands will determine whether we're attacking or helping and how it is done
					{
						//order selected units to target
						foreach (UnitScript unit in mySelectedUnits)
						{
							if (null != unit.myCommands)
							{
								if (false == Input.GetKey(KeyCode.LeftShift))
								{
									unit.myCommands.Clear();
								}

								//follow allies
								if(target.myOwner.owner == myPlayerScript)
								{
									unit.myCommands.Order(new CommandScript.Follow(target, 2.5f));
								}
								//attack enemies
								else
								{
									unit.myCommands.Order(new CommandScript.Interact(target, unit.myAttack));
								}
							}
						}
					}
					else
					{
						//order selected units to move
						foreach (UnitScript unit in mySelectedUnits)
						{
							if (null != unit.myCommands)
							{
								if (false == Input.GetKey(KeyCode.LeftShift))
								{
									unit.myCommands.Clear();
								}

								unit.myCommands.Order(new CommandScript.Move(hit.point));
							}
						}
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

			if (null != GlobalScript.ourUnitScripts)
			{
				foreach (UnitScript unit in GlobalScript.ourUnitScripts)
				{
					if (unit.myOwner.owner == myPlayerScript && unit.ScreenCheck(selectionRect))
					{
						mySelectedUnits.Add(unit);
					}
				}
			}

			myIsDragging = false;

			mySelectionBegin = Vector3.zero;
			mySelectionEnd = Vector3.zero;

		}

	}
}
