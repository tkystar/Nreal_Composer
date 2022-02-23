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
    public AudioClip resultBGM;
    public AudioClip playBGM;
    public Button[] buttons;
    public GameObject scoreUI;
    public GameObject resultTextObj;
    public GameObject returnBtn;
    private Text _tesultText;
    [SerializeField] private Metronome _metronome;
    
    private bool isGaming;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in buttons)
        {
            var name = item.name;
            Debug.Log("BtnName "+name);
            item.onClick.AddListener(() => OnClicked(name));
        }
        _metronome = soundManager.GetComponent<Metronome>();
        mainSound = soundManager.GetComponent<AudioSource>();
        scoreUI.SetActive(false);
        _tesultText = resultTextObj.GetComponent<Text>();
        //GameStart();
        StartCoroutine(StartDelay());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Editrでのテスト用
        //if(Input.GetKeyDown(KeyCode.Space)) GameStart();
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
            
        }
        else if (key == "ReturnBtn")
        {
            SceneManager.LoadScene("StartScene");
        }
        
    }
    

    private void GameStart()
    {
        isGaming = true;
        mainSound.clip = playBGM;
        mainSound.Play();
        _metronome.MetronomeStart();
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(2);
        GameStart();
    }

    private void GameFinish()
    {
        isGaming = false;
        _metronome.enabled = false;
        StartCoroutine(DisplayResult());
        
    }
    
    IEnumerator DisplayResult()
    {
        yield return new WaitForSeconds(2);
        mainSound.clip = resultBGM;
        mainSound.Play();
        yield return new WaitForSeconds(3);
        scoreUI.SetActive(true);
        _tesultText.text = "";
        returnBtn.SetActive(false);
        yield return new WaitForSeconds(2);
        var score = _metronome.totalPoints;
        _tesultText.text = score.ToString();
        yield return new WaitForSeconds(3.5f);
        returnBtn.SetActive(true);

    }
}
