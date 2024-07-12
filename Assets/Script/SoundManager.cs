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
        BassAudioSource.volume = BassSlider.value;
        GuitarAudioSource.volume = GuitarSlider.value;
        DrumAudioSource.volume = DrumSlider.value;
        VocalAudioSource.volume = VocalSlider.value;

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
        switch (total_error)
        {
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
            case 6:

                break;
            case 7:

                break;
            case 8:

                break;
        }
    }
}
