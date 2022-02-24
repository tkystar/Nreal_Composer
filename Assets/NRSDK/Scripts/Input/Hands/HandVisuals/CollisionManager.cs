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
        public GameObject[] sushiPrefab;
        private GameObject _sushi;
        public GameObject explosionParticle;
        //public GameObject numTextObj;
        private Text _numText;
        public int num;
        private bool Collidable = true;
        private void Awake()
        {
            //NOTE:editor上でアタッチできないため、文字列を使用
            _metronome = GameObject.Find("SoundManager").GetComponent<Metronome>();
            //_logText = GameObject.Find("CollisionDetection").GetComponent<Text>();
            explosionParticle = GameObject.Find("Hit_04");
            //numTextObj = GameObject.Find("NumText");
            //_numText = numTextObj.GetComponent<Text>();
            Collidable = true;

            sushiPrefab = Resources.LoadAll<GameObject>("Sushi");
        }

        
        private void OnTriggerEnter(Collider other)
        {
            //if (!Collidable) return;
            num++;
            //_numText.text = num.ToString();
            _metronome.TrueorFalse();
            //_logText.text = "当たった";
            Vector3 _hitPos = other.ClosestPointOnBounds(this.transform.position);
            CollisionEffect(_hitPos);
            //NOTE 文字列はなるべく使わない
            //StartCoroutine(PauseCollision());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                num ++;
                _metronome.TrueorFalse();
                CollisionEffect(Vector3.one);

            }
        }

        private void OnTriggerExit(Collider other)
        {
            //_logText.text = "離れた";
        }

        private void CollisionEffect(Vector3 _appearPos)
        {
            Debug.Log("CollisionEffect");
            var sushi_num = UnityEngine.Random.Range(0, sushiPrefab.Length);
            if(sushiPrefab[sushi_num] == null) Debug.Log("ない");
            _sushi = Instantiate(sushiPrefab[sushi_num], _appearPos, Quaternion.Euler(0, 0, 0));
            Rigidbody sushiRB = _sushi.GetComponent<Rigidbody>();
            sushiRB.AddForce(Vector3.up * 30);
            //Destroy(_sushi, 2);
            Instantiate(explosionParticle, _appearPos, Quaternion.Euler(0, 0, 0));
        }
        
        IEnumerator PauseCollision()
        {
            Collidable = false;
            yield return new WaitForSeconds(0.3f);
            Collidable = true;
        }
        
        
        
        
    }
}