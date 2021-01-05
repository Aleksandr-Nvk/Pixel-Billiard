using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Models;

namespace Views
{
    public class PauseView : MonoBehaviour
    {
        [SerializeField] private Animations _animations = default;

        [SerializeField] private CanvasGroup _canvasGroup = default;

        [SerializeField] private Button _homeButton = default;
        [SerializeField] private Button _resumeButton = default;
        [SerializeField] private Button _settingsButton = default;

        [SerializeField] private SettingsView _settingsView = default;

        private Coroutine _currentAnimation;
        
        public void Init(GameSession gameSession)
        {
            gameSession.OnSessionPaused += Show;
            gameSession.OnSessionResumed += Hide;
            gameSession.OnSessionExited += Hide;

            _homeButton.onClick.AddListener(gameSession.Exit);
            _resumeButton.onClick.AddListener(gameSession.Resume);
            _settingsButton.onClick.AddListener(Hide);
            _settingsButton.onClick.AddListener(() => _settingsView.Show(Show));
        }

        public void Show()
        {
            _canvasGroup.gameObject.SetActive(true);

            if (_currentAnimation != null) StopCoroutine(_currentAnimation);
            _currentAnimation = StartCoroutine(_animations.Fade(_canvasGroup, 1f, 0.5f));
            
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public void Hide()
        {
            if (_currentAnimation != null) StopCoroutine(_currentAnimation);
            _currentAnimation = StartCoroutine(Hide());
            
            IEnumerator Hide()
            {
                _canvasGroup.interactable = false;
                yield return _animations.Fade(_canvasGroup, 0f, 0.5f);
                gameObject.SetActive(false);
                _canvasGroup.blocksRaycasts = false;
            }
        }
    }
}