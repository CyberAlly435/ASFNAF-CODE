using System;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using ASFNAF.Mangle;

using TMPro;
using ASFNAF.Discord;
using Discord.Sdk;

using System.Collections;

public class Settings : Extras
{
    [Header("Mangle Data")]
    public MangleData mangleData;

    [Header("Title System")]
    public MainTitle title;

    [Header("Settings")]
    [SerializeField] protected Extras_Settings settings;
    [SerializeField] private Image currentSettings;

    public Image indicator;

    #region Atalhos

    #region Video
    private int ResolutionIndex
    {
        set
        {
            mangleData.settings.video.resolutionIndex = value;

            title.UpdateScreenResolution();
        }
    }

    private FullScreenMode WindowMode
    {
        set
        {
            mangleData.settings.video.windowMode = value;

            title.UpdateScreenResolution();
        }
    }

    private bool VideoArea
    {
        set
        {
            mangleData.settings.video.constantArea = value;

            title.UpdateScreenResolution();
        }
    }

    private int FPSIndex
    {
        set
        {
            mangleData.settings.video.framesPerSecondIndex = value;

            title.UpdateScreenVsyncFPS();
        }
    }

    private bool Vsync
    {
        set
        {
            mangleData.settings.video.vsync = value;

            title.UpdateScreenVsyncFPS();
        }
    }
    #endregion

    #region Language
    private LanguageID languageID
    {
        set
        {
            mangleData.settings.language.language = value;

            title.UpdateLanguageID();
            LanguageUpdate();
        }
    }
    #endregion

    #region Features
    private bool Discord
    {
        set
        {
            IEnumerator WaitForDiscord()
            {
                while (RichPresence.GetStatus() != Client.Status.Connected)
                {
                    settings.Screen.featuresButtons.Status.text = $"Status: {RichPresence.GetStatus() ?? Client.Status.Disconnected}";

                    yield return new WaitForFixedUpdate();
                }

                settings.Screen.featuresButtons.Status.text = $"Status: {(RichPresence.GetAvaliable() ? "Connected" : "Disconnected")}";

                yield return null;
            }

            if (value == true)
            {
                RichPresence.SetupClient();
                RichPresence.SetupActivity();

                RichPresence.SetDetails(MangleLanguage.Get(mangleData.settings.language.language).settings.DiscordDetails);
            }
            else
                RichPresence.ForceKill();

            StartCoroutine(WaitForDiscord());

            mangleData.settings.features.discordSync = value;
        }
    }
    #endregion

    #endregion

    // Voids do script
    private void Awake() =>
        LanguageUpdate();

    private void Start()
    {
        currentSettings = settings.Screen.Language;
        currentSettings.gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (settings.Screen.featuresButtons.Time.isActiveAndEnabled)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(mangleData.settings.features.gameTime);

            settings.Screen.featuresButtons.Time.text =
                string.Format(MangleLanguage.Get(mangleData.settings.language.language).settings.featuresSettings.TimeText, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }
    }

    // Voids para selecionar as configurações

    #region Buttons
    public void OnButtonHover(BaseEventData eventData)
    {
        PointerEventData pointer = eventData as PointerEventData;
        Button button = pointer.pointerEnter.GetComponentInParent<Button>();

        Vector2 position = indicator.rectTransform.anchoredPosition;

        indicator.transform.SetParent(button.transform);
        indicator.rectTransform.anchoredPosition = position;
    }

    public void OnOptionSelected(BaseEventData eventData)
    {
        PointerEventData pointer = eventData as PointerEventData;
        Button selectedButton = pointer.pointerClick.GetComponentInParent<Button>();

        currentSettings.gameObject.SetActive(false);
        currentSettings = null;

        switch (selectedButton.name)
        {
            case "Video":
                if (!settings.Screen.Video.isActiveAndEnabled)
                {
                    settings.Screen.Video.gameObject.SetActive(true);
                    currentSettings = settings.Screen.Video;
                }
                break;

            case "Audio":
                if (!settings.Screen.Audio.isActiveAndEnabled)
                {
                    settings.Screen.Audio.gameObject.SetActive(true);
                    currentSettings = settings.Screen.Audio;
                }
                break;

            case "Language":
                if (!settings.Screen.Language.isActiveAndEnabled)
                {
                    settings.Screen.Language.gameObject.SetActive(true);
                    currentSettings = settings.Screen.Language;
                }
                break;

            case "Controls":
                if (!settings.Screen.Controls.isActiveAndEnabled)
                {
                    settings.Screen.Controls.gameObject.SetActive(true);
                    currentSettings = settings.Screen.Controls;
                }
                break;

            case "Features":
                if (!settings.Screen.Features.isActiveAndEnabled)
                {
                    settings.Screen.Features.gameObject.SetActive(true);
                    currentSettings = settings.Screen.Features;
                }
                break;

            default:
                break;
        }
    }
    #endregion

