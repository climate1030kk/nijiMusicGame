using DG.Tweening;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class test : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> units = new List<GameObject>();

    private sdMotionScript _sdMotionScript;
    private NotesCreator _notesCreator;
    private bool _isClimax = false;

    private void Start()
    {
        _sdMotionScript = GetComponent<sdMotionScript>();
        _notesCreator = GameObject.FindGameObjectWithTag("Respawn").GetComponent<NotesCreator>();
    }

    private void Update()
    {
        if (_notesCreator.isEnd())
        {
            return;
        }

        // 小節に来たフレームで true になる
        if (Music.IsJustChangedBar())
        {
            if (Music.Just.Bar % Music.rate == 0)
            {
                DOTween
                .To(value => OnRotate(value), 0, 1, 0.5f)
                .SetEase(Ease.OutCubic)
                .OnComplete(() => transform.localEulerAngles = new Vector3(0, 0, 0))
            ;

                if (_isClimax)
                {
                    _sdMotionScript.Jump();
                }
            }
        }
        // 拍に来たフレームで true になる
        if (Music.IsJustChangedBeat())
        {
            if ((Music.Just.Bar * 4 + Music.Just.Beat + 1) % Music.rate == 0)
            {
                DOTween
                    .To(value => OnScale(value), 0, 1, 0.1f)
                    .SetEase(Ease.InQuad)
                    .SetLoops(2, LoopType.Yoyo)
                ;
            }
        }

        // 指定した小節、拍、ユニットに来たフレームで true になる
        if (Music.IsJustChangedAt((int)(SelectSceneManager.climaxBar * Music.rate), 0, 0) && _isClimax == false)
        {
            setClimax(true);
            foreach (var unit in units)
            {
                GameObject obj = Instantiate(unit);
                obj.GetComponent<test>().setClimax(_isClimax);
            }
        }
    }

    private void OnScale(float value)
    {
        var scale = Mathf.Lerp(1, 1.2f, value);
        transform.localScale = new Vector3(scale * 0.4f, scale * 0.4f, scale * 0.4f);
    }

    private void OnRotate(float value)
    {
        var rot = transform.localEulerAngles;
        rot.y = Mathf.Lerp(0, 360, value);
        transform.localEulerAngles = rot;
    }

    public void setClimax(bool isClimax_)
    {
        _isClimax = isClimax_;
    }
}