using UnityEngine.UI;
using UnityEngine;
using Models;
using TMPro;

namespace Views
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameSession _gameSession = default;
        
        [SerializeField] private GameObject _moveView = default;
        [SerializeField] private GameObject _gameplayRoot = default;
        
        [Header("Main View")]
        
        [SerializeField] private TextMeshProUGUI _titleText1 = default;
        [SerializeField] private TextMeshProUGUI _titleText2 = default;

        [SerializeField] private Button _playButton = default;
        [SerializeField] private Image _playButtonImage = default;

        [Header("Settings View")]
        
        [SerializeField] private Image _settingsView = default;

        [SerializeField] private Button _settingsButton = default;
        [SerializeField] private Image _settingsButtonImage = default;
        
        [SerializeField] private Button _closeSettingsButton = default;
        [SerializeField] private Image _closeSettingsButtonImage = default;

        [SerializeField] private Button _musicButton = default;
        [SerializeField] private Image _musicButtonImage = default;
        
        [SerializeField] private Button _soundButton = default;
        [SerializeField] private Image _soundButtonImage = default;

        [SerializeField] private Button _aboutButton = default;
        [SerializeField] private Image _aboutButtonImage = default;
        
        public void Init(GameSession gameSession)
        {
            _gameSession = gameSession;
            _gameSession.OnSessionEnded += SetResultViewActivity;

            _playButton.onClick.AddListener(Play);
            
            _settingsButton.onClick.AddListener(Settings);
            _closeSettingsButton.onClick.AddListener(ExitSettings);
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(Play);
            
            _settingsButton.onClick.RemoveListener(Settings);
            _closeSettingsButton.onClick.RemoveListener(ExitSettings);
        }

        #region Unity Buttons Methods

        /// <summary>
        /// Play button method. Starts a new session. Hides the main view and shows the gameplay root objects
        /// </summary>
        private void Play()
        {
            SetMainViewActivity(false);
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
        /// Settings button method. Hides the main view and shows the settings view
        /// </summary>
        private void Settings()
        {
            SetMainViewActivity(false);
            SetSettingsViewActivity(true);
        }

        /// <summary>
        /// Close settings button method. Hides the settings view and shows the main view
        /// </summary>
        private void ExitSettings()
        {
            SetSettingsViewActivity(false);
            SetMainViewActivity(true);
        }

        #endregion

        /// <summary>
        /// Shows or hides the main view
        /// </summary>
        private void SetMainViewActivity(bool isShown)
        {
            var targetAlpha = isShown ? 1f : 0f;
            
            _playButton.interactable = isShown;
            Animations.Fade(_playButtonImage, targetAlpha, 1f);
            _settingsButton.interactable = isShown;
            Animations.Fade(_settingsButtonImage, targetAlpha, 1f);
            
            Animations.Fade(_titleText1, targetAlpha, 1f);
            Animations.Fade(_titleText2, targetAlpha, 1f);
        }

        /// <summary>
        /// Shows or hides the settings view
        /// </summary>
        private void SetSettingsViewActivity(bool isShown)
        {
            var targetAlpha = isShown ? 1f : 0f;
            
            Animations.Fade(_settingsView, targetAlpha, 1f);
            
            _closeSettingsButton.interactable = isShown;
            Animations.Fade(_closeSettingsButtonImage, targetAlpha, 1f);
            
            _musicButton.interactable = isShown;
            Animations.Fade(_musicButtonImage, targetAlpha, 1f);
            _soundButton.interactable = isShown;
            Animations.Fade(_soundButtonImage, targetAlpha, 1f);
            _aboutButton.interactable = isShown;
            Animations.Fade(_aboutButtonImage, targetAlpha, 1f);
        }
        
        /// <summary>
        /// Shows or hides the result view
        /// </summary>
        /// <param name="winner"> Player who won the game lol </param>
        private void SetResultViewActivity(Player winner)
        {
            // show result view
        }
    }
}