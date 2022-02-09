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
        [SerializeField]public HandState rightHandState;
        private Text m_leftHandSpeed_text;
        public GameObject leftHandSpeed_text_obj;
        public HandEnum handEnum;

     
        public class ComposeHands
        {
            
        }

        void Start()
        {
            pitch_audiosource = 1.0f;
            instrument.pitch = 1.0f;
            bgm_audioMixer.SetFloat("pitch",1.0f);
            m_leftHandSpeed_text = leftHandSpeed_text_obj.GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            //取得した左手人差し指の速度を曲のピッチ調整に適当な値に修正し、ピッチ調整する関数PitchAdjustに渡している
            indexfingerSpeed = Mathf.Lerp(0.3f , 3f , nrHandCapsuleVisual.indexTip.indexfingerSpeed / 55f);
            PitchAdjust(indexfingerSpeed);


            //VolumeAdjust(instrumentVolume);
            
            //右手の状態を取得し、特定のジェスチャーに応じて曲の再生・停止を実行
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
            //速度確認のためのテキスト表示
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