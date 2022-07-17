using NikolayTrofimov_StrategyGame.Abstractions;
using UniRx;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.Core
{
    public class ProduceUnitCommandExecutor : CommandExecutorBase<IProduceUnitCommand>, IUnitProducer
    {
        [SerializeField] private Transform _unitsParent;
        [SerializeField] private int _maximumUnitsInQueue = 6;

        public IReadOnlyReactiveCollection<IUnitProductionTask> Queue => _queue;
        private ReactiveCollection<IUnitProductionTask> _queue = new();


        private void Update()
        {
            if (_queue.Count == 0) return;

            var innerTask = (UnitProductionTask)_queue[0];
            innerTask.TimeLeft -= Time.deltaTime;
            if(innerTask.TimeLeft <= 0)
            {
                RemoveTaskAtIndex(0);
                Vector3 position = new(Random.Range(-10, 10), 0, Random.Range(-10, 10));
                Instantiate(innerTask.UnitPrefab, position, Quaternion.identity, _unitsParent);
            }
        }

        public void Cancel(int index) => RemoveTaskAtIndex(index);

        private void RemoveTaskAtIndex(int index)
        {
            for (int i = index; i < _queue.Count - 1; i++)
            {
                _queue[i] = _queue[i + 1];
            }
            _queue.RemoveAt(_queue.Count - 1);
        }

        public override void ExecuteSpecificCommand(IProduceUnitCommand command)
        {
            if(_queue.Count < _maximumUnitsInQueue)
                _queue.Add(new UnitProductionTask(command.ProductionTime, command.Icon, command.UnitPrefab, command.UnitName));
        }
    }
}