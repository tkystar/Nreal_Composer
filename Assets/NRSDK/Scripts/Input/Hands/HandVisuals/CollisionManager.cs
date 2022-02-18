namespace NRKernal
{

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class CollisionManager : MonoBehaviour
    {
        
        private Text _logText;
        [SerializeField] private Metronome _metronome;
        public GameObject soundManager;

        private void Awake()
        {
            //NOTE:editor上でアタッチできないため、文字列を使用
            _metronome = GameObject.Find("SoundManager").GetComponent<Metronome>();
            _logText = GameObject.Find("CollisionDetection").GetComponent<Text>();
        }


        private void OnCollisionEnter(Collision collision)
        {
            _metronome.TrueorFalse();
            _logText.text = "collision ON";
            //NOTE 文字列はなるべく使わない
            StartCoroutine(State2());
        }


        

        IEnumerator State2()
        {
            yield return new WaitForSeconds(0.1f);
            _logText.text = "collision OFF";
        }
        
        
    }
}