using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

using TMPro;
using System;

using ASFNAF.Miscelaneus;
using ASFNAF.Discord;
using ASFNAF.Mangle;

public class Journals : MonoBehaviour
{
    [Header("Mangle Data:")]
    public MangleData mangleData;

    #region Classes Esqueleto: ->
    [Serializable]
    public struct Journal {
        [Header("Principais:")]
        public Canvas Canvas;
        public Image Image;

        public Sprite[] Announcements;

        public TMP_Text jumpText;
    };
    #endregion

    #region Variáveis: ->
    [Header("Variáveis Locais:")]
    // publicos:
    public Journal journal = new();
    public Canvas BeatCanvas;

    public Image Fade;

    // privados:
    private bool hasRequestedToSkip = false;

    #endregion

    private void Awake()
    {
        #region Aplicar Mudanças: ->
        if (
            Video.Resolution[mangleData.settings.video.resolutionIndex].x >= 640 &&
            Video.Resolution[mangleData.settings.video.resolutionIndex].y >= 480
        )
        {
            if (
                Screen.currentResolution.width != Video.Resolution[mangleData.settings.video.resolutionIndex].x || 
                Screen.currentResolution.height != Video.Resolution[mangleData.settings.video.resolutionIndex].y
            )
                Screen.SetResolution(
                    Video.Resolution[mangleData.settings.video.resolutionIndex].x, 
                    Video.Resolution[mangleData.settings.video.resolutionIndex].y,
                    mangleData.settings.video.windowMode
                );

            journal.Canvas.GetComponent<CanvasScaler>().referenceResolution = mangleData.settings.video.constantArea ? new Vector2Int(1366, 768) : Video.Resolution[mangleData.settings.video.resolutionIndex];
        }
        else
            MangleFiles.ShowError(4);

        if (Video.FramesPerSecond[mangleData.settings.video.framesPerSecondIndex] < -1)
            MangleFiles.ShowError(42);

        if (!mangleData.settings.video.vsync)
            Application.targetFrameRate = Video.FramesPerSecond[mangleData.settings.video.framesPerSecondIndex];

        QualitySettings.vSyncCount = mangleData.settings.video.vsync ? 1 : 0;

        if (mangleData.mangle.night != 1)
            mangleData.mangle.night = 1;            

        journal.jumpText.text = MangleLanguage.Get(mangleData.settings.language.language).SkipText;
        #endregion
    }

    private void Start()
    {
        if (!Fade.enabled)
            Fade.enabled = true;

        if (!journal.jumpText.enabled)
            journal.jumpText.enabled = true;

        RichPresence.SetupClient();
        RichPresence.SetupActivity();

        RichPresence.SetDetails(MangleLanguage.Get(mangleData.settings.language.language).discord.Journals);

        StartCoroutine(Effects.Fade_Play(Fade, null, 10f, () => hasRequestedToSkip, "PreparingToNight"));
    }

    public void PlayerOnSkip(InputAction.CallbackContext context)
    {
        if (!context.action.triggered)
            return;

        hasRequestedToSkip = true;
    }
}