using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance => _instance;

    [SerializeField] private GameObject _audioMusic;
    [SerializeField] private GameObject _audioAudience;

    public AudioClip[] _reactionClips; //0-4: Applaud, Yeah, Gasp, Owch, Kill

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

    public void CombatMusic(bool isActive)
    {
        _audioMusic.SetActive(isActive);
    }

    //Repeating music
    //Repeatable drone sound (make sure he's loopable)
        //

    //Good reactions on kills
        //Good reaction on wave clear
            //Good reaction on game win

    //Bad reactions when player hurt
        //Gasp when player low health
        //Bad reaction when player dead
}
