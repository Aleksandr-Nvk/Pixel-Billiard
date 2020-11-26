using UnityEngine.UI;
using UnityEngine;
using Models;
using TMPro;

namespace Views
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameSession _gameSession = default;
        
        [Space]
        
        [SerializeField] private GameObject _moveView = default;
        [SerializeField] private GameObject _gameplayRoot = default;
        
        [SerializeField] private TextMeshProUGUI _titleText1 = default;
        [SerializeField] private TextMeshProUGUI _titleText2 = default;
        
        [Space]
        
        [SerializeField] private Button _playButton = default;
        [SerializeField] private Image _playButtonImage = default;

        
        public void Init(GameSession gameSession)
        {
            _gameSession = gameSession;
            
            _playButton.onClick.AddListener(Play);

            _gameSession.OnSessionEnded += ShowResultView;
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(Play);
        }

        /// <summary>
        /// Play button method. Starts a new session. Hides the main view and shows the gameplay root objects
        /// </summary>
        private void Play()
        {
            HideMainView();
            _moveView.SetActive(true);
            _gameplayRoot.SetActive(true);
        }

        /// <summary>
        /// Replay button method. Resets the session and hides the result or pause view
        /// </summary>
        private void Replay()
        {
            _gameSession.ResetSession();
            // hide result or pause view
        }

        /// <summary>
        /// Hides the main view
        /// </summary>
        private void HideMainView()
        {
            _playButton.interactable = false;
            Animations.Fade(_playButtonImage, 0f, 1f, true);
            
            Animations.Fade(_titleText1, 0f, 1f, true);
            Animations.Fade(_titleText2, 0f, 1f, true);
        }
        
        /// <summary>
        /// Shows the main view
        /// </summary>
        private void ShowMainView()
        {
            _playButton.interactable = true;
            Animations.Fade(_playButtonImage, 1f, 1f);
            
            Animations.Fade(_titleText1, 1f, 1f);
            Animations.Fade(_titleText2, 1f, 1f);
        }

        /// <summary>
        /// Shows data about winner
        /// </summary>
        /// <param name="winner"> Player who won the game lol </param>
        private void ShowResultView(Player winner)
        {
            // show result view
        }
    }
}