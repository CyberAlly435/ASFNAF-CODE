using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using TMPro;

using ASFNAF.Miscelaneus;
using ASFNAF.Discord;
using ASFNAF.Mangle;

public class Warning : MonoBehaviour
{
    // Atualizado em 1/25/2026

    #region Variables
    [Header("Mangle Files")]
    public MangleData mangleData;

    [Header("Warning UI:")]
    public Canvas MainCanvas;
    public TMP_Text WarningText;
    public Image FadeImage;
    
    private bool hasRequestedToSkip = false;

    private void Awake()
    {
        if (!MangleFiles.GetFileState())
            SceneManager.LoadSceneAsync("FirstTime");

        if (
            Video.Resolution[mangleData.settings.video.resolutionIndex].x >= 800 && 
            Video.Resolution[mangleData.settings.video.resolutionIndex].y >= 600
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

            MainCanvas.GetComponent<CanvasScaler>().referenceResolution = mangleData.settings.video.constantArea ? new Vector2Int(1366, 768) : Video.Resolution[mangleData.settings.video.resolutionIndex];
        }
        else
            MangleFiles.ShowError(4);

        if (Video.FramesPerSecond[mangleData.settings.video.framesPerSecondIndex] < -1)
            MangleFiles.ShowError(42);

        if (!mangleData.settings.video.vsync)
            Application.targetFrameRate = Video.FramesPerSecond[mangleData.settings.video.framesPerSecondIndex];

        QualitySettings.vSyncCount = mangleData.settings.video.vsync ? 1 : 0;

        // -- # ABAIXO ESTARÃO AS ATUALIZAÇÕES QUE NÃO NECESSITAM DE COMPARADORES # -- //

        WarningText.text = MangleLanguage.Get(mangleData.settings.language.language).WarningText;
        #endregion
    }

    private void Start()
    {
        RichPresence.SetupClient();
        RichPresence.SetupActivity();

        RichPresence.SetDetails(MangleLanguage.Get(mangleData.settings.language.language).discord.Warning);

        StartCoroutine(Effects.Fade_Play(FadeImage, null, 5f, () => hasRequestedToSkip, Random.Range(0, 10000) == 1 ? "Rares" : "Title"));
    }

    public void PlayerOnSkip(InputAction.CallbackContext context) => hasRequestedToSkip = true;
}