using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesTimingMaker : MonoBehaviour
{

    private AudioSource _audioSource;
    private CSVWriter _CSVWriter;

    private bool _isPlaying = false;
    public GameObject startButton;

    private List<KeyCode> _keys = new List<KeyCode>();
    private List<float> _keyTimes = new List<float>();
    private List<NoteType> _noteTypes = new List<NoteType>();

    private Dictionary<KeyCode, bool> _untreatedKeys = new Dictionary<KeyCode, bool>();

    // ロングノーツの最小時間
    private const float LONG_MIN_TIME = 0.3f;
    enum NoteType
    {
        NORMAL,
        LONG_START,
        LONG_END
    }

    //private float _preMax = 0;
    private float _intv = 0;
    private const float INTERVAL = 0.1f;
    private const float MINIMUMDIFFERENCE = 2f;

    void Start()
    {
        string _musicName = "audio_" + SelectSceneManager.stageNumber.ToString();
        AudioClip audioClip = Resources.Load("mainGame/" + _musicName) as AudioClip;
        _audioSource = GameObject.Find("GameMusic").GetComponent<AudioSource>();
        _audioSource.clip = audioClip;
        _audioSource.Play();
        _isPlaying = true;
        //_CSVWriter = GameObject.Find("CSVWriter").GetComponent<CSVWriter>();
    }

    void Update()
    {
        if (_isPlaying)
        {
            DetectKeys();
            //AutoMakeNotes();
        }
    }

    void OnGUI()
    {
        //  押されたキーとその時間を保存
        if (Event.current.type == EventType.KeyDown)
        {
            if (_untreatedKeys.ContainsKey(Event.current.keyCode) && !_untreatedKeys[Event.current.keyCode])
            {
                _keys.Add(Event.current.keyCode);
                _keyTimes.Add(_audioSource.time);
                _noteTypes.Add(NoteType.NORMAL);

                if (_untreatedKeys.ContainsKey(Event.current.keyCode))
                {
                    _untreatedKeys[Event.current.keyCode] = true;
                }
                else
                {
                    _untreatedKeys.Add(Event.current.keyCode, true);
                }
            }
        }

        if (Event.current.type == EventType.KeyUp)
        {
            _untreatedKeys[Event.current.keyCode] = false;

            int i = 0;
            foreach (float keyTime in _keyTimes)
            {
                if (_keys[i] == Event.current.keyCode && keyTime > LONG_MIN_TIME && _noteTypes[i] == NoteType.NORMAL)
                {
                    // スタートに変更
                    _noteTypes[i] = NoteType.LONG_START;

                    // ロングノーツの終了地点を設定
                    _keys.Add(Event.current.keyCode);
                    _keyTimes.Add(_audioSource.time);
                    _noteTypes.Add(NoteType.LONG_END);
                }
                i++;
            }
        }
    }

    public void StartMusic()
    {
        startButton.SetActive(false);
         _audioSource.Play();
        _isPlaying = true;
    }

    void DetectKeys()
    {
        int i = 0;
        bool skip = false;
        foreach (KeyCode key in _keys)
        {
            float time = _keyTimes[i];
            // ロングノーツになるかノーマルノーツか判定がつかないものがあるのでスルー
            if (time <= LONG_MIN_TIME)
            {
                skip = true;
                break;
            }
        }

        // スルー判定ではない場合保存処理
        if (!skip)
        {
            foreach (KeyCode key in _keys)
            {
                float time = _keyTimes[i];
                NoteType noteType = _noteTypes[i];
                if (key == KeyCode.D || key == KeyCode.F || key == KeyCode.J || key == KeyCode.K)
                {
                    WriteNotesTiming(time, key, noteType);
                }
                ++i;
            }
            // 処理済みなので初期化
            _keys = new List<KeyCode>();
            _keyTimes = new List<float>();
            _noteTypes = new List<NoteType>();
        }
    }

    void WriteNotesTiming(float time, KeyCode num, NoteType keyType)
    {
        _CSVWriter.WriteCSV(time.ToString() + "," + num.ToString() + "," + keyType.ToString());
    }

    /*
    void AutoMakeNotes()
    {
        _intv -= Time.deltaTime;

        float[] spectrum = new float[1024];
        _audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);
        // 最も大きい音を取得
        float nowMax = 0;
        for (int i = 1; i < spectrum.Length - 1; i++)
        {
            if (spectrum[i] >= nowMax)
            {
                nowMax = spectrum[i];
            }
        }
        nowMax *= 100;

        if (nowMax >= _preMax * MINIMUMDIFFERENCE && _intv <= 0)
        {
            WriteNotesTiming(_audioSource.time, KeyCode.D);
            _intv = INTERVAL;
            Debug.Log(_audioSource.time);
        }

        _preMax = nowMax;
    }
    */
}