    // Voids das configurações

    #region Video
    public void SetVideoResolution(int integer) => ResolutionIndex = integer;
    public void SetVideoWindow(int integer) => WindowMode = (FullScreenMode)integer;
    public void SetVideoArea(bool boolean) => VideoArea = boolean;

    public void SetVideoFPS(int integer) => FPSIndex = integer;
    public void SetVideoVsync(bool boolean) => Vsync = boolean;

    public void SetVideoProcessing(bool boolean) => mangleData.settings.video.postProcessing = boolean;
    #endregion

    #region Audio
    public void SetAudioMain(float single) => mangleData.settings.audio.main = single;
    public void SetAudioMusic(float single) => mangleData.settings.audio.music = single;
    public void SetAudioAmbient(float single) => mangleData.settings.audio.ambient = single;
    public void SetAudioVoice(float single) => mangleData.settings.audio.voice = single;

    public void SetAudioMute(bool boolean) => mangleData.settings.audio.mute = boolean;
    #endregion

    #region Language
    public void SetLanguageID(int integer) => languageID = (LanguageID)integer;
    public void SetLanguageVoicer(int integer) => mangleData.settings.language.voicer = (Voicer)integer;
    public void SetLanguageSubtitles(bool boolean) => mangleData.settings.language.subtitles = boolean;
    public void SetLanguageSystemSync(bool boolean) => mangleData.settings.language.systemSync = boolean;
    #endregion

    #region Features
    public void SetFeatureDiscord(bool boolean) => Discord = boolean;
    #endregion

    // Voids para atualizar a linguagem
    private void LanguageUpdate()
    {
        #region Video

        #region Atualizar Valores
        // Janelas
        settings.Screen.videoButtons.Resolution.value = mangleData.settings.video.resolutionIndex;
        settings.Screen.videoButtons.WindowMode.value = (int)mangleData.settings.video.windowMode;
        settings.Screen.videoButtons.ConstantSize.isOn = mangleData.settings.video.constantArea;
        // FPS
        settings.Screen.videoButtons.FPS.value = mangleData.settings.video.framesPerSecondIndex;
        settings.Screen.videoButtons.Vsync.isOn = mangleData.settings.video.vsync;
        // Pós processamento
        settings.Screen.videoButtons.PostProcessing.isOn = mangleData.settings.video.postProcessing;
        #endregion

        #region Atualizar Idioma
        // Janelas
        settings.Screen.videoButtons.Resolution.transform.Find("OutsideText").GetComponent<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).settings.videoSettings.ResolutionTitle;

