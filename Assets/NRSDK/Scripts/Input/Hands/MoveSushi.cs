using System;

namespace NRKernal
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MoveSushi : MonoBehaviour
    {
        private float _speed;
        private double deltaDistance;

        private bool isMove;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            //this.transform.position += (float)_speed * new Vector3(1, 0, 0);
            
        }

        private void FixedUpdate()
        {
            if(isMove)
            this.transform.position += Vector3.right * (float)deltaDistance;
            
            if(this.transform.position.x > 3) Destroy(this.gameObject);
        }

        public void GetDeltaDistance(double _deltadistance)
        {
            deltaDistance = _deltadistance;
            isMove = true;
        }
        
        
    }
    
}