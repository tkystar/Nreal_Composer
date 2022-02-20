using System.Collections;
using System.Collections.Generic;
using NRKernal;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    public Button startGameButton;

    public AudioSource mainSound;
    public GameObject soundManager;

    [SerializeField] private Metronome _metronome;
    // Start is called before the first frame update
    void Start()
    {
        startGameButton.onClick.AddListener(OnClicked);
        _metronome = soundManager.GetComponent<Metronome>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Editrでのテスト用
        if(Input.GetKeyDown(KeyCode.Space)) GameStart();
        
        if(NRInput.IsTouching()) GameStart();
        
    }

    private void OnClicked()
    {
        GameStart();
    }

    private void GameStart()
    {
        mainSound.Play();
        _metronome.MetronomeStart();
    }
}
