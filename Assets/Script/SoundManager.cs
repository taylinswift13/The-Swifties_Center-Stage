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

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        BassAudioSource.volume = BassSlider.value;
        GuitarAudioSource.volume = GuitarSlider.value;
        DrumAudioSource.volume = DrumSlider.value;
        VocalAudioSource.volume = VocalSlider.value;

        BassAudioSource.panStereo = Math.Abs(BassKnob.transform.rotation.z/360);

        // variables to set 'ideal' positions for faders and knobs. Change these as needed!
        double range_width = 0.1;

        double bass_fad_range = 0.7, guitar_fad_range = 0.7, drum_fad_range = 0.7, vocal_fad_range = 0.7;
        double bass_knob_range = 0.7, guitar_knob_range = 0.7, drum_knob_range = 0.7, vocal_knob_range = 0.7;

        // Variables to store individual errors
        double bass_fad_error = 0, guitar_fad_error = 0, drum_fad_error = 0, vocal_fad_error = 0;
        double bass_knob_error = 0, guitar_knob_error = 0, drum_knob_error = 0, vocal_knob_error = 0;

        // variables that store the score
        double high_score = 0;

        // Helper method to check range and calculate error
        void CheckRangeAndCalculateError(Slider slider, double idealRange, double rangeWidth, ref double error)
        {
            if (slider.value > idealRange && slider.value < idealRange + rangeWidth)
            {

            }
            else
            {
                error = Math.Abs(slider.value - idealRange);
            }
        }

        // Check fader ranges
        CheckRangeAndCalculateError(BassSlider, bass_fad_range, range_width, ref bass_fad_error);
        CheckRangeAndCalculateError(GuitarSlider, guitar_fad_range, range_width, ref guitar_fad_error);
        CheckRangeAndCalculateError(DrumSlider, drum_fad_range, range_width, ref drum_fad_error);
        CheckRangeAndCalculateError(VocalSlider, vocal_fad_range, range_width, ref vocal_fad_error);

        // Check knob ranges
        CheckRangeAndCalculateError(BassSlider, bass_knob_range, range_width, ref bass_knob_error);
        CheckRangeAndCalculateError(GuitarSlider, guitar_knob_range, range_width, ref guitar_knob_error);
        CheckRangeAndCalculateError(DrumSlider, drum_knob_range, range_width, ref drum_knob_error);
        CheckRangeAndCalculateError(VocalSlider, vocal_knob_range, range_width, ref vocal_knob_error);

        // Calculate total error
        double total_error = bass_fad_error + guitar_fad_error + drum_fad_error + vocal_fad_error + bass_knob_error + guitar_knob_error + drum_knob_error + vocal_knob_error;
        Debug.Log(total_error);

        // calculates the high score
        //Debug.Log(high_score);

        // controls audience reactions based on error
        if (total_error < 0.5)
        {
            Debug.Log("Good!");
        }
        else if (total_error < 1.0)
        {
            // Handle case for total_error between 0.5 and 1.0
            Debug.Log("Okay!");
        }
        else if (total_error < 1.5)
        {
            // Handle case for total_error between 1.0 and 1.5
        }
        else if (total_error < 2.0)
        {
            // Handle case for total_error between 1.5 and 2.0
        }
        else if (total_error < 2.5)
        {
            // Handle case for total_error between 2.0 and 2.5
        }
        else if (total_error < 3.0)
        {
            // Handle case for total_error between 2.5 and 3.0
        }
        else if (total_error < 3.5)
        {
            // Handle case for total_error between 3.0 and 3.5
        }
        else if (total_error < 4.0)
        {
            // Handle case for total_error between 3.5 and 4.0
        }
        else if (total_error < 4.5)
        {
            // Handle case for total_error between 4.0 and 4.5
        }
        else if (total_error < 5.0)
        {
            // Handle case for total_error between 4.5 and 5.0
        }

    }
}
