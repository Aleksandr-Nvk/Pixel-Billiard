using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using FieldData;
using TMPro;

namespace Views
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Animations _animations = default;
        [SerializeField] private CanvasGroup _canvasGroup = default;
        
        [Header("Elements")]

        [SerializeField] private TMP_Text PlayerName = default;
        
        [SerializeField] private Image _pointer = default;
        [SerializeField] private Image[] PlayerBallIcons = default;

        private Player Player;

        private int _playerBallIndex;

        private Coroutine _currentAnimation;
        
        public void Init(Player player, MoveManager moveManager)
        {
            Player = player;
            
            moveManager.OnPlayerSwitched += SwitchToPlayer;
            player.OnBallAdded += AddBallToView;
            
            PlayerName.text = player.Name;

            _playerBallIndex = 0;

            foreach (var ballIcon in PlayerBallIcons)
                ballIcon.enabled = false;

            SwitchToPlayer(moveManager.CurrentPlayer);
        }

        public void Show()
        {
            _canvasGroup.gameObject.SetActive(true);

            if (_currentAnimation != null) StopCoroutine(_currentAnimation);
            _currentAnimation = StartCoroutine(_animations.Fade(_canvasGroup, targetAlpha: 1f, duration: 0.25f));
            
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
                yield return _animations.Fade(_canvasGroup, targetAlpha: 0f, duration: 0.25f);
                _canvasGroup.gameObject.SetActive(false);
                _canvasGroup.blocksRaycasts = false;
            }
        }

        private void SwitchToPlayer(Player playerToSwitchTo)
        {
            _pointer.enabled = Player == playerToSwitchTo;
        }
        
        private void AddBallToView(Sprite icon)
        {
            var image = PlayerBallIcons[_playerBallIndex];
            image.sprite = icon;
            image.enabled = true;
        
            _playerBallIndex++;
        }
    }
}