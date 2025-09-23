using System;
using Zenject;

namespace _Project.Scripts.GameFlow
{
    public class GameOverPresenter : IInitializable, IDisposable
    {
        private GameOverModel _model;
        private GameOverView _view;

        public GameOverPresenter(
            GameOverModel model,
            GameOverView view
            )
        {
            _model = model;
            _view = view;
        }

        public void RestartGame()
        {
            _model.RestartGame();
        }

        private void ContinueGame()
        {
            _model.Continue();
        }

        public void EnableView()
        {
            _view.EnableObject();
        }

        public void Initialize()
        {
            _model.GameOverTriggered += EnableView;
            _view.RestartClicked += RestartGame;
            _view.ContinueClicked += ContinueGame;
        }

        public void Dispose()
        {
            _model.GameOverTriggered -= EnableView;
            _view.RestartClicked -= RestartGame;
            _view.ContinueClicked -= ContinueGame;
        }
    }
}