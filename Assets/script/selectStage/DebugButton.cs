using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugButton : MonoBehaviour {
    public void ChangeMode()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("sceneManager");
        sceneManager sceneManager = obj.GetComponent<sceneManager>();
        sceneManager.SceneName = sceneManager.SceneName == "mainGame" ? "notesTimingMake" : "mainGame";

        transform.Find("Text").transform.gameObject.GetComponent<Text>().text = sceneManager.SceneName == "mainGame" ? "譜面作成モードへ変更" : "ゲームモードへ変更";
    }
}
