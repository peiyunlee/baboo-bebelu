using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    protected IGameSystem parent = null;
    public IGameSystem Parent { set { if (parent == null) parent = value; } }
    AudioSource song;
    float songPosition;
    public bool isSongPlay;
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
    }
    public void PauseSong(){
        song.Pause();
    }
    public void StopSong(){
        song.Stop();
    }
}