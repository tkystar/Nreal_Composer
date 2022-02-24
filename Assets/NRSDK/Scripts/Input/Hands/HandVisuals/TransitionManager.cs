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
        public Button[] buttons;
        public GameObject scoreUI;
        public GameObject resultTextObj;
        public GameObject inGameUI;
        public GameObject startUI;
        public GameObject returnBtn;
        public GameObject replayBtn;
        private Text _tesultText;
        public GameObject target;
        [SerializeField] private Metronome _metronome;
        [SerializeField] private CollisionManager _collisionManager;

        private bool isGaming;

        // Start is called before the first frame update
        void Start()
        {
            foreach (var item in buttons)
            {
                var name = item.name;
                Debug.Log("BtnName " + name);
                item.onClick.AddListener(() => OnClicked(name));
            }

            _metronome = soundManager.GetComponent<Metronome>();
            mainSound = soundManager.GetComponent<AudioSource>();
            mainSound.clip = startBGM;
            scoreUI.SetActive(false);
            inGameUI.SetActive(false);
            startUI.SetActive(true);
            target.SetActive(false);
            _tesultText = resultTextObj.GetComponent<Text>();
            //GameStart();
            StartCoroutine(StartDelay());
            StartCoroutine(GameStartBGM());
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //Editrでのテスト用
            if (Input.GetKeyDown(KeyCode.P)) PlayMode();
            if (Input.GetKeyDown(KeyCode.Space)) GameStart();
            if (Input.GetKeyDown(KeyCode.G)) GoHome();
            //if(NRInput.IsTouching()) GameStart();
            //if(NRInput.GetButton(ControllerButton.TRIGGER)) GameStart();

            if (!mainSound.isPlaying && isGaming)
            {
                GameFinish();
            }
        }

        private void OnClicked(string key)
        {
            if (key == "StartBtn")
            {
                GameStart();
            }
            else if (key == "ReturnBtn")
            {
                GoHome();
            }
            else if (key == "GotoPlayModeBtn")
            {
                PlayMode();
            }
            else if (key == "RePlayBtn")
            {
                PlayMode();
            }

        }

        private void PlayMode()
        {
            startUI.SetActive(false);
            inGameUI.SetActive(true);
            scoreUI.SetActive(false);
            GameStart();

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
            StartCoroutine(DisplayResult());

        }

        private void GoHome()
        {
            /*
            startUI.SetActive(true);
            scoreUI.SetActive(false);
            mainSound.Pause();
            StartCoroutine(GameStartBGM());
            */
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        IEnumerator DisplayResult()
        {
            yield return new WaitForSeconds(1);
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
            var score = _metronome.totalPoints;
            _tesultText.text = score.ToString();
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