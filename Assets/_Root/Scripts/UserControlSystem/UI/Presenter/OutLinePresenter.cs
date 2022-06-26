using NikolayTrofimov_StrategyGame.Abstractions;
using NikolayTrofimov_StrategyGame.UserControlSystem.Model;
using System.Linq;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.Presenter
{
    public class OutLinePresenter : MonoBehaviour
    {
        [SerializeField] private SelectableValue _selectedObject;

        [SerializeField] private Material _outLineMask;
        [SerializeField] private Material _outLineFill;

        private Renderer[] _renderers;


        private void Start()
        {
            _selectedObject.OnSelected += OnSelected;

            _renderers = GetComponentsInChildren<Renderer>();
        }

        private void OnSelected(ISelectable selected)
        {
            SetOutLine(selected != null);
        }

        private void SetOutLine(bool isSelected)
        {
            if (isSelected)
            {
                foreach (var renderer in _renderers)
                {
                    var materials = renderer.sharedMaterials.ToList();

                    if(!materials.Contains(_outLineMask)) materials.Add(_outLineMask);
                    if(!materials.Contains(_outLineFill)) materials.Add(_outLineFill);
                    
                    renderer.materials = materials.ToArray();
                }
            }
            else
            {
                foreach (var renderer in _renderers)
                {
                    var materials = renderer.sharedMaterials.ToList();

                    materials.Remove(_outLineMask);
                    materials.Remove(_outLineFill);

                    renderer.materials = materials.ToArray();
                }
            }
        }
    }
}