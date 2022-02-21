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

        
        private void OnTriggerEnter(Collider other)
        {
            
            num++;
            _numText.text = num.ToString();
            _metronome.TrueorFalse();
            _logText.text = "当たった";

            Vector3 _hitPos = other.ClosestPointOnBounds(this.transform.position);
            CollisionEffect(_hitPos);
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
            Rigidbody sushiRB = _sushi.GetComponent<Rigidbody>();
            sushiRB.AddForce(Vector3.up * 50);
            Destroy(_sushi, 2);
            Instantiate(explosionParticle, _appearPos, Quaternion.Euler(0, 0, 0));
        }
        
        IEnumerator DeleteSushi()
        {
            yield return new WaitForSeconds(2);
            Destroy(_sushi);
        }
        
        
        
        
    }
}