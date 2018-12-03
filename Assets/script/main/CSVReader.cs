using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text;

public class CSVReader : MonoBehaviour
{

    private string _tsvName; // 読み込む譜面の名前
    private string _level; // 難易度
    private TextAsset _csvFile; // CSVファイル
    private List<string[]> _csvDatas = new List<string[]>(); // CSVの中身を入れるリスト
    private int _height = 0; // CSVの行数

    void Start()
    {
        _tsvName = "audio_" + SelectSceneManager.stageNumber.ToString() + "-" + SelectSceneManager.difficulyLevel.ToString();
        // CSV読み込み
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            TextAsset csvFile = Resources.Load("mainGame/CSV/" + _tsvName) as TextAsset; // Resouces下のCSV読み込み
            StringReader reader = new StringReader(csvFile.text);

            // , で分割しつつ一行ずつ読み込み
            // リストに追加していく
            while (reader.Peek() != -1) // reader.Peaekが-1になるまで
            {
                string line = reader.ReadLine(); // 一行ずつ読み込み
                _csvDatas.Add(line.Split(',')); // , 区切りでリストに追加
            }
        }
        else
        {
            string FilePath = Application.dataPath + "/Resources/mainGame/CSV/" + _tsvName + ".csv";
            //string FilePath = Application.dataPath + "/Resources/" + _tsvName + ".csv";
            FileInfo fiA = new FileInfo(FilePath);
            StreamReader reader = new StreamReader(fiA.OpenRead(), Encoding.UTF8);

            while (reader.Peek() > -1)
            {
                string line = reader.ReadLine();
                _csvDatas.Add(line.Split(',')); // リストに入れる
                _height++; // 行数加算
            }
        }

        // var index = stringValue1.IndexOf ("-");
        // var stageNum = s.Substring(0, index + 1);
        // var difficultyNum = s.Substring(index - 1);
    }

    public List<string[]> GetCsvDatas()
    {
        return _csvDatas;
    }
}
