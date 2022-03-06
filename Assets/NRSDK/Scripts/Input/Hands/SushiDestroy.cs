using System;

namespace NRKernal
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SushiDestroy : MonoBehaviour
    {
        
        public bool isBeat;
        public bool isEnter;
        public float deadLine;
        public void CreateEffect(Vector3 _pos)
        {
            this.transform.position = _pos;
            this.transform.rotation = Quaternion.Euler(0, -60, 0);
            Rigidbody sushiRB = this.GetComponent<Rigidbody>();
            if(sushiRB == null) return;
            sushiRB.AddForce(Vector3.up * 30);
        }
        private void OnTriggerStay(Collider other)
        {
            if (isBeat)
            {
                StartCoroutine(ActivateTrigger());
                
                if (other.gameObject.tag == "Player") Destroy(other.gameObject);
                Debug.Log("OnTriggerStay");
                
            }
            
        }
        
        private void OnTriggerExit(Collider other)
        {
            isEnter = false;
        }

        IEnumerator ActivateTrigger()
        {
            yield return new WaitForSeconds(0.2f);
            isBeat = false;
            isEnter = false;
        }

        
    }
}