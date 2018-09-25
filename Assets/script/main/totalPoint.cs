using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class totalPoint : MonoBehaviour {

	static public int pointSum = 0;
	static public int comboMax = 0;
	private int _comboSum = 0;

	void Awake(){
		pointSum = 0;
		comboMax = 0;
	}

	void Start () {
		this.gameObject.GetComponent<TextMesh>().text = "0";
	}

	void Update () {
		var point = 0;
		GameObject[] lanes = GameObject.FindGameObjectsWithTag ("lane");
		foreach (GameObject lane in lanes) {
			point += lane.GetComponent<laneScript>().getPoint ();
		}
		TextMesh textMesh = this.gameObject.GetComponent<TextMesh> ();
		textMesh.text = point.ToString();

		pointSum = point;

		if (comboMax < _comboSum) {
			comboMax = _comboSum;
		}
	}

	public int AddCombo(int addCombo){
		if (addCombo > 0) {
			_comboSum += addCombo;
		} else {
			_comboSum = 0;
		}
		return _comboSum;
	}

	public int GetCombo(){
		return _comboSum;
	}

	public int GetTotalPoint(){
		return pointSum;
	}
}
