using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour {

	// Use this for initialization
	public bool closed = true;
	public bool random = true;
	void Start () {
		
	}
	public void press(){
		if (!random || Random.value > 0.5f) {
			changePos ();
		}
	}

	public void changePos() {
		if (closed) {
			transform.position = transform.position + transform.forward * 6;
		} else {
			transform.position = transform.position - transform.forward * 6;
		}
		closed = !closed;

	}
}
