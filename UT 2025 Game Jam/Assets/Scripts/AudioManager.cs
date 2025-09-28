using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour 
{
    public AudioClip[] songs;

    private AudioSource songPlayer;
    private AudioClip playingSong;
    private int index;

    void Start()
    {
        songPlayer = GetComponent<AudioSource>();
        index = Random.Range(0, songs.Length);
        playingSong = songs[index];
        SetAndPlay();
    }

    void Update()
    {
        if (!songPlayer.isPlaying)
        {
            SetAndPlay();
        }
    }

    public void SetAndPlay()
    {
        if (index < songs.Length - 1)
        {
            index++;
            playingSong = songs[index];
        }
        else
        {
            index = 0;
            playingSong = songs[index];
        }
        songPlayer.clip = playingSong;
        songPlayer.Play();
    }
}