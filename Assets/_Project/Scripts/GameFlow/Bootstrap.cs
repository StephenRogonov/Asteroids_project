using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameFlow
{
    public class Bootstrap : MonoBehaviour
    {
        private HUD_Controller _hudController;

        [Inject]
        private void Construct(HUD_Controller controller)
        {
            _hudController = controller;
        }

        private void Update()
        {
            _hudController.Update(Time.deltaTime);
        }
    }
}