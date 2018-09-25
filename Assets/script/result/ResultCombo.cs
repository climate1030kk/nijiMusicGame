using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultCombo : MonoBehaviour {

	private int _comboMax;

	void Awake(){
		_comboMax = totalPoint.comboMax;
	}

	void Start () {
		TextMesh textMesh = this.gameObject.GetComponent<TextMesh> ();
		textMesh.text = _comboMax.ToString();
	}
}
