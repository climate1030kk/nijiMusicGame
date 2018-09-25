using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessText : MonoBehaviour {

	private string _text;
	private float _a;
	[SerializeField]
	public float speed;

	void Start () {
		_a = this.gameObject.GetComponent<TextMesh> ().color.a;
	}
	
	// Update is called once per frame
	void Update () {
		_a -= speed;
		if (_a <= 0) {
			Destroy (this.gameObject);
		}
		TextMesh textMesh = this.gameObject.GetComponent<TextMesh> ();
		Color color = new Color (textMesh.color.r, textMesh.color.g, textMesh.color.b, _a);
		textMesh.color = color;
	}

	public void SetText(string text){
		_text = text;
		this.gameObject.GetComponent<TextMesh> ().text = _text;
	}
		
	public void deleteSelf(){
		Destroy (this.gameObject);
	}
}
