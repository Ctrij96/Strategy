using Base;
using Infrastructure.Factory;
using Unity.AI.Navigation;
using UnityEditor.SceneTemplate;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        private const string Surface = "Surface";
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
            
            var handler = _gameFactory.CreateCameraHandler();
            Camera.main!.transform.SetParent(handler.transform);
            
            Vector3 initialPoint = GameObject.FindWithTag(InitialPointTag).transform.position;
            RenderFirstBase(initialPoint);
            
            _gameFactory.CreateOreGenerator();

            _gameFactory.CreateConstructHandler();
        }

        private void RenderFirstBase(Vector3 initialPoint)
        {
            GameObject firstBase = _gameFactory.CreateBase(at: initialPoint);
            GameObject.FindWithTag(Surface).GetComponent<NavMeshSurface>().BuildNavMesh();
            BaseManager baseManager = firstBase.GetComponent<BaseManager>();
            baseManager.AddWorker();
            baseManager.AddWorker();
            baseManager.AddWorker();

        }

        public void Exit()
        {
        }
    }
}