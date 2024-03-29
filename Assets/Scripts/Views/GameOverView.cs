using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using FieldData;
using Zenject;
using Models;
using TMPro;

namespace Views
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private Animations _animations = default;
        [SerializeField] private CanvasGroup _canvasGroup = default;
        
        [Header("Elements")]
        
        [SerializeField] private Button _restartButton = default;
        [SerializeField] private Button _homeButton = default;

        [SerializeField] private TMP_Text _winnerText = default;
        
        private Coroutine _currentAnimation;

        [Inject]
        public void Init(GameSession gameSession)
        {
            gameSession.OnSessionEnded += Show;
            gameSession.OnSessionExited += Hide;
            gameSession.OnSessionRestarted += Hide;
            
            _restartButton.onClick.AddListener(gameSession.Restart);
            _homeButton.onClick.AddListener(gameSession.Exit);
        }

        private void Show(Player winner)
        {
            _winnerText.text = winner.Name + " wins!";
            _canvasGroup.gameObject.SetActive(true);
            
            if (_currentAnimation != null) StopCoroutine(_currentAnimation);
            _currentAnimation = StartCoroutine(_animations.Fade(_canvasGroup, targetAlpha: 1f, duration: 0.25f));
            
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        private void Hide()
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
        }
    }
}