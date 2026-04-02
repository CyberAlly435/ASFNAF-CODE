using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

using TMPro;

using System;
using System.Collections;
using System.Collections.Generic;

// ASFNAF
using ASFNAF.Discord;
using ASFNAF.Miscelaneus;
using ASFNAF.Mangle;

namespace ASNAF.Minigame
{
    public class HeCried : MonoBehaviour
    {
        [SerializeField] private MangleData mangleData;

        [Serializable]
        private struct AvaliableObjects
        {
            [Header("Personagens")]
            [Tooltip("A criança que chora no primeiro cenário.")]
            public SpriteRenderer cryingChild;
            [Tooltip("A mulher mistoriosa que aparece no cenário")]
            public SpriteRenderer mysteriousWoman;

            [Space(20)]
            [Header("UI")]
            public Canvas canvas;
            public Image dialog;
            public Vector2[] dialogVector;
            [Range(0f, 1f)] public float dialogTime;

            public Image fade;
        }

        [Serializable]
        private struct Dialogues
        {
            [SerializeField, TextArea] private string[] Portuguese;
            [SerializeField, TextArea] private string[] Japanese;
            [SerializeField, TextArea] private string[] English;

            public Dictionary<LanguageID, string[]> keyValues;

            public string[] Get(LanguageID lang)
            {
                keyValues = new()
                {
                    { LanguageID.Portuguese, Portuguese },
                    { LanguageID.Japanese, Japanese },
                    { LanguageID.English, English }
                };

                return keyValues[lang];
            }
        };

        [Space(20)]
        [SerializeField] private AvaliableObjects minigame;

        [Header("Diálogos")]
        [SerializeField] private Dialogues dialogues;
        private String[] currentDialog;

        // Outros valores
        private byte _minigamePhase;
        private byte minigamePhase
        {
            get { return _minigamePhase; }
            set
            {
                _minigamePhase = value;

                if (value <= currentDialog[minigamePhase].Length)
                {
                    minigameJump = false;
                    StartCoroutine(StartDialog());
                }
                else
                {
                    StartCoroutine(Effects.Fade(minigame.fade, true));
                }
            }
        }
        private bool minigameEnded;
        private bool minigameCooldown;
        private bool minigameJump;

        private float dialogElapsed;
        private float textElapsed;

        // Métodos principais
        private void Awake()
        {
            Resources.UnloadUnusedAssets();

            currentDialog = dialogues.Get(mangleData.settings.language.language);
        }

        private void Start()
        {   
            Resources.UnloadUnusedAssets();

            // Iniciar o FadeIn
            if (!minigame.fade.isActiveAndEnabled)
                minigame.fade.gameObject.SetActive(true);

            StartCoroutine(Effects.Fade(minigame.fade, false));

            // Fechar o diálogo
            minigame.dialog.rectTransform.sizeDelta = minigame.dialogVector[0];
            minigame.dialog.transform.Find("Text").gameObject.SetActive(false);
            minigame.dialog.transform.Find("Indicator").gameObject.SetActive(false);

            StartCoroutine(StartDialog());

            //Debug.Log(minigame.dialog.rectTransform.sizeDelta);

            //minigame.dialog.transform.GetComponentInChildren<TMP_Text>().gameObject.SetActive(false);
            //minigame.dialog.transform.Find("Indicator").gameObject.SetActive(false);

            //StartCoroutine(StartMinigamePhase());
        }

        // Outros métodos
        public void JumpDialog(InputAction.CallbackContext context)
        {
            if (!context.action.triggered || minigameCooldown)
                return;

            minigameJump = true;
        }

        private IEnumerator StartDialog()
        {
            if (minigamePhase == 0)
            {
                yield return new WaitForSeconds(2f);

                yield return OpenDialog();
            }

            for (byte length = 0; length < currentDialog[minigamePhase].Length; length++)
            {
                if (minigameJump)
                    length = (byte)currentDialog[minigamePhase].Length;

                string str = currentDialog[minigamePhase].Substring(0, length);

                Debug.Log(str);

                minigame.dialog.transform.Find("Text").GetComponent<TMP_Text>().text = str;

                yield return new WaitForSeconds(0.1f);
            }

            minigameJump = false;

            while (textElapsed < 5f && minigameJump == false)
            {
                textElapsed += Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            textElapsed = 0f;

            if (minigamePhase > currentDialog[minigamePhase].Length)
                yield return CloseDialog();

            minigamePhase++;
        }

        private IEnumerator OpenDialog()
        {
            minigameCooldown = true;

            dialogElapsed = 0f;

            while (dialogElapsed < minigame.dialogTime)
            {
                dialogElapsed += Time.deltaTime;
                float percentage = dialogElapsed / minigame.dialogTime;

                minigame.dialog.rectTransform.sizeDelta = Vector2.Lerp(
                    minigame.dialogVector[0],
                    minigame.dialogVector[1],
                    percentage
                );

                yield return new WaitForEndOfFrame();
            }

            minigame.dialog.transform.Find("Text").gameObject.SetActive(true);
            minigame.dialog.transform.Find("Indicator").gameObject.SetActive(true);
        
            minigameCooldown = false;
        }
    
        private IEnumerator CloseDialog()
        {
            minigameCooldown = true;

            dialogElapsed = 0f;

            minigame.dialog.transform.Find("Text").gameObject.SetActive(false);
            minigame.dialog.transform.Find("Indicator").gameObject.SetActive(false);

            while (dialogElapsed < minigame.dialogTime)
            {
                dialogElapsed += Time.deltaTime;
                float percentage = dialogElapsed / minigame.dialogTime;

                minigame.dialog.rectTransform.sizeDelta = Vector2.Lerp(
                    minigame.dialogVector[1],
                    minigame.dialogVector[0],
                    percentage
                );

                yield return new WaitForEndOfFrame();
            }

            minigameCooldown = false;
        }
    }
}