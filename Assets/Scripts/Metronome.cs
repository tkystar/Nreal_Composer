using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    [SerializeField] private AudioSource _ring;

    private double _bpm = 82d;
    private double _metronomeStartDspTime;
    private double _buffer = 2 / 60d;

    void Start() 
    {
        _metronomeStartDspTime = AudioSettings.dspTime;
    }

    void FixedUpdate() 
    {
        var nxtRng = NextRingTime();

        if (nxtRng < AudioSettings.dspTime + _buffer) _ring.PlayScheduled(nxtRng);
        
    }

    double NextRingTime() 
    {
        var beatInterval = 60d / _bpm;
        var elapsedDspTime = AudioSettings.dspTime - _metronomeStartDspTime;
        var beats = System.Math.Floor(elapsedDspTime / beatInterval);

        return _metronomeStartDspTime + (beats + 1d) * beatInterval;
    }
}
