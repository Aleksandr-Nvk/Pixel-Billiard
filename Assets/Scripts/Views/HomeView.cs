using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Models;
using TMPro;

namespace Views
{
    public class HomeView : MonoBehaviour
    {
        [SerializeField] private Animations _animations = default;
        [SerializeField] private CanvasGroup _canvasGroup = default;
        
        [Header("Elements")]

        [SerializeField] private Button _settingsButton = default;
        [SerializeField] private Button _playButton = default;

        [SerializeField] private TMP_InputField _firstPlayerNameField = default;
        [SerializeField] private TMP_InputField _secondPlayerNameField = default;
        
        [Header("Views")]
        
        [SerializeField] private SettingsView _settingsView = default;

        private Coroutine _currentAnimation;

        public void Init(GameSession gameSession)
        {
            gameSession.OnSessionExited += Show;
            
            _settingsButton.onClick.AddListener(() =>
            {
                Hide();
                _settingsView.Show(Show);
            });
            
            _playButton.onClick.AddListener(() =>
            {
                Hide();

                var firstPlayerName = string.IsNullOrWhiteSpace(_firstPlayerNameField.text)
                    ? "Player1"
                    : _firstPlayerNameField.text;
                
                var secondPlayerName = string.IsNullOrWhiteSpace(_secondPlayerNameField.text)
                    ? "Player2"
                    : _secondPlayerNameField.text;
                
                gameSession.Start(firstPlayerName, secondPlayerName);
            });
        }
        
        public void Show()
        {
            _canvasGroup.gameObject.SetActive(true);
            
            if (_currentAnimation != null) StopCoroutine(_currentAnimation);
            _currentAnimation = StartCoroutine(_animations.Fade(_canvasGroup, targetAlpha: 1f, duration: 0.5f));
            
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public void Hide()
        {
            _canvasGroup.gameObject.SetActive(true);
            
            if (_currentAnimation != null) StopCoroutine(_currentAnimation);
            _currentAnimation = StartCoroutine(Hide());
            
            IEnumerator Hide()
            {
                _canvasGroup.interactable = false;
                yield return _animations.Fade(_canvasGroup, targetAlpha: 0f, duration: 0.5f);
                _canvasGroup.gameObject.SetActive(false);
                _canvasGroup.blocksRaycasts = false;
            }
        }
    }
}