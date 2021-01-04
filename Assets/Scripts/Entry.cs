using System;
using UnityEngine;
using FieldData;
using CueData;
using Models;
using Balls;
using Views;

public class Entry : MonoBehaviour
{
    [Header("Prefabs for factories")]
    
    [SerializeField] private Triangle _trianglePrefab = default;
    [SerializeField] private Cue _cuePrefab = default;
    [SerializeField] private Field _fieldPrefab = default;
    
    [Header("Models")]
    
    [SerializeField] private AudioManager _audioManager = default;
    [SerializeField] private Animations _animations = default;

    [Header("Views")]
    
    [SerializeField] private HomeView _homeView = default;
    [SerializeField] private SettingsView _settingsView = default;
    [SerializeField] private GameSessionView _gameSessionView = default;
    
    private void Awake()
    {
        var ballsFactory = new Func<Triangle>(() =>
        {
            var triangle = Instantiate(_trianglePrefab);
            triangle.Init(_audioManager);

            return triangle;
        });
        
        var fieldFactory = new Func<Triangle, Field>(triangle =>
        {
            var field = Instantiate(_fieldPrefab);
            field.Init(triangle.AllBalls, triangle.WhiteBall);

            return field;
        });

        var cueFactory = new Func<Triangle, Field, Cue>((triangle, field) =>
        {
            var cue = Instantiate(_cuePrefab);
            cue.Init(triangle.WhiteBall, field, _animations);
            
            return cue;
        });
        
        // Model-View initializations
        
        var gameSession = new GameSession(ballsFactory, fieldFactory, cueFactory);
        _gameSessionView.Init(gameSession);

        _homeView.Init(gameSession);
        
        var settings = new Settings(_audioManager);
        _settingsView.Init(settings);
    }
}