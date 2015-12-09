using UnityEngine;
using System.Collections;

public class Card_Animation_FSM_Script : Finite_State_Machine_Script
{
	public Card_Script myCard;

	void Awake()
	{
		myCard = GetComponent<Card_Script>();
		myAnim = GetComponent<Animation>();
	}

	public AnimationClip MakeClip(float inDuration, Vector3 inLocalPosition, Vector3 inLocalScale, Quaternion inLocalRotation)
	{
		AnimationClip outClip = new AnimationClip();

		outClip.legacy = true;

		outClip.SetCurve("", typeof(Transform), "localPosition.x", AnimationCurve.Linear(0, transform.localPosition.x, inDuration, inLocalPosition.x));
		outClip.SetCurve("", typeof(Transform), "localPosition.y", AnimationCurve.Linear(0, transform.localPosition.y, inDuration, inLocalPosition.y));
		outClip.SetCurve("", typeof(Transform), "localPosition.z", AnimationCurve.Linear(0, transform.localPosition.z, inDuration, inLocalPosition.z));

		outClip.SetCurve("", typeof(Transform), "localScale.x", AnimationCurve.Linear(0, transform.localScale.x, inDuration, inLocalScale.x));
		outClip.SetCurve("", typeof(Transform), "localScale.y", AnimationCurve.Linear(0, transform.localScale.y, inDuration, inLocalScale.y));
		outClip.SetCurve("", typeof(Transform), "localScale.z", AnimationCurve.Linear(0, transform.localScale.z, inDuration, inLocalScale.z));

		outClip.SetCurve("", typeof(Transform), "localRotation.w", AnimationCurve.Linear(0, transform.localRotation.w, inDuration, inLocalRotation.w));
		outClip.SetCurve("", typeof(Transform), "localRotation.x", AnimationCurve.Linear(0, transform.localRotation.x, inDuration, inLocalRotation.x));
		outClip.SetCurve("", typeof(Transform), "localRotation.y", AnimationCurve.Linear(0, transform.localRotation.y, inDuration, inLocalRotation.y));
		outClip.SetCurve("", typeof(Transform), "localRotation.z", AnimationCurve.Linear(0, transform.localRotation.z, inDuration, inLocalRotation.z));

		return outClip;
	}

	public static Vector3 CardSize()
	{
		return new Vector3(.5f, .75f, .01f);
	}

	public void PlayClip(float inDuration, Vector3 inLocalPosition, Vector3 inLocalScale, Quaternion inLocalRotation, string inName)
	{
		myAnim.AddClip(MakeClip(inDuration, inLocalPosition, inLocalScale, inLocalRotation), inName);
		myAnim.Play(inName);
	}

	public void PlayCard(GameObject inSubject, GameObject inObject)
	{
		myCard.Affect(0, inSubject, inObject);
	}

	private Animation myAnim;
}
