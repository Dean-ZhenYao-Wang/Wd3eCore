using System;
using Newtonsoft.Json.Linq;

namespace Wd3eCore.Entities
{
    public static class EntityExtensions
    {
        /// <summary>
        /// 提取指定类型的属性。
        /// </summary>
        /// <typeparam name="T">要提取的属性的类型。</typeparam>
        /// <returns>如果没有找到属性，则为请求类型的新实例。</returns>
        public static T As<T>(this IEntity entity) where T : new()
        {
            var typeName = typeof(T).Name;
            return entity.As<T>(typeName);
        }

        /// <summary>
        /// 提取指定名称的属性。
        /// </summary>
        /// <typeparam name="T">要提取的属性的类型。</typeparam>
        /// <param name="entity"></param>
        /// <param name="name">要提取的属性的名称。</param>
        /// <returns>如果没有找到属性，则为请求类型的新实例。</returns>
        public static T As<T>(this IEntity entity, string name) where T : new()
        {
            JToken value;

            if (entity.Properties.TryGetValue(name, out value))
            {
                return value.ToObject<T>();
            }

            return new T();
        }

        /// <summary>
        /// 指示指定的属性类型是否附加到<see cref="IEntity"/>实例。
        /// </summary>
        /// <typeparam name="T">要检查的属性的类型。</typeparam>
        /// <returns>如果发现属性则为true，否则为false。</returns>
        public static bool Has<T>(this IEntity entity)
        {
            var typeName = typeof(T).Name;
            return entity.Has(typeName);
        }

        /// <summary>
        /// 指示指定的属性是否附加到<see cref="IEntity"/>实例。
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="name">要检查的属性的名称。</param>
        /// <returns>如果发现属性则为true，否则为false。</returns>
        public static bool Has(this IEntity entity, string name)
        {
            return entity.Properties[name] != null;
        }

        public static IEntity Put<T>(this IEntity entity, T aspect) where T : new()
        {
            return entity.Put(typeof(T).Name, aspect);
        }

        public static IEntity Put(this IEntity entity, string name, object property)
        {
            entity.Properties[name] = JObject.FromObject(property);
            return entity;
        }

        /// <summary>
        /// 修改或创建 aspect.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="name">aspect的名称。</param>
        /// <param name="action">要应用于aspect的Action</param>
        /// <returns>当前 <see cref="IEntity"/> 实例。</returns>
        public static IEntity Alter<TAspect>(this IEntity entity, string name, Action<TAspect> action) where TAspect : new()
        {
            JToken value;
            TAspect obj;

            if (!entity.Properties.TryGetValue(name, out value))
            {
                obj = new TAspect();
            }
            else
            {
                obj = value.ToObject<TAspect>();
            }

            action?.Invoke(obj);

            entity.Put(name, obj);

            return entity;
        }
    }
}
