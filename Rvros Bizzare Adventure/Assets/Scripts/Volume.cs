using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
	public AudioMixer am;

    public void setVolume(float volume){
    	am.SetFloat("MasterVolume",volume);
    }
}
