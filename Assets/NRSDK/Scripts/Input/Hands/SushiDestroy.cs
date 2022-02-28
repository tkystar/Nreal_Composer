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
        // Start is called before the first frame update
        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {

        }

        /*
        private void OnTriggerStay(Collider other)
        {
            isEnter = true;
            if (isBeat)
            {
                if(other.gameObject.tag == "Player") Destroy(other.gameObject);
                isBeat = false;
                Debug.Log("OnTriggerStay");
            }
        }
    */
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