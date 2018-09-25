using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class CSVReader : MonoBehaviour {

	private string _tsvName; // 読み込む譜面の名前
	private string _level; // 難易度
	private TextAsset _csvFile; // CSVファイル
	private List<string[]> _csvDatas = new List<string[]>(); // CSVの中身を入れるリスト
	private int _height = 0; // CSVの行数

	void Start(){
		_tsvName = "audio_" + SelectSceneManager.stageNumber.ToString() + "-" + SelectSceneManager.difficulyLevel.ToString();
		// マスタデータを取得
		_csvFile = Resources.Load("mainGame/CSV/" + _tsvName) as TextAsset;
		StringReader reader = new StringReader(_csvFile.text);
		while(reader.Peek() > -1) {
			string line = reader.ReadLine();
			_csvDatas.Add(line.Split(',')); // リストに入れる
			_height++; // 行数加算
		}

		// var index = stringValue1.IndexOf ("-");
		// var stageNum = s.Substring(0, index + 1);
		// var difficultyNum = s.Substring(index - 1);
	}

	public List<string[]> GetCsvDatas(){
		return _csvDatas;
	}
}
