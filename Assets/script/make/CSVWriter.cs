using UnityEngine;
using System.Collections;
using System.IO;  // <- ここに注意

public class CSVWriter : MonoBehaviour {


	private string fileName = "audio_2-1"; // 保存するファイル名

	// CSVに書き込む処理
	public void WriteCSV(string txt){
		StreamWriter streamWriter;
		FileInfo fileInfo;
		fileInfo = new FileInfo (Application.dataPath +"/resources/mainGame/CSV/"+ fileName + ".csv");
		streamWriter = fileInfo.AppendText ();
		streamWriter.WriteLine (txt);
		streamWriter.Flush();
		streamWriter.Close ();
	}
}