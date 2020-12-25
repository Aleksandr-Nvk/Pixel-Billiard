using FieldData;
using UnityEngine;

namespace Views
{
    public class PlayerViewFactory
    {
        public PlayerView Create(PlayerView playerViewPrefab, GameSessionView parentView, Player player, MoveManager moveManager)
        {
            var playerView = Object.Instantiate(playerViewPrefab, parentView.transform);
            playerView.Init(player, moveManager);

            return playerView;
        }
    }
}