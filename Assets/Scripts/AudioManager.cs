using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance => _instance;

    AudioSource _audioSource;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioClip(AudioClip clip, float volume = 0.8f, float pitch = 0.8f)
    {

        _audioSource.PlayOneShot(clip, volume);
        
            //Options for volume
            //Options for pitch
    }    

    //Repeatable drone sound (make sure he's loopable)

    //Good reactions on kills
        //Good reaction on wave clear
            //Good reaction on game win

    //Bad reactions when player hurt
        //Gasp when player low health
        //Bad reaction when player dead
}
