using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;

using System.Collections;
using ASFNAF.Mangle;

public class Fade_Functions
{
    // Sim, uma cópia do script do Journal.cs

    // Hoje, dia 31 de Outubro, eu dei uma pesquisada sobre como eu posso usar classes como auxiliares
    // a classe primária MonoBehaviour. Agora, me sinto cada vez mais poderoso!
    // A questão é, o quão leve eu consigo deixar os meus scripts?
    private MonoBehaviour hostBehaviour;
    public Fade_Functions(MonoBehaviour monoBehaviour) => this.hostBehaviour = monoBehaviour;

    public void StartSequence(Image Fade, float Interval, float Duration)
    {
        hostBehaviour.StartCoroutine(Play(Fade, Interval, Duration));
    }

    private IEnumerator Fading(Image Fade, float Duration, bool isEnding)
    {
        float elapsedTime = 0f;
        float StartAlpha = !isEnding ? 1f : 0f;
        float EndAlpha = !isEnding ? 0f : 1f;

        Color obj_c = Fade.color;

        while (elapsedTime < Duration)
        {
            elapsedTime += Time.deltaTime;

            obj_c.a = Mathf.Lerp(StartAlpha, EndAlpha, elapsedTime / Duration);
            Fade.color = obj_c;

            yield return null;
        }

        obj_c.a = EndAlpha;
        Fade.color = obj_c;
    }

    private IEnumerator Play(Image Fade, float Interval, float Duration)
    {
        float elapsedTime = 0f;

        yield return hostBehaviour.StartCoroutine(Fading(Fade, Duration, false));

        while (elapsedTime < Interval)
        {
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        yield return hostBehaviour.StartCoroutine(Fading(Fade, Duration, true));

        Fade.enabled = false;
    }
}

public class FirstTimeScript : MonoBehaviour
{
    #region variables
    [SerializeField] private MangleData mangleData;
    private Fade_Functions fadeFunctions;

    [SerializeField] private Image Background;
    [SerializeField] private Image Fade;
    [SerializeField] private Sprite[] Sprites;

    [SerializeField] private TMP_Text text;

    private string token;
    private LanguageID languageID = LanguageID.English;
    private string TextString = string.Empty;
    private float LanguageClock = 0f;
    private float FadeDuration = 10f;
    private float FadeInterval = 1f;
    private int LanguageStage = 0;
    #endregion
    private void Awake()
    {
        MangleFiles.LoadMangle(mangleData);
    }

    private void Start()
    {
        fadeFunctions = new Fade_Functions(this);
        fadeFunctions.StartSequence(Fade, FadeDuration, FadeInterval);
    }

    private void Update()
    {
        if (LanguageClock >= 2.5f && LanguageStage >= 0 && Fade.color.a == 0f)
        {
            if (LanguageStage == 0)
            {
                TextString = "Comparing System Language with ASFNAF";
            }
            else if (LanguageStage == 1)
            {
                if (Application.systemLanguage == SystemLanguage.Portuguese || Application.systemLanguage == SystemLanguage.Japanese || Application.systemLanguage == SystemLanguage.English) {
                    token = Application.systemLanguage.ToString();
                }
                else
                {
                    TextString = "No common language was found.\nThe game language was selected as English.\n\nStarting";

                    LanguageStage = -1;
                    languageID = LanguageID.English;
                }
            }
            else if (LanguageStage == 2)
            {
                if (token == "Portuguese")
                {
                    TextString = "Uma língua disponível foi encontrada.\nA língua do jogo foi selecionada como Português.\n\nIniciando.";

                    languageID = LanguageID.Portuguese;
                }
                else if (token == "Japanese")
                {
                    TextString = "利用可能な言語が見つかりました。\nゲーム言語は日本語が選択されました。\n\n起動中。";

                    languageID = LanguageID.Japanese;
                }
                else if (token == "English")
                {
                    TextString = "An available language was found.\nThe game language was selected as English.\nStarting";

                    languageID = LanguageID.English;
                }

                Background.sprite = Sprites[1];
            }
            else if (LanguageStage == 3 || LanguageStage == -1)
            {
                mangleData.settings.language.language = languageID;

                MangleFiles.SaveMangle(mangleData);

                SceneManager.LoadScene("Warning");
                return;
            }

            LanguageClock = 0f;
            LanguageStage += 1;
        }

        text.text = TextString;
        LanguageClock += Time.deltaTime;   
    }
}