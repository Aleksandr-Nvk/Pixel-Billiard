using System.Collections;
using System.Collections.Generic;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class HomeView : MonoBehaviour
    {
        [SerializeField] private Animations _animations = default;

        [SerializeField] private CanvasGroup _canvasGroup = default;
        
        [SerializeField] private Button _settingsButton = default;
        [SerializeField] private Button _playButton = default;

        [SerializeField] private TMP_InputField _firstPlayerNameField = default;
        [SerializeField] private TMP_InputField _secondPlayerNameField = default;
        
        [SerializeField] private SettingsView _settingsView = default;

        public void Init(GameSession gameSession)
        {
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
                
                gameSession.StartSession(firstPlayerName, secondPlayerName);
            });
        }
        
        public void Show()
        {
            _canvasGroup.gameObject.SetActive(true);
            _animations.Fade(_canvasGroup, 1f, 0.5f);
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public void Hide()
        {
            _canvasGroup.interactable = false;
            StartCoroutine(Hide());
            
            IEnumerator Hide()
            {
                yield return _animations.Fade(_canvasGroup, 0f, 0.5f);
                gameObject.SetActive(false);
                _canvasGroup.blocksRaycasts = false;
            }
        }
    }
}