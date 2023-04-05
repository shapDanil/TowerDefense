using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource _audioSource;

    [SerializeField]
    AudioClip[] allClips;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        _audioSource = CameraController.instance.gameObject.GetComponent<AudioSource>();
    }


    public void Play(string Tag)
    {
        switch (Tag)
        {
            case "select":
                {
                    _audioSource.PlayOneShot(allClips[Random.Range(2,8)]);
                    break;
                }
                
            case "reserche":
                {
                    _audioSource.PlayOneShot(allClips[Random.Range(17, 19)]);
                    break;
                }
            case "base":
                {
                    _audioSource.PlayOneShot(allClips[Random.Range(8, 11)]);
                    break;
                }
            case "move":
                {
                    _audioSource.PlayOneShot(allClips[Random.Range(11, 15)]);
                    break;
                }
            case "sos":
                {
                    _audioSource.PlayOneShot(allClips[19]);
                    break;
                }
            case "money":
                {
                    _audioSource.PlayOneShot(allClips[Random.Range(15, 17)]);
                    break;
                }
        }

    }
}
