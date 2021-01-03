using UnityEngine.UI;
using FieldData;
using UnityEngine;

namespace Views
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private AnimatedText PlayerName = default;
        
        [SerializeField] private AnimatedImage _pointer = default;

        [SerializeField] private Image[] PlayerBallIcons = default;

        private Player Player;

        private int _playerBallIndex;

        public void Init(Player player, MoveManager moveManager)
        {
            Player = player;
            
            moveManager.OnPlayerSwitched += SwitchToPlayer;
            player.OnBallRolled += AddBallToView;
            
            PlayerName.TextMeshPro.text = player.Name;

            SwitchToPlayer(moveManager.CurrentPlayer);
        }

        public void Show()
        {
            PlayerName.SetActivity(true);
            _pointer.SetActivity(true);
        }

        public void Hide()
        {
            PlayerName.SetActivity(false);
            _pointer.SetActivity(false);
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
            _pointer.SetActivity(Player == playerToSwitchTo);
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