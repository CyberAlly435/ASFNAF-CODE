using ASFNAF;
using ASFNAF.Discord;
using ASFNAF.Mangle;
using ASFNAF.Miscelaneus;

using Discord.Sdk;

using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

using Random = UnityEngine.Random;

namespace ASFNAF.Game.Title
{

    public class MainTitle : MonoBehaviour
    {
        /// <summary>
        /// Logs de atualizações essenciais do Title
        /// 
        /// 11 / 15 / 2025: Refazer todo o script para otimização.
        /// 
        /// Esquece: Classes auxiliadoras não deram certo -> {
        ///     A primeira coisa que eu vou fazer é criar classes auxiliadoras
        ///     para cada Canvas, facilitando a indexação dos elementos a partir
        ///     do Unity 6.
        /// }
        /// 
        /// 11 / 21 / 2025: Atualização essencial dos scripts para uma otimização ainda maior.
        /// 
        /// Hoje, eu adicionei um limitador de FPS dentro do jogo para que ele puxe apenas
        /// o necessário da placa de vídeo do jogador e nada mais.
        /// 
        /// Rapaz, o script está bom todo, não preciso mais mexer em nada por enquanto.
        /// 
        /// </summary>

        [Header("MangleData")]
        public MangleData mangleData;

        #region Variáveis: ->
        #region Canvas
        [Header("Canvas:")]
        [Tooltip("Teste")]
        public Camera titleCamera;

        public Canvas AnimatronicUI;
        public Canvas TitleUI;
        public Canvas ExtrasUI;
        public Canvas CreditsUI;

        private Canvas _lastCanvasEnabled;
        private Canvas lastCanvasEnabled
        {
            get { return _lastCanvasEnabled; }
            set
            {
                _lastCanvasEnabled = value;

                if (RichPresence.GetStatus() != Client.Status.Connected)
                {
                    if (value == TitleUI)
                        RichPresence.SetDetails(MangleLanguage.Get(mangleData.settings.language.language).discord.Title);
                    else if (value == ExtrasUI)
                        RichPresence.SetDetails(MangleLanguage.Get(mangleData.settings.language.language).discord.Extras);
                    else if (value == CreditsUI)
                        RichPresence.SetDetails(MangleLanguage.Get(mangleData.settings.language.language).discord.Credits);
                }
            }
        }
        #endregion

        #region Sprite, Imagens e Textos
        [Header("Sprite, Imagens e Textos:")]
        [SerializeField]
        private int maxAnimatronicsRange = 50;
        [SerializeField] private int xAnimatroncs = 3;
        [SerializeField] private int yAnimatronics = 3;
        private int AnimatronicsAlterable = 0;
        private int AnimatronicsChanged = 0;
        private Sprite lastAnimatronicFrame;
        public Sprite[] AnimatronicsFrames;
        public Image AnimatronicsImage;
        public Image FadeImage;

        private int starIndex;
        public Image[] stars;

        public Button Newgame;
        public Button Continue;
        public Button SixNight;
        public Button CustomNight;
        public Button Extras;
        public Button Credits;

        public TMP_Text DeleteText;
        public TMP_Text Pointer;
        public TMP_Text ExitText;
        public TMP_Text NightText;

        private bool disableRefresh = false;
        #endregion

        #region Áudio e Sons
        [Header("Áudio e Sons:")]
        public AudioSource audioSource;
        public AudioClip blipSound;

        public enum AvalaibleSongs
        {
            Serenade,
            JingleBell
        }

        public AvalaibleSongs selectedSongs;

        [System.Serializable]
        public class Songs
        {
            public AvalaibleSongs songType;
            public bool isSongLoopable;
            public AudioClip Song;

            [Range(0f, 1f)]
            public float Volume;
        }

        public Songs[] TitleSongs;
        #endregion

        #region Datas
        private DateTime dateTime = DateTime.Now;
        #endregion

        // Outras variáveis:
        [SerializeField] private bool OptionsLeft;
        private int Option;

        /*
        private float DeleteClock = 0f;
        private float DeleteInterval = 5f;
        */

        private Dictionary<AvalaibleSongs, Songs> BackgroundSongs = new Dictionary<AvalaibleSongs, Songs>();
        #endregion

