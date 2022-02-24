using System;

namespace NRKernal
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MoveSushi : MonoBehaviour
    {
        private float _speed;
        private Rigidbody _rb;
        [SerializeField] TimingVisualize _timingVisualize;

        private double deltaDistance;
        // Start is called before the first frame update
        void Start()
        {
            //deltaDistance = _timingVisualize._divided_dis;
            _speed = (float)_timingVisualize._distance_per_sec;
            Debug.Log("deltaDistance" + deltaDistance);
            _rb = this.gameObject.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            //this.transform.position += (float)_speed * new Vector3(1, 0, 0);
            
        }

        private void FixedUpdate()
        {
            //_rb.velocity = new Vector3(1, 0, 0) * _speed;
            //deltaDistance = _timingVisualize._divided_dis;
            this.transform.position += Vector3.right * (float)deltaDistance;
        }
    }
    
}