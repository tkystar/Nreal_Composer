namespace NRKernal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class Metronome : MonoBehaviour
    {
        [SerializeField] private AudioSource _ring;

        public double bpm = 39d; //一分間あたりの打つ回数
        private double _metronomeStartDspTime;
        private double _buffer = 3 / 60d;
        private double _shootDspTime;
        [SerializeField] private float _succesDifferenceTime;
        [SerializeField] private float _evaluationDifferenceTime;
        [SerializeField] private float _startTiming;
        [SerializeField] private SushiDestroy _sushiManager;
        [SerializeField]private NootsManager _nootsManager;
        public String latestState;
        public GameObject pointsTextObj;
        public Text _pointsText;
        public int totalPoints;
        private bool _evaluationNow;
        public double nxtRng;
        private double _pastRng;
        private double _elapsedTime_Since_pstRng;
        private double _remainingTime_Until_nxtRng;
        private double _evaluationPuaseTime;
        public GameObject earlyTextObj;
        private Text _earlyText;
        public GameObject lateTextObj;
        private Text _lateText;
        public GameObject evaluationStateTextObj;
        public double beatInterval;
        [SerializeField] private TimingVisualize _timingVisualize;
        private Text _evaluationStateText;
        public double elapsedDspTime;
        //テスト用
        public GameObject SE;
        private AudioSource test;
        private bool _metronomeValid;
        public ParticleSystem circleParticle_red;
        public ParticleSystem circleParticle_blue;
        public ParticleSystem circleParticle_yellow;
        public ParticleSystem circleParticle_green;
        public ParticleSystem circleParticle_pink;
        public ParticleSystem circleParticle_glay;
        private ParticleSystem _currentParticle;
        private GameObject redeffect;
        private int _combo;
        void Start()
        {
            _pointsText = pointsTextObj.GetComponent<Text>();
            _earlyText = earlyTextObj.GetComponent<Text>();
            _lateText = lateTextObj.GetComponent<Text>();
            _evaluationStateText = evaluationStateTextObj.GetComponent<Text>();
            test = SE.GetComponent<AudioSource>();
            circleParticle_blue.Pause();
            circleParticle_red.Pause();
            circleParticle_yellow.Pause();
            circleParticle_green.Pause();
            circleParticle_pink.Pause();
            circleParticle_glay.Pause();
            _currentParticle = circleParticle_glay;
            _timingVisualize.GetDividedDistance(bpm);
        }

        public void MetronomeStart()
        {
            StartCoroutine(StartBeat());
            ResetUI();
        }

        void ResetUI()
        {
            _earlyText.text = "early";
            _lateText.text = "late";
        }

        void FixedUpdate()
        {
            NootsInstantiateTimig();
            Mistake();
            ShootTest();
        }
        
        double NextRingTime()
        {
            beatInterval = 60d / bpm;
            elapsedDspTime = AudioSettings.dspTime - _metronomeStartDspTime;
            var beats = System.Math.Floor(elapsedDspTime / beatInterval);

            return _metronomeStartDspTime + (beats + 1d) * beatInterval;
        }

        double PastRingTime()
        {
            var beatInterval = 60d / bpm;
            elapsedDspTime = AudioSettings.dspTime - _metronomeStartDspTime;
            var beats = System.Math.Floor(elapsedDspTime / beatInterval) - 1;

            return _metronomeStartDspTime + (beats + 1d) * beatInterval;
        }

        IEnumerator StartBeat()
        {
            elapsedDspTime = 0;
            yield return new WaitForSeconds(_startTiming);
            _metronomeValid = true;
            _metronomeStartDspTime = AudioSettings.dspTime;
        }

        public void EarlyorLate()
        {
            _ring.Play();
            if (!_evaluationNow) return;

            _sushiManager.isBeat = true;
            StartCoroutine(CollisionReset());
            Debug.Log("_sushiDestroy.isBeat" +_sushiManager.isBeat);
            _shootDspTime = AudioSettings.dspTime;
            nxtRng = NextRingTime();
            _pastRng = PastRingTime();
            _elapsedTime_Since_pstRng = _shootDspTime - _pastRng;
            _remainingTime_Until_nxtRng = nxtRng - _shootDspTime;
            
            //打つタイミングが早い場合
            if (_elapsedTime_Since_pstRng > _remainingTime_Until_nxtRng)
            {
                JudgeMent_early();
            }
            else //打つタイミングが遅い場合
            {
                Judgement_late();
            }
        }

        void JudgeMent_early()
        {
            _evaluationPuaseTime = (nxtRng + _evaluationDifferenceTime) - _shootDspTime;
            StartCoroutine(PauseEvaluation(_evaluationPuaseTime));
            var differenceTime = nxtRng - _shootDspTime;

            //判定処理
            if (differenceTime < _succesDifferenceTime)
            {
                _combo ++;
                totalPoints += (int)(1000 * (0.9f+(0.1*_combo)));
                StartCoroutine(PointTextEffect());
                _earlyText.text = "perfect";
                _lateText.text = "perfect";
                StartCoroutine(DeleteLog());
                CircleEffect(true);
            }
            else
            {
                _combo = 0;
                _earlyText.text = "early";
                _lateText.text = "";
                StartCoroutine(DeleteLog());
                CircleEffect(false);
            }
        }

        void Judgement_late()
        {
            _evaluationPuaseTime = (_pastRng + _evaluationDifferenceTime) - _shootDspTime;
            StartCoroutine(PauseEvaluation(_evaluationPuaseTime));
            var differenceTime = _shootDspTime - _pastRng;
            if (differenceTime < _succesDifferenceTime) //成功
            {
                _combo ++;
                totalPoints += (int)(1000 * (0.9f+(0.1*_combo)));
                StartCoroutine(PointTextEffect());
                _earlyText.text = "perfect";
                _lateText.text = "perfect";
                //StartCoroutine(DeleteLog());
                CircleEffect(true);
            }
            else
            {
                _combo = 0;
                _earlyText.text = "";
                _lateText.text = "late";
                //StartCoroutine(DeleteLog());
                CircleEffect(false);
            }
        }

        void NootsInstantiateTimig()
        {
            if (_metronomeValid)
            {
                nxtRng = NextRingTime();

                if (nxtRng < AudioSettings.dspTime + _buffer)
                {
                    //_ring.PlayScheduled(nxtRng);
                    if (elapsedDspTime > 16)
                    {
                        _timingVisualize.CreateNoots(); 
                        _evaluationNow = true;
                    }
                    else
                    {
                        _evaluationNow = false;
                    }
                    
                }

                _evaluationStateText.text = "判定可能 : " + _evaluationNow.ToString();
                
            }

        }
        void CircleEffect(bool _success)
        {
            if (_success)
            {
                circleParticle_blue.Play();
                _currentParticle = circleParticle_blue;
            }
            else
            {
                _currentParticle.Clear();
                _currentParticle.Pause();
            }
            
        }

        void Mistake()
        {
            if (_nootsManager.isMistake)
            {
                CircleEffect(false);
            }
        }

        IEnumerator PauseEvaluation(double _waitTime)
        {
            _evaluationNow = false;
            float _pauseTime = (float) _waitTime;
            yield return new WaitForSeconds(_pauseTime);

            _evaluationNow = true;
        }

        IEnumerator CollisionReset()
        {
            yield return new WaitForSeconds(0.2f);

            _sushiManager.isBeat = false;
        }

        IEnumerator DeleteLog()
        {
            yield return new WaitForSeconds(0.7f);
            _earlyText.text = "";
            _lateText.text = "";
        }

        IEnumerator PointTextEffect()
        {
            _pointsText.text = totalPoints.ToString();
            yield return new WaitForSeconds(0.1f);
            pointsTextObj.transform.localScale = new Vector3(1.1f,1.1f,1.1f);
            yield return new WaitForSeconds(0.1f);
            pointsTextObj.transform.localScale = new Vector3(1f,1f,1f);
            yield return new WaitForSeconds(0.1f);
            pointsTextObj.transform.localScale = new Vector3(1.1f,1.1f,1.1f);
            yield return new WaitForSeconds(0.1f);
            pointsTextObj.transform.localScale = new Vector3(1f,1f,1f);
        }


        void ShootTest()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                EarlyorLate();
                _ring.Play();
            }
        }
        
        
    }
}