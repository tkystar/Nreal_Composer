using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    public Button startBtn;
    // Start is called before the first frame update
    void Start()
    {
        startBtn.onClick.AddListener(() => StartBtnClicked());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartBtnClicked()
    {
        SceneManager.LoadScene("HelloMR");
    }
}
