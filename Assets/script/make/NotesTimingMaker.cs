using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesTimingMaker : MonoBehaviour {

	private AudioSource _audioSource;
	private CSVWriter _CSVWriter;

	private bool _isPlaying = false;
	public GameObject startButton;

	private List<KeyCode> _keys = new List<KeyCode>();
	private List<float> _keyTimes = new List<float>();

	void Start (){
		string _musicName = "audio_2"; // 曲名
		AudioClip audioClip = Resources.Load("mainGame/" + _musicName) as AudioClip;
		_audioSource = GameObject.Find("GameMusic").GetComponent<AudioSource> ();
		_audioSource.clip = audioClip;
		_CSVWriter = GameObject.Find ("CSVWriter").GetComponent<CSVWriter> ();
	}

	void Update (){
		if (_isPlaying) {
			DetectKeys ();
			// 処理済みなので初期化
			_keys = new List<KeyCode>();
			_keyTimes = new List<float>();
		}
	}

	void OnGUI(){
		//  押されたキーとその時間を保存
		if(Event.current.type == EventType.KeyDown) {
			_keys.Add(Event.current.keyCode);
			_keyTimes.Add (_audioSource.time);

		}
	}

	public void StartMusic(){
		startButton.SetActive (false);
		_audioSource.Play ();
		_isPlaying = true;
	}

	void DetectKeys(){
		int i = 0;
		foreach (KeyCode key in _keys) {
			float time = _keyTimes[i];
			if (key == KeyCode.D || key == KeyCode.F || key == KeyCode.J || key == KeyCode.K) {
				WriteNotesTiming (time, key);
			}
			++i;
		}
	}

	void WriteNotesTiming(float time, KeyCode num){
		_CSVWriter.WriteCSV (time.ToString () + "," + num.ToString());
	}
}