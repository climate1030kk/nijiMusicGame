using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logo : MonoBehaviour {

	private float i = 0;
	private SpriteRenderer spriteRenderer;

	void Start () {
		spriteRenderer = this.gameObject.GetComponent<SpriteRenderer> ();
	}

	void Update () {
		i+=0.05f;
		spriteRenderer.color = new Color(spriteRenderer.color.r,spriteRenderer.color.g,spriteRenderer.color.b, Mathf.Abs(Mathf.Sin (i)));
	}
}
