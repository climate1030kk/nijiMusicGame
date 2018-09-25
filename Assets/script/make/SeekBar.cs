using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekBar : MonoBehaviour {
	private Vector3 clickPosition;
	private int max_time = 108;
	private AudioSource _audioSource;

	void Start(){
		_audioSource = GameObject.Find("GameMusic").GetComponent<AudioSource> ();
	}

	void Update () {
		// マウス入力で左クリックをした瞬間
		if (Input.GetMouseButtonDown(0) && _audioSource.time > 0) {
			clickPosition = Input.mousePosition;
			float sizeRate = clickPosition.y / Screen.height;
			this.transform.localScale = new Vector3(1, 18 * sizeRate, 1);
			_audioSource.time = max_time * sizeRate;
		}
	}
}
