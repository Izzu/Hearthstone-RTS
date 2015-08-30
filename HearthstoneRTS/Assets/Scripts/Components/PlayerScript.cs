using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public string myName;

	public int myMana;

	public HandScript myHandScript;

	public PanningScript myPanningScript;

	public SelectionScript mySelectionScript;

	public UnitScript myHeroUnitScript;

	public DeckScript myDeckScript;

	public Color myColor;

	void Start () {

	}
	
	void Update () {

	}

	public Message.Term ToTerm (CardScript inCardScript = null)
	{
		return new Message.Term(this, inCardScript, myHeroUnitScript, null != myHeroUnitScript ? myHeroUnitScript.transform.position : transform.position);
	}
	
	public Message ToMessage (CardScript inCardScript = null)
	{
		//return new Message(ToTerm(inCardScript), null == myHeroUnitScript ? new Message.Term() : null == myHeroUnitScript.myTargetUnit ? new Message.Term() : myHeroUnitScript.myTargetUnit.myOwningPlayerToTerm(null));
	}

}
