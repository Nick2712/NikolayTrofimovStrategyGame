using System;
using System.Linq;
using System.Reflection;


namespace NikolayTrofimov_StrategyGame.Utils
{
    public static class AssetsInjector
    {
        private static readonly Type _injectAssetAttributeType = typeof(InjectAssetAttribute);

        public static T Inject<T>(this AssetsContext context, T target)
        {
            var targetType = target.GetType();
            var allFields = targetType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).ToList();
            while(targetType.BaseType != typeof(object))
            {
                targetType = targetType.BaseType;
                allFields.AddRange(targetType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance));
            }

            for (int i = 0; i < allFields.Count; i++)
            {
                var fieldInfo = allFields[i];
                var injectAssetAttribute = fieldInfo.GetCustomAttribute(_injectAssetAttributeType) as InjectAssetAttribute;
                if(injectAssetAttribute == null)
                {
                    continue;
                }
                var objectToInject = context.GetObjectOfType(fieldInfo.FieldType, injectAssetAttribute.AssetName);
                fieldInfo.SetValue(target, objectToInject);
            }

            return target;
        }
    }
}