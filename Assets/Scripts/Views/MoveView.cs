using FieldGameplay;
using UnityEngine;

namespace Views
{
    public class MoveView : MonoBehaviour
    {
        [SerializeField] private GameObject _firstPlayerPointer = default;
        [SerializeField] private GameObject _secondPlayerPointer = default;

        [SerializeField] private PlayerView _firstPlayerView = default;
        [SerializeField] private PlayerView _secondPlayerView = default;
        
        private MoveManager _moveManager;

        public void Init(MoveManager moveManager)
        {
            _moveManager = moveManager;
            _moveManager.OnPlayerSwitched += SwitchPointer;
            
            _firstPlayerView.Init(moveManager._firstPlayer);
            _secondPlayerView.Init(moveManager._secondPlayer);
        }

        private void OnDestroy()
        {
            _moveManager.OnPlayerSwitched -= SwitchPointer;
        }

        /// <summary>
        /// Switches the pointer
        /// </summary>
        private void SwitchPointer()
        {
            _firstPlayerPointer.SetActive(!_firstPlayerPointer.activeSelf);
            _secondPlayerPointer.SetActive(!_secondPlayerPointer.activeSelf);
        }
    }
}