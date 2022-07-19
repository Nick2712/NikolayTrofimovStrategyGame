using NikolayTrofimov_StrategyGame.Abstractions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.Core
{
    public class FactionMember : MonoBehaviour, IFactionMember
    {
        public static int FactionsCount
        {
            get
            {
                lock (_membersCount)
                {
                    return _membersCount.Count;
                }
            }
        }

        public int FactionId => _factionId;
        [SerializeField] private int _factionId;

        private static readonly Dictionary<int, List<int>> _membersCount = new();
        private static int _instanceIdGenerator;
        [SerializeField] private int _instanceId;


        public static int GetWinner()
        {
            lock (_membersCount)
            {
                return _membersCount.Keys.First();
            }
        }

        private void Awake()
        {
            _instanceIdGenerator++;
            _instanceId = _instanceIdGenerator;
            if (_factionId != 0) Register();
        }

        private void Register()
        {
            lock (_membersCount)
            {
                if (!_membersCount.ContainsKey(_factionId))
                    _membersCount.Add(_factionId, new List<int>());
                if (!_membersCount[_factionId].Contains(_instanceId))
                    _membersCount[_factionId].Add(_instanceId);
            }
        }

        private void OnDestroy()
        {
            Unregister();
        }

        private void Unregister()
        {
            lock(_membersCount)
            {
                if (_membersCount[_factionId].Contains(_instanceId))
                    _membersCount[_factionId].Remove(_instanceId);
                if (_membersCount[_factionId].Count == 0)
                    _membersCount.Remove(_factionId);
            }
        }

        public void SetFaction(int factionId)
        {
            _factionId = factionId;
            Register();
        }
    }
}