using Base;
using Infrastructure.Factory;
using UnityEditor;
using UnityEngine;

namespace Infrastructure.Services.Construct
{
    public class ConstructService : IConstructService
    {
        private IGameFactory _gameFactory;
        private BaseManager _builder;

        public ConstructService(IGameFactory factory)
        {
            _gameFactory = factory;
        }

        public void Construct(Vector3 constructPos)
        {
            if (_builder != null)
            {
                _builder.ConstructNewBase(constructPos);
                _builder = null;
                _gameFactory.CreateMarker(constructPos);
            }
        }

        public void ChooseBuilder(BaseManager builder)
        {
            if (_builder == null && !builder.Constructing)
            {
                _builder = builder;
            }
        }
    }
}