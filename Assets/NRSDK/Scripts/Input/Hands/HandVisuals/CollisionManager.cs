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
        public GameObject[] sushiPrefab;
        private GameObject _sushi;
        public GameObject explosionParticle;
        //public GameObject numTextObj;
        private Text _numText;
        public int num;
        private bool Collidable = true;
        private GameObject _triggerObj;
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
            num++;
            _metronome.TrueorFalse();
            //_logText.text = "当たった";
            Vector3 _hitPos = other.ClosestPointOnBounds(this.transform.position);
            //_triggerObj = GetTriggerObjName(other.gameObject);
            _triggerObj = other.gameObject;
            CollisionEffect(_hitPos,_triggerObj);
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                num ++;
                _metronome.TrueorFalse();
                //CollisionEffect(Vector3.one);

            }
        }

        private void OnTriggerExit(Collider other)
        {
            //_logText.text = "離れた";
        }

      

        private void CollisionEffect(Vector3 _appearPos,GameObject collisionObj)
        {
            Debug.Log("CollisionEffect");
            /*
            var sushi_num = UnityEngine.Random.Range(0, sushiPrefab.Length);
            if(sushiPrefab[sushi_num] == null) Debug.Log("ない");
            if (sushiPrefab[sushi_num].name.Contains("Uni") || sushiPrefab[sushi_num].name.Contains("Negitoro"))
            {
                _sushi = Instantiate(sushiPrefab[sushi_num], _appearPos, Quaternion.Euler(-90,0,0));
            }
            else
            {
                _sushi = Instantiate(sushiPrefab[sushi_num], _appearPos, Quaternion.identity);
            }
            */
            Instantiate(collisionObj, _appearPos, Quaternion.identity);
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