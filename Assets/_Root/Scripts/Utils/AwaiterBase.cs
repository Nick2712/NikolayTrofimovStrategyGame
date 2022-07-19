using System;


namespace NikolayTrofimov_StrategyGame.Utils
{
    public abstract class AwaiterBase<TAwaited> : IAwaiter<TAwaited>
    {
        protected Action _continuation;
        protected bool _isCompleted;
        protected TAwaited _result;


        public void OnCompleted(Action continuation)
        {
            if (_isCompleted) continuation?.Invoke();
            else _continuation = continuation;
        }

        public bool IsCompleted => _isCompleted;
        public TAwaited GetResult() => _result;

        protected void OnWaitFinish(TAwaited result)
        {
            _result = result;
            _isCompleted = true;
            _continuation?.Invoke();
        }
    }
}