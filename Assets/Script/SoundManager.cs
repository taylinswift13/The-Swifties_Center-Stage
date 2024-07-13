using UnityEngine;
using UnityEngine.UI;
using System;

public class SoundManager : MonoBehaviour
{
    public AudioSource BassAudioSource;
    public AudioSource GuitarAudioSource;
    public AudioSource DrumAudioSource;
    public AudioSource VocalAudioSource;
    public Slider BassSlider;
    public Slider GuitarSlider;
    public Slider DrumSlider;
    public Slider VocalSlider;
    public GameObject BassKnob;
    public GameObject GuitarKnob;
    public GameObject DrumKnob;
    public GameObject VocalKnob;
    public GameObject ReverbKnob; // Single reverb knob
    public GameObject DistortionKnob; // Single distortion knob
    // public GameObject EchoKnob; // Single echo knob
    public static double total_error;

    // Object for spot light
    public UnityEngine.Rendering.Universal.Light2D spotlight;

    // Object for particle system
    public ParticleSystem particleSys;

    // Object for scrollbar
    public Scrollbar scrollbar;

    private AudioReverbFilter bassReverbFilter;
    private AudioReverbFilter guitarReverbFilter;
    private AudioReverbFilter drumReverbFilter;
    private AudioReverbFilter vocalReverbFilter;

    private AudioDistortionFilter bassDistortionFilter;
    private AudioDistortionFilter guitarDistortionFilter;
    private AudioDistortionFilter drumDistortionFilter;
    private AudioDistortionFilter vocalDistortionFilter;

    private AudioEchoFilter bassEchoFilter;
    private AudioEchoFilter guitarEchoFilter;
    private AudioEchoFilter drumEchoFilter;
    private AudioEchoFilter vocalEchoFilter;

    void Start()
    {
        // Ensure AudioReverbFilter components are attached to the AudioSources
        bassReverbFilter = AssignReverbFilter(BassAudioSource);
        guitarReverbFilter = AssignReverbFilter(GuitarAudioSource);
        drumReverbFilter = AssignReverbFilter(DrumAudioSource);
        vocalReverbFilter = AssignReverbFilter(VocalAudioSource);

        // Ensure AudioDistortionFilter components are attached to the AudioSources
        bassDistortionFilter = AssignDistortionFilter(BassAudioSource);
        guitarDistortionFilter = AssignDistortionFilter(GuitarAudioSource);
        drumDistortionFilter = AssignDistortionFilter(DrumAudioSource);
        vocalDistortionFilter = AssignDistortionFilter(VocalAudioSource);

        // Ensure AudioEchoFilter components are attached to the AudioSources
        bassEchoFilter = AssignEchoFilter(BassAudioSource);
        guitarEchoFilter = AssignEchoFilter(GuitarAudioSource);
        drumEchoFilter = AssignEchoFilter(DrumAudioSource);
        vocalEchoFilter = AssignEchoFilter(VocalAudioSource);
    }

