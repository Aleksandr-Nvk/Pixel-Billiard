using System.Collections;
using UnityEngine.UI;
using FieldData;
using TMPro;
using UnityEngine;

namespace Views
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Animations _animations = default;

        [SerializeField] private CanvasGroup _canvasGroup = default;
        
        [SerializeField] private TMP_Text PlayerName = default;
        
        [SerializeField] private Image _pointer = default;
        [SerializeField] private Image[] PlayerBallIcons = default;

        private Player Player;

        private int _playerBallIndex;

        public void Init(Player player, MoveManager moveManager)
        {
            Player = player;
            
            moveManager.OnPlayerSwitched += SwitchToPlayer;
            player.OnBallRolled += AddBallToView;
            
            PlayerName.text = player.Name;

            SwitchToPlayer(moveManager.CurrentPlayer);
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