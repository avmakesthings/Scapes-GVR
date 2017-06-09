using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof (AudioSource))]
public class AudioPeer : MonoBehaviour {

	AudioSource myAudiosource;
	public static float[] samples = new float[512];
	public static float[] freqBand = new float[8];
	public static float[] bandBuffer = new float[8];
	float[] bufferDecrease = new float[8];
	
	float[] freqBandHighest = new float[8];
	public static float[] audioBand = new float[8];
	public static float[] audioBandBuffer = new float[8];

	public static float amplitude, amplitudeBuffer;
	float amplitudeHighest;

	// Update is called once per frame
	void Update () {
		myAudiosource = GetComponent<AudioSource>();
		GetSpectrumAudioSource();
		MakeFrequencyBands();
		BandBuffer();
		createAudioBands();
		getAmplitude();
	}

	void getAmplitude(){
		float currentAmplitude = 0;
		float currentAmplitudeBuffer = 0;
		for (int i=0; i<8; i++) {
			currentAmplitude += audioBand[i];
			currentAmplitudeBuffer += audioBandBuffer[i];
		}
		if (currentAmplitude > amplitudeHighest){
			amplitudeHighest = currentAmplitude;
		}
		amplitude = currentAmplitude / amplitudeHighest;
		amplitudeBuffer = currentAmplitudeBuffer / amplitudeHighest;
	}

	void createAudioBands(){
		for (int i=0; i<8; i++){
			if(freqBand[i] > freqBandHighest[i]){
				freqBandHighest[i] = freqBand[i];
			}
			audioBand[i] = (freqBand[i]/freqBandHighest[i]);
			audioBandBuffer[i] = (bandBuffer[i]/freqBandHighest[i]);
		}

	}
	void GetSpectrumAudioSource(){
		myAudiosource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
	}

	void BandBuffer(){
		for(int g =0; g<8; g++){
			if(freqBand[g] > bandBuffer[g]){
				bandBuffer[g] = freqBand [g];
				bufferDecrease[g] = 0.005f;
			}
			if(freqBand[g] < bandBuffer[g]){
				bandBuffer[g] -= bufferDecrease[g];
				bufferDecrease[g] *= 1.2f;
			}
		}
	}

	void MakeFrequencyBands(){
		/*
		 * 22050 / 512 = 43 hertz per sample
		 * 20 -60 hertz
		 	60-250 hertz
			250-500 hertz
			500-2000 hertz
			2000-4000 hertz
			4000-6000 hertz
			6000-20000 hertz

		 * 0 - 2 = 86 hertz
		 * 1 - 4 = 172 hertz  -- range 87 to 258
		 * 2 - 8 = 344 hertz  -- range 259 to 602
		 * 3 - 16 = 688 hertz  -- range 603 to 1290
		 * 4 - 32 = 1376 hertz  -- range 1291 to 2666
		 * 5 - 64 = 2752 hertz  -- range 2667 to 5418
		 * 6 - 128 = 5504 hertz  -- range 5419 to 10922
		 * 7 - 256 = 11008 hertz  -- range 10923 to 21930
		 *  == total 510 samples so need to add 2
		 */

		int count = 0;

		for(int i=0; i<8; i++){
			
			float average = 0;
			int sampleCount = (int)Mathf.Pow(2,i) * 2;
			//add remaining 2 samples
			if (i==7){
				sampleCount += 2;
			}
			for(int j =0; j<sampleCount; j++){
				average += samples[count] *(count + 1);
				count ++;
			}
			average /= count;
			freqBand [i] = average * 10;


		}
	}
}
