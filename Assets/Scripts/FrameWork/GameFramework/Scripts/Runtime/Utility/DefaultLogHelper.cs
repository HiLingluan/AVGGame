//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 默认游戏框架日志辅助器。
    /// </summary>
    public class DefaultLogHelper : GameFrameworkLog.ILogHelper
    {
        /// <summary>
        /// 记录日志。
        /// </summary>
        /// <param name="level">日志等级。</param>
        /// <param name="message">日志内容。</param>
        public void Log(GameFrameworkLogLevel level, object message)
        {
            switch (level)
            {
                case GameFrameworkLogLevel.Debug:
#if ENABLE_LOG || ENABLE_DEBUG_LOG || ENABLE_DEBUG_AND_ABOVE_LOG
                    Debug.Log(Utility.Text.Format("<color=#888888>{0}</color>", message.ToString()));
#endif
                    break;

                case GameFrameworkLogLevel.Info:
#if ENABLE_LOG || ENABLE_INFO_LOG || ENABLE_DEBUG_AND_ABOVE_LOG || ENABLE_INFO_AND_ABOVE_LOG
                    Debug.Log(message.ToString());
#endif
                    break;

                case GameFrameworkLogLevel.Warning:
#if ENABLE_LOG || ENABLE_WARNING_LOG || ENABLE_DEBUG_AND_ABOVE_LOG || ENABLE_INFO_AND_ABOVE_LOG || ENABLE_WARNING_AND_ABOVE_LOG
                    Debug.LogWarning(message.ToString());
#endif
                    break;

                case GameFrameworkLogLevel.Error:
#if ENABLE_LOG || ENABLE_ERROR_LOG || ENABLE_DEBUG_AND_ABOVE_LOG || ENABLE_INFO_AND_ABOVE_LOG || ENABLE_WARNING_AND_ABOVE_LOG || ENABLE_ERROR_AND_ABOVE_LOG
                    Debug.LogError(message.ToString());
#endif
                    break;

                default:
                    throw new GameFrameworkException(message.ToString());
            }
        }
        
    }
}
