﻿using UnityEngine;
using System.Collections;

public class AggroScript : MonoBehaviour
{
	public float myRange;

	public bool myToggle;

	private UnitScript myUnit;

	void Awake()
	{
		myUnit = GetComponent<UnitScript>();
	}


	void Update()
	{
		GameObject target = Search();

		if (null == target)
			
			return;

		UnitScript unit = target.GetComponent<UnitScript>();

		if(null != unit)
		{
			//Debug.Log(unit);
			myUnit.myCommands.Order(new CommandScript.Interact(unit, myUnit.myAttack));
		}
	}


	public GameObject Search ()
	{
		Vector3 position = myUnit.transform.position;

		Collider[] colliders = Physics.OverlapSphere(position, myRange, 1 << 9);
		
		if(null == colliders)
		{
			return null;
		}

		GameObject closestUnit = null;

		float closestDist = myRange;

		foreach(Collider collider in colliders)
		{
			if (collider.gameObject != gameObject)
			{
				float distance = (collider.transform.position - position).magnitude;

				if (distance < closestDist)
				{
					closestUnit = collider.transform.gameObject;

					closestDist = distance;
				}
			}
		}

		return closestUnit;
	}

}
