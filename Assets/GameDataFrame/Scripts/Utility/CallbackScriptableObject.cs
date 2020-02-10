using System.Collections;
using UnityEngine;

namespace GameDataFrame
{
    /// <summary>
    ///     回调型ScriptableObject类
    /// </summary>
    public class CallbackScriptableObject : ScriptableObject
    {
        // 回调：数据加载完毕
        public virtual void OnLoadFinished()
        {
        }

        public virtual void Release()
        {
        }

        protected void ClearDictionary(IDictionary source)
        {
            source?.Clear();
        }

        protected void ClearList(IList source)
        {
            source?.Clear();
        }
    }
}