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
        public AudioSource instrument;
        public float instrumentVolume;
        public float indexfingerSpeed;
        public AudioMixer bgm_audioMixer;
        [SerializeField]public NRHandCapsuleVisual nrHandCapsuleVisual;
        //[SerializeField]public NRHandCapsuleVisual nrHandCapsuleVisual_L;
        //[SerializeField]public NRHandCapsuleVisual nrHandCapsuleVisual_R;

        [SerializeField]public HandState rightHandState;
        private Text m_leftHandSpeed_text;
        public GameObject leftHandSpeed_text_obj;
        public HandEnum handEnum;

        //private CapsuleVisual capsulevisual;
        // Start is called before the first frame update
        public class ComposeHands
        {
            
        }

        void Start()
        {
            pitch_audiosource = 1.0f;
            instrument.pitch = 1.0f;
            bgm_audioMixer.SetFloat("pitch",1.0f);
            //capsulevisual = NRHandCapsuleVisual.capsulevisual;
            //capsulevisual = this.gameObject.GetComponent<CapsuleVisual>();
            m_leftHandSpeed_text = leftHandSpeed_text_obj.GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            //m_leftHandSpeed_text.text = nrHandCapsuleVisual.indexTip.handSpeed.ToString("N2");
            
            indexfingerSpeed = Mathf.Lerp(0.3f , 3f , nrHandCapsuleVisual.indexTip.indexfingerSpeed / 55f);
            PitchAdjust(indexfingerSpeed);


            //VolumeAdjust(instrumentVolume);
            
            var rightHandState = NRInput.Hands.GetHandState(handEnum);
            
            if(rightHandState.currentGesture == HandGesture.Grab)
            {
                instrument.Pause();
            }
            else if(rightHandState.currentGesture == HandGesture.OpenHand && !instrument.isPlaying)
            {
                instrument.Play();
            }
            
        }

        private void PitchAdjust(float _pitch)
        {
            m_leftHandSpeed_text.text = _pitch.ToString("N2");
            //入力に合わせてAudioSourceのピッチを調整(ピッチのパラメータを調整するが、変えるのはテンポ)
            instrument.pitch = _pitch;

            //音のPitchSifterのピッチを調整(上記テンポ調整によって変わってしまったピッチを戻す)
            pitch_pitchshifter = 1/_pitch;
            bgm_audioMixer.SetFloat("pitch",pitch_pitchshifter);
        }

        private void VolumeAdjust(float volume)
        {
            instrument.volume = volume;
        }
    }
}