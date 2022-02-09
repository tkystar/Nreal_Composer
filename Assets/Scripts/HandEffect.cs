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
     

        void Start()
        {
            
        }


        void Update()
        {

            //指先の位置に動的にエフェクトを表示させる
            indextip = nrHandCapsuleVisual.indexTip.m_VisualGO;
            indexparticle.transform.position = indextip.transform.position;
        }
    }
}