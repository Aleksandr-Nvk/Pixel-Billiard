using UnityEngine.Advertisements;
using UnityEngine;
using FieldData;
using CueData;
using Zenject;
using System;
using Models;
using Balls;
using Views;

public class Entry : MonoInstaller
{
    [Header("Prefabs for factories")]
    
    [SerializeField] private Triangle _trianglePrefab = default;
    [SerializeField] private Cue _cuePrefab = default;
    [SerializeField] private Field _fieldPrefab = default;
    
    [Header("Models")]
    
    [SerializeField] private AudioManager _audioManager = default;
    [SerializeField] private Animations _animations = default;
    [SerializeField] private InputManager _inputManager = default;

    [Header("Views")]
    
    [SerializeField] private HomeView _homeView = default;
    [SerializeField] private SettingsView _settingsView = default;
    [SerializeField] private GameSessionView _gameSessionView = default;
    [SerializeField] private PauseView _pauseView = default;
    [SerializeField] private GameOverView _gameOverView = default;
    
    public override void InstallBindings()
    {
        Advertisement.Initialize("3966907", true);
        
        var ballsFactory = new Func<Triangle>(() =>
        {
            var triangle = Instantiate(_trianglePrefab);
            triangle.Init(_audioManager);

            return triangle;
        });
        
        var fieldFactory = new Func<Triangle, Field>(triangle =>
        {
            var field = Instantiate(_fieldPrefab);
            field.Init(triangle.AllBalls, triangle.WhiteBall, _inputManager);

            return field;
        });

        var cueFactory = new Func<Triangle, Field, Cue>((triangle, field) =>
        {
            var cue = Instantiate(_cuePrefab);
            cue.Init(triangle.WhiteBall, field, _inputManager, _animations);
            
            return cue;
        });
        
        // Injections
        
        Container.Bind<Func<Triangle>>().FromInstance(ballsFactory).AsSingle();
        Container.Bind<Func<Triangle, Field>>().FromInstance(fieldFactory).AsSingle();
        Container.Bind<Func<Triangle, Field, Cue>>().FromInstance(cueFactory).AsSingle();

        Container.Bind<InputManager>().FromInstance(_inputManager).AsSingle();

        Container.Bind<GameSession>().FromNew().AsSingle();
        Container.Bind<GameSessionView>().FromInstance(_gameSessionView).AsSingle();
        
        Container.Bind<HomeView>().FromInstance(_homeView).AsSingle();

        Container.Bind<Settings>().FromNew().AsSingle().WithArguments(_audioManager);
        Container.Bind<SettingsView>().FromInstance(_settingsView).AsSingle();
        
        Container.Bind<PauseView>().FromInstance(_pauseView).AsSingle();
        
        Container.Bind<GameOverView>().FromInstance(_gameOverView).AsSingle();
    }
}