    AudioReverbFilter AssignReverbFilter(AudioSource audioSource)
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is null");
            return null;
        }

        AudioReverbFilter reverbFilter = audioSource.GetComponent<AudioReverbFilter>();
        if (reverbFilter == null)
        {
            Debug.Log("AudioReverbFilter not found on " + audioSource.gameObject.name + ", adding new one.");
            reverbFilter = audioSource.gameObject.AddComponent<AudioReverbFilter>();
        }
        else
        {
            Debug.Log("AudioReverbFilter found on " + audioSource.gameObject.name);
        }
        return reverbFilter;
    }

    AudioDistortionFilter AssignDistortionFilter(AudioSource audioSource)
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is null");
            return null;
        }

        AudioDistortionFilter distortionFilter = audioSource.GetComponent<AudioDistortionFilter>();
        if (distortionFilter == null)
        {
            Debug.Log("AudioDistortionFilter not found on " + audioSource.gameObject.name + ", adding new one.");
            distortionFilter = audioSource.gameObject.AddComponent<AudioDistortionFilter>();
        }
        else
        {
            Debug.Log("AudioDistortionFilter found on " + audioSource.gameObject.name);
        }
        return distortionFilter;
    }

    AudioEchoFilter AssignEchoFilter(AudioSource audioSource)
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is null");
            return null;
        }

        AudioEchoFilter echoFilter = audioSource.GetComponent<AudioEchoFilter>();
        if (echoFilter == null)
        {
            Debug.Log("AudioEchoFilter not found on " + audioSource.gameObject.name + ", adding new one.");
            echoFilter = audioSource.gameObject.AddComponent<AudioEchoFilter>();
        }
        else
        {
            Debug.Log("AudioEchoFilter found on " + audioSource.gameObject.name);
        }
        return echoFilter;
    }

    // Update is called once per frame
    void Update()
    {
        // Update volume from sliders
        BassAudioSource.volume = BassSlider.value;
        GuitarAudioSource.volume = GuitarSlider.value;
        DrumAudioSource.volume = DrumSlider.value;
        VocalAudioSource.volume = VocalSlider.value;

        // Update pan from knobs
        UpdatePanFromKnob(BassAudioSource, BassKnob);
        UpdatePanFromKnob(GuitarAudioSource, GuitarKnob);
        UpdatePanFromKnob(DrumAudioSource, DrumKnob);
        UpdatePanFromKnob(VocalAudioSource, VocalKnob);

        // Update reverb from reverb knob
        UpdateReverbFromKnob(bassReverbFilter, ReverbKnob);
        UpdateReverbFromKnob(guitarReverbFilter, ReverbKnob);
        UpdateReverbFromKnob(drumReverbFilter, ReverbKnob);
        UpdateReverbFromKnob(vocalReverbFilter, ReverbKnob);

        // Update distortion from distortion knob
        UpdateDistortionFromKnob(bassDistortionFilter, DistortionKnob);
        UpdateDistortionFromKnob(guitarDistortionFilter, DistortionKnob);
        UpdateDistortionFromKnob(drumDistortionFilter, DistortionKnob);
        UpdateDistortionFromKnob(vocalDistortionFilter, DistortionKnob);

        // Update echo from echo knob
        // UpdateEchoFromKnob(bassEchoFilter, EchoKnob);
        // UpdateEchoFromKnob(guitarEchoFilter, EchoKnob);
        // UpdateEchoFromKnob(drumEchoFilter, EchoKnob);
        // UpdateEchoFromKnob(vocalEchoFilter, EchoKnob);

        // Calculate errors
        double range_width_slider = 0.1;
        double range_width_knob = 0.1;

        double bass_fad_range = 0.8, guitar_fad_range = 0.5, drum_fad_range = 0.5, vocal_fad_range = 0.8;
        double bass_knob_range = 0.7, guitar_knob_range = -0.7, drum_knob_range = 0.2, vocal_knob_range = -0.2;
        double reverb_knob_range = 0.5, distortion_knob_range = -0.1, echo_knob_range = 0;

        double bass_fad_error = 0.5, guitar_fad_error = 0.5, drum_fad_error = 0.5, vocal_fad_error = 0.5;
        double bass_knob_error = 0.5, guitar_knob_error = 0.5, drum_knob_error = 0.5, vocal_knob_error = 0.5;
        double reverb_knob_error = 0.5, distortion_knob_error = 0.5, echo_knob_error = 0.5;

        CheckRangeAndCalculateError_slider(BassSlider, bass_fad_range, range_width_slider, ref bass_fad_error);
        CheckRangeAndCalculateError_slider(GuitarSlider, guitar_fad_range, range_width_slider, ref guitar_fad_error);
        CheckRangeAndCalculateError_slider(DrumSlider, drum_fad_range, range_width_slider, ref drum_fad_error);
        CheckRangeAndCalculateError_slider(VocalSlider, vocal_fad_range, range_width_slider, ref vocal_fad_error);

        CheckRangeAndCalculateError_knob(BassKnob, bass_knob_range, range_width_knob, ref bass_knob_error);
        CheckRangeAndCalculateError_knob(GuitarKnob, guitar_knob_range, range_width_knob, ref guitar_knob_error);
        CheckRangeAndCalculateError_knob(DrumKnob, drum_knob_range, range_width_knob, ref drum_knob_error);
        CheckRangeAndCalculateError_knob(VocalKnob, vocal_knob_range, range_width_knob, ref vocal_knob_error);

        CheckRangeAndCalculateError_knob(ReverbKnob, reverb_knob_range, range_width_knob, ref reverb_knob_error);
        CheckRangeAndCalculateError_knob(DistortionKnob, distortion_knob_range, range_width_knob, ref distortion_knob_error);
        // CheckRangeAndCalculateError_knob(EchoKnob, echo_knob_range, range_width_knob, ref echo_knob_error);

        total_error = bass_fad_error + guitar_fad_error + drum_fad_error + vocal_fad_error + bass_knob_error + guitar_knob_error + drum_knob_error + vocal_knob_error + reverb_knob_error + distortion_knob_error;
        Debug.Log(total_error);

        // Audience reactions
        HandleAudienceReactions(total_error);
    }

    void UpdatePanFromKnob(AudioSource audioSource, GameObject knob)
    {
        float knobAngle = GetKnobAngle(knob);
        audioSource.panStereo = (knobAngle);
    }

    void UpdateReverbFromKnob(AudioReverbFilter reverbFilter, GameObject knob)
    {
        float knobAngle = GetKnobAngle(knob);
        float reverbValue = Mathf.Clamp((knobAngle), -1.0f, 10.0f);
        reverbFilter.decayTime = ((reverbValue+1)/2*10)-1;
    }

    void UpdateDistortionFromKnob(AudioDistortionFilter distortionFilter, GameObject knob)
    {
        float knobAngle = GetKnobAngle(knob);
        float distortionValue = Mathf.Clamp((knobAngle), -1.0f, 1.0f);
        distortionFilter.distortionLevel = (distortionValue+1)/2;
    }

    void UpdateEchoFromKnob(AudioEchoFilter echoFilter, GameObject knob)
    {
        float knobAngle = GetKnobAngle(knob);
        float echoValue = Mathf.Clamp((knobAngle), 0.0f, 1.0f);
        echoFilter.decayRatio = echoValue;
    }

    float GetKnobAngle(GameObject knob)
    {
        float knobRotation = knob.transform.rotation.z;
        //float knobAngle = knobRotation.eulerAngles.z;
        //if (knobAngle > 180)
        //{
        //    knobAngle = knobAngle - 360;
        //}
        return knobRotation*-1;
    }

    void CheckRangeAndCalculateError_slider(Slider slider, double fad_range, double range_width_slider, ref double fad_error)
    {
        double e = slider.value - fad_range;
        double d = Math.Abs(e);
        double scale;
        scale = Math.Pow(d, 1);

        fad_error += d * scale;

    }

    void CheckRangeAndCalculateError_knob(GameObject knob, double knob_range, double range_width_knob, ref double knob_error)
    {
        float knob_angle = knob.transform.rotation.z;

        float e = knob_angle - (float)knob_range;
        knob_error = Math.Abs(e);
    }

    void HandleAudienceReactions(double error)
    {

    }
}