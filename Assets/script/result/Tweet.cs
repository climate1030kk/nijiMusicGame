using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweet : MonoBehaviour {

	void OnMouseDown(){
		Application.OpenURL("http://twitter.com/intent/tweet?text=" + WWW.EscapeURL("テキスト #hashtag"));
	}
}
