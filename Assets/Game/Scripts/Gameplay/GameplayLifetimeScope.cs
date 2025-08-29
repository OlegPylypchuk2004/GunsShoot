using BlasterSystem;
using CameraManagment;
using ComboSystem;
using GameModeSystem;
using GameModeSystem.Modes;
using Gameplay.States;
using Gameplay.UI;
using Global;
using HealthSystem;
using InputSystem;
using LeTai.Asset.TranslucentImage;
using ObstacleSystem;
using Patterns.StateMachine;
using PauseManagment;
using SceneManagment;
using ScoreSystem;
using StageSystem;
using System;
using TimeManagment;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Gameplay
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        [Header("Gameplay"), SerializeField] private BlasterController _blasterController;
        [SerializeField] private CameraShaker _cameraShaker;
        [SerializeField] private ObstacleSpawner _obstacleSpawner;

        [Header("UI"), SerializeField] private TranslucentImageSource _translucentImageSource;
        [SerializeField] private BlurBackground _blurBackground;
        [SerializeField] private GameOverPanel _gameOverDisplay;

        [Header("Prefabs"), SerializeField] private ScoreDisplay _scoreDisplayPrefab;
        [SerializeField] private StagesLeftDisplay _starsLeftDisplayPrefab;

        [Header("Configs"), SerializeField] private ComboConfig _comboConfig;
        [SerializeField] private TimeConfig _timeConfig;

        private IContainerBuilder _builder;

        protected override void Awake()
        {
            base.Awake();

            switch (LocalGameData.GameModeConfig.Type)
            {
                case GameModeType.Endless:

                    Container.Resolve<ScoreDisplay>();

                    break;

                case GameModeType.Level:

                    Container.Resolve<ScoreDisplay>();
                    Container.Resolve<StagesLeftDisplay>();

                    break;
            }
        }

        protected override void Configure(IContainerBuilder builder)
        {
            _builder = builder;

            RegisterGameMode();
            RegisterSystems();
            RegisterInputHandler();
            RegisterComponents();
            RegisterStateMachine();
            RegisterUI();
        }

        private void RegisterGameMode()
        {
            switch (LocalGameData.GameModeConfig.Type)
            {
                case GameModeType.Endless:

                    _builder.Register<EndlessGameMode>(Lifetime.Singleton)
                        .As<IGameMode>();

                    break;

                case GameModeType.Level:

                    _builder.Register<LevelGameMode>(Lifetime.Singleton)
                        .As<IGameMode>();

                    break;

                default:
                    throw new Exception("Game mode type not defined");
            }
        }

        private void RegisterSystems()
        {
            _builder.RegisterEntryPoint<GameplayManager>(Lifetime.Singleton);
            _builder.Register<SceneLoader>(Lifetime.Singleton);
            _builder.Register<PauseHandler>(Lifetime.Singleton);
            _builder.Register<StageManager>(Lifetime.Singleton);
            _builder.Register<BlasterHolder>(Lifetime.Singleton);
            _builder.Register<ObstacleContainer>(Lifetime.Singleton);
            _builder.Register<HealthManager>(Lifetime.Singleton);
            _builder.Register<ScoreCounter>(Lifetime.Singleton);

            _builder.Register<ComboCounter>(Lifetime.Singleton)
                .WithParameter(_comboConfig);

            _builder.Register<TimeSlower>(Lifetime.Singleton)
                .WithParameter(_timeConfig);
        }

        private void RegisterInputHandler()
        {
#if UNITY_EDITOR
            _builder.Register<MouseInputHandler>(Lifetime.Singleton)
                .As<IInputHandler>();
#else
            _builder.Register<MobileInputHandler>(Lifetime.Singleton)
                .As<IInputHandler>();
#endif
        }

        private void RegisterComponents()
        {
            _builder.RegisterComponent(_blasterController);
            _builder.RegisterComponent(_cameraShaker);
            _builder.RegisterComponent(_obstacleSpawner);
        }

        private void RegisterStateMachine()
        {
            _builder.Register<StateMachine>(Lifetime.Singleton);

            _builder.Register<PreGameplayState>(Lifetime.Singleton);
            _builder.Register<GameplayState>(Lifetime.Singleton);
            _builder.Register<GameOverState>(Lifetime.Singleton);
        }

        private void RegisterUI()
        {
            _builder.RegisterComponent<BlurBackground>(_blurBackground);
            _builder.RegisterComponent<GameOverPanel>(_gameOverDisplay);
            _builder.RegisterComponentInNewPrefab(_scoreDisplayPrefab, Lifetime.Singleton);
            _builder.RegisterComponentInNewPrefab(_starsLeftDisplayPrefab, Lifetime.Singleton);
        }
    }
}