using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesCreator : MonoBehaviour {

	private List<string[]> _csvDatas = new List<string[]>();
	private GameObject[] _objs;
	private CSVReader _reader;
	private AudioPlayer _audioPlayer;
	[SerializeField]
	public GameObject note;
	private const int _TIME_KEY = 0;
	private const int _KEYCODE_KEY = 1;

	[SerializeField]
	public float arriveTime = 0.1f;

	void Start () {
		_reader = this.GetComponent<CSVReader> ();
		_csvDatas = _reader.GetCsvDatas ();
		_objs = GameObject.FindGameObjectsWithTag ("lane");

		_audioPlayer = this.GetComponent<AudioPlayer> ();
	}
		
	void Update () {
		this.createNote ();
	}

	// 時間に合わせてノーツ生成
	private void createNote(){
		// 未作成ノーツが存在しない場合は帰る
		if (_csvDatas.Count <= 0) {
			return;
		}

		// あとからsetSyncSpritを呼ぶ可能性があるので保存
		List<noteScript> noteScripts = new List<noteScript>();

		// csvからノーツを生成
		for (int i = 0; i < _csvDatas.Count; i++){
			float time = float.Parse (_csvDatas [i] [_TIME_KEY]);
			time = Mathf.RoundToInt (time / (60.0f / (float)SelectSceneManager.bpm)) * (60.0f / (float)SelectSceneManager.bpm);
			if (time <= _audioPlayer.GetTiming () + arriveTime) {
				foreach (var obj in _objs) {
					// レーン取得
					laneScript lane = obj.GetComponent<laneScript> ();
					if (lane.key.ToString () == _csvDatas [i] [_KEYCODE_KEY]) {
						// ノーツ生成
						var noteObj = Instantiate (note);
						noteObj.transform.position = new Vector3 (noteObj.transform.position.x, this.transform.position.y, lane.transform.position.z - 1);
						noteScript noteScript = noteObj.GetComponent<noteScript> ();
						// ノーツ初期設定
						noteScript.init(obj, arriveTime - (_audioPlayer.GetTiming () + arriveTime) - time);
						noteScripts.Add(noteScript);
						_csvDatas.RemoveAt (i);
						i--;
						break;
					}
				}
			}
		}

		// 生成済みcsv削除
		if(noteScripts.Count >= 2){
			foreach (var noteScript in noteScripts) {
				noteScript.setSyncSprit ();
			}
		}
	}

	// 到着までの時間を取得
	public float GetArriveTime(){
		return arriveTime;
	}
}