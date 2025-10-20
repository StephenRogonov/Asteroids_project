using System;

namespace _Project.Scripts.GameFlow
{
    public class GameOverPresenter : IDisposable
    {
        private GameOverModel _model;
        private GameOverView _view;

        public GameOverPresenter(
            GameOverModel model
            )
        {
            _model = model;
        }

        public void Init(GameOverView gameOverView)
        {
            _view = gameOverView;

            _model.GameOverTriggered += EnableView;
            _view.RestartClicked += RestartGame;
            _view.ContinueClicked += ContinueGame;
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

        public void Dispose()
        {
            _model.GameOverTriggered -= EnableView;
            _view.RestartClicked -= RestartGame;
            _view.ContinueClicked -= ContinueGame;
        }
    }
}