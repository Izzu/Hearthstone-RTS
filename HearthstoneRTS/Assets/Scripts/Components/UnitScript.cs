﻿using UnityEngine;
using System.Collections;

public class UnitScript : MonoBehaviour {

	
	public int myHealth;
	public int myDamage;
	public int myOffense;
	public int myDefense;

	public float myRange;

	private float myCooldown = 0;

	private Vector3 myScreenPosition;

	public PlayerScript myOwningPlayer;

	public UnitScript myTargetUnit;

	public CardScript myCardScript;

	public EffectScript[] myDeathEffects;

	public EffectScript[] myOffenseEffects;

	public EffectScript[] myDefenseEffects;

	public int myDemand = 0;

	public NavMeshAgent myNavMeshAgent;

	void Awake()
	{
		myNavMeshAgent = GetComponent<NavMeshAgent>();
	}

	void Start ()
	{
		transform.GetComponent<Renderer>().material.color = myOwningPlayer.myColor;
	}

	public bool myDamageImmune;
	public int myDivineShield, myTemperature;
	
	public void Damage (int input) 
	{
		if (myDamageImmune)
		{
			return;
		}

		if(myDivineShield > 0)
		{
			myDivineShield--;

			return;
		}

		/*myOwningPlayer.myFrameData.myDamages++;
		myOwningPlayer.myTurnData.myFrameData.myDamages++;
		myOwningPlayer.myMatchData.myTurnData.myFrameData.myDamages++;

		GlobalScript.ourPlayerFrameData.myDamages++;
		GlobalScript.ourPlayerTurnData.myFrameData.myDamages++;
		GlobalScript.ourPlayerMatchData.myTurnData.myFrameData.myDamages++;*/

		myDamage += input;

		Checkup();
	}

	public void Checkup ()
	{
		if(myDamage >= myHealth)
		{
			Death();
		}
	}
	
	public void Death ()
	{
		/*myOwningPlayer.myFrameData.myDeaths++;
		myOwningPlayer.myTurnData.myFrameData.myDeaths++;
		myOwningPlayer.myMatchData.myTurnData.myFrameData.myDeaths++;

		GlobalScript.ourPlayerFrameData.myDeaths++;
		GlobalScript.ourPlayerTurnData.myFrameData.myDeaths++;
		GlobalScript.ourPlayerMatchData.myTurnData.myFrameData.myDeaths++;*/

		EffectScript.AffectsList(myDeathEffects, ToMessage());

		Destroy(gameObject);
	}

	public void Attack ()
	{
		/*myOwningPlayer.myFrameData.myAttacks++;
		myOwningPlayer.myTurnData.myFrameData.myAttacks++;
		myOwningPlayer.myMatchData.myTurnData.myFrameData.myAttacks++;

		GlobalScript.ourPlayerFrameData.myAttacks++;
		GlobalScript.ourPlayerTurnData.myFrameData.myAttacks++;
		GlobalScript.ourPlayerMatchData.myTurnData.myFrameData.myAttacks++;*/

		EffectScript.AffectsList(myOffenseEffects, ToMessage());
	}

	public void Heal (int input)
	{
		/*myOwningPlayer.myFrameData.myHeals++;
		myOwningPlayer.myTurnData.myFrameData.myHeals++;
		myOwningPlayer.myMatchData.myTurnData.myFrameData.myHeals++;

		GlobalScript.ourPlayerFrameData.myHeals++;
		GlobalScript.ourPlayerTurnData.myFrameData.myHeals++;
		GlobalScript.ourPlayerMatchData.myTurnData.myFrameData.myHeals++;*/

		myDamage = input > myDamage ? 0 : myDamage - input;
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
			else if(null != myNavMeshAgent)
			{
				myNavMeshAgent.destination = myTargetUnit.transform.position;
			}
		}
		else if (null != myNavMeshAgent && myNavMeshAgent.hasPath)
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
					if (unitScript && unitScript.myOwningPlayer != myOwningPlayer)
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
		if (myOwningPlayer == GlobalScript.ourGlobalScript.myMainPlayerScript)
		{
			Vector2 GUIposition = new Vector2(myScreenPosition.x - 10f, Screen.height - myScreenPosition.y - 10f);

			GUI.Box(new Rect(GUIposition, new Vector2(20f, 20f)), (myHealth - myDamage).ToString());
		}
	}

	public Message.Term ToTerm()
	{
		return new Message.Term(myOwningPlayer, myCardScript, this, transform.position);
	}

	public Message ToMessage()
	{
		//has a target
		if(myTargetUnit)
		{
			return new Message(ToTerm(), myTargetUnit.ToTerm()); 
		}
		//doesn't have a target
		{
			return new Message(ToTerm(), new Message.Term(null, null, null, Vector3.zero)); 
		}
	}

}