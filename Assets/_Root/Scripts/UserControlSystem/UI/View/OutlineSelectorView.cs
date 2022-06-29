using System.Linq;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.View
{
    public sealed class OutlineSelectorView : MonoBehaviour
    {
        private Renderer[] _renderers;
        private bool _isSelected;


        private void Start()
        {
            _renderers = GetComponentsInChildren<Renderer>();
        }

        public void SetSelected(bool isSelected, Material[] selectMaterials)
        {
            if (_isSelected == isSelected) return;

            foreach (var renderer in _renderers)
            {
                var materials = renderer.materials.ToList();
                if (isSelected)
                    materials.AddRange(selectMaterials);
                else
                    materials.RemoveRange(materials.Count - selectMaterials.Length, selectMaterials.Length);

                renderer.materials = materials.ToArray();
            }

            _isSelected = isSelected;
        }
    }
}