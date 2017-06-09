using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour {

	int helperCount;
	Animation anim;
	GvrAudioSource helperVoice;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animation>();
		helperVoice = GetComponent<GvrAudioSource>();
		if (helperCount==0 && !anim.isPlaying) {
			StartCoroutine("helperLanding");
		}
	}
	

	IEnumerator helperLanding() {
        yield return new WaitForSeconds(2f);
		this.gameObject.GetComponent<Renderer>().enabled = true;
		anim["helper-landing"].speed = .5f;
		anim.Play();
		helperCount ++ ;
		helperVoice.Play();
    }
}



