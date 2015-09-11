using UnityEngine;
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

	public GameObject Search ()
	{
		Vector3 position = myUnit.transform.position;

		Collider[] colliders = Physics.OverlapSphere(position, myRange, 1 << 9);
		
		if(null == colliders)
		{
			return null;
		}

		GameObject closestUnit = colliders[0].gameObject;

		float closestDist = (colliders[0].transform.position - position).magnitude;

		if(colliders.Length > 1)
		{
			for(int i = 1; i < colliders.Length; i++)
			{
				Collider collider = colliders[i];

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
		}

		return closestUnit;
	}

}
