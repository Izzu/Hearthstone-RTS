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
	
	public void Damage (int input) 
	{
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

		PlayerScript mainPlayerScript = GlobalScript.ourGlobalScript.myMainPlayerScript;

		if (null != GlobalScript.ourCursorScript)
		{
			//should actually check with all players to see if it's on screen but w/e
			if (ScreenCheck(new Rect(0f, 0f, Screen.width, Screen.height)))
			{
				if (mainPlayerScript == myOwningPlayer) 
				{
					GlobalScript.ourCursorScript.myOnScreenOwnedUnitScripts.Add(this);
				} else {
					GlobalScript.ourCursorScript.myOnScreenUnownedUnitScripts.Add(this);
				}
			}
			else
			{
				if(mainPlayerScript == myOwningPlayer)
				{
					GlobalScript.ourCursorScript.myOnScreenOwnedUnitScripts.Remove(this);
				} else {
					GlobalScript.ourCursorScript.myOnScreenUnownedUnitScripts.Remove(this);
				}
			}
		}

		/*Color color = Color.black;

		if (null != GlobalScript.ourCursorScript)
		{
			if (GlobalScript.ourCursorScript.myOnScreenUnits.Contains(this))
			{
				color.b = 1f;
			}
			if (null != myOwningPlayer
				&& null != myOwningPlayer.mySelectionScript
				&& myOwningPlayer.mySelectionScript.mySelectedUnits.Contains(this))
			{
				color.g = 1f;
			}
			if (GlobalScript.ourCursorScript.myUnitScript == this)
			{
				color.r = 1f;
			}
		}*/

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

		transform.GetComponent<Renderer>().material.color = myOwningPlayer.myColor;

		if (null != myOwningPlayer.mySelectionScript 
			&& myOwningPlayer.mySelectionScript.mySelectedUnits.Contains(this))
		{
			transform.GetComponent<Renderer>().material.color = Color.white;
		}

	}

	

	void OnMouseDown()
	{
		GlobalScript.ourCursorScript.myUnitScript = this;
	}

	void OnMouseUp()
	{
		GlobalScript.ourCursorScript.myUnitScript = null;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Time.time - GlobalScript.ourCursorScript.LastDoubleClickTime > GlobalScript.ourCursorScript.myDoubleClickWait)
		{
			if (Physics.Raycast(ray, out hit, GlobalScript.ourCursorScript.myRayLength))
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

	void OnGUI()
	{
		Vector2 GUIposition = new Vector2(myScreenPosition.x, Screen.height - myScreenPosition.y);

		GUI.Box(new Rect(GUIposition, new Vector2(20f, 20f)), (myHealth - myDamage).ToString());
	}
	
}
