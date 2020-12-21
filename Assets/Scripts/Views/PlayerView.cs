using UnityEngine.UI;
using FieldData;
using UnityEngine;
using TMPro;

namespace Views
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI PlayerName = default;

        [SerializeField] private Image[] PlayerBallIcons = default;

        private int _playerBallIndex;

        public void Init(Player playerModel)
        {
            playerModel.OnBallRolled += AddBallToView;
            PlayerName.text = playerModel.Name;
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