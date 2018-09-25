using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Return : MonoBehaviour {
	private sceneManager _sceneManager;
	private AudioSource _audioSources;
	void Start(){
		GameObject obj = GameObject.FindGameObjectWithTag ("sceneManager");
		_sceneManager = obj.GetComponent<sceneManager> ();

		_audioSources = this.gameObject.GetComponent<AudioSource> ();
	}

	void OnMouseDown(){
		_sceneManager.nextSceneTrigger ();
		_audioSources.PlayOneShot (_audioSources.clip);
	}
}
