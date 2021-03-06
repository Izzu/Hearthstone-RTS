﻿using UnityEngine;
using System.Collections;

public class PhaseScript : MonoBehaviour {

	private static Repeater ourRepeater;
    private static int ourLastCycle; 

	public enum Phase
	{
		Null,
		Ceasefire,
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
				case Phase.Ceasefire:

					Debug.Log("Ceasefire Phase");
					ourPhase = Phase.Ceasefire;
					break;

				case Phase.Battle:

					Debug.Log("Battle Phase");
					ourPhase = Phase.Battle;
					break;

			}
		}

	}

	public static bool isAggressive
	{
		get
		{
			return true;// ourPhase == Phase.Battle;
		}
	}

	// Use this for initialization
	void Start()
	{
       ourRepeater = new Repeater(30f);
       ourLastCycle = ourRepeater.Cycle();
	
	}

	// Update is called once per frame
	void Update()
	{
		for (; ourLastCycle <= ourRepeater.Cycle(); ourLastCycle++)
		{
			switch (ourPhase)
			{
				case Phase.Null:

					PlayerScript[] playerScripts = GlobalScript.ourPlayerScripts;
					if (null != playerScripts)
					{
						foreach (PlayerScript playerScript in playerScripts)
						{
							if (playerScript && playerScript.myDeckScript)
							{
								/*for (int i = 0; i < 10; i++)
								{
									CardScript cardScript = GlobalScript.New("Prefabs/Cards/Dual Mana").GetComponent<CardScript>();

									if (null != cardScript)
									{
										if(!playerScript.myDeckScript.InsertCard(cardScript))
										{
											Destroy(cardScript.gameObject);
										}
									}
								}

								for (int i = 0; i < 4; i++)
								{
									playerScript.Draw();
								}*/
							}
						}
					}

					Current = Phase.Ceasefire;
					break;

				case Phase.Ceasefire:

					Current = Phase.Battle;
					break;

				case Phase.Battle:

					Current = Phase.Ceasefire;
					break;
			}
		}
	}

}
