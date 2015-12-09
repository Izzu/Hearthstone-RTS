using UnityEngine;
using System.Collections;

public class RainScript : MonoBehaviour {

	public float myHeight, myRadius;

	public GameObject myObject;

	public Repeater myRepeater;

	public int myLastCycle, myWaves;

	public Message myMessage;

	void Awake ()
	{
		myRepeater = new Repeater(1f);
	}

	// Update is called once per frame
	void Update ()
	{
		if (null != myRepeater && null != myObject && myLastCycle < myWaves)
		{
			for (; myLastCycle <= myRepeater.Cycle(); myLastCycle++)
			{
				for (float i = 0f; i < myRadius * 5f; i++)
				{
					ProjectileScript projectileScript = GlobalScript.New(
						"Prefabs/Projectiles/Icicle",
						transform.position + Vector3.up * myHeight + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * myRadius,
						Quaternion.identity
					).GetComponent<ProjectileScript>();

					projectileScript.myDeleteOnUnit = true;

					projectileScript.myMessage = myMessage;

					projectileScript.myClocker = new Clocker(3f);
				}
			}
		}
		else
		{
			Destroy(gameObject);
		}
	}

	//trigger functions activate on collisions but don't respond to the collisions
}
