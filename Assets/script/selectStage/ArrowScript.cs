using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {
    private SelectImageScrpt _selectImageScript;
    private AudioSource _audioSources;

    [SerializeField]
    int way = 0;

	void Start () {
        _selectImageScript = GameObject.FindGameObjectWithTag("selectImage").GetComponent<SelectImageScrpt>();

        _audioSources = this.gameObject.GetComponent<AudioSource> ();
		_audioSources.Stop ();
	}

	void OnMouseDown(){
        _selectImageScript.setIndex(_selectImageScript.getIndex() + way);
        _audioSources.PlayOneShot (_audioSources.clip);
	}
}
