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

	private float _intv = 0;
	private const float INTERVAL = 0.1f;
	private const float MINIMUMDIFFERENCE = 15.0f;

	void Start (){
		string _musicName = "audio_2"; // 曲名
		AudioClip audioClip = Resources.Load("mainGame/" + _musicName) as AudioClip;
		_audioSource = GameObject.Find("GameMusic").GetComponent<AudioSource> ();
		_audioSource.clip = audioClip;
		_CSVWriter = GameObject.Find ("CSVWriter").GetComponent<CSVWriter> ();
	}

	void Update (){
		if (_isPlaying) {
			// DetectKeys ();
			AutoMakeNotes ();
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

	voif AutoMakeNotes(){
		intv -= Time.deltaTime;

		float[] spectrum = new float [1024];
		_audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);
		// 最も大きい音を取得
		float nowMax = 0;
        for (int i = 1; i < spectrum.Length - 1; i++)
        {
            if (spectrum[i] >= nowMax)
            {
                nowMax = spectrum[i];
            }
        }
        nowMax *= 100;
        float preMax = nowMax;

 		if (nowMax >= preMax + MINIMUMDIFFERENCE){
            if (intv <= 0)
            {
            	WriteNotesTiming(_audioSource.time,KeyCode.D);
                intv = INTERVAL;
            }
        }
	}
}