using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class UwUManager : MonoBehaviour
{
    public AudioClip[] uwus;

    private AudioSource audioSource;
    private AudioClip playingUwU;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playingUwU = uwus[Random.Range(0, uwus.Length)];
    }

    public void SetAndPlay()
    {
        playingUwU = uwus[Random.Range(0, uwus.Length)];
        
        audioSource.clip = playingUwU;
        audioSource.Play();
    }
}