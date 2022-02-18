using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Metronome : MonoBehaviour
{
    [SerializeField] private AudioSource _ring;

    private double _bpm = 39d;      //一分間あたりの打つ回数
    private double _metronomeStartDspTime;
    private double _buffer = 5 / 60d;
    private double _shootDspTime;
    [SerializeField] private float point1_differenceTime;
    [SerializeField] private float point2_differenceTime;
    [SerializeField] private float point3_differenceTime;
    [SerializeField] private float _succesDifferenceTime;
    [SerializeField] private float _startTiming;
    public GameObject scoreTextObj;
    private Text _scoteText;
    public String latestState;
    public GameObject pointsTextObj;
    private Text _pointsText;
    [SerializeField] private int _totalPoints;
    void Start() 
    {
        //_metronomeStartDspTime = AudioSettings.dspTime;
        _scoteText = scoreTextObj.GetComponent<Text>();
        _pointsText = pointsTextObj.GetComponent<Text>();
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
                _scoteText.text = "Perfect";
                latestState = "Perfect";
            }
            else if(differenceTime < point2_differenceTime)  //中得点
            {
                Debug.Log("中得点");
                _scoteText.text = "Good";
                latestState = "Good";
            }
            else　//低得点
            {
                Debug.Log("低得点");
                _scoteText.text = "Soso";
                latestState = "Soso";
            }
        }
        else　//遅すぎた
        {
            var differenceTime = _shootDspTime - pastRng;
            if (differenceTime < point1_differenceTime) //高得点
            {
                Debug.Log("高得点");
                _scoteText.text = "Perfect";
                latestState = "Perfect";
            }
            else if (differenceTime < point2_differenceTime)  //中得点
            {
                Debug.Log("中得点");
                _scoteText.text = "Good";
                latestState = "Good";
            }
            else　//低得点
            {
                Debug.Log("低得点");
                _scoteText.text = "Soso";
                latestState = "Soso";
            }
        }
    }
    
    
    public void TrueorFalse()
    {
        Debug.Log("trueorfalse");
        _shootDspTime = AudioSettings.dspTime;
        var nxtRng = NextRingTime();
        var pastRng = PastRingTime();
        if(_shootDspTime - pastRng > nxtRng - _shootDspTime) //打つタイミングが早い場合
        {
            var differenceTime = nxtRng - _shootDspTime;
            if(differenceTime < _succesDifferenceTime)  //成功
            {
                _totalPoints++;
                _pointsText.text = _totalPoints.ToString();
                
            }
            
        }
        else　//打つタイミングが遅い場合
        {
            var differenceTime = _shootDspTime - pastRng;
           if (differenceTime < _succesDifferenceTime)  //成功
            {
                _totalPoints++;
                _pointsText.text = _totalPoints.ToString();
            }
            
        }
    }
}
