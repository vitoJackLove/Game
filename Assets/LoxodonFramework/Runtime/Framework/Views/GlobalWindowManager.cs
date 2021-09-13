using UnityEngine;

namespace Loxodon.Framework.Views
{
    [RequireComponent(typeof(RectTransform), typeof(Canvas))]
    public class GlobalWindowManager : GlobalWindowManagerBase
    {
        public static GlobalWindowManager Root;

        protected virtual void Start()
        {
            Root = this;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Root = null;
        }
    }
}
