using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPoint : MonoBehaviour {

	private int _pointSum;

	void Awake(){
		_pointSum = totalPoint.pointSum;
	}

	void Start () {
		TextMesh textMesh = this.gameObject.GetComponent<TextMesh> ();
		textMesh.text = _pointSum.ToString();
	}
}
