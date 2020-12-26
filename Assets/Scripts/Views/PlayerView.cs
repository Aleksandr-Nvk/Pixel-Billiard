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

            SwitchToPlayer(moveManager.FirstPlayer);
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

        private void SwitchToPlayer(Player playerToSwitchTo)
        {
            _pointer.gameObject.SetActive(Player == playerToSwitchTo);
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

        /// <summary>
        /// Adds an icon to rolled balls view
        /// </summary>
        /// <param name="icon"> Rolled ball icon </param>
        private void AddBallToView(Sprite icon)
        {
            var image = PlayerBallIcons[_playerBallIndex];
            image.sprite = icon;
            image.enabled = true;
        
            _playerBallIndex++;
        }
    }
}