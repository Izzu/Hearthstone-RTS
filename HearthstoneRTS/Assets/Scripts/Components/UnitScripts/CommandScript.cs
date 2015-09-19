using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandScript : MonoBehaviour {

	private UnitScript myUnit;

	private Queue<Command> myCommands;

	public UnitScript target
	{
		get
		{
			if(myCommands.Count > 0)
			{
				return myCommands.Peek().target;
			}
			return null;
		}
	}

	void Awake ()
	{
		myUnit = GetComponent<UnitScript>();

		myCommands = new Queue<Command>();
	}

	public CommandScript Clear()
	{
		myCommands.Clear();

		return this;
	}

	public CommandScript Order(Command input)
	{
		if(myCommands.Count == 0)
		{
			myCommands.Enqueue(input);

			input.Begin(myUnit);
		}
		else
		{
			myCommands.Enqueue(input);
		}

		return this;
	}

	void Update()
	{
		if (null != myCommands)
		{
			//emptyQ.Peek will cry if it's empty,
			if(myCommands.Count > 0)
			{
				Command currentCommand = myCommands.Peek();
				
				//null commands are obvs a problem
				if (null == currentCommand)
				{
					//this is not done
				}
				else
				{
					if (currentCommand.IsDone(myUnit))
					{
						myCommands.Dequeue();

						if (myCommands.Count > 0)
						{
							currentCommand = myCommands.Peek();

							if (null != currentCommand)
							{
								currentCommand.Begin(myUnit);
							}
						}
					}

					currentCommand.Update(myUnit);
				}
			}
		}
	}

	public abstract class Command
	{
		public abstract void Begin(UnitScript self);

		public abstract void Update(UnitScript self);

		public abstract bool IsDone(UnitScript self);

		public abstract UnitScript target
		{
			get;
		}
	}

	public class Move : Command
	{
		public Vector3 myDestination;

		public float myRange;

		public Move(Vector3 input, float range = .5f)
		{
			myDestination = input;
			myRange = range;
		}

		public override void Begin(UnitScript self)
		{
			self.myNavMeshAgent.destination = myDestination;
		}

		public override void Update(UnitScript self)
		{
			//do nothing;
		}

		public override bool IsDone(UnitScript self)
		{
			return (myDestination - self.gameObject.transform.position).magnitude < myRange;
		}

		public override UnitScript target
		{
			get
			{
				return null;
			}
		}
	}

	public class Follow : Move
	{
		public UnitScript myTarget;

		public Follow(UnitScript target, float range = 3f)
			: base(target.transform.position, range)
		{
			myTarget = target;
		}

		public override void Begin(UnitScript self)
		{
			Vector3 position = self.transform.position;

			self.myNavMeshAgent.destination = myDestination;
		}

		public override void Update(UnitScript self)
		{
			Vector3 position = self.transform.position;

			Vector3 destination = myTarget.transform.position;

			if ((position - destination).magnitude > myRange)
			{
				if((destination - myDestination).magnitude > 1f)
				{
					self.myNavMeshAgent.destination = destination;
				}
			}
			else
			{
				self.myNavMeshAgent.destination = position;
			}
		}

		public override bool IsDone(UnitScript self)
		{
 			return null == target;
		}

		public override UnitScript target
		{
			get
			{
				return myTarget;
			}
		}
	}

	public class Interact : Follow
	{
		public InteractionScript myAction;

		public Interact(UnitScript target, InteractionScript action) :
			base(target, .5f)//it would make more sense to pull range from whatever is attacking
		{
			if (null == target)
			{
				throw new System.ArgumentException("Parameter cannot be null", "target");
			}
			if (null == action)
			{
				throw new System.ArgumentException("Parameter cannot be null", "action");
			}
			else
			{
				myAction = action;
			}
		}

		public override void Begin(UnitScript self)
		{
			Update(self);
		}

		public override void Update(UnitScript self)
		{
			//if in range
			//	if interaction started
			//		update interaction
			//	else
			//		start interaction
			//else
			//	stop interaction
			//	move in range
			Vector3 position = self.transform.position;

			Vector3 destination = myTarget.transform.position;

			float distance = (position - destination).magnitude;

			if(distance < myRange)
			{
				if(false == myAction.isActive)
				{
					myAction.self = self;
					myAction.target = myTarget;
					myAction.begin = Time.time;
				}
			}
			else
			{
				myAction.Stop();
				self.myNavMeshAgent.destination = destination;
			}
		}

		public override bool IsDone(UnitScript self)
		{
			return null == myTarget;
		}

		public override UnitScript target
		{
			get
			{
				return myTarget;
			}
		}
	}

	public class Hold : Command
	{
		public override void Begin(UnitScript self)
		{
			throw new System.NotImplementedException();
		}

		public override void Update(UnitScript self)
		{
			throw new System.NotImplementedException();
		}

		public override bool IsDone(UnitScript self)
		{
			throw new System.NotImplementedException();
		}

		public override UnitScript target
		{
			get
			{
				throw new System.NotImplementedException();
			}
		}
	}

	public class Idle : Command
	{
		public override void Begin(UnitScript self)
		{
			throw new System.NotImplementedException();
		}

		public override void Update(UnitScript self)
		{
			throw new System.NotImplementedException();
		}

		public override bool IsDone(UnitScript self)
		{
			throw new System.NotImplementedException();
		}

		public override UnitScript target
		{
			get { throw new System.NotImplementedException(); }
		}
	}
}
