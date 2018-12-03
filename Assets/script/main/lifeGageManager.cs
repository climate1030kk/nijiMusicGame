using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lifeGageManager : MonoBehaviour {

	[SerializeField]
	public int maxLife;
	private int _nowLife;
	private float _maxScaleX;

	void Start () {
		_maxScaleX = this.transform.localScale.x;
		_nowLife = maxLife;
	}

	void Update () {
		float gageRate = (float)_nowLife / (float)maxLife;
		gageRate = gageRate < 0 ? 0 : gageRate;
		this.transform.localScale = new Vector3 (_maxScaleX * gageRate,this.transform.localScale.y * 1.0f,this.transform.localScale.z * 1.0f);
	}

	// ライフ減少
	public int AddDamage(int damage){
		_nowLife -= damage;
		if(_nowLife <= 0){
			GameOver();
		}
		return _nowLife;
	}

	// ゲームオーバー
	public void GameOver(){
		//SceneManager.LoadScene("selectStage");
	}
}
