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
        public GameObject sushiPrefab;
        private GameObject _sushi;
        public GameObject explosionParticle;
        public GameObject numTextObj;
        private Text _numText;
        private int num;
        private void Awake()
        {
            //NOTE:editor上でアタッチできないため、文字列を使用
            _metronome = GameObject.Find("SoundManager").GetComponent<Metronome>();
            _logText = GameObject.Find("CollisionDetection").GetComponent<Text>();
            sushiPrefab = GameObject.Find("Sushi");
            explosionParticle = GameObject.Find("Hit_04");
            numTextObj = GameObject.Find("NumText");
            _numText = numTextObj.GetComponent<Text>();
        }

        private void Start()
        {
            StartCoroutine(EffectTest());
        }

        /*
        private void OnCollisionEnter(Collision collision)
        {
            
            _metronome.TrueorFalse();
            _logText.text = "collision ON";
            //NOTE 文字列はなるべく使わない
            StartCoroutine(State2());
            
        }
        */

        private void OnTriggerEnter(Collider other)
        {
            
            num++;
            _numText.text = num.ToString();
            _metronome.TrueorFalse();
            _logText.text = "当たった";

            Vector3 _hitPos = other.ClosestPointOnBounds(this.transform.position);
            //CollisionEffect(_hitPos);
            //NOTE 文字列はなるべく使わない
            //StartCoroutine(State2());
        }

        private void OnTriggerExit(Collider other)
        {
            _logText.text = "離れた";
        }

        private void CollisionEffect(Vector3 _appearPos)
        {
            
            _sushi = Instantiate(sushiPrefab, _appearPos, Quaternion.Euler(0, 0, 0));
            StartCoroutine(DeleteSushi());
            //Instantiate(explosionParticle, _appearPos, Quaternion.Euler(0, 0, 0));
        }

        IEnumerator State2()
        {
            yield return new WaitForSeconds(0.1f);
            _logText.text = "collision OFF";
        }

        IEnumerator EffectTest()
        {
            yield return new WaitForSeconds(3);
            //CollisionEffect(Vector3.zero);
        }

        IEnumerator DeleteSushi()
        {
            yield return new WaitForSeconds(2);
            Destroy(_sushi);
        }
        
        
        
        
    }
}