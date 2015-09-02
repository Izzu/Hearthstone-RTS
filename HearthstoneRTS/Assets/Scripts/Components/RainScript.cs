using UnityEngine;
using System.Collections;

public class RainScript : MonoBehaviour {

	public float myHeight, myRadius;

	public GameObject myObject;

	public Repeater myRepeater;

	public int myLastCycle = 0, myWaves = 1;

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
				for (float i = 0f; i < myRadius * 3.14159f; i++)
				{
					Object prefab = Resources.Load("Prefabs/Projectiles/Icicle");

					Vector3 position = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * myRadius;

					GameObject gameObject = Transform.Instantiate(prefab, position + Vector3.up * myHeight, Quaternion.Euler(Vector3.up)) as GameObject;

					ProjectileScript projectileScript = gameObject.GetComponent<ProjectileScript>();

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
}
