using UnityEngine;
using System.Collections;
using System.IO;  // <- ここに注意

public class CSVWriter : MonoBehaviour {

    private string fileName; // 保存するファイル名

	// CSVに書き込む処理
	public void WriteCSV(string txt){
		StreamWriter streamWriter;
		FileInfo fileInfo;
        fileName = "audio_" + SelectSceneManager.stageNumber.ToString() + "-" + SelectSceneManager.difficulyLevel.ToString();
        //fileInfo = new FileInfo (Application.dataPath +"/Resources/mainGame/CSV/"+ fileName + ".csv");
        fileInfo = new FileInfo(Application.dataPath + "/Resources/" + fileName + ".csv");
        streamWriter = fileInfo.AppendText ();
		streamWriter.WriteLine (txt);
		streamWriter.Flush();
		streamWriter.Close ();
	}
}