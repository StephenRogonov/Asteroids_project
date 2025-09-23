using _Project.Scripts.UI;
using System;
using Zenject;

namespace _Project.Scripts.GameFlow
{
    public class PausePresenter : IInitializable, IDisposable
    {
        private PauseModel _model;
        private PauseView _view;

        public PausePresenter(PauseView view, PauseModel model)
        {
            _model = model;
            _view = view;
        }

        public void EnableView()
        {
            _view.EnableObject();
        }

        public void ContinueGame()
        {
            _model.UnpauseGame();
        }

        public void Exit()
        {
            _model.ExitToMainMenu();
        }

        public void Initialize()
        {
            _model.Paused += EnableView;
            _view.ContinueClicked += ContinueGame;
            _view.ExitClicked += Exit;
        }

        public void Dispose()
        {
            _model.Paused -= EnableView;
            _view.ContinueClicked -= ContinueGame;
            _view.ExitClicked -= Exit;
        }
    }
}
