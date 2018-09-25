using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectDifficulty : MonoBehaviour {

	[SerializeField]
	public int difficulyLvel = 1;
	[SerializeField]
	public Color onColor = new Color();
	[SerializeField]
	public Color offColor = new Color();
	private SelectSceneManager _sceneManager;

	void Start () {
		GameObject obj = GameObject.FindGameObjectWithTag ("sceneManager");
		_sceneManager = obj.GetComponent<SelectSceneManager> ();
	}

	void OnMouseDown(){
		_sceneManager.setDifficulyLevel(difficulyLvel);
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("difficulty");
		// カラー初期化
		foreach (GameObject obj in objs) {
			SelectDifficulty selectDifficulty = obj.GetComponent<SelectDifficulty> ();
			selectDifficulty.SetColor (offColor);
		}
		// 有効カラーに変更
		SetColor (onColor);
	}

	// 色を変更
	public void SetColor(Color setColor){
		SpriteRenderer spriteRenderer = this.gameObject.GetComponent<SpriteRenderer> ();
		spriteRenderer.color = setColor;
	}
}
