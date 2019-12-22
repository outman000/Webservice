using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Webservice.BuildingBlocks.EventBus.Extensions
{
    //命令总线的通用类型拓展
    public static class GenericTypeExtensions
    {
        /// <summary>
        /// 获取类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetGenericTypeName(this Type type)
        {
            var typeName = string.Empty;
            //是否是通用类型
            if (type.IsGenericType)
            {                      
                var genericTypes = string.Join(",", type.GetGenericArguments().Select(t => t.Name).ToArray());
                typeName = $"{type.Name.Remove(type.Name.IndexOf('`'))}<{genericTypes}>";
            }
            else
            {
                typeName = type.Name;
            }

            return typeName;
        }

        public static string GetGenericTypeName(this object @object)
        {
            return @object.GetType().GetGenericTypeName();
        }
    }
}
