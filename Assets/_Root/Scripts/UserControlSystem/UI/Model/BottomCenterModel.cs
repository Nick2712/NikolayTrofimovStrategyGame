using NikolayTrofimov_StrategyGame.Abstractions;
using System;
using UniRx;
using UnityEngine;
using Zenject;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.Model
{
    public sealed class BottomCenterModel
    {
        public IObservable<IUnitProducer> UnitProducers { get; private set; }

        [Inject]
        public void Init(IObservable<ISelectable> currentlySelected)
        {
            UnitProducers = currentlySelected
                .Select(selectable => selectable as Component)
                .Select(component => component?.GetComponent<IUnitProducer>());
        }
    }
}