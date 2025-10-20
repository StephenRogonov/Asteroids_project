using UnityEngine;

namespace _Project.Scripts.UI
{
    public class MobileControls : MonoBehaviour
    {
        private CanvasGroup _mobileButtonsCanvasGroup;

        private void Awake()
        {
            _mobileButtonsCanvasGroup = GetComponent<CanvasGroup>();
        }

        public void BlockButtons()
        {
            _mobileButtonsCanvasGroup.interactable = false;
            _mobileButtonsCanvasGroup.blocksRaycasts = false;
        }

        public void UnblockButtons()
        {
            _mobileButtonsCanvasGroup.interactable = true;
            _mobileButtonsCanvasGroup.blocksRaycasts = true;
        }
    }
}