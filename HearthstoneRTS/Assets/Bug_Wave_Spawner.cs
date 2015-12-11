using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class Bug_Wave_Spawner : MonoBehaviour {

	[SerializeField]
	GameObject[] mySpawns;

	[SerializeField]
	UnitScript myBoss;

	[SerializeField]
	GameObject myUnit;

	[SerializeField]
	UnitScript myTarget;

	[SerializeField]
	int myWaves;

	float myWait;

	HashSet<UnitScript> myBugs = new HashSet<UnitScript>();

	void Update()
	{
		if (myWaves > 0)
		{
			if (myWait <= 0f)
			{
				myWait = 1f;
				Debug.Log("Wait done");
				bool ready = true;

				foreach (UnitScript unit in myBugs)
				{
					if(unit && unit.GetComponent<HealthScript>().isAlive)
					{
						ready = false;
						break;
					}
				}
				if (ready)
				{
					Debug.Log("All Clear");
					foreach (GameObject spawn in mySpawns)
					{
						for (int i = 0; i < 6; i++)
						{
							UnitScript unit = Instantiate(myUnit).GetComponent<UnitScript>();

							unit.Attack(myTarget);

							myBugs.Add(unit);
						}
					}

					if(1 == myWaves)
					{
						Debug.Log("Boss Wave");
						myBoss.Attack(myTarget);
					}

					myWaves--;
				}
			}
			else
			{
				myWait -= Time.deltaTime;
			}
		}

	}

}
