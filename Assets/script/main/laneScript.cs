using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laneScript : MonoBehaviour {

	[SerializeField]
	public KeyCode key;
	[SerializeField]
	private int _totalPoint = 0;
	[SerializeField]
	public SuccessText succesText;
	[SerializeField]
	public RippleEffect rippleEffect;

	private AudioSource[] _audioSources;
	private totalPoint _totalPointObj;
	private lifeGageManager lifeGageManager;

	const float MISS = 1.0f;
	const float BAD = 0.9f;
	const float GOOD = 0.8f;
	const float GREAT = 0.6f;
	const float EXCELLENT = 0.4f;

	int excellentPoint;

	void Start(){
		_audioSources = this.gameObject.GetComponents<AudioSource>();
		GameObject totalPointObj = GameObject.FindGameObjectWithTag ("point");
		_totalPointObj = totalPointObj.GetComponent<totalPoint> ();
		GameObject obj = GameObject.FindGameObjectWithTag ("life");
		lifeGageManager = obj.gameObject.GetComponent<lifeGageManager>();
	}

	void OnGUI(){
		//  押されたキーとその時間を取得
		if(Event.current.type == EventType.KeyDown) {
			if (key == Event.current.keyCode) {
				GameObject[] notes = GameObject.FindGameObjectsWithTag ("note");
				checkNotes (notes);
				var obj = Instantiate (rippleEffect);
				obj.transform.position = this.transform.position;
			}

		}
	}

	// このlaneでのポイントを取得
	public int getPoint () {
		return _totalPoint;
	}

	// ノーツがかぶっているかどうかを確認
	private void checkNotes(GameObject[] notes){
		foreach (GameObject note in notes) {
			var noteStartY = note.transform.position.y + note.transform.localScale.y / 1.5f;
			var noteEndY = note.transform.position.y - note.transform.localScale.y / 1.5f;
			var startY = this.transform.position.y + this.transform.localScale.y / 1.5f;
			var endY = this.transform.position.y - this.transform.localScale.y / 1.5f;

			var noteStartX = note.transform.position.x + note.transform.localScale.x / 1.5f;
			var noteEndX = note.transform.position.x - note.transform.localScale.x / 1.5f;
			var startX = this.transform.position.x + this.transform.localScale.x / 1.5f;
			var endX = this.transform.position.x - this.transform.localScale.x / 1.5f;

			if (noteEndX <= startX && noteStartX >= endX) {
				if (noteEndY <= startY && noteStartY >= endY) {
					int point = note.GetComponent<noteScript> ().deleteSelf();
					float diff = System.Math.Abs(this.transform.position.y - note.transform.position.y);
					float maxDiff = note.transform.localScale.y / 1.5f + this.transform.localScale.y / 1.5f;
					float rate = 0;
					int addCombo = 0;
					if (diff / maxDiff <= EXCELLENT) {
						rate = EXCELLENT;
						addCombo = 1;
					} else if (diff / maxDiff <= GREAT) {
						rate = GREAT;
						addCombo = 1;
					} else if (diff / maxDiff <= GOOD) {
						rate = GOOD;
					} else if (diff / maxDiff <= BAD) {
						rate = BAD;
					} else if (diff / maxDiff <= MISS) {
						rate = MISS;
						lifeGageManager.AddDamage (1);
					}
					addPoint (point,rate,addCombo);
					break;
				}
			}
		}
	}

	// 加点
	private void addPoint(float addPoint,float rate,int addCombo){

		GameObject[] textObjs = GameObject.FindGameObjectsWithTag ("successText");
		foreach (var textobj in textObjs) {
			Destroy (textobj);
		}
			
		int sumCombo = _totalPointObj.GetComponent<totalPoint>().AddCombo(addCombo);

		if (rate == EXCELLENT) {
			var obj = Instantiate (succesText);
			obj.GetComponent<SuccessText> ().SetText (sumCombo.ToString() + " combo\n" + "EXCELLENT");
		} else if (rate == GREAT) {
			var obj = Instantiate (succesText);
			obj.GetComponent<SuccessText> ().SetText (sumCombo.ToString() + " combo\n" + "GREAT");
		} else if(rate == GOOD){
			var obj = Instantiate (succesText);
			obj.GetComponent<SuccessText> ().SetText ("GOOD");
		} else if (rate == BAD) {
			var obj = Instantiate (succesText);
			obj.GetComponent<SuccessText> ().SetText ("BAD");
		} else if(rate == MISS){
			var obj = Instantiate (succesText);
			obj.GetComponent<SuccessText> ().SetText ("MISS");
		}
		_totalPoint += (int)(addPoint * 1 - rate);
		_audioSources [addCombo].PlayOneShot (_audioSources [addCombo].clip);
	}

	public int GetExcellentPoint(){
		// めんどいので直打ちで
		return (int)(100 * 1 - EXCELLENT);
	}
}
