namespace NRKernal
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class HandEffect : MonoBehaviour
    {
        [SerializeField]public NRHandCapsuleVisual nrHandCapsuleVisual;
        private GameObject _indextip;
        public GameObject _indexparticle;
        public GameObject tempParticle;
        
        
     

        void Start()
        {
            
        }


        void Update()
        {

            //指先の位置に動的にエフェクトを表示させる
            _indextip = nrHandCapsuleVisual.indexTip.m_VisualGO;
            _indexparticle.transform.position = _indextip.transform.position;
        }

        public void AppearParticle()
        {
            Instantiate(tempParticle, _indexparticle.transform.position, Quaternion.identity);
        }
    }
}