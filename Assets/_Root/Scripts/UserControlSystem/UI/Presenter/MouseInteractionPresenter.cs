using NikolayTrofimov_StrategyGame.Abstractions;
using NikolayTrofimov_StrategyGame.UserControlSystem.Model;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.Presenter
{
    public sealed class MouseInteractionPresenter : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private SelectableValue _selectedObject;
        [SerializeField] private EventSystem _eventSystem;


        private void Update()
        {
            if (!Input.GetMouseButtonUp(0)) return;
            if (_eventSystem.IsPointerOverGameObject()) return;

            var hits = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition));
            if (hits.Length == 0) return;
            var selectable = hits
                .Select(hit => hit.collider.GetComponentInParent<ISelectable>())
                .FirstOrDefault(c => c != null);
            _selectedObject.SetValue(selectable);
        }
    }
}