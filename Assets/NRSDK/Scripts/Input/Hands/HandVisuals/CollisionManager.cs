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

        private void Start()
        {
            _logText = GameObject.Find("CollisionDetection").GetComponent<Text>();

        }


        private void OnCollisionEnter(UnityEngine.Collision collision)
        {
            _logText.text = "collision ON";
            StartCoroutine("resetState");
        }

        IEnumerator resetState()
        {
            yield return new WaitForSeconds(0.1f);
            _logText.text = "collision OFF";
        }
    }
}