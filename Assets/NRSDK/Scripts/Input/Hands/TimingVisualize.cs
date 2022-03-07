using System;

namespace NRKernal
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class TimingVisualize : MonoBehaviour
    {
        public GameObject hitPosObj;
        public GameObject spawnPosObj;
        private Vector3 _hitPos;
        private Vector3 _spawnPos;
        private double _nxtRng;
        public GameObject noot;
        private Rigidbody _nootRB;
        //private double _distance;
        private double _bpm;
        private double _time;
        public double _speed;
        private double _divided_num;
        private double _divided_dis;
        private GameObject _noot;
        //public double _distance_per_sec;
        private GameObject[] sushiPrefab;
        private bool isDone;
        [SerializeField] private MoveSushi _moveSushi;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        
        public void CreateNoots()
        {
            if (isDone) return;

            StartCoroutine(WaitCreateNootsisCalled());
            Debug.Log("CreateNoots");
            _noot = Instantiate(noot,_spawnPos,Quaternion.Euler(0,-57.169f,0));
            _moveSushi = _noot.AddComponent<MoveSushi>();
            _moveSushi.GetDeltaDistance(_divided_dis);

        }

        public void GetDividedDistance(double bpm)
        {
            //_bpm = bpm;
            _time = 60d / bpm;
            _divided_num = _time / 0.02;
            _divided_dis = GetSushiMovingDistance() / _divided_num;
        }
        
        private double GetSushiMovingDistance()
        {
            _hitPos = hitPosObj.transform.position;
            _spawnPos = spawnPosObj.transform.position;
            return (_hitPos - _spawnPos).magnitude;
        }

        IEnumerator WaitCreateNootsisCalled()
        {
            isDone = true;
            yield return new WaitForSeconds(0.3f);
            isDone = false;
        }
    }
}