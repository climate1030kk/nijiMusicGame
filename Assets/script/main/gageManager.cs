using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gageManager : MonoBehaviour {

	private int _totalPoint = 0;
	private totalPoint _totalPointObj;
	private CSVReader _reader;
	private List<string[]> _csvDatas;
	private float _maxPoint = 0;
	private float _maxScaleX = 0;

	void Start () {
		// トータルポイント
		GameObject pointObj = GameObject.FindGameObjectWithTag ("point");
		_totalPointObj = pointObj.GetComponent<totalPoint> ();
		_totalPoint = 0;
	}

	void Update () {

		if (_maxPoint == 0 && _maxScaleX == 0) {
			init ();
		}

		if (_totalPointObj == null) {
			GameObject pointObj = GameObject.FindGameObjectWithTag ("point");
			_totalPointObj = pointObj.GetComponent<totalPoint> ();
		}
		_totalPoint = _totalPointObj.GetTotalPoint ();
		float gageRate = _totalPoint / _maxPoint;
		this.transform.localScale = new Vector3 (_maxScaleX * gageRate,this.transform.localScale.y * 1.0f,this.transform.localScale.z * 1.0f);
	}

	private void init(){
		// CSVデータ取得
		GameObject obj = GameObject.FindGameObjectWithTag ("Respawn");
		_reader = obj.gameObject.GetComponent<CSVReader> ();
		// レーン情報取得
		obj = GameObject.FindGameObjectWithTag ("lane");
		laneScript lane = obj.gameObject.GetComponent<laneScript> ();
		int excellentPoint = lane.GetExcellentPoint ();

		_csvDatas = _reader.GetCsvDatas ();
		_maxPoint = excellentPoint * _csvDatas.Count;
		_maxScaleX = this.transform.localScale.x;
	}
}
