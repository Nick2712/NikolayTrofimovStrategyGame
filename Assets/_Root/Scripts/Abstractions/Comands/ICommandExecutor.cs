namespace NikolayTrofimov_StrategyGame.Abstractions
{
    public interface ICommandExecutor
    {
        void ExecuteCommand(object command);
    }
}