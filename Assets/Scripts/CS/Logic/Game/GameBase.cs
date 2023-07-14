//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game
{
    public abstract class GameBase
    {
        public abstract GameMode GameMode
        {
            get;
        }

        public bool GameOver
        {
            get;
            protected set;
        }

        public virtual void Initialize()
        {
            
        }

        public virtual void Shutdown()
        {
            
        }

        public virtual void Update(float elapseSeconds, float realElapseSeconds)
        {
            
        }

        protected virtual void OnShowEntitySuccess(object sender, GameEventArgs e)
        {
            
        }

        protected virtual void OnShowEntityFailure(object sender, GameEventArgs e)
        {
            
        }
    }
}
