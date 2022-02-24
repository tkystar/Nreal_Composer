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
        [SerializeField] private SushiDestroy _sushiDestroy;
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
        public GameObject hanteiTextObj;
        private Text _hanteiText;
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
            _evaluationNow = true;
            _pointsText = pointsTextObj.GetComponent<Text>();
            _hanteiText = hanteiTextObj.GetComponent<Text>();
            _evaluationStateText = evaluationStateTextObj.GetComponent<Text>();
            test = SE.GetComponent<AudioSource>();
            circleParticle_blue.Pause();
            circleParticle_red.Pause();
            circleParticle_yellow.Pause();
            circleParticle_green.Pause();
            circleParticle_pink.Pause();
            circleParticle_glay.Pause();
            _currentParticle = circleParticle_glay;
        }

        public void MetronomeStart()
        {
            StartCoroutine(StartBeat());
        }

        void FixedUpdate()
        {
            if (_metronomeValid)
            {
                nxtRng = NextRingTime();

                if (nxtRng < AudioSettings.dspTime + _buffer)
                {
                    _ring.PlayScheduled(nxtRng);
                    if(elapsedDspTime > 16)
                    _timingVisualize.CreateNoots();
                }

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
            yield return new WaitForSeconds(_startTiming);
            _metronomeValid = true;
            _metronomeStartDspTime = AudioSettings.dspTime;
        }

        public void TrueorFalse()
        {
            Debug.Log("trueorfalse");
            if (!_evaluationNow) return;

            _sushiDestroy.isBeat = true;
            Debug.Log("_sushiDestroy.isBeat" +_sushiDestroy.isBeat);
            _shootDspTime = AudioSettings.dspTime;
            nxtRng = NextRingTime();
            _pastRng = PastRingTime();
            _elapsedTime_Since_pstRng = _shootDspTime - _pastRng;
            _remainingTime_Until_nxtRng = nxtRng - _shootDspTime;
            //打つタイミングが早い場合
            if (_elapsedTime_Since_pstRng > _remainingTime_Until_nxtRng)
            {
                _evaluationPuaseTime = (nxtRng + _evaluationDifferenceTime) - _shootDspTime;
                StartCoroutine(PauseEvaluation(_evaluationPuaseTime));
                var differenceTime = nxtRng - _shootDspTime;

                //判定処理
                if (differenceTime < _succesDifferenceTime)
                {
                    totalPoints += 1000;
                    _combo ++;
                    _pointsText.text = totalPoints.ToString();
                    _hanteiText.text = "ナイス";
                    StartCoroutine(DeleteLog());
                    CircleEffect(true);
                }
                else
                {
                    _combo = 0;
                    _hanteiText.text = "はやい";
                    StartCoroutine(DeleteLog());
                    CircleEffect(false);
                }

            }
            else //打つタイミングが遅い場合
            {
                _evaluationPuaseTime = (_pastRng + _evaluationDifferenceTime) - _shootDspTime;
                StartCoroutine(PauseEvaluation(_evaluationPuaseTime));
                var differenceTime = _shootDspTime - _pastRng;
                if (differenceTime < _succesDifferenceTime) //成功
                {
                    totalPoints += 1000;
                    _combo ++;
                    _pointsText.text = totalPoints.ToString();
                    _hanteiText.text = "ナイス";
                    //StartCoroutine(DeleteLog());
                    CircleEffect(true);
                }
                else
                {
                    _combo = 0;
                    _hanteiText.text = "おそい";
                    //StartCoroutine(DeleteLog());
                    CircleEffect(false);
                }

            }
        }

        void CircleEffect(bool _success)
        {
            if (_success)
            {
                
                switch (_combo % 6)
                {
                    case 1 :
                        circleParticle_glay.Play();
                        _currentParticle = circleParticle_glay;
                        break;
                    case 2:
                        _currentParticle.Clear();
                        _currentParticle.Pause();
                        circleParticle_green.Play();
                        _currentParticle = circleParticle_green;
                        break;
                    case 3 :
                        _currentParticle.Clear();
                        _currentParticle.Pause();
                        circleParticle_yellow.Play();
                        _currentParticle = circleParticle_yellow;
                        break;
                    case 4:
                        _currentParticle.Clear();
                        _currentParticle.Pause();
                        circleParticle_pink.Play();
                        _currentParticle = circleParticle_pink;
                        break;
                    case 5 :
                        _currentParticle.Clear();
                        _currentParticle.Pause();
                        circleParticle_blue.Play();
                        _currentParticle = circleParticle_blue;
                        break;
                    case 0:
                        _currentParticle.Clear();
                        _currentParticle.Pause();
                        circleParticle_red.Play();
                        _currentParticle = circleParticle_red;
                        break;
                }
            }
            else
            {
                _currentParticle.Clear();
                _currentParticle.Pause();
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
}