﻿using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;

public class UnitScript : MonoBehaviour {

	
	public int myOffense;
	public int myDefense;

	public float myRange;
	private float myCooldown = 0;

	private Vector3 myScreenPosition;

	public OwnerScript myOwner;

	public HealthScript myHealth;

	public CommandScript myCommands;

	public InteractionScript myAttack;
	public Messenger_Script myAttackMessenger;

	public CardScript myCardScript;

	public EffectScript[] myDeathEffects;

	public EffectScript[] myOffenseEffects;

	public EffectScript[] myDefenseEffects;

	public int myDemand = 0;

	public NavMeshAgent myNavMeshAgent;

	public Animation myAttackAnimation, myIdleAnimation, myWalkingAnimation;

    public Messenger_Script myDeathMessenger;

	void Awake()
	{
		myNavMeshAgent = GetComponent<NavMeshAgent>();

		if(null == myOwner)
		{
			myOwner = GetComponent<OwnerScript>();
		}

		if(null == myHealth)
		{
			myHealth = GetComponent<HealthScript>();
		}

		if(null == myCommands)
		{
			myCommands = GetComponent<CommandScript>();
		}

		if(null == myAttackMessenger)
		{
			myAttackMessenger = gameObject.AddComponent<Messenger_Script>() as Messenger_Script;
		}
	}

	void Start ()
	{
		
	}

	public UnitScript target
	{
		get
		{
			return null == myCommands ? null : myCommands.target;
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

        myDeathMessenger.Publish();

		myNavMeshAgent.speed = 0f;

		Destroy(gameObject, 10f);
	}

	public void Attack (UnitScript target)
	{

		if (PhaseScript.isAggressive)
		{
			myCommands.Order(new CommandScript.Target(target, myAttack));
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
				if (mainPlayerScript == myOwner.owner) 
				{
					GlobalScript.ourCursorScript.myOnScreenOwnedUnitScripts.Add(this);
				} else {
					GlobalScript.ourCursorScript.myOnScreenUnownedUnitScripts.Add(this);
				}
			}
			else
			{
				if (mainPlayerScript == myOwner.owner)
				{
					GlobalScript.ourCursorScript.myOnScreenOwnedUnitScripts.Remove(this);
				} else {
					GlobalScript.ourCursorScript.myOnScreenUnownedUnitScripts.Remove(this);
				}
			}
		}

		/*if (PhaseScript.isAggressive && null != myTargetUnit)
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
		}*/
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
						myOwner.owner.mySelectionScript.mySelectedUnits.Add(this);
					} 
					else 
					{
						if (null != myOwner && null != myOwner.owner && null != myOwner.owner.mySelectionScript)
						{
							myOwner.owner.mySelectionScript.mySelectedUnits.Clear();

							myOwner.owner.mySelectionScript.mySelectedUnits.Add(this);
						}
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
		if (
			null != myOwner && 
			myOwner.owner == GlobalScript.ourGlobalScript.myMainPlayerScript && 
			null != myHealth && 
			null != GlobalScript.ourGlobalScript.myMainPlayerScript &&
			null != GlobalScript.ourGlobalScript.myMainPlayerScript.mySelectionScript &&
			GlobalScript.ourGlobalScript.myMainPlayerScript.mySelectionScript.mySelectedUnits.Contains(this))
		{
			Vector2 GUIposition = new Vector2(myScreenPosition.x - 10f, Screen.height - myScreenPosition.y - 10f);

			GUI.Box(new Rect(GUIposition, new Vector2(20f, 20f)), myHealth.health.ToString());

			Behaviour h = (Behaviour)GetComponent("Halo");
			h.enabled = true;
		}
		else
		{
			Behaviour h = (Behaviour)GetComponent("Halo");
			if(h) h.enabled = false;
		}
	}

	public Message.Term ToTerm()
	{
		return new Message.Term(myOwner.owner, myCardScript, this, transform.position);
	}

	public Message ToMessage()
	{
		//has a target
		if(null != target)
		{
			return new Message(ToTerm(), myCommands.target.ToTerm()); 
		}

		//doesn't have a target
		return new Message(ToTerm(), new Message.Term(null, null, null, Vector3.zero)); 
	}

}
