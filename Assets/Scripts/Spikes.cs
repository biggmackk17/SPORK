using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField]
    private int _damageAmount = 5;
    [SerializeField]
    private float _damageDelay = 3.0f;

    private bool _sawDamageAudioCooldown = false;

    [SerializeField]
    private Vector3 _rotSpeed;

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _sliceSound;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.localRotation *= Quaternion.Euler(_rotSpeed * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out var target))
        {
            target.TakeDamage(_damageAmount * Time.deltaTime);
            if(!_sawDamageAudioCooldown)
                StartCoroutine(SawDamageAudio());
        }
    }

    private IEnumerator SawDamageAudio()
    {
        _sawDamageAudioCooldown = true;
        _audioSource.PlayOneShot(_sliceSound); //Play slice locally since enemies hitting saw should be quieter if player is far away
        yield return new WaitForSeconds(_sliceSound.length);
        _sawDamageAudioCooldown = false;
    }
}
    