        private void Awake()
        {
#if UNITY_EDITOR
            MangleFiles.LoadMangle(mangleData);
#endif

            #region Atualizar Elementos do Jogo
            #region Atualizar As Opções
            UpdateScreenResolution();

            UpdateScreenVsyncFPS();

            UpdateLanguageID();

            #endregion

            starIndex =
                (mangleData.mangle.stars.Five ? 1 : 0) +
                (mangleData.mangle.stars.Six ? 1 : 0) +
                (mangleData.mangle.stars.CustomNight ? 1 : 0) +
                (mangleData.mangle.stars.GoodEnding ? 1 : 0) +
                (mangleData.mangle.stars.AllStar ? 1 : 0);

            SixNight.gameObject.SetActive(mangleData.mangle.stars.Five);
            CustomNight.gameObject.SetActive(mangleData.mangle.stars.Six);

            for (int index = 0; index < starIndex; index++)
                stars[index].gameObject.SetActive(true);
            #endregion

            #region Datas
            dateTime = DateTime.Now;

            if (dateTime.Day == AppGlobals.GetXmasDay() && dateTime.Month == AppGlobals.GetXmasMonth())
                selectedSongs = AvalaibleSongs.JingleBell;
            else
                selectedSongs = AvalaibleSongs.Serenade;
            #endregion

            #region Dicionários
            foreach (var songs in TitleSongs)
                BackgroundSongs.Add(songs.songType, songs);
            #endregion
        }

        private void Start()
        {
            SetSong();

            if (mangleData.settings.features.discordSync)
            {
                if (RichPresence.GetStatus() != Client.Status.Connected)
                {
                    RichPresence.SetupClient();
                    RichPresence.SetupActivity();
                }

                RichPresence.SetDetails(MangleLanguage.Get(mangleData.settings.language.language).discord.Title);
            }

            if (AnimatronicsFrames.Length > 0)
                AnimatronicsImage.sprite = AnimatronicsFrames[0];

            StartCoroutine(Effects.Fade(FadeImage, false));

            #region Animatronics
            StartCoroutine(Change_Alpha());
            StartCoroutine(Change_Sprites());
            StartCoroutine(Change_Animatronics());
            #endregion
        }

        private void OnApplicationQuit() => MangleFiles.SaveMangle(mangleData);

        #region Voids e IEnumeradores: ->
        // voids essenciais
        private void SetSong()
        {
            if (audioSource.clip == null)
            {
                if (BackgroundSongs.TryGetValue(selectedSongs, out Songs songs))
                {
                    audioSource.clip = songs.Song;
                    audioSource.volume = songs.Volume;
                    audioSource.loop = songs.isSongLoopable;

                    audioSource.Play();
                }
            }
        }

        // voids públicos
        public void UpdateScreenResolution()
        {
            if (
                Video.Resolution[mangleData.settings.video.resolutionIndex].x >= 800 &&
                Video.Resolution[mangleData.settings.video.resolutionIndex].y >= 600
            )
            {
                Screen.SetResolution(
                    Video.Resolution[mangleData.settings.video.resolutionIndex].x,
                    Video.Resolution[mangleData.settings.video.resolutionIndex].y,
                    mangleData.settings.video.windowMode
                );

                AnimatronicUI.GetComponent<CanvasScaler>().referenceResolution = mangleData.settings.video.constantArea ? new Vector2Int(1366, 768) : Video.Resolution[mangleData.settings.video.resolutionIndex];
                TitleUI.GetComponent<CanvasScaler>().referenceResolution = mangleData.settings.video.constantArea ? new Vector2Int(1366, 768) : Video.Resolution[mangleData.settings.video.resolutionIndex];
                ExtrasUI.GetComponent<CanvasScaler>().referenceResolution = mangleData.settings.video.constantArea ? new Vector2Int(1366, 768) : Video.Resolution[mangleData.settings.video.resolutionIndex];
                CreditsUI.GetComponent<CanvasScaler>().referenceResolution = mangleData.settings.video.constantArea ? new Vector2Int(1366, 768) : Video.Resolution[mangleData.settings.video.resolutionIndex];
            }
            else
                MangleFiles.ShowError(4);
        }

        public void UpdateScreenVsyncFPS()
        {
            if (Video.FramesPerSecond[mangleData.settings.video.framesPerSecondIndex] < -1)
                MangleFiles.ShowError(42);

            if (!mangleData.settings.video.vsync)
                Application.targetFrameRate = Video.FramesPerSecond[mangleData.settings.video.framesPerSecondIndex];

            QualitySettings.vSyncCount = mangleData.settings.video.vsync ? 1 : 0;
        }

