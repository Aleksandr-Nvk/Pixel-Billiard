using UnityEngine;
using FieldData;
using CueData;
using Models;
using Balls;
using Views;

public class Entry : MonoBehaviour
{
    [Header("Field object factories' data")]
    
    [SerializeField] private Triangle _trianglePrefab = default;
    [SerializeField] private Cue _cuePrefab = default;
    [SerializeField] private Field _fieldPrefab = default;
    
    [Header("Models")]
    
    [SerializeField] private AudioManager _audioManager = default;

    [Header("Views")]
    
    [SerializeField] private HomeView _homeView = default;
    [SerializeField] private SettingsView _settingsView = default;
    [SerializeField] private GameSessionView _gameSessionView = default;
    
    private void Awake()
    {
        var ballsFactory = new BallsFactory(_trianglePrefab, _audioManager);
        var fieldFactory = new FieldFactory(_fieldPrefab);
        var cueFactory = new CueFactory(_cuePrefab);
        
        var gameSession = new GameSession(ballsFactory, fieldFactory, cueFactory);

        _homeView.Init(gameSession);
        
        var settings = new Settings(_audioManager);
        _settingsView.Init(settings);
        
        _gameSessionView.Init(gameSession);
    }
}