using Infrastructure.Factory;
using UnityEditor.SceneTemplate;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        private void OnLoaded()
        {
            if (Camera.main != null)
            {
                var handler = _gameFactory.CreateCameraHandler();
                Camera.main.transform.SetParent(handler.transform);
            }

            Vector3 initialPoint = new Vector3 (0,0,0);
            _gameFactory.CreateBase(at: initialPoint);
        }

        public void Exit()
        {
        }
    }
}