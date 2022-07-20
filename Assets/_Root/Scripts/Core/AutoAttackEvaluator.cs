﻿using NikolayTrofimov_StrategyGame.Abstractions;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.Core
{
    public class AutoAttackEvaluator : MonoBehaviour
    {
        public class FactionMemberParallelInfo
        {
            public Vector3 Position;
            public int Faction;

            public FactionMemberParallelInfo(Vector3 position, int faction)
            {
                Position = position;
                Faction = faction;
            }
        }

        public class AttackerParallelInfo
        {
            public float VisionRadius;
            public ICommand CurrentCommand;

            public AttackerParallelInfo(float visionRadius, ICommand currentCommand)
            {
                VisionRadius = visionRadius;
                CurrentCommand = currentCommand;
            }
        }

        public class Command
        {
            public GameObject Attacker;
            public GameObject Target;

            public Command(GameObject attacker, GameObject target)
            {
                Attacker = attacker;
                Target = target;
            }
        }


        public static ConcurrentDictionary<GameObject, AttackerParallelInfo> AttackersInfo = new();
        public static ConcurrentDictionary<GameObject, FactionMemberParallelInfo> FactionMembersInfo = new();

        public static Subject<Command> AutoAttackCommands = new();


        private void Update()
        {
            Parallel.ForEach(AttackersInfo, kvp => Evaluate(kvp.Key, kvp.Value));
        }

        private void Evaluate(GameObject go, AttackerParallelInfo info)
        {
            if (info.CurrentCommand is IMoveCommand) return;
            if (info.CurrentCommand is IAttackCommand && info.CurrentCommand is not Command) return;
            
            var factionInfo = default(FactionMemberParallelInfo);
            if(!FactionMembersInfo.TryGetValue(go, out factionInfo)) return;

            foreach(var (otherGO, otherFactionInfo) in FactionMembersInfo)
            {
                if (factionInfo.Faction == otherFactionInfo.Faction) continue;

                var distance = Vector3.Distance(factionInfo.Position, otherFactionInfo.Position);
                if (distance > info.VisionRadius) continue;

                AutoAttackCommands.OnNext(new Command(go, otherGO));
                break;
            }
        }
    }
}
