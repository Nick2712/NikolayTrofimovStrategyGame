using NikolayTrofimov_StrategyGame.Abstractions;
using NikolayTrofimov_StrategyGame.UserControlSystem.Model;
using NikolayTrofimov_StrategyGame.UserControlSystem.View;
using NikolayTrofimov_StrategyGame.UserControlSystem;
using NikolayTrofimov_StrategyGame.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.Presenter
{
    public sealed class CommandButtonPresenter : MonoBehaviour
    {
        [SerializeField] private SelectableValue _selectable;
        [SerializeField] private CommandButtonsView _view;
        [SerializeField] private AssetsContext _context;

        private ISelectable _currentSelectable;


        private void Start()
        {
            _selectable.OnSelected += OnSelected;
            OnSelected(_selectable.CurrentValue);

            _view.OnClick += OnButtonClick;
        }

        private void OnSelected(ISelectable selectable)
        {
            if (_currentSelectable == selectable) return;

            _currentSelectable = selectable;

            _view.Clear();
            if(selectable != null)
            {
                var commandExecutors = new List<ICommandExecutor>();
                commandExecutors.AddRange((selectable as Component).GetComponentsInParent<ICommandExecutor>());
                _view.MakeLayout(commandExecutors);
            }
        }

        private void OnButtonClick(ICommandExecutor commandExecutor)
        {
            var unitProducer = commandExecutor as CommandExecutorBase<IProduceUnitCommand>;
            if(unitProducer != null)
            {
                unitProducer.ExecuteSpecificCommand(_context.Inject(new ProduceUnitCommand()));
                return;
            }

            throw new ApplicationException($"{nameof(CommandButtonPresenter)}.{nameof(OnButtonClick)}: " + 
                $"Unknown type of commands executor: {commandExecutor.GetType().FullName}!");
        }


    }
}