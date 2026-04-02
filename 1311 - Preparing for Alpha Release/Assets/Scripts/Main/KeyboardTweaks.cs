using UnityEngine;

namespace ASFNAF.InternalScenarioScripts
{
    public class KeyboardTweaks : MonoBehaviour
    {
        [SerializeField] private Main gameScript;

        public KeyCode flashControl;

        private void Update()
        {
            switch (Input.GetKey(flashControl))
            {
                case true:
                    if (gameScript.canFlash)
                        gameScript.IsFlashing = true;

                    break;

                default:
                    gameScript.IsFlashing = false;

                    break;
            }

            //gameScript.IsFlashing = Input.GetKeyDown(KeyCode.LeftControl);
        }
    }
}