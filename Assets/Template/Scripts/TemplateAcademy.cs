using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateAcademy : Academy {

	public SlidingDoor sd;
	public UnityEngine.Video.VideoPlayer vp;
	public UnityEngine.Video.VideoPlayer vp2;
	public Renderer tv;
	public Renderer tv2;
	public Texture initT;
	public override void AcademyReset()
	{
		if (resetParameters ["render"] == 1.0f) {
			Time.maximumDeltaTime = 0.0f;
		} else {
			Time.maximumDeltaTime = 0.333f;
		}

		if (resetParameters ["door"] == 0.0f) {
			if (sd.closed) {
				sd.changePos ();
			}
			sd.random = true;
		} else if (resetParameters ["door"] == 1.0f) {
			if (!sd.closed) {
				sd.changePos ();
			}
			sd.random = false;
		} else {
			if (!sd.closed) {
				sd.changePos ();
			}
			sd.random = true;
		}

		if (resetParameters ["tv"] == 0.0f) {
			vp.enabled = false;
			vp2.enabled = false;
		} else if (resetParameters ["tv"] == 1.0f) {
			vp.enabled = true;
			vp2.enabled = true;
		} else {
			vp.enabled = false;
			vp2.enabled = false;
			tv.material.mainTexture = initT;
			tv2.material.mainTexture = initT;
		}
	}

	public override void AcademyStep()
	{


	}

}
