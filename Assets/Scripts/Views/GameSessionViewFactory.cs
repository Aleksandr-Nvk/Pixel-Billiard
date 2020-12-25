using FieldData;
using Models;
using UnityEngine;

namespace Views
{
    public class GameSessionViewFactory
    {
        private readonly GameSessionView _gameSessionViewPrefab;
        private readonly HomeView _parentView;

        public GameSessionViewFactory(GameSessionView gameSessionViewPrefab, HomeView parentView)
        {
            _gameSessionViewPrefab = gameSessionViewPrefab;
            _parentView = parentView;
        }

        public GameSessionView Create(GameSession gameSession, MoveManager moveManager)
        {
            var gameSessionView = Object.Instantiate(_gameSessionViewPrefab, _parentView.transform);
            gameSessionView.Init(gameSession);

            return gameSessionView;
        }
    }
}