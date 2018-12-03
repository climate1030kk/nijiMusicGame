using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SelectImageScrpt : MonoBehaviour {
	[SerializeField]
	public int index;

	private Vector3 _arrivePos;
	private Vector3 _posSpeed;

	private Vector3 _arriveScale;
	private Vector3 _scaleSpeed;
	private float _arriveTime = 1.0f;

	private SelectSceneManager _sceneManager;

	private AudioSource _audioSources;
    private AudioSource _previewAudioSource;

    public List<Sprite> imageSprites = new List<Sprite>();
    public List<Sprite> infoSprites = new List<Sprite>();
    public List<float> bpms = new List<float>();
    public List<int> climaxBars = new List<int>();

    private SpriteRenderer _imageSpriteRenderer;
    private SpriteRenderer _infoSpriteRenderer;

    void Start(){
        _imageSpriteRenderer = this.transform.Find("selectImage").transform.gameObject.GetComponent<SpriteRenderer>();
        _infoSpriteRenderer = this.transform.Find("selectInfo").transform.gameObject.GetComponent<SpriteRenderer>();

        GameObject obj = GameObject.FindGameObjectWithTag("sceneManager");
        _sceneManager = obj.GetComponent<SelectSceneManager>();
        _sceneManager.setStageNumber(index + 1);
        _sceneManager.setBpm(bpms[index]);
        _sceneManager.setClimaxBar(climaxBars[index]);
        _sceneManager.setDifficulyLevel(1);

        _previewAudioSource = this.gameObject.GetComponents<AudioSource>()[1];
        _previewAudioSource.clip = Resources.Load("mainGame/preview_" + (index + 1)) as AudioClip;
        _previewAudioSource.Play();
        _previewAudioSource.loop = true;
    }

    public void setIndex(int index_)
    {
        index_ = imageSprites.Count <= index_ ? 0 : index_;
        index_ = 0 > index_ ? imageSprites.Count -  1 : index_;
        index = index_;
        UpdateSprite();

        _previewAudioSource.Stop();
        _previewAudioSource.clip = Resources.Load("mainGame/preview_" + (index + 1)) as AudioClip;
        _previewAudioSource.Play();
        _previewAudioSource.loop = true;
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

	public void nextScene(){
		_audioSources = this.gameObject.GetComponents<AudioSource> ()[0];
		_audioSources.PlayOneShot (_audioSources.clip);
       
        _sceneManager.setStageNumber(index + 1);
        _sceneManager.setBpm(bpms[index]);
        _sceneManager.setClimaxBar(climaxBars[index]);
        _sceneManager.nextScene ();
	}

    private void UpdateSprite()
    {
        _imageSpriteRenderer.sprite = imageSprites[index];
        _infoSpriteRenderer.sprite = infoSprites[index];
    }
}
