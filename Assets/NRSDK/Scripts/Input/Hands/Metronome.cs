using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Metronome : MonoBehaviour
{
    [SerializeField] private AudioSource _ring;

    private double _bpm = 82d;      //一分間あたりの打つ回数
    private double _metronomeStartDspTime;
    private double _buffer = 5 / 60d;
    private double _shootDspTime;
    [SerializeField] private float point1_differenceTime;
    [SerializeField] private float point2_differenceTime;
    [SerializeField] private float point3_differenceTime;
    [SerializeField] private float _startTiming;
    public GameObject scoreTextObj;
    private Text _scoteText;
    void Start() 
    {
        //_metronomeStartDspTime = AudioSettings.dspTime;
        _scoteText = scoreTextObj.GetComponent<Text>();
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

    double PastRingTime() 
    {
        var beatInterval = 60d / _bpm;       ///音の間隔
        var elapsedDspTime = AudioSettings.dspTime - _metronomeStartDspTime;
        var beats = System.Math.Floor(elapsedDspTime / beatInterval) -1;   //打った回数

        return _metronomeStartDspTime + (beats + 1d) * beatInterval;
    }
    IEnumerator StartBeat()
    {
        yield return new WaitForSeconds(_startTiming);
        _metronomeStartDspTime = AudioSettings.dspTime;
    }
    
    public void TimingScoring()
    {
        _shootDspTime = AudioSettings.dspTime;
        var nxtRng = NextRingTime();
        var pastRng = PastRingTime();
        if(_shootDspTime - pastRng > nxtRng-_shootDspTime) //早すぎた
        {
            var differenceTime = nxtRng - _shootDspTime;
            if(differenceTime < point1_differenceTime) //高得点
            {
                Debug.Log("高得点");
                _scoteText.text = "HightScore";
            }
            else if(differenceTime < point2_differenceTime)  //中得点
            {
                Debug.Log("中得点");
                _scoteText.text = "MiddleScore";
            }
            else　//低得点
            {
                Debug.Log("低得点");
                _scoteText.text = "LowScore";
            }
        }
        else　//遅すぎた
        {
            var differenceTime = _shootDspTime - pastRng;
            if (differenceTime < point1_differenceTime) //高得点
            {
                Debug.Log("高得点");
                _scoteText.text = "HightScore";
            }
            else if (differenceTime < point2_differenceTime)  //中得点
            {
                Debug.Log("中得点");
                _scoteText.text = "MiddleScore";
            }
            else　//低得点
            {
                Debug.Log("低得点");
                _scoteText.text = "LowScore";
            }
        }
    }
}
