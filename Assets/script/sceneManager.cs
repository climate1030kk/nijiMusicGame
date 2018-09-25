using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour {

	[SerializeField]
	public string sceneName = "";
	bool fade = false;
	Material mat;
	float fadeSpeed = 0.05f;

	[SerializeField]
	public bool anyKeyStart = false;

	void Start(){
		GameObject child = transform.Find ("fade").gameObject;
		mat = child.GetComponent<MeshRenderer>().material;
	}

	void Update () {
		// フェード開始
		if (anyKeyStart && (Input.anyKey || Input.GetMouseButtonDown(0))) {
			if (!fade) {
				AudioSource audioSource = this.gameObject.GetComponent<AudioSource> ();
				audioSource.PlayOneShot (audioSource.clip);
			}
			fade = true;
		}
		// フェード処理
		if (fade) {
			mat.color = new Color(mat.color.r,mat.color.g,mat.color.b,mat.color.a + fadeSpeed);
		}

		if (mat.color.a >= 1) {
			nextScene();
		}
	}

	// 呼び出すのはこれ
	// シーン名とフェードの色を変えれるようにしよう
	public void  nextSceneTrigger(){
		fade = true;
	}

	private void nextScene(){
		SceneManager.LoadScene(sceneName);
	}
}
