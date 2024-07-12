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

        // variables to set 'ideal' positions for faders and knobs. Change these as needed!
        double bass_fad_range = 0.7;
        double guitar_fad_range = 0.7;
        double drum_fad_range = 0.7;
        double vocal_fad_range = 0.7;
        double bass_knob_range = 0.7;
        double guitar_knob_range = 0.7;
        double drum_knob_range = 0.7;
        double vocal_knob_range = 0.7;
        double range_width = 0.1;

        // variable that store the error
        double total_error = 0;
        double bass_fad_error = 0;
        double guitar_fad_error = 0;
        double drum_fad_error = 0;
        double vocal_fad_error = 0;
        double bass_knob_error = 0;
        double guitar_knob_error = 0;
        double drum_knob_error = 0;
        double vocal_knob_error = 0;

        // checks if fader is in 'ideal' range and calculates error if not
        if (BassSlider.value > bass_fad_range && BassSlider.value < bass_fad_range+ range_width) ;
        else
            bass_fad_error = Math.Abs(BassSlider.value - bass_fad_range);

        if (GuitarSlider.value > guitar_fad_range && GuitarSlider.value < guitar_fad_range + range_width) ;
        else
            guitar_fad_error = Math.Abs(GuitarSlider.value - guitar_fad_range);

        if (DrumSlider.value > drum_fad_range && DrumSlider.value < drum_fad_range + range_width) ;
        else
            drum_fad_error = Math.Abs(DrumSlider.value - drum_fad_range);

        if (VocalSlider.value > vocal_fad_range && VocalSlider.value < vocal_fad_range + range_width) ;
        else
            vocal_fad_error = Math.Abs(VocalSlider.value - vocal_fad_range);

        // checks if knob is in 'ideal' range and calculates error if not
        if (BassSlider.value > bass_knob_range && BassSlider.value < bass_knob_range + range_width) ;
        else
            bass_knob_error = Math.Abs(BassSlider.value - bass_knob_range);

        if (GuitarSlider.value > guitar_knob_range && GuitarSlider.value < guitar_knob_range + range_width) ;
        else
            guitar_knob_error = Math.Abs(GuitarSlider.value - guitar_knob_range);

        if (DrumSlider.value > drum_knob_range && DrumSlider.value < drum_knob_range + range_width) ;
        else
            drum_knob_error = Math.Abs(DrumSlider.value - drum_knob_range);

        if (VocalSlider.value > vocal_knob_range && VocalSlider.value < vocal_knob_range + range_width) ;
        else
            vocal_knob_error = Math.Abs(VocalSlider.value - vocal_knob_range);

        // calculates total error
        total_error = bass_fad_error + guitar_fad_error + drum_fad_error + vocal_fad_error + bass_knob_error + guitar_knob_error + drum_knob_error + vocal_knob_error;
        Debug.Log(total_error);


    }
}
