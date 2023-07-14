using GameFramework;
using GameFramework.DataTable;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game
{
    public class NomalGame : GameBase
    {
        private float m_ElapseSeconds = 0f;

        public override GameMode GameMode
        {
            get
            {
                return GameMode.Normal;
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            Log.Info("game run");
        }

        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            base.Update(elapseSeconds, realElapseSeconds);
        }


    }
}
