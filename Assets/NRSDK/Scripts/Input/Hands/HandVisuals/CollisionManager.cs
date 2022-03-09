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
        private Metronome _metronome;
        public GameObject soundManager;
        private GameObject[] sushiPrefab;
        private GameObject _sushi;
        private GameObject explosionParticle;
        private Text _numText;
        public int num;
        private bool Collidable = true;
        private GameObject _triggerObj;
        private Vector3 _hitPos;
        public ICreateCollisionEffect _iCreateCollisionEffect;
        private void Awake()
        {
            //NOTE:editor上でアタッチできないため、文字列を使用
            _metronome = GameObject.Find("SoundManager").GetComponent<Metronome>();
            explosionParticle = Resources.Load<GameObject>("CollisionParticle");
            sushiPrefab = Resources.LoadAll<GameObject>("SushiNoots");
            Collidable = true;
        }

        
        private void OnTriggerEnter(Collider other)
        {
            _metronome.EarlyorLate();
            _hitPos = other.ClosestPointOnBounds(this.transform.position);
            CreateCollisionEffect(_hitPos);
        }

        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _metronome.EarlyorLate();
            }
        }


        void CreateCollisionEffect(Vector3 _hitPos)
        {
            var sushi_num = UnityEngine.Random.Range(0, sushiPrefab.Length);
            GameObject _sishiParticle = Instantiate(sushiPrefab[sushi_num]);
            GameObject _particle = Instantiate(explosionParticle);
            _sishiParticle.GetComponent<ICreateCollisionEffect>().CreateEffect(_hitPos);
            _particle.GetComponent<ICreateCollisionEffect>().CreateEffect(_hitPos);
            
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