        public void UpdateLanguageID()
        {
            // Linguagem:
            Newgame.GetComponentInChildren<TMP_Text>().text = MangleLanguage.Get(mangleData.settings.language.language).Newgame;
            Continue.GetComponentInChildren<TMP_Text>().text = MangleLanguage.Get(mangleData.settings.language.language).Continue;
            SixNight.GetComponentInChildren<TMP_Text>().text = MangleLanguage.Get(mangleData.settings.language.language).SixNight;
            CustomNight.GetComponentInChildren<TMP_Text>().text = MangleLanguage.Get(mangleData.settings.language.language).CustomNight;

            Extras.GetComponentInChildren<TMP_Text>().text = MangleLanguage.Get(mangleData.settings.language.language).Extras;
            Credits.GetComponentInChildren<TMP_Text>().text = MangleLanguage.Get(mangleData.settings.language.language).Credits;

            DeleteText.text = MangleLanguage.Get(mangleData.settings.language.language).DeleteContent;
            ExitText.text = MangleLanguage.Get(mangleData.settings.language.language).ExitText;

            /*
            star1.enabled = mdat_BeatFive;
            star2.enabled = mdat_BeatSix;
            star3.enabled = mdat_BeatSeven;
            star4.enabled = GoodEnding;
            star5.enabled = AllStar;
            */

            NightText.text = $"{MangleLanguage.Get(mangleData.settings.language.language).NightText} {mangleData.mangle.night}";
        }

        // ienumeradores essenciais
        private IEnumerator Change_Alpha()
        {
            while (!disableRefresh)
            {
                AnimatronicsImage.canvasRenderer.SetAlpha(Random.Range(0.2f, 0.5f));

                yield return new WaitForSecondsRealtime(0.25f);
            }
        }
        private IEnumerator Change_Sprites()
        {
            while (!disableRefresh)
            {
                if (AnimatronicsImage.sprite != lastAnimatronicFrame)
                    AnimatronicsImage.sprite = lastAnimatronicFrame;

                if (AnimatronicsAlterable < xAnimatroncs)
                    lastAnimatronicFrame = AnimatronicsFrames[AnimatronicsChanged * 4 + AnimatronicsAlterable];
                else
                    lastAnimatronicFrame = AnimatronicsFrames[AnimatronicsChanged * 4];

                AnimatronicsAlterable = Random.Range(0, maxAnimatronicsRange);

                yield return new WaitForSecondsRealtime(0.1f);
            }
        }
        private IEnumerator Change_Animatronics()
        {
            while (!disableRefresh || yAnimatronics > 0)
            {
                yield return new WaitForSeconds(10f);

                if (AnimatronicsChanged < yAnimatronics)
                    AnimatronicsChanged += 1;
                else
                    AnimatronicsChanged = 0;
            }
        }
        #endregion

        #region Interação com o Teclado:
        public void OnRequestExit()
        {
            switch (TitleUI.isActiveAndEnabled)
            {
                case true:
                    ExitText.gameObject.SetActive(!ExitText.isActiveAndEnabled);
                    break;

                default:
                    lastCanvasEnabled.gameObject.SetActive(false);
                    lastCanvasEnabled = TitleUI;

                    TitleUI.gameObject.SetActive(true);

                    break;
            }
        }

        public void OnConfirm(InputAction.CallbackContext context)
        {
            if (!context.action.triggered)
                return;

            if (ExitText.isActiveAndEnabled)
                Application.Quit();
        }

        public void OnFlushData(InputAction.CallbackContext context)
        {
            if (!context.action.triggered)
                return;

            MangleFiles.FlushData();
        }
        #endregion

