using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource song;
    float songPosition;
    bool isSongPlay;
    public float GetSongPosition { get { return songPosition; } }

    void Start()
    {
        song = GameObject.Find("SongAudioSource").GetComponent<AudioSource>();
        isSongPlay=false;
    }

    void Update()
    {
        if(isSongPlay)
            songPosition = song.time;
    }
    public void PlaySong(){
        song.Play();
        isSongPlay=true;
        GameManager.IsSongPlay=true;
    }
    public void PauseSong(){
        song.Pause();
    }
    public void StopSong(){
        song.Stop();
    }
}