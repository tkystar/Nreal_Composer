using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetronomeTest : MonoBehaviour
{
    [SerializeField] AudioSource _ring;

    double _bpm = 39d;
    double _metronomeStartDspTime;
    double _buffer = 5 / 60d;
    private Text _nxtringTimeDebugText;
    public GameObject _nxtringTimeDebugTextObject;
    private Text _pstringTimeDebugText;
    public GameObject _pstringTimeDebugTextObject;
    private Text _currentTimeDebugText;
    public GameObject _currentTimeDebugTextObject;

    void Start() {
        _metronomeStartDspTime = AudioSettings.dspTime;
        _pstringTimeDebugText = _pstringTimeDebugTextObject.GetComponent<Text>();
        _nxtringTimeDebugText = _nxtringTimeDebugTextObject.GetComponent<Text>();
        _currentTimeDebugText = _currentTimeDebugTextObject.GetComponent<Text>();

    }

    void FixedUpdate() {
        var nxtRng = NextRingTime();
        var pstRng = PstRingTime();
        _nxtringTimeDebugText.text = nxtRng.ToString();
        _currentTimeDebugText.text = AudioSettings.dspTime.ToString();
        _pstringTimeDebugText.text = pstRng.ToString();
        if (nxtRng < AudioSettings.dspTime + _buffer) {
            _ring.PlayScheduled(nxtRng);
        }
    }

    double NextRingTime() {
        var beatInterval = 60d / _bpm;
        var elapsedDspTime = AudioSettings.dspTime - _metronomeStartDspTime;
        var beats = System.Math.Floor(elapsedDspTime / beatInterval);

        return _metronomeStartDspTime + (beats + 1d) * beatInterval;
    }
    double PstRingTime() {
        var beatInterval = 60d / _bpm;
        var elapsedDspTime = AudioSettings.dspTime - _metronomeStartDspTime;
        var beats = System.Math.Floor(elapsedDspTime / beatInterval) - 1;

        return _metronomeStartDspTime + (beats + 1d) * beatInterval;
    }
}
