﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noteScript : MonoBehaviour
{

    [SerializeField]
    public int point = 100;
    public float arriveTime;
    private Vector2 _arrivalLength;
    private Vector2 _lanePos;
    private float _maxScale;

    [SerializeField]
    public Sprite syncSprite;

    private lifeGageManager lifeGageManager;
    private NotesCreator _notesCreator;

    private totalPoint _totalPointObj;

    [SerializeField]
    public SuccessText succesText;

    public Vector2 LanePos
    {
        get
        {
            return _lanePos;
        }
    }

    void Start()
    {
        GameObject csvReaderObj = GameObject.FindGameObjectWithTag("Respawn");
        _notesCreator = csvReaderObj.GetComponent<NotesCreator>();
        arriveTime = _notesCreator.GetArriveTime();
        _maxScale = this.transform.localScale.x;
        this.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);

        GameObject obj = GameObject.FindGameObjectWithTag("life");
        lifeGageManager = obj.gameObject.GetComponent<lifeGageManager>();

        GameObject totalPointObj = GameObject.FindGameObjectWithTag("point");
        _totalPointObj = totalPointObj.GetComponent<totalPoint>();
    }

    void Update()
    {

        if (!_notesCreator.isPlaying())
        {
            return;
        }
        // 指定位置より下に移動したら削除
        //		if (this.transform.position.y <= _END_POS_Y) {
        //			lifeGageManager.AddDamage (1);
        //			deleteSelf ();
        //		}
        float deltaTime = Time.deltaTime;
        Vector3 pos = new Vector3(
            _arrivalLength.x / arriveTime * deltaTime * -1,
            _arrivalLength.y / arriveTime * deltaTime * -1,
            0
        );
        this.transform.position += pos;

        if (this.transform.localScale.x < _maxScale)
        {
            float addScale = _maxScale / arriveTime * deltaTime;
            this.transform.localScale += new Vector3(addScale, addScale, addScale);
        }

        // レーンとの距離をはかる
        if (this.transform.position.y < LanePos.y)
        {
            float length = Mathf.Pow((LanePos.x - this.transform.position.x) * (LanePos.x - this.transform.position.x) + (LanePos.y - this.transform.position.y) * (LanePos.y - this.transform.position.y), 0.5f);
            if (length > this.transform.localScale.x)
            {
                lifeGageManager.AddDamage(1);
                _totalPointObj.AddCombo(0);
                var obj = Instantiate(succesText);
                obj.GetComponent<SuccessText>().SetText("MISS");
                deleteSelf();
            }
        }
    }

    // 自壊して獲得ポイントを返す
    public int deleteSelf()
    {
        Destroy(this.gameObject);
        return point;
    }

    // 生成後にLaneを引数いれて呼ぶ
    public void init(GameObject obj, float setArriveTime)
    {
        float arrivalLengthX = this.transform.position.x - obj.transform.position.x;
        float arrivalLengthY = this.transform.position.y - obj.transform.position.y;
        _arrivalLength = new Vector2(arrivalLengthX, arrivalLengthY);
        arriveTime = setArriveTime;
        _lanePos = new Vector2(obj.transform.position.x, obj.transform.position.y);
    }

    // 同時押しの場合は画像を変える
    public void setSyncSprit()
    {
        this.transform.GetComponentInChildren<SpriteRenderer>().sprite = syncSprite;
    }
}
