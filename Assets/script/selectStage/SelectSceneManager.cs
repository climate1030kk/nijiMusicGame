using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectSceneManager : MonoBehaviour {

	static public int stageNumber = 1;
	static public float bpm = 0;
    static public int climaxBar = 0;
    static public int difficulyLevel = 1;

	public void setDifficulyLevel(int setDifficulyLevel){
		difficulyLevel = setDifficulyLevel;
	}

	public void setStageNumber(int setStageNumber){
		stageNumber = setStageNumber;
	}

	public void setBpm(float setBpm){
		bpm = setBpm;
	}

    public void setClimaxBar(int setClimaxBar)
    {
        climaxBar = setClimaxBar;
    }

    public void nextScene(){
		sceneManager obj = this.gameObject.GetComponent<sceneManager> ();
		obj.nextSceneTrigger ();
	}
}
