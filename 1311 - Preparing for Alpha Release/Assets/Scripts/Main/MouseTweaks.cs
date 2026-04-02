using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ASFNAF.Mangle;

public class MouseTweaks : MonoBehaviour
{
    [Header("MangleFiles")]
    [SerializeField] private MangleData mangleData;
    
    [Header("MouseTweaks")]
    [SerializeField] private Main mainScript;

    private float _cameraRotation = 2009f;
    public bool Cooldown = false;

    #region Alvo múltiplo: ->
    public GraphicRaycaster raycaster1;
    public GraphicRaycaster raycaster2;
    public EventSystem eventSystem;

    private PointerEventData pointer;
    private Dictionary<string, Action> mtVoids;
    private List<RaycastResult> results = new List<RaycastResult>(3);

    private GameObject lastHover;
    #endregion

    #region Voids Essenciais: ->
    private void LeftClick()
    {
        pointer.position = Input.mousePosition;

        if (results.Count > 0)
            results.Clear();

        raycaster1.Raycast(pointer, results);
        raycaster2.Raycast(pointer, results);

        foreach (var hitted in results)
        {
            GameObject go = hitted.gameObject;

            if (go.name.Contains("cam"))
            {
                mainScript.LastCameraValue = mainScript.CameraValues;
                mainScript.CameraValues = int.Parse(Regex.Match(go.name, @"\d+").Value);
            }
            else if (mtVoids.TryGetValue(go.name, out Action action))
                action();
        }
    }
    #endregion

    private void Awake()
    {
        mtVoids = new Dictionary<string, Action>()
        {
            #region Layer0: ->
            { "LeftLight", () => mainScript.SetLeftLight() },

            { "LeftDoor", () => mainScript.SetLeftDoor() },

            { "RightLight", () => mainScript.SetRightLight() },
            #endregion
        };

        pointer = new PointerEventData(eventSystem);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            LeftClick();
    }

    private void FixedUpdate()
    {
        /// <summary>
        /// 12/23/2025:
        /// 
        /// Fiz uma atualização para o script, trocando o Update, LateUpdate e outros voids que usem o FPS base para o FixedUpdate.
        /// FixedUpdate mantém o jogo em uma atualização constante, como se estivesse rodando a 60FPS, mesmo com o contador acima de 300FPS.
        /// 
        /// Sem dúvidas, bem melhor, até porquê a rotação das câmeras não vai depender do FPS do cliente.
        /// </summary>

        #region Multiple Render Targeting: ->
        pointer.position = Input.mousePosition;

        if (results.Count > 0)
            results.Clear();

        raycaster2.Raycast(pointer, results);

        GameObject current = (results.Count > 0) ? results[0].gameObject : null;

        if (lastHover != current)
        {
            lastHover = current;
        }
        else if (current != null && current == lastHover)
        {
            #region Controle de Câmera do escritório: ->
            if (mainScript.CameraValues == 0)
            {
                if (current.name.Contains("LeftPanel") || current.name.Contains("RightPanel"))
                {
                    float direction = current.name.Contains("LeftPanel") ? 1 : -1;
                    float speed = (float)Video.Resolution[mangleData.settings.video.resolutionIndex].x / 1920 * (int.Parse(Regex.Match(current.name, @"\d+").Value) * 4.43f);
                    Vector2 currentCameraPosition = mainScript.MainCanvas_Layer0.rectTransform.anchoredPosition;
                    float targetCameraPositionX = Mathf.Clamp(currentCameraPosition.x + direction * speed, -mainScript.halfSize, mainScript.halfSize);
                    Vector2 targetCameraPosition = new Vector2(targetCameraPositionX, currentCameraPosition.y);

                    mainScript.MainCanvas_Layer0.rectTransform.anchoredPosition = Vector2.MoveTowards(currentCameraPosition, targetCameraPosition, _cameraRotation * Time.deltaTime);
                }
            }
            #endregion
        }
        #endregion
    }

    #region Puppet
    // Adicionado em 1/15/2026:
    public void RebootPuppet(BaseEventData _)
    {
        if (mainScript.CameraValues != 18)
            return;

        AnimatronicsBodies.EndoBase endoBase = mainScript.Animatronics.GetValueOrDefault(AnimatronicsBodies.Animatronics.Seven);

        if (endoBase is AnimatronicsBodies.EndoMarionette marionette)
            StartCoroutine(marionette.RebootingCounter(true));

        mainScript.layer5Class.MusicBox.sprite = mainScript.layer5Class.MusicBoxButtonSprites[1];
    }

    public void UnRebootPuppet(BaseEventData _)
    {
        AnimatronicsBodies.EndoBase endoBase = mainScript.Animatronics.GetValueOrDefault(AnimatronicsBodies.Animatronics.Seven);

        if (endoBase is AnimatronicsBodies.EndoMarionette marionette)
            StartCoroutine(marionette.RebootingCounter(false));

        mainScript.layer5Class.MusicBox.sprite = mainScript.layer5Class.MusicBoxButtonSprites[0];
    }

    public void ChangePuppetSong(BaseEventData _)
    {
        mainScript.audioManager.currentMusicBox++;

        if (mainScript.audioManager.currentMusicBox > mainScript.audioManager.puppetSongs.Length - 1)
            mainScript.audioManager.currentMusicBox = 0;

        if (mainScript.audioManager.musicbox.isPlaying)
            mainScript.audioManager.musicbox.Stop();

        mainScript.audioManager.musicbox.clip = mainScript.audioManager.puppetSongs[mainScript.audioManager.currentMusicBox].song;
        mainScript.audioManager.musicbox.loop = mainScript.audioManager.puppetSongs[mainScript.audioManager.currentMusicBox].isSongLoopable;
        mainScript.audioManager.musicbox.volume = mainScript.audioManager.puppetSongs[mainScript.audioManager.currentMusicBox].volume;

        mainScript.audioManager.musicbox.Play();
    }

    public void ChangePuppetSong_Hold(BaseEventData _)
    {
        if (mainScript.CameraValues != 18)
            return;

        mainScript.layer5Class.ChangeSong.sprite = mainScript.layer5Class.ChangeSongButtonSprites[1];
    }

    public void ChangePuppetSong_UnHold(BaseEventData _)
    {
        mainScript.layer5Class.ChangeSong.sprite = mainScript.layer5Class.ChangeSongButtonSprites[0];
    }
    #endregion

    #region Câmera e Máscara
    public void SetCamera(BaseEventData _)
    {
        if (!mainScript.WithMask && !Cooldown)
            StartCoroutine(mainScript.FlipCamera(!mainScript.WithCamera));
        else if (mainScript.WithMask && !Cooldown)
            StartCoroutine(mainScript.FlipMask(false));
    }

    public void SetMask(BaseEventData _)
    {
        if (!mainScript.WithCamera && !Cooldown)
            StartCoroutine(mainScript.FlipMask(!mainScript.WithMask));
        else if (mainScript.WithCamera && !Cooldown)
            StartCoroutine(mainScript.FlipCamera(false));
    }
    #endregion
}