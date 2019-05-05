using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumAnalyzer : MonoBehaviour 
{
    AudioSource audioSource;
    public static float[] samples = new float[512];
    public static float[] freqBand = new float[8];
    float[] freqBandHighest = new float[8];
    public static float[] audioBands = new float[8];

    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();

	}
	void GetSpectrumAudioSource() 
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);

    }
	// Update is called once per frame
	void Update ()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        CreateAudioBands();
    }

    void MakeFrequencyBands()
    {
        int count = 0;
        //Iterate through the 8 bins
        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i + 1);

            //Adding the remaining two samples into teh last bin.
            if(i == 7)
            {
                sampleCount += 2; 
            }

            for (int j = 0; j < sampleCount; j++)
            {
                average += samples[count];
                count++;
            }
            //Divide to create the average, and scale it appropriately
            average /= count;
            freqBand[i] = (i + 1) * 100 * average;

        }
    }

    void CreateAudioBands()
    {
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            if(i==7)
            {
                sampleCount += 2;
            }
            for (int j = 0; j < sampleCount; j++)
            {
                average += samples[count] * (count + 1);
                count++;
            }
            average /= count;
            freqBand[i] = average * 10;
        }

    }
}
