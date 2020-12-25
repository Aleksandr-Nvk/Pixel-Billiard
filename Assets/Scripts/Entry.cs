using UnityEngine;
using FieldData;
using CueData;
using Models;
using Balls;
using Views;

public class Entry : MonoBehaviour
{
    [Header("View factories' data")]
    
    [SerializeField] private GameSessionView _gameSessionViewPrefab = default;
    
    [SerializeField] private PlayerView _firstPlayerViewPrefab = default;
    [SerializeField] private PlayerView _secondPlayerViewPrefab = default;
    
    [Header("Field object factories' data")]
    
    [SerializeField] private Triangle _trianglePrefab = default;
    [SerializeField] private Cue _cuePrefab = default;
    [SerializeField] private Field _fieldPrefab = default;
    
    [Header("Models")]
    
    [SerializeField] private AudioManager _audioManager = default;

    [Header("Views")]
    
    [SerializeField] private HomeView _homeView = default;
    [SerializeField] private SettingsView _settingsView = default;
    

    private void Awake()
    {
        var ballsFactory = new BallsFactory(_trianglePrefab, _audioManager);
        var fieldFactory = new FieldFactory(_fieldPrefab);
        var cueFactory = new CueFactory(_cuePrefab);
        
        var gameSessionViewFactory = new GameSessionViewFactory(_gameSessionViewPrefab, _homeView);
        var playerViewFactory = new PlayerViewFactory();
        
        var gameSession = new GameSession(ballsFactory, fieldFactory, cueFactory, gameSessionViewFactory,
            playerViewFactory, _firstPlayerViewPrefab, _secondPlayerViewPrefab);

        var settings = new Settings(_audioManager);
        _homeView.Init(gameSession);
        _settingsView.Init(settings);
    }
}