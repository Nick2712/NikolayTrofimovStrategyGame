using System;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.Utils
{
    [CreateAssetMenu(fileName = nameof(AssetsContext), menuName = "StrategyGame/" + nameof(AssetsContext))]
    public sealed class AssetsContext : ScriptableObject
    {
        [SerializeField] private UnityEngine.Object[] _objects;

        public UnityEngine.Object GetObjectOfType(Type targetType, string targetName = null)
        {
            for (int i = 0; i < _objects.Length; i++)
            {
                var obj = _objects[i];
                if(obj.GetType().IsAssignableFrom(targetType))
                {
                    if(targetName == null || obj.name == targetName)
                    {
                        return obj;
                    }
                }
            }
            return null;
        }
    }
}