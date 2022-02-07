using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PitchManager : MonoBehaviour
{
    [SerializeField][Range (0.5f , 2.0f)]public float pitch_audiosource;
    public float pitch_pitchshifter;
    public AudioSource audioSource_instrument1;
    public AudioSource audioSource_instrument2;
    public AudioSource audioSource_instrument3;
    public float[] instrument_vol;
    public AudioMixer audioMixer;
    // Start is called before the first frame update
    void Start()
    {
        pitch_audiosource = 1.0f;
        audioSource_instrument1.pitch = 1.0f;
        audioSource_instrument2.pitch = 1.0f;
        audioSource_instrument3.pitch = 1.0f;
        audioMixer.SetFloat("pitch",1.0f);
        instrument_vol = new float[3];
    }

    // Update is called once per frame
    void Update()
    {
        PitchAdjust(pitch_audiosource);
        VolumeAdjust(instrument_vol[0],instrument_vol[1],instrument_vol[2]);
    }

    private void PitchAdjust(float _pitch)
    {
        //入力に合わせてAudioSourceのピッチを調整(ピッチのパラメータを調整するが、変えるのはテンポ)
        audioSource_instrument1.pitch = _pitch;
        audioSource_instrument2.pitch = _pitch;
        audioSource_instrument3.pitch = _pitch;
       
        
        //音のPitchSifterのピッチを調整(上記テンポ調整によって変わってしまったピッチを戻す)
        pitch_pitchshifter = 1/_pitch;
        audioMixer.SetFloat("pitch",pitch_pitchshifter);
    }

    private void VolumeAdjust(float volume1 , float volume2 , float volume3)
    {
        audioSource_instrument1.volume = volume1;
        audioSource_instrument2.volume = volume2;
        audioSource_instrument3.volume = volume3;
    }
}
