namespace NRKernal
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Audio;
    using UnityEngine.UI;

    public class PitchManager_single : MonoBehaviour
    {
        [SerializeField][Range (0.5f , 2.0f)]public float pitch_audiosource;
        public float pitch_pitchshifter;
        public AudioSource audioSource_instrument1;
        public float instrument_vol;
        public float indexSpeed;
        public AudioMixer audioMixer;
        [SerializeField]public NRHandCapsuleVisual nrHandCapsuleVisual;

        [SerializeField]public HandState handState;
        private Text _handSpeedText;
        public GameObject handSpeedTextObj;
        //public Handenum handenum;

        //private CapsuleVisual capsulevisual;
        // Start is called before the first frame update
        void Start()
        {
            pitch_audiosource = 1.0f;
            audioSource_instrument1.pitch = 1.0f;
            audioMixer.SetFloat("pitch",1.0f);
            //capsulevisual = NRHandCapsuleVisual.capsulevisual;
            //capsulevisual = this.gameObject.GetComponent<CapsuleVisual>();
            _handSpeedText = handSpeedTextObj.GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            //_handSpeedText.text = nrHandCapsuleVisual.indexTip.handSpeed.ToString("N2");
            
            indexSpeed = Mathf.Lerp(0.3f , 3f , nrHandCapsuleVisual.indexTip.handSpeed / 55f);
            PitchAdjust(indexSpeed);
            //VolumeAdjust(instrument_vol);
            /*
            var handNRInput.Hands.GetHandState(handenum);
            
            if(handState.currentGesture == HandGesture.Grab)
            {
                audioSource_instrument1.Pause();
            }
            */
        }

        private void PitchAdjust(float _pitch)
        {
            _handSpeedText.text = _pitch.ToString("N2");
            //入力に合わせてAudioSourceのピッチを調整(ピッチのパラメータを調整するが、変えるのはテンポ)
            audioSource_instrument1.pitch = _pitch;

            //音のPitchSifterのピッチを調整(上記テンポ調整によって変わってしまったピッチを戻す)
            pitch_pitchshifter = 1/_pitch;
            audioMixer.SetFloat("pitch",pitch_pitchshifter);
        }

        private void VolumeAdjust(float volume)
        {
            audioSource_instrument1.volume = volume;
        }
    }
}