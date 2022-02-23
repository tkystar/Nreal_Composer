using System.Collections;
using System.Collections.Generic;
using NRKernal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public Button startGameButton;
    public Button finishGameButton;
    public AudioSource mainSound;
    public GameObject soundManager;
    public Button[] buttons;
    [SerializeField] private Metronome _metronome;
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
        mainSound.Play();
        _metronome.MetronomeStart();
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(2);
        GameStart();
    }
}
