using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateAgent : Agent {


	public Rigidbody rb;
	public ConfigurableJoint cj;
	public int ts;

	public override void InitializeAgent() {
		rb = GetComponent<Rigidbody> ();
		cj = GetComponent<ConfigurableJoint> ();

	}
	public override List<float> CollectState()
	{
		List<float> state = new List<float>();

	//	state.Add (cj.targetAngularVelocity[0]);
//		state.Add (cj.targetAngularVelocity[1]);
//		state.Add (cj.targetAngularVelocity[2]);
		state.Add (transform.position.x);
		state.Add (transform.position.y);
		state.Add (transform.position.z);
		state.Add (transform.rotation.x);
		state.Add (transform.rotation.y);
		state.Add (transform.rotation.z);


		return state;
	}

	public override void AgentStep(float[] act)
	{	
		if (rb.IsSleeping ()) {
			rb.WakeUp ();
		}
		cj.targetAngularVelocity = new Vector3 (act [0] * ts, act [1] * ts, act [2] * ts);

	}

	public override void AgentReset()
	{

	}

	public override void AgentOnDone()
	{

	}
}
