using NikolayTrofimov_StrategyGame.Abstractions;
using NikolayTrofimov_StrategyGame.Core;
using NikolayTrofimov_StrategyGame.Utils;
using System;
using Zenject;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.Model
{
    public class ProduceUnit2CommandCommandCreator : CommandCreatorBase<IProduceUnit2Command>
    {
        [Inject] private AssetsContext _context;
        [Inject] private DiContainer _diContainer;


        protected override void ClassSpecificCommandCreation(Action<IProduceUnit2Command> creationCallback)
        {
            var produceUnitCommand = _context.Inject(new ProduceUnit2Command());
            _diContainer.Inject(produceUnitCommand);

            creationCallback?.Invoke(produceUnitCommand);
        }
    }
}