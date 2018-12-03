using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSceneScript : MonoBehaviour {

    void OnMouseDown()
    {
         GameObject.FindGameObjectWithTag("selectImage").GetComponent<SelectImageScrpt>().nextScene();
    }
}
