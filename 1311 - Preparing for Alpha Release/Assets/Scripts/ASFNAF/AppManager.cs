using UnityEngine;

using ASFNAF.Discord;

namespace ASFNAF
{
    public class AppManager : MonoBehaviour
    {
        public MangleData mangleData;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        
            MangleFiles.LoadMangle(mangleData);
        }

        private void Start()
        {
            Debug.Log("ASFNAF Iniciou");
        }

        private void FixedUpdate()
        {
            mangleData.settings.features.gameTime += Time.deltaTime;
        }

        private void OnDestroy()
        {
            MangleFiles.SaveMangle(mangleData);
            RichPresence.DestroyPresence();
        
            Debug.Log("ASFNAF Parou");
        }
    }
}