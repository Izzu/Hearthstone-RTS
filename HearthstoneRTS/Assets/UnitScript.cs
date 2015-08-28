using UnityEngine;
using System.Collections;

public class UnitScript : MonoBehaviour {

	public int myHealth, myDamage, myOffense, myDefense;

	public float myRange;

	private float myCooldown = 0;

	private Vector3 myScreenPosition;

	public PlayerScript myOwningPlayer;

	public UnitScript myTargetUnit;

	void Start () {
		
	}
	
	public void Damage (int input) {

		myDamage += input;

		Checkup();
	}
	
	public void Checkup ()
	{
		if(myDamage > myHealth)
		{
			Destroy(gameObject);
		}
	}
	
	void Update () {

		Vector3 thisPosition = this.transform.position;

		myScreenPosition = Camera.main.WorldToScreenPoint(thisPosition);

		if (null != myOwningPlayer.myCurserScript)
		{
			//should actually check with all players to see if it's on screen but w/e
			if (ScreenCheck(new Rect(0f, 0f, Screen.width, Screen.height)))
			{
				myOwningPlayer.myCurserScript.myOnScreenUnits.Add(this);
			}
			else
			{
				myOwningPlayer.myCurserScript.myOnScreenUnits.Remove(this);
			}
		}

		Color color = Color.black;

		if (null != myOwningPlayer.myCurserScript)
		{
			if (myOwningPlayer.myCurserScript.myOnScreenUnits.Contains(this))
			{
				color.b = 1f;
			}
			if (myOwningPlayer.mySelectionScript.mySelectedUnits.Contains(this))
			{
				color.g = 1f;
			}
			if (myOwningPlayer.myCurserScript.myUnitScript == this)
			{
				color.r = 1f;
			}
		}

		if (PhaseScript.IsAggressive() && null != myTargetUnit)
		{
			if ((thisPosition - myTargetUnit.transform.position).magnitude < myRange + (myTargetUnit.transform.localScale.magnitude * 0.5f) + (transform.localScale.magnitude * 0.5f)) 
			{
				if (myCooldown-- < 0f)
				{
					myCooldown = 100f;
					myTargetUnit.Damage(myOffense);
				}
			}
			else
			{
				GetComponent<NavMeshAgent>().destination = myTargetUnit.transform.position;
			}
		}
		else if(GetComponent<NavMeshAgent>().hasPath)
		{

		}
		else //idle
		{
			if (null != GlobalScript.ourUnitScripts)
			{
				const float aggroRange = 5f;
				UnitScript closestUnitScript = null;
				float min = aggroRange;

				//find enemies
				foreach (UnitScript unitScript in GlobalScript.ourUnitScripts)
				{
					if (unitScript.myOwningPlayer != myOwningPlayer)
					{
						float dist = (unitScript.transform.position - thisPosition).magnitude;
						if (dist < aggroRange && dist < min)
						{
							min = dist;
							closestUnitScript = unitScript;
						}
					}
				}
				myTargetUnit = closestUnitScript;
			}
		}

		transform.GetComponent<Renderer>().material.color = color;

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
