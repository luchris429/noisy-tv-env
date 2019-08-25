using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVAgent: Agent {

	public GameObject goal;

	public Rigidbody rb;
	public Vector3 m_EulerAngleVelocity = new Vector3(0, 1, 0);
	public SlidingDoor sd;
	//public UnityEngine.Video.VideoPlayer vp;
	//public UnityEngine.Video.VideoClip[] videoClips;

	public Texture[] ts;
	public Renderer tv;

	public Renderer tv2;

	List<Vector3> initLocations;
	List<Vector3> initRotations;

	private int count;

	public override void InitializeAgent() {
		rb = this.GetComponent<Rigidbody> ();

		initLocations = new List<Vector3> ();
		initRotations = new List<Vector3> ();

		initLocations.Add (new Vector3 (20.0f, 1.5f, 40.0f));
		initRotations.Add (new Vector3 (0.0f, 90.0f, 0.0f));

		initLocations.Add (new Vector3 (40.0f, 1.5f, 40.0f));
		initRotations.Add (new Vector3 (0.0f, 180.0f, 0.0f));

		initLocations.Add (new Vector3 (40.0f, 1.5f, 20.0f));
		initRotations.Add (new Vector3 (0.0f, 180.0f, 0.0f));

		initLocations.Add (new Vector3 (20.0f, 1.5f, 20.0f));
		initRotations.Add (new Vector3 (0.0f, 180.0f, 0.0f));

		initLocations.Add (new Vector3 (0.0f, 1.5f, 20.0f));
		initRotations.Add (new Vector3 (0.0f, 90.0f, 0.0f));

		initLocations.Add (new Vector3 (40.0f, 1.5f, 0.0f));
		initRotations.Add (new Vector3 (0.0f, -90.0f, 0.0f));

		initLocations.Add (new Vector3 (20.0f, 1.5f, 0.0f));
		initRotations.Add (new Vector3 (0.0f, 180.0f, 0.0f));

		initLocations.Add (new Vector3 (20.0f, 1.5f, -20.0f));
		initRotations.Add (new Vector3 (0.0f, -90.0f, 0.0f));

		initLocations.Add (new Vector3 (0.0f, 1.5f, -20.0f));
		//initRotations.Add (new Vector3 (0.0f, 0.0f, 0.0f));
		initRotations.Add (new Vector3 (0.0f, 180.0f, 0.0f));

		initLocations.Add(new Vector3(0.0f, 1.5f, 0.0f));
		initRotations.Add(new Vector3(0.0f, -90.0f, 0.0f));

		initLocations.Add(new Vector3(-20.0f, 1.5f, 0.0f));
		initRotations.Add (new Vector3 (0.0f, -90.0f, 0.0f));

		initLocations.Add(new Vector3(-20.0f, 1.5f, 20.0f));
		initRotations.Add(new Vector3(0.0f, 180.0f, 0.0f));

		initLocations.Add(new Vector3(-35.0f, 1.5f, 20.0f));
		initRotations.Add(new Vector3(0.0f, 0.0f, 0.0f));

		initLocations.Add(new Vector3(-55.0f, 1.5f, 20.0f));
		initRotations.Add(new Vector3(0.0f, 90.0f, 0.0f));

		initLocations.Add(new Vector3(-35.0f, 1.5f, 40.0f));
		initRotations.Add(new Vector3(0.0f, 90.0f, 0.0f));

		initLocations.Add(new Vector3(-10.0f, 1.5f, 40.0f));
		initRotations.Add(new Vector3(0.0f, 0.0f, 0.0f));

		count = 3;

		AgentReset ();

	}

	public override List<float> CollectState()
	{
		List<float> state = new List<float> ();
		state.Add (transform.position.x);
		state.Add (transform.position.z);
		return state;
	}

	public override void AgentStep(float[] act)
	{	
		if (Vector3.Distance (goal.transform.position, this.transform.position) < 2.5f) {
			reward = 1.0f;
			done = true;
		} else {
			reward = 0.0f;
		}
		if (act [0] == 1) {
			rb.velocity = transform.forward * 5.0f;
		} else {
			rb.velocity = Vector3.zero;
		}
		if (act[0] == 2) {
			Quaternion deltaRotation = Quaternion.Euler (m_EulerAngleVelocity * 1.5f);
			rb.MoveRotation (rb.rotation * deltaRotation);
		} else if (act[0] == 3) {
			Quaternion deltaRotation = Quaternion.Euler (-m_EulerAngleVelocity * 1.5f);
			rb.MoveRotation (rb.rotation * deltaRotation);
		} else if (act[0] == 4) {
			count = (count + 1) % 10;
			if (count == 0) {
				sd.press ();
			}
		} else if(act[0] == 5){
			/*videoClipIndex = (videoClipIndex + 1) % 40;
			if (videoClipIndex % 10 == 0) {
				videoClipIndex = videoClipIndex;
				vp.clip = videoClips [videoClipIndex / 10];
			}*/
			if (Vector3.Distance (this.transform.position, tv.gameObject.transform.position) < 18.0f) {
				count = (count + 1) % 10;
				if (count == 0) {
					int r = Random.Range (0, ts.Length - 1);
					tv.material.mainTexture = ts [r];
				}
			}
			if (tv2 != null) {
				if (Vector3.Distance (this.transform.position, tv2.gameObject.transform.position) < 18.0f) {
					count = (count + 1) % 10;
					if (count == 0) {
						int r = Random.Range (0, ts.Length - 1);
						tv2.material.mainTexture = ts [r];
					}
				}
			}
		}
	}

	public override void AgentReset()
	{
		Academy a = GameObject.Find ("Academy").GetComponent<Academy> ();
		int startLoc = (int)(a.resetParameters ["startLoc"]);
		if (startLoc == 0) {
			this.transform.position = initLocations [Random.Range (0, initLocations.Count)];
			this.transform.rotation = Quaternion.Euler(0.0f, Random.Range (0, 24) * 15.0f, 0.0f);
		} else {
			this.transform.position = initLocations[startLoc - 1];
			this.transform.rotation = Quaternion.Euler(initRotations[startLoc - 1]);
		}
	}

	public override void AgentOnDone()
	{

	}
}