        #region Interação com o Mouse:
        public void OnPointerEnter(BaseEventData eventData)
        {
            /* Dia 12/28:
             * 
             * Dei uma melhorada na função que pega o ponteiro do mouse. Ao invés de sempre chamar a ação através de outro script
             * e fazer a ponte para este, eu decidi que seria muito melhor se eu integrasse ambos os scripts, deixando outro script
             * apenas para guardar dados.
             *
             * Com isso, o jogo provavelmente vai ficar mais leve. O script anterior estará na build *Front - Mang 1207*.
             */

            PointerEventData pointerEvent = eventData as PointerEventData;
            Button catchedButton = pointerEvent.pointerEnter.GetComponentInParent<Button>();

            if (catchedButton == null)
                return;

            int Index = 0;

            // Try Catch funciona exatamente para tentar executar a ação, caso ele não consiga, irá retornar o erro
            try
            {
                Index = catchedButton.GetComponent<ButtonIndex>().Index;
            }
            catch (Exception exception)
            {
                Debug.LogError($"ASFNAF DMT Debug: Componente *ButtonIndex* Inexistente para {catchedButton}! Erro: {exception}");
            }

            #region resto
            float xPosCalc;
            float yPosCalc;
            Vector2 newPos;

            OptionsLeft = Index > 3;

            Pointer.rectTransform.anchorMin = new Vector2(OptionsLeft ? 0f : 1f, 0f);
            Pointer.rectTransform.anchorMax = new Vector2(OptionsLeft ? 0f : 1f, 0f);
            Pointer.rectTransform.pivot = new Vector2(OptionsLeft ? 0f : 1f, 0f);

            xPosCalc = OptionsLeft ? 25f : -25f;
            yPosCalc = catchedButton.GetComponent<RectTransform>().anchoredPosition.y + 7.5f;
            newPos = new Vector2(xPosCalc, yPosCalc);

            Pointer.text = OptionsLeft ? ">>" : "<<";

            Option = Index;

            if (Pointer.rectTransform.anchoredPosition != newPos)
                Pointer.rectTransform.anchoredPosition = newPos;

            audioSource.PlayOneShot(blipSound);
            #endregion
        }

        public void OnPointerClick(BaseEventData eventData)
        {
            // Script serve apenas para trocar de cena. Só isso.

            PointerEventData pointerEvent = eventData as PointerEventData;
            Button catchedButton = pointerEvent.pointerEnter.GetComponentInParent<Button>();

            if (catchedButton == null)
                return;

            int Index = 0;

            // Try Catch funciona exatamente para tentar executar a ação, caso ele não consiga, irá retornar o erro
            try
            {
                Index = catchedButton.GetComponent<ButtonIndex>().Index;
            }
            catch (Exception exception)
            {
                Debug.LogError($"ASFNAF DMT Debug: Componente *ButtonIndex* Inexistente para {catchedButton}! Erro: {exception}");
            }

            #region resto
            OptionsLeft = Index > 3;

            switch (Index)
            {
                case 0:
                    mangleData.mangle.night = 1;
                    StartCoroutine(Effects.Fade(FadeImage, true));
                    StartCoroutine(Effects.LoadScene("Journals"));

                    break;

                case 1:
                    if (mangleData.mangle.night == 1)
                    {
                        StartCoroutine(Effects.Fade(FadeImage, true));
                        StartCoroutine(Effects.LoadScene("Journals"));
                    }
                    else
                    {
                        mangleData.mangle.night = Math.Clamp(mangleData.mangle.night, 1, 5);

                        StartCoroutine(Effects.Fade(FadeImage, true));
                        StartCoroutine(Effects.LoadScene("PreparingToNight"));
                    }

                    break;

                case 2:
                    mangleData.mangle.night = 6;

                    StartCoroutine(Effects.Fade(FadeImage, true));
                    StartCoroutine(Effects.LoadScene("PreparingToNight"));

                    break;

                case 3:
                    mangleData.mangle.night = 7;

                    StartCoroutine(Effects.Fade(FadeImage, true));
                    StartCoroutine(Effects.LoadScene("CustomNight"));

                    break;

                case 4:
                    if (lastCanvasEnabled != null)
                        lastCanvasEnabled = null;

                    lastCanvasEnabled = ExtrasUI;

                    TitleUI.gameObject.SetActive(false);
                    ExtrasUI.gameObject.SetActive(true);
                    break;

                case 5:
                    if (lastCanvasEnabled != null)
                        lastCanvasEnabled = null;

                    lastCanvasEnabled = CreditsUI;

                    TitleUI.gameObject.SetActive(false);
                    CreditsUI.gameObject.SetActive(true);
                    break;
            }
            #endregion
        }

        public void OnPointerOpenLinks(BaseEventData eventData)
        {
            // Lidar com links entre textos dentro do jogo.

            // Comentário antigo, Ignorar:
            // hoje, dia 30 de outubro eu consegui abrir links através do texto do meu jogo!

            PointerEventData pointerEvent = eventData as PointerEventData;
            TMP_Text text = pointerEvent.pointerEnter.GetComponentInParent<TMP_Text>();

            if (!text)
                return;

            int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, pointerEvent.position, titleCamera);

            if (linkIndex != -1)
            {
                TMP_LinkInfo linkInfo = text.textInfo.linkInfo[linkIndex];
                string linkID = linkInfo.GetLinkID();

                if (linkID != string.Empty)
                {
                    Application.OpenURL(linkID);

                    return;
                }
            }
            else
                return;
        }
        #endregion
    }
}