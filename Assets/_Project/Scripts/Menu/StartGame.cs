using _Project.Scripts.Common;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Menu
{
    public class StartGame : MonoBehaviour
    {
        private Button _startButton;
        private SceneSwitcher _sceneSwitcher;

        public void Init(SceneSwitcher sceneSwitcher)
        {
            _sceneSwitcher = sceneSwitcher;

            _startButton = GetComponent<Button>();
            _startButton.onClick.AddListener(_sceneSwitcher.LoadGame);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveAllListeners();
        }
    }
}