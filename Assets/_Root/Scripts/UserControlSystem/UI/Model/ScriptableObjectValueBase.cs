using NikolayTrofimov_StrategyGame.Utils;
using System;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.Model
{
    public abstract class ScriptableObjectValueBase<T> : ScriptableObject, IAwaitable<T>
    {
        public class NewValueNotifier<TAwaited> : AwaiterBase<TAwaited>
        {
            private readonly ScriptableObjectValueBase<TAwaited> _scriptableObjectValueBase;
            

            public NewValueNotifier(ScriptableObjectValueBase<TAwaited> scriptableObjectValueBase)
            {
                _scriptableObjectValueBase = scriptableObjectValueBase;
                _scriptableObjectValueBase.OnNewValue += OnNewValue;
            }

            private void OnNewValue(TAwaited obj)
            {
                _scriptableObjectValueBase.OnNewValue -= OnNewValue;

                _result = obj;
                _isCompleted = true;
                _continuation?.Invoke();
            }
        }

        public T CurrentValue { get; private set; }
        public Action<T> OnNewValue;

        public void SetValue(T value)
        {
            CurrentValue = value;
            OnNewValue?.Invoke(CurrentValue);
        }

        public IAwaiter<T> GetAwaiter() => new NewValueNotifier<T>(this);
    }
}