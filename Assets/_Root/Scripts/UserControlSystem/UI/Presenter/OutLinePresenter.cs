using NikolayTrofimov_StrategyGame.Abstractions;
using NikolayTrofimov_StrategyGame.UserControlSystem.Model;
using NikolayTrofimov_StrategyGame.UserControlSystem.View;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.Presenter
{
    public class OutLinePresenter : MonoBehaviour
    {
        [SerializeField] private SelectableValue _selectable;
        [SerializeField] private Material[] _outLine;

        private ISelectable _currentSelectable;
        private OutlineSelectorView _currentOutlineSelector;


        private void Start()
        {
            _selectable.OnSelected += OnSelected;
        }

        private void OnSelected(ISelectable selectable)
        {
            if (_currentSelectable == selectable) return;

            _currentSelectable = selectable;

            SetSelected(false, _currentOutlineSelector);
            
            if(selectable != null)
            {
                _currentOutlineSelector = (selectable as Component).GetComponent<OutlineSelectorView>();
                SetSelected(true, _currentOutlineSelector);
            }
        }

        private void SetSelected(bool isSelected, OutlineSelectorView outlineSelector)
        {
            if (outlineSelector != null)
                outlineSelector.SetSelected(isSelected, _outLine);
        }
    }
}