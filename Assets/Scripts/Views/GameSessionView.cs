using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Zenject;
using Models;

namespace Views
{
    public class GameSessionView : MonoBehaviour
    {
        [SerializeField] private Animations _animations = default;
        [SerializeField] private CanvasGroup _canvasGroup = default;
        
        [Header("Elements")]
        
        [SerializeField] private Button _pauseButton = default;
        
        [Header("Views")]

        [SerializeField] private PlayerView _firstPlayerView = default;
        [SerializeField] private PlayerView _secondPlayerView = default;
        
        private GameSession _gameSession;
        
        private Coroutine _currentAnimation;

        [Inject]
        public void Init(GameSession gameSession)
        {
            _gameSession = gameSession;
            
            gameSession.OnSessionStarted += Show;
            gameSession.OnSessionStarted += InitPlayers;
            gameSession.OnSessionResumed += Show;
            gameSession.OnSessionEnded += _ => Hide();
            
            _pauseButton.onClick.AddListener(gameSession.Pause);
            _pauseButton.onClick.AddListener(Hide);
        }
        
        public void Show()
        {
            _canvasGroup.gameObject.SetActive(true);

            if (_currentAnimation != null) StopCoroutine(_currentAnimation);
            _currentAnimation = StartCoroutine(_animations.Fade(_canvasGroup, targetAlpha: 1f, duration: 0.25f));
            
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;

            _firstPlayerView.Show();
            _secondPlayerView.Show();
        }

        public void Hide()
        {
            _canvasGroup.gameObject.SetActive(true);
            
            if (_currentAnimation != null) StopCoroutine(_currentAnimation);
            _currentAnimation = StartCoroutine(Hide());
            
            IEnumerator Hide()
            {
                _canvasGroup.interactable = false;
                yield return _animations.Fade(_canvasGroup, targetAlpha: 0f, duration: 0.25f);
                _canvasGroup.gameObject.SetActive(false);
                _canvasGroup.blocksRaycasts = false;
            }
            
            _firstPlayerView.Hide();
            _secondPlayerView.Hide();
        }
        
        /// <summary>
        /// Initializes information for the first and second player views
        /// </summary>
        private void InitPlayers()
        {
            _firstPlayerView.Init(_gameSession.FirstPlayer, _gameSession.MoveManager);
            _firstPlayerView.Show();
            
            _secondPlayerView.Init(_gameSession.SecondPlayer, _gameSession.MoveManager);
            _secondPlayerView.Show();
        }
    }
}