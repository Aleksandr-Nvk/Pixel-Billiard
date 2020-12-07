using UnityEngine;

namespace Views
{
    public class UIController : MonoBehaviour
    {
        [Header("Views")]
        
        [SerializeField] private HomeView _homeView = default;
        [SerializeField] private SettingsView _settingsView = default;
        
        public void Init(GameSession gameSession)
        {
            _homeView.Init();
            _settingsView.Init();
        }
    }
}