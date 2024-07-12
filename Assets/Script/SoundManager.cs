using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource BassAudioSource;
    public AudioSource GuitarAudioSource;
    public AudioSource DrumAudioSource;
    public AudioSource VocalAudioSource;
    public Slider BassSilder;
    public Slider GuitarSilder;
    public Slider DrumSilder;
    public Slider VocalSilder;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        BassAudioSource.volume = BassSilder.value;
        GuitarAudioSource.volume = GuitarSilder.value;
        DrumAudioSource.volume = DrumSilder.value;
        VocalAudioSource.volume = VocalSilder.value;
    }
}
