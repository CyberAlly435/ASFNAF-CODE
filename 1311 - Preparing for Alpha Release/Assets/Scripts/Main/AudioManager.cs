using System;

using UnityEngine;

[Serializable]
public class AudioManager
{   
    // Audio Master
    [Header("Esquema Principal")]
    public AudioSource mainSource;
    public AudioListener mainListener;
    
    // caixa de musica
    public enum MusicBoxSongs
    {
        JingleBells,
        SilentNight,
        SwanLake
    }
    
    public class PuppetSongCameraListener
    {
        public float volume;
        public float headphoneBalance;

        public PuppetSongCameraListener(float vol, float balance)
        {
            volume = vol;
            headphoneBalance = balance;
        }
    }

    [System.Serializable]
    public class Songs
    {
        public MusicBoxSongs musicBoxSongs;
        public AudioClip song;
        public bool isSongLoopable;

        [Range(0f, 1f)]
        public float volume;
    }

    public int currentMusicBox;

    [Header("Esquema da câmera 12")]
    public AudioSource musicbox;
    public AudioSource windup;
    public AudioSource outofbox;
    public Songs[] puppetSongs;
}