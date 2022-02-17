/****************************************************************************
* Copyright 2019 Nreal Techonology Limited. All rights reserved.
*                                                                                                                                                          
* This file is part of NRSDK.                                                                                                          
*                                                                                                                                                           
* https://www.nreal.ai/        
* 
*****************************************************************************/

using System;
using UnityEngine;
using UnityEngine.UI;

namespace NRKernal.NRExamples
{
    /// <summary> Controls the HelloAR example. </summary>
    [HelpURL("https://developer.nreal.ai/develop/unity/controller")]
    public class HelloMRController : MonoBehaviour
    {
        /// <summary> A model to place when a raycast from a user touch hits a plane. </summary>
        public GameObject AndyPlanePrefab;

        private GameObject _newStage;
        private GameObject _oldStage;
        public GameObject debugTextobj;
        private Text debugText;
        private bool _finishPutPlane;
        public Button ConfirmButton;

        /// <summary> Updates this object. </summary>
        private void Start()
        {
            debugText = debugTextobj.GetComponent<Text>();
            ConfirmButton.onClick.AddListener(FinishPutPlane);
        }

        void Update()
        {
            // If the player doesn't click the trigger button, we are done with this update.
            /*
            if (!NRInput.GetButtonDown(ControllerButton.TRIGGER))
            {
                return;
            }

            // Get controller laser origin.
            var handControllerAnchor = NRInput.DomainHand == ControllerHandEnum.Left ? ControllerAnchorEnum.LeftLaserAnchor : ControllerAnchorEnum.RightLaserAnchor;
            Transform laserAnchor = NRInput.AnchorsHelper.GetAnchor(NRInput.RaycastMode == RaycastModeEnum.Gaze ? ControllerAnchorEnum.GazePoseTrackerAnchor : handControllerAnchor);

            RaycastHit hitResult;
            if (Physics.Raycast(new Ray(laserAnchor.transform.position, laserAnchor.transform.forward), out hitResult, 10))
            {
                if (hitResult.collider.gameObject != null && hitResult.collider.gameObject.GetComponent<NRTrackableBehaviour>() != null)
                {
                    var behaviour = hitResult.collider.gameObject.GetComponent<NRTrackableBehaviour>();
                    if (behaviour.Trackable.GetTrackableType() != TrackableType.TRACKABLE_PLANE)
                    {
                        return;
                    }

                    // Instantiate Andy model at the hit point / compensate for the hit point rotation.
                    Instantiate(AndyPlanePrefab, hitResult.point, Quaternion.identity, behaviour.transform);
                }
            }*/
            
            if (NRInput.GetButtonDown(ControllerButton.TRIGGER)&& !_finishPutPlane)
            {
                debugText.text = "Input now";
                _oldStage = _newStage;
                Destroy(_oldStage);
                // コントローラのレイの原点の取得
                Transform laserAnchor = NRInput.AnchorsHelper.GetAnchor(NRInput.RaycastMode == RaycastModeEnum.Gaze ?
                    ControllerAnchorEnum.GazePoseTrackerAnchor : ControllerAnchorEnum.RightLaserAnchor);

                // レイと平面の衝突判定
                RaycastHit hitResult;
                if (Physics.Raycast(new Ray(laserAnchor.transform.position, laserAnchor.transform.forward), out hitResult, 1000))
                {
                    if (hitResult.collider.gameObject != null &&
                        hitResult.collider.gameObject.GetComponent<NRTrackableBehaviour>() != null)
                    {
                        var behaviour = hitResult.collider.gameObject.GetComponent<NRTrackableBehaviour>();
                        if (behaviour.Trackable.GetTrackableType() == TrackableType.TRACKABLE_PLANE)
                        {
                            // インスタンスの生成
                            _newStage = Instantiate(AndyPlanePrefab, hitResult.point, Quaternion.identity, behaviour.transform);
                        }
                    }
                }
            }
            else
            {
                debugText.text = "...";
            }
        }

        public void FinishPutPlane()
        {
            _finishPutPlane = true;
        }
    }
}
