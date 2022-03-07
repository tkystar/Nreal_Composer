//using UnityEditor.Search;

using System.Configuration;
using System.Diagnostics;

namespace NRKernal
{

    using System.Collections;
    using System.Collections.Generic;
    using NRKernal;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    public class TransitionManager : MonoBehaviour
    {
        public AudioSource mainSound;
        public GameObject soundManager;
        public AudioClip startBGM;
        public AudioClip resultBGM;
        public AudioClip playBGM;
        public AudioSource applause;
        public Button GotoPlaymodeButton;
        public Button ReplayButton;
        public Button GotoExplainModeButton;
        public Button NextExplainButton;
        public Button FinishAppButton;
        public Button GotoStartModeButtonfromExplainMode;
        public Button PinchGestureConfirmButton;
        public Button BothHandsConfirmButton;
        public GameObject scoreUI;
        public GameObject resultTextObj;
        public GameObject inGameUI;
        public GameObject startUI;
        public GameObject startMainUI;
        public GameObject bothHandsUI;
        public GameObject pintchUI;
        public GameObject explainUI;
        public GameObject explainUI_part1;
        public GameObject explainUI_part2;
        public GameObject returnBtn;
        public GameObject replayBtn;
        private Text _tesultText;
        public GameObject lazer_R;
        public GameObject lazer_L;
        public GameObject target;
        [SerializeField] private Metronome _metronome;
        [SerializeField] private CollisionManager _collisionManager;
        public ParticleSystem circleParticle;
        private bool isGaming;
        

        // Start is called before the first frame update
        void Start()
        {
            _metronome = soundManager.GetComponent<Metronome>();
            mainSound = soundManager.GetComponent<AudioSource>();
            mainSound.clip = startBGM;
            ButtonMethodMapping();
            StartActiveSetting();
            _tesultText = resultTextObj.GetComponent<Text>();
            StartCoroutine(StartDelay());
            StartCoroutine(GameStartBGM());
        }

        void StartActiveSetting()
        {
            pintchUI.SetActive(true);
            bothHandsUI.SetActive(false);
            scoreUI.SetActive(false);
            inGameUI.SetActive(false);
            startUI.SetActive(true);
            startMainUI.SetActive(false);
            target.SetActive(false);
            explainUI.SetActive(false);
            mainSound.loop = true;
            target.SetActive(false);
            lazer_L.SetActive(true);
            lazer_R.SetActive(true);
        }

        void ButtonMethodMapping()
        {
            GotoPlaymodeButton.onClick.AddListener(()=>PlayMode());
            ReplayButton.onClick.AddListener(()=>PlayMode());
            GotoExplainModeButton.onClick.AddListener(()=>GotoExplainMode());
            NextExplainButton.onClick.AddListener(()=>GotoNextExplain());
            FinishAppButton.onClick.AddListener(()=>ReLoad());
            GotoStartModeButtonfromExplainMode.onClick.AddListener(()=>StartMain());
            PinchGestureConfirmButton.onClick.AddListener(()=> DisplayPrepareInfo());
            BothHandsConfirmButton.onClick.AddListener(()=>StartMain());

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //Editrでのテスト用
            if (Input.GetKeyDown(KeyCode.B)) DisplayPrepareInfo();
            if (Input.GetKeyDown(KeyCode.P)) PlayMode();
            if (Input.GetKeyDown(KeyCode.Space)) GameStart();
            if (Input.GetKeyDown(KeyCode.R)) ReLoad();
            if (Input.GetKeyDown(KeyCode.S)) StartMain();
            if (Input.GetKeyDown(KeyCode.E)) GotoExplainMode();
            if (Input.GetKeyDown(KeyCode.N)) GotoNextExplain();

            //if(NRInput.IsTouching()) GameStart();
            //if(NRInput.GetButton(ControllerButton.TRIGGER)) GameStart();

            if (!mainSound.isPlaying && isGaming)
            {
                GameFinish();
            }
        }
        
        private void PlayMode()
        {
            startUI.SetActive(false);
            startMainUI.SetActive(false);
            inGameUI.SetActive(true);
            scoreUI.SetActive(false);
            target.SetActive(true);
            GameStart();

        }

        private void DisplayPrepareInfo()
        {
            pintchUI.SetActive(false);
            bothHandsUI.SetActive(true);
        }
        private void DisplayPrepareInfo2()
        {
            
        }
        private void　GotoExplainMode()
        {
            startUI.SetActive(false);
            explainUI.SetActive(true);
            explainUI_part1.SetActive(true);
            explainUI_part2.SetActive(false);
        }
        private void　GotoNextExplain()
        {
            explainUI_part1.SetActive(false);
            explainUI_part2.SetActive(true);
        }

        private void GameStart()
        {
            isGaming = true;
            _metronome.totalPoints = 0;
            _metronome._pointsText.text = "";
            mainSound.clip = playBGM;
            mainSound.Play();
            _metronome.enabled = true;
            _metronome.MetronomeStart();
            inGameUI.SetActive(true);
            target.SetActive(true);
            mainSound.loop = false;
            lazer_L.SetActive(false);
            lazer_R.SetActive(false);

        }

        IEnumerator StartDelay()
        {
            yield return new WaitForSeconds(2);
            //GameStart();
        }

        private void GameFinish()
        {
            isGaming = false;
            _metronome.enabled = false;
            lazer_L.SetActive(true);
            lazer_R.SetActive(true);
            StartCoroutine(DisplayResult());
            
        }

        private void StartMain()
        {
            startUI.SetActive(true);
            pintchUI.SetActive(false);
            bothHandsUI.SetActive(false);
            explainUI.SetActive(false);
            startMainUI.SetActive(true);

        }

        private void ReLoad()
        {
            startUI.SetActive(true);
            pintchUI.SetActive(true);
            scoreUI.SetActive(false);
            StartCoroutine(GameStartBGM());
        }

        IEnumerator DisplayResult()
        {
            yield return new WaitForSeconds(1);
            circleParticle.Clear();
            circleParticle.Pause();
            target.SetActive(false);
            inGameUI.SetActive(false);
            yield return new WaitForSeconds(2);
            mainSound.clip = resultBGM;
            mainSound.Play();
            yield return new WaitForSeconds(3);
            scoreUI.SetActive(true);
            _tesultText.text = "";
            returnBtn.SetActive(false);
            replayBtn.SetActive(false);
            yield return new WaitForSeconds(2);
            _tesultText.text = _metronome.totalPoints.ToString();
            applause.Play();
            yield return new WaitForSeconds(3.5f);
            returnBtn.SetActive(true);
            replayBtn.SetActive(true);
        }

        IEnumerator GameStartBGM()
        {
            yield return new WaitForSeconds(1.5f);
            mainSound.clip = startBGM;
            mainSound.Play();
        }
        


    }
}