using System;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace ASFNAF.Game.Title
{
    public class Extras : MonoBehaviour
    {
        [Serializable]
        protected struct Extras_Settings
        {
            [Serializable]
            public struct Buttons
            {
                // Botões Superiores
                public Button Video;
                public Button Audio;
                public Button Language;
                public Button Controls;
                public Button Features;

                // Botões Inferiores
                public Button Exit;
            };

            [Serializable]
            public struct Screens
            {
                [Serializable]
                public struct VideoButtons
                {
                    public TMP_Dropdown Resolution;
                    public TMP_Dropdown WindowMode;
                    public Toggle ConstantSize;

                    public TMP_Dropdown FPS;
                    public Toggle Vsync;

                    public Toggle PostProcessing;
                }

                [Serializable]
                public struct AudioButtons
                {
                    public Slider Main;
                    public Slider Music;
                    public Slider Ambient;
                    public Slider Voice;

                    public Toggle Mute;
                }

                [Serializable]
                public struct ControlsButtons
                {
                    public TMP_Text controls;
                }

                [Serializable]
                public struct LanguageButtons
                {
                    public TMP_Dropdown Language;
                    public TMP_Dropdown Voicer;

                    public Toggle Subtitles;
                    public Toggle SystemSync;
                }

                [Serializable]
                public struct FeaturesButtons
                {
                    public Toggle RichPresence;
                    public TMP_Text Status;

                    public TMP_Text Activity;
                    public TMP_Text Time;
                }

                public VideoButtons videoButtons;
                public AudioButtons audioButtons;
                public LanguageButtons languageButtons;
                public ControlsButtons controlsButtons;
                public FeaturesButtons featuresButtons;

                public Image Video;
                public Image Audio;
                public Image Language;
                public Image Controls;
                public Image Features;
            }

            public Buttons Button;
            public Screens Screen;

            [Header("Title")]
            public TMP_Text Title;
        };

        [Serializable]
        protected struct Extras_Layers
        {
            public Image animatronics;
            public Image interview;
            public Image minigames;
            public Image extras;
            public Image cheats;

            public Image setting;

            public Image Selector;

        }

        [Header("Extras")]
        public Canvas extrasCanvas;

        public AudioSource audioSource;
        public AudioClip coinFlip;

        public void OnCoinFlip() =>
            audioSource.PlayOneShot(coinFlip);
    }
}