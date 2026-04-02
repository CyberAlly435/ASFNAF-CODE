using System;

using UnityEngine;

namespace ASFNAF.Mangle;

#region Garbage

public static class Video
{
    public static Vector2Int[] Resolution { get; } = new Vector2Int[7]
    {
        new(1024,768), // XGA
        new(1280,720), // HD
        new(1280,960), // Standard HD
        new(1600,1200), // UXGA
        new(1920,1080), // Full HD
        new(2560,1440), // Quad HD
        new(3840,2160) // 4K
    };

    public static int[] FramesPerSecond { get; } = new int[9]
    {
        -1, // Ilimitado
        240,
        180,
        144,
        120,
        90,
        75,
        60,
        50
    };
}

public enum ImageQuality
{
    Trilinear,
    Bilinear,
    Point,
}


public enum LanguageID
{
    Portuguese,
    Japanese,
    English
}

public enum Voicer
{
    Imperatriz,
    Angelo
}

#endregion

[Serializable]
public struct SettingsDataStruct
{
    [Serializable]
    public struct Video
    {
        public int resolutionIndex;
        public int framesPerSecondIndex;
        public bool constantArea;
        public bool vsync;
        public bool postProcessing;
        public FullScreenMode windowMode;
        public ImageQuality quality;
    }

    [Serializable]
    public struct Audio
    {
        public float main;
        public float music;
        public float ambient;
        public float voice;
        public bool mute;
    }

    [Serializable]
    public struct Language
    {
        public LanguageID language;
        public Voicer voicer;

        public bool subtitles;
        public bool systemSync;
    }

    [Serializable]
    public struct Features
    {
        public bool discordSync;
        public double gameTime;
    }

    public Video video;
    public Audio audio;
    public Language language;
    public Features features;
}