        settings.Screen.videoButtons.WindowMode.transform.Find("OutsideText").GetComponent<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).settings.videoSettings.WindowModeTitle;

        foreach (TMP_Dropdown.OptionData item in settings.Screen.videoButtons.WindowMode.options)
        {
            item.text = MangleLanguage.Get(mangleData.settings.language.language).settings.videoSettings.WindowModeOptions[settings.Screen.videoButtons.WindowMode.options.IndexOf(item)];
        }

        settings.Screen.videoButtons.ConstantSize.transform.Find("Text").GetComponent<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).settings.videoSettings.ConstantSizeTitle;

        // FPS
        settings.Screen.videoButtons.FPS.transform.Find("OutsideText").GetComponent<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).settings.videoSettings.FPSTitle;

        settings.Screen.videoButtons.FPS.options[0].text =
            MangleLanguage.Get(mangleData.settings.language.language).settings.videoSettings.FPSUnlimitedOption;

        settings.Screen.videoButtons.Vsync.transform.Find("Text").GetComponent<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).settings.videoSettings.VsyncTitle;

        // Pós processamento
        settings.Screen.videoButtons.PostProcessing.transform.Find("Text").GetComponent<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).settings.videoSettings.PostProcessingTitle;
        #endregion

        #endregion

        #region Audio

        #region Atualizar Valores
        // Controlador de Volume
        settings.Screen.audioButtons.Main.value = mangleData.settings.audio.main;
        settings.Screen.audioButtons.Music.value = mangleData.settings.audio.music;
        settings.Screen.audioButtons.Ambient.value = mangleData.settings.audio.ambient;
        settings.Screen.audioButtons.Voice.value = mangleData.settings.audio.voice;

        // Mute
        settings.Screen.audioButtons.Mute.isOn = mangleData.settings.audio.mute;
        #endregion

        #region Atualizar Idioma
        // Controlador de Volume
        settings.Screen.audioButtons.Main.transform.Find("Text").GetComponent<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).settings.audioSettings.mainTitle;

        settings.Screen.audioButtons.Music.transform.Find("Text").GetComponent<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).settings.audioSettings.musicTitle;

        settings.Screen.audioButtons.Ambient.transform.Find("Text").GetComponent<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).settings.audioSettings.ambeintTitle;

        settings.Screen.audioButtons.Voice.transform.Find("Text").GetComponent<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).settings.audioSettings.voiceTitle;

        // Mute
        settings.Screen.audioButtons.Mute.transform.Find("Text").GetComponent<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).settings.audioSettings.muteTitle;
        #endregion

        #endregion

        #region Language

        #region Atualizar Valores
        // Dropdown
        settings.Screen.languageButtons.Language.value = (int)mangleData.settings.language.language;
        settings.Screen.languageButtons.Voicer.value = (int)mangleData.settings.language.voicer;

        // Toggles
        settings.Screen.languageButtons.Subtitles.isOn = mangleData.settings.language.subtitles;
        settings.Screen.languageButtons.SystemSync.isOn = mangleData.settings.language.systemSync;
        #endregion

        #region Atualizar Idioma
        // Dropdown
        settings.Screen.languageButtons.Language.transform.Find("OutsideText").GetComponent<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).settings.languageSettings.LanguageTitle;

        settings.Screen.languageButtons.Voicer.transform.Find("OutsideText").GetComponent<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).settings.languageSettings.VoicerTitle;

        // Toggles
        settings.Screen.languageButtons.Subtitles.transform.Find("Text").GetComponent<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).settings.languageSettings.SubtitlesTitle;

        settings.Screen.languageButtons.SystemSync.transform.Find("Text").GetComponent<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).settings.languageSettings.SystemSyncTitle;
        #endregion

        #endregion

        #region Controls
        settings.Screen.controlsButtons.controls.text =
            MangleLanguage.Get(mangleData.settings.language.language).settings.controlsSettings.ControlsText;
        #endregion

        #region Features

        #region Atualizar Valores
        settings.Screen.featuresButtons.RichPresence.isOn = mangleData.settings.features.discordSync;
        #endregion

        #region Atualizar Idioma
        settings.Screen.featuresButtons.RichPresence.transform.Find("Text").GetComponent<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).settings.featuresSettings.DiscordTitle;

        settings.Screen.featuresButtons.Status.text =
            RichPresence.GetStatus() == Client.Status.Connected ? "Status: Connected" : "Status: Disconnected";

        settings.Screen.featuresButtons.Status.text =
            MangleLanguage.Get(mangleData.settings.language.language).settings.featuresSettings.DiscordStatus[mangleData.settings.features.discordSync ? 0 : 1];

        settings.Screen.featuresButtons.Activity.text =
            MangleLanguage.Get(mangleData.settings.language.language).settings.featuresSettings.TimeActivity;
        #endregion

        #endregion

        RichPresence.SetDetails(MangleLanguage.Get(mangleData.settings.language.language).settings.DiscordDetails);

        settings.Title.text =
            MangleLanguage.Get(mangleData.settings.language.language).settings.Title;

        settings.Button.Video.GetComponentInChildren<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).settings.videoButton;

        settings.Button.Audio.GetComponentInChildren<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).settings.audioButton;

        settings.Button.Language.GetComponentInChildren<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).settings.languageButton;

        settings.Button.Controls.GetComponentInChildren<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).settings.controlsButton;

        settings.Button.Features.GetComponentInChildren<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).settings.featuresButton;

        settings.Button.Exit.GetComponentInChildren<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).selector.Exit;
    }
}