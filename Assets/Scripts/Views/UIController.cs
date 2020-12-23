using Models;
using UnityEngine;

namespace Views
{
    public class UIController : MonoBehaviour
    {
        [Header("Views")]
        
        [SerializeField] private HomeView _homeView = default;
        
        public void Init(GameSession gameSession, Settings settings)
        {
            _homeView.Init(gameSession, settings);
        }
    }
}