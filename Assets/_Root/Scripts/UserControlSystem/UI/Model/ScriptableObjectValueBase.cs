using NikolayTrofimov_StrategyGame.Utils;
using System;
using UniRx;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.Model
{
    public abstract class ScriptableObjectValueBase<T> : ScriptableObject, IAwaitable<T>, IObservable<T>
    {
        public class NewValueNotifier<TAwaited> : AwaiterBase<TAwaited>
        {
            private readonly ScriptableObjectValueBase<TAwaited> _scriptableObjectValueBase;
            private readonly IDisposable _disposable;

            public NewValueNotifier(ScriptableObjectValueBase<TAwaited> scriptableObjectValueBase)
            {
                _scriptableObjectValueBase = scriptableObjectValueBase;
                _disposable = _scriptableObjectValueBase.ReactiveValue.Subscribe(OnNewValue);
            }

            private void OnNewValue(TAwaited obj)
            {
                _disposable.Dispose();

                _result = obj;
                _isCompleted = true;
                _continuation?.Invoke();
            }
        }


        public readonly ReactiveProperty<T> ReactiveValue = new();
        

        public void SetValue(T value)
        {
            ReactiveValue.Value = value;
        }

        public IAwaiter<T> GetAwaiter() => new NewValueNotifier<T>(this);

        public IDisposable Subscribe(IObserver<T> observer) => ReactiveValue.Subscribe(observer);
    }
}