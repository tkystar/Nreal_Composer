namespace NRKernal
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class HandEffect : MonoBehaviour
    {
        [SerializeField]public NRHandCapsuleVisual nrHandCapsuleVisual;
        private GameObject indextip;
        public GameObject indexparticle;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            indextip = nrHandCapsuleVisual.indexTip.m_VisualGO;
            indexparticle.transform.position = indextip.transform.position;
        }
    }
}