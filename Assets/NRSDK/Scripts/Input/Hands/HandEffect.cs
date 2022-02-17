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
        public GameObject hamon;
        private bool hamonnow;
        private GameObject nrCameraRig;
        private Vector3 nrCameraRigPos;
        private float hamoninterval;
        private Color tempEffectColor;
        [SerializeField] Metronome metronome;
        private GameObject[] hamonarray;
        private float[] hamonintervalarray;
        private int hamonnum = 0;
        private float currentTime;
        void Start()
        {
            _indexparticleRenderer = _indexparticle.GetComponent<Renderer>();
            _indexTrailRenderer = trail.GetComponent<TrailRenderer>();
            nrCameraRigPos = GameObject.Find("NRCameraRig").GetComponent<Transform>().position;
            metronome = GameObject.Find("SoundManager").GetComponent<Metronome>();
            hamonarray = new GameObject[20];
            hamonintervalarray = new float[20];
            AppearParticle();
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                AppearParticle();
            }
            currentTime += Time.deltaTime;
            //指先の位置に動的にエフェクトを表示させる
            _indextip = nrHandCapsuleVisual.indexTip.m_VisualGO;
            _indexparticle.transform.position = _indextip.transform.position;
            trail.transform.position = _indextip.transform.position;

            if (hamonnow)
            {
                
                //hamoninterval += Time.deltaTime;
                for (int i = 0; i < hamonarray.Length; i++)
                {
                    if (hamonarray[i] == null) return;
                        hamonarray[i].transform.localScale = Vector3.one * (currentTime - hamonintervalarray[i]) / 8;

                    if (currentTime - hamonintervalarray[i] > 2)
                    {
                        
                        for (int j = i; j < hamonarray.Length - 1; j++)
                        {
                            if(hamonarray[j+1] != null) hamonarray[j] = hamonarray[j + 1];
                            if(hamonintervalarray[j + 1] != null) hamonintervalarray[j] = hamonintervalarray[j + 1];
                            
                        }
                        Destroy(hamonarray[i]);
                        
                    }
                }
                
                
            }

            
            
        }

        private Color EffectColorSellect()
        {
            /*
            if (metronome.latestState == "Perfect")
            {
                return Color.green;
            }
            else if (metronome.latestState == "Good")
            {
                return Color.yellow;
                
            }
            else if (metronome.latestState == "Soso")
            {
                return Color.red;
                
            }
            */
            return Color.red;
        }

        public void AppearParticle()
        {
            
            for (int i = 0; i < hamonarray.Length; i++)
            {
                if (hamonarray[i] == null)
                {
                    Debug.Log("AppearParticle() is called");
                    hamonarray[i] = 
                        Instantiate(hamon, new Vector3(nrCameraRigPos.x, -1.5f, nrCameraRigPos.z),Quaternion.Euler(90, 0, 0));
                    hamonintervalarray[i] = currentTime;
                    hamonarray[i].GetComponent<SpriteRenderer>().color = EffectColorSellect();
                }
            }
            
            _indexparticleRenderer.material.color = EffectColorSellect();
            _indexTrailRenderer.startColor = EffectColorSellect();
            //波紋
            
            
            hamonnow = true;
            
        }
        
        
    }
}