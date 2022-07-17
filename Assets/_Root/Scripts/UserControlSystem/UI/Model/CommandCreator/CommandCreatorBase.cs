using NikolayTrofimov_StrategyGame.Abstractions;
using NikolayTrofimov_StrategyGame.Core;
using System;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.Model
{
    public abstract class CommandCreatorBase<T> where T : class, ICommand
    {
        public ICommandExecutor ProcessCommandExecutor (ICommandExecutor commandExecutor, Action<T> callback)
        {
            var classSpecificExecutor = commandExecutor as CommandExecutorBase<T>;
            if(classSpecificExecutor != null)
            {
                ClassSpecificCommandCreation(callback);
            }
            return classSpecificExecutor;
        }

        protected abstract void ClassSpecificCommandCreation(Action<T> creationCallback);

        public virtual void ProcessCancel() { }
    }
}