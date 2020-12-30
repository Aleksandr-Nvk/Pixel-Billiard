using UnityEngine.UI;
using FieldData;
using UnityEngine;
using TMPro;

namespace Views
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI PlayerName = default;
        
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
            Animations.Fade(PlayerName, 1f, 1f);
            Animations.Fade(_pointer, 1f, 1f);
        }

        public void Hide()
        {
            Animations.Fade(PlayerName, 0f, 1f);
            Animations.Fade(_pointer, 0f, 1f);
        }

        public void ResetView()
        {
            PlayerName = null;
            
            foreach (var playerBallIcon in PlayerBallIcons)
            {
                playerBallIcon.enabled = false;
            }
            
            PlayerName = null;
        }
        
        private void SwitchToPlayer(Player playerToSwitchTo)
        {
            _pointer.gameObject.SetActive(Player == playerToSwitchTo);
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