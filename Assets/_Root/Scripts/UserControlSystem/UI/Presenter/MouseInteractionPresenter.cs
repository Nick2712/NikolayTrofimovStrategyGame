using NikolayTrofimov_StrategyGame.Abstractions;
using NikolayTrofimov_StrategyGame.UserControlSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.Presenter
{
    public sealed class MouseInteractionPresenter : MonoBehaviour
    {
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private Camera _camera;
        [SerializeField] private SelectableValue _selectedObject;

        [SerializeField] private Vector3Value _groundClickRMB;
        [SerializeField] private Transform _groundTransform;

        [SerializeField] private AttackableValue _attackablesRMB;

        private Plane _groundPlane;
        private readonly List<IDisposable> _disposables = new(); 


        private void Start()
        {
            _groundPlane = new Plane(_groundTransform.up, 0);

            _disposables.Add(Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonUp(0))
                .Where(_ => !_eventSystem.IsPointerOverGameObject())
                .Subscribe(
                _ =>
                {
                    var ray = _camera.ScreenPointToRay(Input.mousePosition);
                    var hits = Physics.RaycastAll(ray);
                    if (WeHit<ISelectable>(hits, out var selectable))
                    {
                        _selectedObject.SetValue(selectable);
                    }
                    else
                    {
                        _selectedObject.SetValue(null);
                    }
                }));

            _disposables.Add(Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButton(1))
                .Where(_ => !_eventSystem.IsPointerOverGameObject())
                .Subscribe
                (
                _ =>
                {
                    var ray = _camera.ScreenPointToRay(Input.mousePosition);
                    var hits = Physics.RaycastAll(ray);
                    if (WeHit<IAttackable>(hits, out var attackable))
                    {
                        _attackablesRMB.SetValue(attackable);
                    }
                    else if (_groundPlane.Raycast(ray, out var enter))
                    {
                        _groundClickRMB.SetValue(ray.origin + ray.direction * enter);
                    }
                }));
        }

        private bool WeHit<T>(RaycastHit[] hits, out T result) where T : class
        {
            result = default;
            if (hits.Length == 0) return false;

            result = hits
                .Select(hit => hit.collider.GetComponentInParent<T>())
                .Where(c => c != null)
                .FirstOrDefault();
            return result != default;
        }

        private void OnDestroy()
        {
            foreach(var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}