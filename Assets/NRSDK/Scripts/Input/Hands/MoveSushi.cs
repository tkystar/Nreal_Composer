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
       
        private void FixedUpdate()
        {
            if(isMove)
            this.transform.position += Vector3.right * (float)deltaDistance;
            
            if(this.transform.position.x > 5) Destroy(this.gameObject);
        }

        public void GetDeltaDistance(double _deltadistance)
        {
            deltaDistance = _deltadistance;
            isMove = true;
        }
        
        
    }
    
}