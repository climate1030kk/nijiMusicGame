using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopScript : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField]
    GameObject pauseMenu;

    private void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("Respawn").GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            pauseMenu.SetActive(true);
        } else
        {
            Restart();
        }
    }

    public void Restart()
    {
        audioSource.Play();
        pauseMenu.SetActive(false);
    }
}
