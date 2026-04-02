using System.Collections;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using ASFNAF.Discord;
using ASFNAF.Miscelaneus;
using ASFNAF.Mangle;

public class PreparingToNight : MonoBehaviour
{
    [Header("Mangle Files:")]
    public MangleData mangleData;

    [Header("Váriaveis locais:")]
    public Canvas PreparingToNightUI;
    public TMP_Text nightText;
    public Image clockImage;
    public Image fadeImage;
    private bool clockStep;

    private void Awake()
    {
        if (
            Video.Resolution[mangleData.settings.video.resolutionIndex].x >= 640 &&
            Video.Resolution[mangleData.settings.video.resolutionIndex].y >= 480)
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

            PreparingToNightUI.GetComponent<CanvasScaler>().referenceResolution = mangleData.settings.video.constantArea ? new Vector2Int(1366, 768) : Video.Resolution[mangleData.settings.video.resolutionIndex];
        }
        else
            MangleFiles.ShowError(4);

        if (Video.FramesPerSecond[mangleData.settings.video.framesPerSecondIndex] < -1)
            MangleFiles.ShowError(42);

        if (!mangleData.settings.video.vsync)
            Application.targetFrameRate = Video.FramesPerSecond[mangleData.settings.video.framesPerSecondIndex];

        QualitySettings.vSyncCount = mangleData.settings.video.vsync ? 1 : 0;

        if (clockImage.isActiveAndEnabled)
            clockImage.gameObject.SetActive(false);

        nightText.text = string.Format(MangleLanguage.Get(mangleData.settings.language.language).WhatNight, mangleData.mangle.night);
    }

    private void Start()
    {
        RichPresence.SetupClient();
        RichPresence.SetupActivity();

        RichPresence.SetDetails(string.Format(MangleLanguage.Get(mangleData.settings.language.language).discord.Waiting, mangleData.mangle.night));

        StartCoroutine(Initializer());
    }

    private IEnumerator Initializer()
    {
        // inicia o fade in;
        StartCoroutine(Effects.Fade(fadeImage, false, 1f));

        yield return new WaitForSeconds(5f);

        // inicia o fade out e habilita o relógio;
        StartCoroutine(Effects.Fade(fadeImage, true, 1f));

        yield return new WaitForSeconds(1f);

        clockImage.gameObject.SetActive(true);

        StartCoroutine(Effects.LoadScene("Main", 2f));
    }
}