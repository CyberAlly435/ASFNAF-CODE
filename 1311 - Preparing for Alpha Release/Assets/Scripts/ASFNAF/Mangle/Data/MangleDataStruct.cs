using System;

namespace ASFNAF.Mangle;

[Serializable]
public struct MangleDataStruct
{
    [Serializable]
    public struct Stars
    {
        // Estrelas principais
        public bool Five;
        public bool Six;
        public bool CustomNight;

        // Estrelas extras
        public bool GoodEnding;
        public bool AllStar;
    }

    public Stars stars;

    public int night;
    public int minigame;
}