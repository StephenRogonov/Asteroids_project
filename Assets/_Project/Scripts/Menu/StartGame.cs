using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StartGame : MonoBehaviour
{
    private Button _startButton;
    private SceneSwitcher _sceneSwitcher;

    [Inject]
    private void Construct (SceneSwitcher sceneSwitcher)
    {
        _sceneSwitcher = sceneSwitcher;
    }

    private void OnEnable()
    {
        _startButton = GetComponent<Button>();
        _startButton.onClick.AddListener(_sceneSwitcher.LoadGame);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveAllListeners();
    }
}
