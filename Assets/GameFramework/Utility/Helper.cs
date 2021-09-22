//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2019 Jiang Yin. All rights reserved.
// Homepage: http://gameframework.cn/
// Feedback: mailto:jiangyin@gameframework.cn
//------------------------------------------------------------

using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameFramework
{
    /// <summary>
    /// 辅助器创建器相关的实用函数。
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// 创建辅助器。
        /// </summary>
        /// <typeparam name="T">要创建的辅助器类型。</typeparam>
        /// <param name="helperTypeName">要创建的辅助器类型名称。</param>
        /// <param name="customHelper">若要创建的辅助器类型为空时，使用的自定义辅助器类型。</param>
        /// <returns>创建的辅助器。</returns>
        public static T CreateHelper<T>(string helperTypeName, T customHelper) where T : MonoBehaviour
        {
            return CreateHelper(helperTypeName, customHelper, 0);
        }

        /// <summary>
        /// 创建辅助器。
        /// </summary>
        /// <typeparam name="T">要创建的辅助器类型。</typeparam>
        /// <param name="helperTypeName">要创建的辅助器类型名称。</param>
        /// <param name="customHelper">若要创建的辅助器类型为空时，使用的自定义辅助器类型。</param>
        /// <param name="index">要创建的辅助器索引。</param>
        /// <returns>创建的辅助器。</returns>
        public static T CreateHelper<T>(string helperTypeName, T customHelper, int index) where T : MonoBehaviour
        {
            T helper = null;
            if (!string.IsNullOrEmpty(helperTypeName))
            {
                Type helperType = Utility.Assembly.GetType(helperTypeName);
                if (helperType == null)
                {
                    Debug.LogWarningFormat("Can not find helper type '{0}'.", helperTypeName);
                    return null;
                }

                if (!typeof(T).IsAssignableFrom(helperType))
                {
                    Debug.LogWarningFormat("Type '{0}' is not assignable from '{1}'.", typeof(T).FullName, helperType.FullName);
                    return null;
                }

                helper = (T)(new GameObject()).AddComponent(helperType);
            }
            else if (customHelper == null)
            {
                Debug.LogWarningFormat("You must set custom helper with '{0}' type first.", typeof(T).FullName);
                return null;
            }
            else if (customHelper.gameObject.InScene())
            {
                helper = (index > 0 ? Object.Instantiate(customHelper) : customHelper);
            }
            else
            {
                helper = Object.Instantiate(customHelper);
            }

            return helper;
        }
    }
}
