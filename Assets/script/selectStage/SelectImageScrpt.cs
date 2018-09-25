using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SelectImageScrpt : MonoBehaviour {
	[SerializeField]
	public int index;
	[SerializeField]
	public int bpm;

	private Vector3 _arrivePos;
	private Vector3 _posSpeed;

	private Vector3 _arriveScale;
	private Vector3 _scaleSpeed;
	private float _arriveTime = 1.0f;

	private SelectSceneManager _sceneManager;
	private int _stageNumber = 1;

	private AudioSource _audioSources;

	void Start(){
		GameObject obj = GameObject.FindGameObjectWithTag ("sceneManager");
		_sceneManager = obj.GetComponent<SelectSceneManager> ();
		string name = this.gameObject.GetComponent<SpriteRenderer> ().sprite.name;
		_stageNumber = int.Parse(name.Replace ("selectImage", ""));

		if (index == 1) {
			_sceneManager.setStageNumber (_stageNumber);
			_sceneManager.setBpm (bpm);
		}
	}

//	void Update(){
//		if (Mathf.Abs (_arrivePos.x - this.gameObject.transform.position.x) <= 0.05f) {
//			this.gameObject.transform.position = _arrivePos;
//		} else {
//			this.gameObject.transform.position += _posSpeed;
//			this.gameObject.transform.localScale += _scaleSpeed;
//		}
//	}

	public void setIndex(int index_){
		index = index_;
	}

	public int getIndex(){
		return index;
	}

	// 押されたときに呼び出される
	public void setPos(Vector3 arrivePos,Vector3 arriveScale){
		_arrivePos = arrivePos;
		_arriveScale = arriveScale;
		Vector3 distancePos = this.gameObject.transform.position - _arrivePos;
		_posSpeed = new Vector3(distancePos.x / _arriveTime,distancePos.y / _arriveTime,distancePos.z / _arriveTime);

		Vector3 distanceScale = this.gameObject.transform.localScale - _arriveScale;
		_scaleSpeed = new Vector3(distanceScale.x / _arriveTime,distanceScale.y / _arriveTime,distanceScale.z / _arriveTime);
	}

	void OnMouseDown(){
		this.transform.parent.gameObject.transform.localScale = new Vector3 (0.0f, 0.0f, 0.0f);
		_audioSources = this.gameObject.GetComponent<AudioSource> ();
		_audioSources.PlayOneShot (_audioSources.clip);
		_sceneManager.setStageNumber (_stageNumber);
		_sceneManager.setBpm (bpm);
		_sceneManager.nextScene ();
	}
}
