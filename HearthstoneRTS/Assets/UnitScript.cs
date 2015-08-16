using UnityEngine;
using System.Collections;

public class UnitScript : MonoBehaviour {

	private int myHealth, myDamage, myOffense, myDefense;

	private Vector3 myScreenPosition;

	public PlayerScript myOwningPlayer;
	
	void Start () {
		
	}
	
	public void Damage (int input) {

		myDamage += input;

		Checkup();
	}
	
	public void Checkup () {
		
	}
	
	void Update () {

		myScreenPosition = Camera.main.WorldToScreenPoint(this.transform.position);

		//should actually check with all players to see if it's on screen but w/e
		if(ScreenCheck(new Rect(0f, 0f, Screen.width, Screen.height)))
		{
			myOwningPlayer.myCurserScript.myOnScreenUnits.Add(this);
		} else {
			myOwningPlayer.myCurserScript.myOnScreenUnits.Remove(this);
		}

		Color color = Color.black;

		if(myOwningPlayer.myCurserScript.myOnScreenUnits.Contains(this))
		{
			color.b = 1f;
		}
		if (myOwningPlayer.mySelectionScript.mySelectedUnits.Contains(this))
		{
			color.g = 1f;
		}
		if(myOwningPlayer.myCurserScript.myUnitScript == this)
		{
			color.r = 1f;
		}

		transform.renderer.material.color = color;

	}

	void OnMouseDown()
	{
		myOwningPlayer.myCurserScript.myUnitScript = this;
	}

	void OnMouseUp()
	{
		myOwningPlayer.myCurserScript.myUnitScript = null;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Time.time - myOwningPlayer.myCurserScript.LastDoubleClickTime > myOwningPlayer.myCurserScript.myDoubleClickWait)
		{
			if (Physics.Raycast(ray, out hit, myOwningPlayer.myCurserScript.myRayLength))
			{
				if(hit.transform == this.transform)
				{
					if (Input.GetKey("left shift"))
					{
						myOwningPlayer.mySelectionScript.mySelectedUnits.Add(this);
					} else {
						myOwningPlayer.mySelectionScript.mySelectedUnits.Clear();
						myOwningPlayer.mySelectionScript.mySelectedUnits.Add(this);

					}
				}
			}
		}
		
	}

	public bool ScreenCheck (Rect rekt)
	{
		return
			myScreenPosition.x < rekt.xMax &&
			myScreenPosition.y < rekt.yMax &&
			myScreenPosition.x > rekt.xMin &&
			myScreenPosition.y > rekt.yMin;
	}

	
}
