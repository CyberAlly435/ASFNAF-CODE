using ASFNAF.Mangle;
using ASFNAF.Discord;

using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Discord.Sdk;

public class Selector : Extras
{
    [Header("Mangle Data")]
    public MangleData mangleData;

    [SerializeField] protected Extras_Layers layers;

    public Image SelectorLayer;
    public Image indicator;

    private Image _layerHandler;
    private Image layerHandler
    {
        set
        {
            _layerHandler = value;

            switch (value.name)
            {
                case "Settings":
                    RichPresence.SetDetails(MangleLanguage.Get(mangleData.settings.language.language).settings.DiscordDetails);
                    break;
            }
        }
    }

    // Voids do script
    private void Awake() =>
        LanguageUpdate();

    // Voids personalizados
    private void LanguageUpdate()
    {
        SelectorLayer.transform.Find("Animatronics").GetComponentInChildren<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).selector.Animatronics;

        SelectorLayer.transform.Find("Interviews").GetComponentInChildren<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).selector.Interviews;

        SelectorLayer.transform.Find("Minigames").GetComponentInChildren<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).selector.Minigames;

        SelectorLayer.transform.Find("Extras").GetComponentInChildren<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).selector.Extras;

        SelectorLayer.transform.Find("Cheats").GetComponentInChildren<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).selector.Cheats;

        SelectorLayer.transform.Find("Settings").GetComponentInChildren<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).selector.Settings;

        SelectorLayer.transform.Find("Exit").GetComponentInChildren<TMP_Text>().text =
            MangleLanguage.Get(mangleData.settings.language.language).selector.Exit;
    }

    public void OnButtonHover(BaseEventData eventData)
    {
        PointerEventData pointer = eventData as PointerEventData;
        Button button = pointer.pointerEnter.GetComponentInParent<Button>();

        Vector2 position = indicator.rectTransform.anchoredPosition;

        indicator.transform.SetParent(button.transform);
        indicator.rectTransform.anchoredPosition = position;
    }

    public void OnButtonClick(BaseEventData eventData)
    {
        PointerEventData pointer = eventData as PointerEventData;
        Button button = pointer.pointerClick.GetComponent<Button>();

        SelectorLayer.gameObject.SetActive(false);
        
        switch (button.name)
        {
            case "Animatronics":
                layers.animatronics.gameObject.SetActive(true);
                break;
            case "Interviews":
                layers.interview.gameObject.SetActive(true);
                break;
            case "Minigames":
                layers.minigames.gameObject.SetActive(true);
                break;
            case "Extras":
                layers.extras.gameObject.SetActive(true);
                break;
            case "Cheats":
                layers.cheats.gameObject.SetActive(true);
                break;
            case "Settings":
                layers.setting.gameObject.SetActive(true);
                layerHandler = layers.setting;
                break;
            case "Exit":
                extrasCanvas.gameObject.SetActive(false);
                break;
        }
    }

    public void OnExit(BaseEventData eventData)
    {
        PointerEventData pointer = eventData as PointerEventData;
        Button button = pointer.pointerClick.GetComponent<Button>();
        
        Image currentLayer = button.GetComponentInParent<Image>();

        Debug.Log(currentLayer);
    }
}