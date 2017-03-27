using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SubPointSolutions.Shelly.Core;
using SubPointSolutions.Shelly.Core.Exceptions;
using SubPointSolutions.Shelly.Core.Utils;
using SubPointSolutions.Shelly.Desktop.Controls;

namespace SubPointSolutions.Shelly.Desktop.Utils
{
    public static class ShUtils
    {
        #region static
        static ShUtils()
        {
            uiAssemblies = new List<Assembly>();
        }
        #endregion

        #region propertis

        private static bool hasInitedAssemblies;
        private static List<Assembly> uiAssemblies;

        #endregion

        #region methods

        static private void InitAssemblies()
        {
            var asm = new List<Assembly>();

            asm.AddRange(ShServiceContainer.Instance.AppMetadataService.AppDataServiceAssemblies);
            asm.AddRange(ShServiceContainer.Instance.AppMetadataService.AppPluginsAssemblies);
            asm.AddRange(ShServiceContainer.Instance.AppMetadataService.AppUIServiceAssemblies);

            foreach (var a in asm)
            {
                if (!uiAssemblies.Contains(a))
                    uiAssemblies.Add(a);
            }
        }

        public static Type GetUIComponentType<TTargetType>()
        {
            return GetUIComponentType(typeof(TTargetType), null);
        }

        public static Type GetUIComponentType<TTargetType, TFallbackType>()
        {
            return GetUIComponentType(typeof(TTargetType), typeof(TFallbackType));
        }

        public static Type GetUIComponentType(Type targetType, Type fallbackType)
        {
            if (!hasInitedAssemblies)
            {
                InitAssemblies();
                hasInitedAssemblies = true;
            }

            Type result = null;

            var allTypes = ShReflectionUtils.GetTypesFromAssemblies(targetType, uiAssemblies)
                .Where(t => typeof(ShUserControlBase).IsAssignableFrom(t)
                            && !t.IsAbstract);

            if (allTypes.Count() == 0)
                throw new ShAppException("Can't find implementation for type: " + targetType.Name);


            if (allTypes.Count() == 1)
                return allTypes.First();

            if (fallbackType != null)
            {
                var implType = allTypes.FirstOrDefault(t => t != fallbackType);

                if (implType != null)
                    return implType;

                return fallbackType;
            }

            throw new ShAppException("Can't find fallback type for type: " + targetType.Name);
        } 

        #endregion
    }
}