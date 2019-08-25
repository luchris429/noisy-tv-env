using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testBot : MonoBehaviour {

	// Use this for initialization
	public Rigidbody rb;
	public Vector3 m_EulerAngleVelocity = new Vector3(0, 1, 0);
	public SlidingDoor sd;
	public int cooldown = 10;
	public int lastAct = 0;
	public Texture[] ts;
	public Renderer tv;
	public Renderer tv2;

	void Start () {
		rb = this.GetComponent<Rigidbody> ();

	}

	// Update is called once per frame
	void FixedUpdate () {
		if (lastAct <= 0) {
			rb.velocity = Vector3.zero;
			if (Input.GetKey (KeyCode.W)) {
				rb.velocity = transform.forward * 20.0f;
				//rb.MovePosition (transform.position + transform.forward * 1.5f);
				lastAct = cooldown;
			} else if (Input.GetKey (KeyCode.S)) {
				rb.velocity = -transform.forward * 20.0f;
				//rb.MovePosition (transform.position - transform.forward * 1.5f);
				lastAct = cooldown;
			} else {
				rb.velocity = Vector3.zero;
			}
			if (Input.GetKey (KeyCode.D)) {
				Quaternion deltaRotation = Quaternion.Euler (m_EulerAngleVelocity * 15);
				rb.MoveRotation (rb.rotation * deltaRotation);
				lastAct = cooldown;
			} else if (Input.GetKey (KeyCode.A)) {
				Quaternion deltaRotation = Quaternion.Euler (-m_EulerAngleVelocity * 15);
				rb.MoveRotation (rb.rotation * deltaRotation);
				lastAct = cooldown;
			}
			if (Input.GetKey (KeyCode.Space)) {
				sd.press ();
				lastAct = cooldown;
			}
			if(Input.GetKey(KeyCode.E)){
				if (Vector3.Distance (this.transform.position, tv.gameObject.transform.position) < 18.0f) {
					int r = Random.Range (0, ts.Length - 1);
					tv.material.mainTexture = ts [r];
				}
				else if (Vector3.Distance (this.transform.position, tv2.gameObject.transform.position) < 18.0f) {
					int r = Random.Range (0, ts.Length - 1);
					tv2.material.mainTexture = ts [r];
				}
				lastAct = cooldown;
			}
		} else {
			lastAct = lastAct - 1;
		}
	}
}
