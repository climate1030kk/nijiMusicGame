using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {
	private int _way = 0;
	private GameObject[] _selectImageObjs;
	private Vector3[] _positions = new Vector3[3];
	private Vector3[] _scales = new Vector3[3];

	private AudioSource _audioSources;

	void Start () {
		if (this.transform.position.x >= 0) {
			_way = - 1;
		} else {
			_way = 1;
		}

		// 画像の位置
		_positions [0] = new Vector3 (-4.5f, 0.0f, 1.0f);
		_positions [1] = new Vector3 (0.0f, 0.0f, 0.0f);
		_positions [2] = new Vector3 (4.5f, 0.0f, 1.0f);

		// 画像のサイズ
		_scales [0] = new Vector3 (0.3f, 0.3f, 1.0f);
		_scales [1] = new Vector3 (0.5f, 0.5f, 1.0f);
		_scales [2] = new Vector3 (0.3f, 0.3f, 1.0f);

		_audioSources = this.gameObject.GetComponent<AudioSource> ();

		_selectImageObjs = GameObject.FindGameObjectsWithTag ("selectImage");
		OnMouseDown ();
		_audioSources.Stop ();
	}

	void OnMouseDown(){
		foreach (GameObject obj in _selectImageObjs) {
			SelectImageScrpt selectImageScript = obj.GetComponent<SelectImageScrpt> ();
			int index = selectImageScript.getIndex () + _way;
			index = index > _selectImageObjs.Length - 1 ? 0 : index;
			index = index < 0 ? _selectImageObjs.Length - 1 : index;
			selectImageScript.setIndex (index);
			// 座標移動
			if (index == 0 || index == 1 || index == 2) {
				obj.transform.position = new Vector3 (_positions [index].x,obj.transform.position.y,_positions [index].z);
				obj.transform.localScale = _scales [index];
			} else {
				obj.transform.localScale = new Vector3 (0.0f, 0.0f, 0.0f);
			}
			// 曲名color
			if (index == 1) {
				obj.transform.Find ("title").gameObject.GetComponent<TextMesh> ().color = new Color (obj.transform.Find ("title").GetComponent<TextMesh> ().color.r, obj.transform.Find ("title").GetComponent<TextMesh> ().color.g, obj.transform.Find ("title").GetComponent<TextMesh> ().color.b, 1);
			} else {
				obj.transform.Find ("title").GetComponent<TextMesh> ().color = new Color (obj.transform.Find ("title").GetComponent<TextMesh> ().color.r, obj.transform.Find ("title").GetComponent<TextMesh> ().color.g, obj.transform.Find ("title").GetComponent<TextMesh> ().color.b, 0);
			}
		}
		_audioSources.PlayOneShot (_audioSources.clip);
	}
}
