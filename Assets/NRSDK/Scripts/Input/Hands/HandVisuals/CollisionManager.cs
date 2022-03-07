using System.Linq;

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
        public GameObject transitionManager;
        public GameObject[] sushiPrefab;
        private GameObject _sushi;
        private GameObject explosionParticle;
        //public GameObject numTextObj;
        private Text _numText;
        public int num;
        private bool Collidable = true;
        private GameObject _triggerObj;
        private ICreateCollisionEffect _iCreateCollisionEffect;
        private void Awake()
        {
            //NOTE:editor上でアタッチできないため、文字列を使用
            //_metronome = GameObject.Find("SoundManager").GetComponent<Metronome>();
            _metronome = soundManager.GetComponent<Metronome>();
            explosionParticle = Resources.Load<GameObject>("CollisionParticle");
            //_iCreateCollisionEffect = GameObject.Find("GameTransitionManager").GetComponent<ICreateCollisionEffect>();
            _iCreateCollisionEffect = transitionManager.GetComponent<ICreateCollisionEffect>();
            Collidable = true;
            sushiPrefab = Resources.LoadAll<GameObject>("SushiNoots");
        }

        
        private void OnTriggerEnter(Collider other)
        {
            
            //num++;
            _metronome.EarlyorLate();
            Vector3 _hitPos = other.ClosestPointOnBounds(this.transform.position);
            //_triggerObj = other.gameObject;
            //CollisionEffect(_hitPos);
            CreateCollisionEffect();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                //num ++;
                _metronome.EarlyorLate();
                //CollisionEffect(Vector3.one);

            }
        }


        void CreateCollisionEffect()
        {
            var sushi_num = UnityEngine.Random.Range(0, sushiPrefab.Length);
            _sushi = Instantiate(sushiPrefab[sushi_num]);
            Instantiate(explosionParticle);
            
            _iCreateCollisionEffect.CreateEffect(Vector3.one);
        }
        
        //NOTE インターフェースを用いてリファクタリングしているため、非アクティブにしている。
        /*
        private void CollisionEffect(Vector3 _appearPos)
        {
            Debug.Log("CollisionEffect");

            for (int i = 0; i < 1; i++)
            {
                var sushi_num = UnityEngine.Random.Range(0, sushiPrefab.Length);
                //if(sushiPrefab[sushi_num] == null) Debug.Log("ない");
                _sushi = Instantiate(sushiPrefab[sushi_num], _appearPos, Quaternion.Euler(0,-60,0));
                Rigidbody sushiRB = _sushi.GetComponent<Rigidbody>();
                if(sushiRB == null) return;
                sushiRB.AddForce(Vector3.up * 30);
            }
            
            Instantiate(explosionParticle, _appearPos, Quaternion.Euler(0, 0, 0));
        }
        */
        
        IEnumerator PauseCollision()
        {
            Collidable = false;
            yield return new WaitForSeconds(0.3f);
            Collidable = true;
        }
        
        
        
        
    }
}