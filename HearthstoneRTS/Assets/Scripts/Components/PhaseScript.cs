using UnityEngine;
using System.Collections;

public class PhaseScript : MonoBehaviour {

	private static Repeater ourRepeater = new Repeater(3f);
	private static int ourLastCycle = ourRepeater.Cycle();

	public enum Phase
	{
		Null,
		Begin,
		Battle
	}

	private static Phase ourPhase = Phase.Null;

	public static Phase Current
	{
		get { return ourPhase; }

		set
		{
			switch (value)
			{
				case Phase.Begin:

					foreach(PlayerScript playerScript in GlobalScript.ourPlayerScripts)
					{
						CardScript cardScript = playerScript.myDeckScript.RemoveCard();

						if (null != cardScript)
						{
							playerScript.myHandScript.InsertCard(cardScript);
						}
					}

					Debug.Log("BEGIN");
					ourPhase = Phase.Begin;
					break;

				case Phase.Battle:

					Debug.Log("BATTLE");
					ourPhase = Phase.Battle;
					break;

			}
		}

	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		for (; ourLastCycle < ourRepeater.Cycle(); ourLastCycle++)
		{
			switch (ourPhase)
			{
				case Phase.Null:

					foreach (PlayerScript playerScript in GlobalScript.ourPlayerScripts)
					{
						for(int i = 0; i < 10; i++)
						{
							if (null != playerScript
								&& null != playerScript.myDeckScript)
							{
								GameObject gameobject = Instantiate(Resources.Load("Prefabs/Card")) as GameObject;

								CardScript cardScript = gameobject.GetComponent<CardScript>();

								if (null != cardScript)
								{
									playerScript.myDeckScript.InsertCard(cardScript);
								}
							}
						}
					}

					Current = Phase.Begin;
					break;

				case Phase.Begin:

					Current = Phase.Battle;
					break;

				case Phase.Battle:

					Current = Phase.Begin;
					break;
			}
		}
	}

	public static bool IsAggressive()
	{
		return ourPhase == Phase.Battle;
	}

}
