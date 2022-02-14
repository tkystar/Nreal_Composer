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
        private Renderer _indexparticleRenderer;
        private TrailRenderer _indexTrailRenderer;
        public GameObject trail;
        
        
     

        void Start()
        {
            _indexparticleRenderer = _indexparticle.GetComponent<Renderer>();
            _indexTrailRenderer = trail.GetComponent<TrailRenderer>();
        }


        void Update()
        {

            //指先の位置に動的にエフェクトを表示させる
            _indextip = nrHandCapsuleVisual.indexTip.m_VisualGO;
            _indexparticle.transform.position = _indextip.transform.position;
            trail.transform.position = _indextip.transform.position;
            
        }

        public void AppearParticle()
        {
            //Instantiate(tempParticle, _indexparticle.transform.position, Quaternion.identity);
            Color tempEffectColor = new Color(Random.Range(0, 255)/255f, Random.Range(0, 255)/255f, Random.Range(0, 255)/255f);
            _indexparticleRenderer.material.color = tempEffectColor;
            _indexTrailRenderer.startColor = tempEffectColor;

        }
    }
}