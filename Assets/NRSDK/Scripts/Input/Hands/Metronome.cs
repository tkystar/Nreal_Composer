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
    private double _buffer = 3/ 60d;
    private double _shootDspTime;
    [SerializeField] private float _succesDifferenceTime;
    [SerializeField] private float _evaluationDifferenceTime;
    [SerializeField] private float _startTiming;
    public String latestState;
    public GameObject pointsTextObj;
    private Text _pointsText;
    public int totalPoints;
    private bool _evaluationNow;
    private double _nxtRng;
    private double _pastRng;
    private double _elapsedTime_Since_pstRng;
    private double _remainingTime_Until_nxtRng;
    private double _evaluationPuaseTime;
    public GameObject hanteiTextObj;
    private Text _hanteiText;
    public GameObject evaluationStateTextObj;
    private Text _evaluationStateText;
    //テスト用
    public GameObject SE;
    private AudioSource test;
    private bool _metronomeValid;
    void Start()
    {
        _evaluationNow = true;
        _pointsText = pointsTextObj.GetComponent<Text>();
        _hanteiText = hanteiTextObj.GetComponent<Text>();
        _evaluationStateText = evaluationStateTextObj.GetComponent<Text>();
        test = SE.GetComponent<AudioSource>();
    }

    public void MetronomeStart()
    {
        StartCoroutine(StartBeat());
    }

    void FixedUpdate() 
    {
        if (_metronomeValid)
        {
            _nxtRng = NextRingTime();
            
            if (_nxtRng < AudioSettings.dspTime + _buffer) _ring.PlayScheduled(_nxtRng);
                    
            _evaluationStateText.text = "判定可能 : " + _evaluationNow.ToString();
        }
        
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            TrueorFalse();
            test.Play();
        }
    }

    double NextRingTime() 
    {
        var beatInterval = 60d / _bpm;       
        var elapsedDspTime = AudioSettings.dspTime - _metronomeStartDspTime;
        var beats = System.Math.Floor(elapsedDspTime / beatInterval);   

        return _metronomeStartDspTime + (beats + 1d) * beatInterval;
    }

    double PastRingTime() 
    {
        var beatInterval = 60d / _bpm;      
        var elapsedDspTime = AudioSettings.dspTime - _metronomeStartDspTime;
        var beats = System.Math.Floor(elapsedDspTime / beatInterval) -1;  

        return _metronomeStartDspTime + (beats + 1d) * beatInterval;
    }
    IEnumerator StartBeat()
    {
        yield return new WaitForSeconds(_startTiming);
        _metronomeValid = true;
        _metronomeStartDspTime = AudioSettings.dspTime;
    }
    
    public void TrueorFalse()
    {
        Debug.Log("trueorfalse");
        if (!_evaluationNow) return;
        
        _shootDspTime = AudioSettings.dspTime;
        _nxtRng = NextRingTime();
        _pastRng = PastRingTime();
        _elapsedTime_Since_pstRng = _shootDspTime - _pastRng;
        _remainingTime_Until_nxtRng = _nxtRng - _shootDspTime;
        //打つタイミングが早い場合
        if(_elapsedTime_Since_pstRng > _remainingTime_Until_nxtRng) 
        {
            _evaluationPuaseTime = (_nxtRng + _evaluationDifferenceTime) - _shootDspTime;
            StartCoroutine(PauseEvaluation(_evaluationPuaseTime));
            var differenceTime = _nxtRng - _shootDspTime;
            
            //判定処理
            if(differenceTime < _succesDifferenceTime)  
            {
                totalPoints ++;
                _pointsText.text = totalPoints.ToString();
                _hanteiText.text = "ナイス";
                StartCoroutine(DeleteLog());
            }
            else
            {
                _hanteiText.text = "はやい";
                StartCoroutine(DeleteLog());
            }
            
        }
        else　//打つタイミングが遅い場合
        {
            _evaluationPuaseTime = (_pastRng + _evaluationDifferenceTime) - _shootDspTime;
            StartCoroutine(PauseEvaluation(_evaluationPuaseTime));
            var differenceTime = _shootDspTime - _pastRng;
            if (differenceTime < _succesDifferenceTime)  //成功
            {
                totalPoints ++;
                _pointsText.text = totalPoints.ToString();
                _hanteiText.text = "ナイス";
                //StartCoroutine(DeleteLog());
            }
           else
           {
               _hanteiText.text = "おそい";
               //StartCoroutine(DeleteLog());
           }
            
        }
    }

    IEnumerator PauseEvaluation(double _waitTime)
    {
        _evaluationNow = false;
        float _pauseTime = (float) _waitTime;
        yield return new WaitForSeconds(_pauseTime);
        _evaluationNow = true;
    }
    
    IEnumerator DeleteLog()
    {
        yield return new WaitForSeconds(0.2f);
        _hanteiText.text = " ";
    }
}
