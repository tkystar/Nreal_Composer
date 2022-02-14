using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    [SerializeField] private AudioSource _ring;

    private double _bpm = 82d;      //一分間あたりの打つ回数
    private double _metronomeStartDspTime;
    private double _buffer = 5 / 60d;
    [SerializeField] private float _startTiming;
    void Start() 
    {
        //_metronomeStartDspTime = AudioSettings.dspTime;
    }

    void FixedUpdate() 
    {
        var nxtRng = NextRingTime();

        if (nxtRng < AudioSettings.dspTime + _buffer) _ring.PlayScheduled(nxtRng);
        
    }

    double NextRingTime() 
    {
        var beatInterval = 60d / _bpm;       ///音の間隔
        var elapsedDspTime = AudioSettings.dspTime - _metronomeStartDspTime;
        var beats = System.Math.Floor(elapsedDspTime / beatInterval);   //打った回数

        return _metronomeStartDspTime + (beats + 1d) * beatInterval;
    }

    IEnumerator StartBeat()
    {
        yield return new WaitForSeconds(_startTiming);
        _metronomeStartDspTime = AudioSettings.dspTime;
    }
}
