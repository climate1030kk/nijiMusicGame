using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayer : MonoBehaviour {
	private AudioSource _audioSource;
	private string _musicName; // 読み込む譜面の名前
	// Use this for initialization
	void Start () {
		_musicName = "audio_" + SelectSceneManager.stageNumber.ToString();
		// 音源再生
		AudioClip audioClip = Resources.Load("mainGame/" + _musicName) as AudioClip;
		_audioSource = this.GetComponent<AudioSource>();
		_audioSource.clip = audioClip;
		_audioSource.Play();
		_audioSource.time = 0.001f;
	}

	void Update(){
		if (GetTiming () == 0.0f) {
			GameClear ();
		}
	}

	public float GetTiming(){
		return _audioSource.time;
	}

	public void GameClear(){
		SceneManager.LoadScene("result");
	}
}
