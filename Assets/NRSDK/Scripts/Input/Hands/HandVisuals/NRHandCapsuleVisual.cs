/****************************************************************************
* Copyright 2019 Nreal Techonology Limited. All rights reserved.
*                                                                                                                                                          
* This file is part of NRSDK.                                                                                                          
*                                                                                                                                                           
* https://www.nreal.ai/         
* 
*****************************************************************************/

using System;

namespace NRKernal
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class NRHandCapsuleVisual : MonoBehaviour
    {
        public CapsuleVisual indexTip;
        public  AudioSource directionChangeSound;
        public float noiseDifference;
        //public GameObject detectionTextObj;
        //private Text _detectiontext;
        [SerializeField] private Metronome Metronome;
        
        public class CapsuleVisualInfo
        {
            public bool shouldRender;
            public HandJointID startHandJointID;
            public HandJointID endHandJointID;
            public float capsuleRadius;
            public Vector3 startPos;
            public Vector3 endPos;
            public Material capsuleMat;
            
            

            

            public CapsuleVisualInfo(HandJointID startHandJointID, HandJointID endHandJointID)
            {
                this.startHandJointID = startHandJointID;
                this.endHandJointID = endHandJointID;
            }
        }

        

        void FixedUpdate()
        {
            //GetHandSpeed();
        }

        public class CapsuleVisual
        {
            public CapsuleVisualInfo capsuleVisualInfo;

            public GameObject m_VisualGO;     ////手（コライダー付きシリンダー）
            private Vector3 m_CapsuleScale;
            private MeshRenderer m_Renderer;
            private CapsuleCollider m_Collider;
            public float indexfingerSpeed;
            private Text m_handSpeedText;
            private GameObject handSpeedTextObj;
            private Text m_handDirectionText_x;
            private GameObject handDirectionTextObj_x;
            private Text m_handDirectionText_y;
            private GameObject handDirectionTextObj_y;
            private Text m_handDifferenceText;
            private GameObject m_handDifferenceTextObj;
            private Vector3 latestPos;
            public float measureSpeed_span = 0.5f;
            public float span_small = 0.1f;         //変数削除
            private float currentTime = 0f;
            private float currentTime_small = 0f;      //変数削除
            private float totalDistance_per_span;
            private Vector3 _latestPos;
            private Vector3 _difference;
            private string _latestState_y;
            private string _latestState_x;
            [SerializeField]public NRHandCapsuleVisual nrHandCapsuleVisual;
            [SerializeField] public HandEffect handeffect;
            [SerializeField] private Metronome metronome;
            [SerializeField] private CollisionManager collision;
            
            
    

            public CapsuleVisual(GameObject rootGO, CapsuleVisualInfo capsuleVisualInfo)
            {
                this.capsuleVisualInfo = capsuleVisualInfo;
                nrHandCapsuleVisual = GameObject.Find("NRHandCapsuleVisual_L").GetComponent<NRHandCapsuleVisual>();
                m_VisualGO = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                m_VisualGO.transform.SetParent(rootGO.transform);
                m_Collider = m_VisualGO.GetComponent<CapsuleCollider>();
                //NOTE: 遅延による当たり判定されない気持ち悪さを解消するため,コライダーを大きく設定している。
                //m_Collider.height *= 1.1f;
                //m_Collider.radius *= 1.1f;
                m_VisualGO.AddComponent<CollisionManager>();
                handSpeedTextObj = GameObject.Find("HandSpeedText");
                m_handSpeedText = handSpeedTextObj.GetComponent<Text>();
                handDirectionTextObj_x = GameObject.Find("HandDirectionText_x");
                handDirectionTextObj_y = GameObject.Find("HandDirectionText_y");
                m_handDirectionText_x = handDirectionTextObj_x.GetComponent<Text>();
                m_handDirectionText_y = handDirectionTextObj_y.GetComponent<Text>();
                handeffect = GameObject.Find("HandEffect").GetComponent<HandEffect>();
                m_handDifferenceText = GameObject.Find("HandDifferenceText").GetComponent<Text>();
                metronome = GameObject.Find("SoundManager").GetComponent<Metronome>();
                
                if (m_Collider)
                {
                    //m_Collider.enabled = false;
                    
                    
                }
                m_Renderer = m_VisualGO.GetComponent<MeshRenderer>();
                if (capsuleVisualInfo.capsuleMat)
                {
                    m_Renderer.material = capsuleVisualInfo.capsuleMat;
                }
                m_CapsuleScale = Vector3.zero;
                m_VisualGO.transform.localScale = m_CapsuleScale;
                
            }

            public void OnUpdate()
            {
                if (m_VisualGO == null)
                    return;

                if (capsuleVisualInfo.shouldRender)
                {
                    DrawCapsuleVisual(capsuleVisualInfo.startPos, capsuleVisualInfo.endPos, capsuleVisualInfo.capsuleRadius);
                    
                }
                else
                {
                    m_VisualGO.SetActive(false);
                }

                //人差し指の先のCapsuleVisualの場合、速度を取得する
                if(capsuleVisualInfo.endHandJointID == HandJointID.IndexTip)
                {
                    GetHandSpeed_accurate();
                    //GetIndexHandDirection();
                }
            }


            public void GetHandSpeed_accurate()
            {
                currentTime += Time.deltaTime;
                currentTime_small += Time.deltaTime;

                
                    if(currentTime_small > span_small)
                    {
                        currentTime_small = 0f;
                        _difference = m_VisualGO.transform.position - latestPos;
                        GetIndexHandDirection(_difference);
                        totalDistance_per_span += _difference.magnitude;
                        latestPos = m_VisualGO.transform.position;
                        
                        if(currentTime > measureSpeed_span)
                        {
                            currentTime = 0f;
                            indexfingerSpeed = totalDistance_per_span / Time.deltaTime;
                            m_handSpeedText.text = indexfingerSpeed.ToString("N2");  
                            totalDistance_per_span = 0f;
                        }
                    }
                    
                    
                                   
            }

            public void GetIndexHandDirection(Vector3 difference)
            {
                m_handDifferenceText.text = difference.ToString();
                if (difference.y > 0 && Mathf.Abs(difference.y) > nrHandCapsuleVisual.noiseDifference)
                {
                    if (_latestState_y == "Down")
                    {
                        m_handDirectionText_y.text = "Up";
                        handeffect.AppearParticle();
                        metronome.TimingScoring();
                    }
                    _latestState_y = "Up";
                }
                else if (difference.y < 0 && Mathf.Abs(difference.y) > nrHandCapsuleVisual.noiseDifference)
                {
                    if (_latestState_y == "Up")
                    {
                        m_handDirectionText_y.text = "Down";
                        handeffect.AppearParticle();
                    }
                    _latestState_y = "Down";
                }
                /*
                if (difference.x > 0 && Mathf.Abs(difference.x) > nrHandCapsuleVisual.noiseDifference)
                {
                    if (_latestState_x == "Left")
                    {
                        m_handDirectionText_x.text = "Right";
                        handeffect.AppearParticle();
                    }
                    _latestState_x = "Left";
                }
                else if (difference.x < 0 && Mathf.Abs(difference.x) > nrHandCapsuleVisual.noiseDifference)
                {
                    if (_latestState_x == "Right")
                    {
                        m_handDirectionText_x.text = "Left";
                        handeffect.AppearParticle();
                    }
                    _latestState_x = "Right";
                }
                */
            }

            public void DirectionChange()
            {
                nrHandCapsuleVisual.directionChangeSound.Play();
            }

            private void DrawCapsuleVisual(Vector3 a, Vector3 b, float radius)
            {
                m_VisualGO.SetActive(true);
                m_VisualGO.transform.position = (a + b) * 0.5f;
                m_VisualGO.transform.up = a - b;

                m_CapsuleScale = Vector3.one;
                m_CapsuleScale.y = Vector3.Distance(a, b) * 0.5f;
                m_CapsuleScale.x = m_CapsuleScale.z = radius * 2f;
                m_VisualGO.transform.localScale = m_CapsuleScale;
            }
        }

        public class JointVisualInfo
        {
            public bool shouldRender;
            public HandJointID handJointID;
            public float jointRadius;
            public Vector3 jointPos;
            public Material jointMat;

            public JointVisualInfo(HandJointID handJointID)
            {
                this.handJointID = handJointID;
            }
        }

        public class JointVisual
        {
            public JointVisualInfo jointVisualInfo;

            private GameObject m_VisualGO;
            private Vector3 m_JointScale;
            private MeshRenderer m_Renderer;
            private SphereCollider m_Collider;

            public JointVisual(GameObject rootGO, JointVisualInfo jointVisualInfo)
            {
                this.jointVisualInfo = jointVisualInfo;

                m_VisualGO = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                m_VisualGO.transform.SetParent(rootGO.transform);
                m_Collider = m_VisualGO.GetComponent<SphereCollider>();
                if (m_Collider)
                {
                    m_Collider.enabled = false;
                }
                m_Renderer = m_VisualGO.GetComponent<MeshRenderer>();
                if (jointVisualInfo.jointMat)
                {
                    m_Renderer.material = jointVisualInfo.jointMat;
                }
                m_JointScale = Vector3.zero;
                m_VisualGO.transform.localScale = m_JointScale;
            }

            public void OnUpdate()
            {
                if (m_VisualGO == null)
                    return;

                if (jointVisualInfo.shouldRender)
                {
                    DrawJointVisual(jointVisualInfo.jointPos, jointVisualInfo.jointRadius);
                }
                else
                {
                    m_VisualGO.SetActive(false);
                }
            }

            private void DrawJointVisual(Vector3 pos, float radius)
            {
                m_VisualGO.SetActive(true);
                m_VisualGO.transform.position = pos;
                m_JointScale = Vector3.one * radius * 2f;
                m_VisualGO.transform.localScale = m_JointScale;
            }
        }

        public HandEnum handEnum;
        public Material capsuleMat;
        public float capsuleRadius = 0.003f;
        public bool showCapsule = true;
        public Material jointMat;
        public float jointRadius = 0.005f;
        public bool showJoint = true;
        //public GameObject handSpeedTextObj;

        private List<CapsuleVisual> m_CapsuleVisuals;
        private List<JointVisual> m_JointVisuals;

        private void Start()
        {
            CreateCapsuleVisuals();
            CreateJointVisuals();
           // _detectiontext = detectionTextObj.GetComponent<Text>();
        }

        

        private void CreateCapsuleVisuals()
        {
            var capsuleVisualInfoList = new List<CapsuleVisualInfo>()
            {
                new CapsuleVisualInfo(HandJointID.Wrist, HandJointID.ThumbMetacarpal),
                new CapsuleVisualInfo(HandJointID.Wrist, HandJointID.PinkyMetacarpal),
                new CapsuleVisualInfo(HandJointID.ThumbMetacarpal, HandJointID.ThumbProximal),
                new CapsuleVisualInfo(HandJointID.PinkyMetacarpal, HandJointID.PinkyProximal),
                new CapsuleVisualInfo(HandJointID.ThumbProximal, HandJointID.ThumbDistal),
                new CapsuleVisualInfo(HandJointID.ThumbDistal, HandJointID.ThumbTip),
                new CapsuleVisualInfo(HandJointID.IndexProximal, HandJointID.IndexMiddle),
                new CapsuleVisualInfo(HandJointID.IndexMiddle, HandJointID.IndexDistal),
                new CapsuleVisualInfo(HandJointID.IndexDistal, HandJointID.IndexTip),
                new CapsuleVisualInfo(HandJointID.MiddleProximal, HandJointID.MiddleMiddle),
                new CapsuleVisualInfo(HandJointID.MiddleMiddle, HandJointID.MiddleDistal),
                new CapsuleVisualInfo(HandJointID.MiddleDistal, HandJointID.MiddleTip),
                new CapsuleVisualInfo(HandJointID.RingProximal, HandJointID.RingMiddle),
                new CapsuleVisualInfo(HandJointID.RingMiddle, HandJointID.RingDistal),
                new CapsuleVisualInfo(HandJointID.RingDistal, HandJointID.RingTip),
                new CapsuleVisualInfo(HandJointID.PinkyProximal, HandJointID.PinkyMiddle),
                new CapsuleVisualInfo(HandJointID.PinkyMiddle, HandJointID.PinkyDistal),
                new CapsuleVisualInfo(HandJointID.PinkyDistal, HandJointID.PinkyTip),
                new CapsuleVisualInfo(HandJointID.ThumbProximal, HandJointID.IndexProximal),
                new CapsuleVisualInfo(HandJointID.IndexProximal, HandJointID.MiddleProximal),
                new CapsuleVisualInfo(HandJointID.MiddleProximal, HandJointID.RingProximal),
                new CapsuleVisualInfo(HandJointID.RingProximal, HandJointID.PinkyProximal)
            };

            m_CapsuleVisuals = new List<CapsuleVisual>();
            for (int i = 0; i < capsuleVisualInfoList.Count; i++)
            {
                var capsuleInfo = capsuleVisualInfoList[i];
                capsuleInfo.capsuleMat = capsuleMat;
                CapsuleVisual capsuleVisual = new CapsuleVisual(gameObject, capsuleInfo);
                if(capsuleInfo.endHandJointID == HandJointID.IndexTip)
                {
                    indexTip = capsuleVisual;
                }
                m_CapsuleVisuals.Add(capsuleVisual);
            }
        }

        private void CreateJointVisuals()
        {
            m_JointVisuals = new List<JointVisual>();
            foreach (var item in System.Enum.GetValues(typeof(HandJointID)))
            {
                var jointID = (HandJointID)item;
                if (jointID > HandJointID.Invalid && jointID < HandJointID.Max)
                {
                    var jointVisualInfo = new JointVisualInfo(jointID);
                    jointVisualInfo.jointMat = jointMat;
                    m_JointVisuals.Add(new JointVisual(gameObject, jointVisualInfo));
                }
            }
        }

        private void OnEnable()
        {
            NRInput.Hands.OnHandStatesUpdated += OnHandTracking;
            NRInput.Hands.OnHandTrackingStopped += OnHandTrackingStopped;
        }

        private void OnDisable()
        {
            NRInput.Hands.OnHandStatesUpdated -= OnHandTracking;
            NRInput.Hands.OnHandTrackingStopped -= OnHandTrackingStopped;
        }

        private void OnHandTrackingStopped()
        {
            OnHandingTrackingLost();
        }

        private void OnHandTracking()
        {
            var handState = NRInput.Hands.GetHandState(handEnum);
            if (m_CapsuleVisuals != null)
            {
                for (int i = 0; i < m_CapsuleVisuals.Count; i++)
                {
                    var capsuleVisual = m_CapsuleVisuals[i];
                    UpstateCapsuleVisualInfo(capsuleVisual.capsuleVisualInfo, handState);
                    capsuleVisual.OnUpdate();                                                       //////////
                    
                }
            }
            if (m_JointVisuals != null)
            {
                for (int i = 0; i < m_JointVisuals.Count; i++)
                {
                    var jointVisual = m_JointVisuals[i];
                    UpdateJointVisualInfo(jointVisual.jointVisualInfo, handState);
                    jointVisual.OnUpdate();
                }
            }

            
        }

        private void UpstateCapsuleVisualInfo(CapsuleVisualInfo info, HandState handState)
        {
            info.shouldRender = handState.isTracked;
            info.startPos = handState.GetJointPose(info.startHandJointID).position;
            info.endPos = handState.GetJointPose(info.endHandJointID).position;
            info.capsuleRadius = capsuleRadius;
        }

        private void UpdateJointVisualInfo(JointVisualInfo info, HandState handState)
        {
            info.shouldRender = handState.isTracked;
            info.jointPos = handState.GetJointPose(info.handJointID).position;
            info.jointRadius = jointRadius;
        }

        private void OnHandingTrackingLost()
        {
            //force refresh visual
            OnHandTracking();
        }
    }

}
