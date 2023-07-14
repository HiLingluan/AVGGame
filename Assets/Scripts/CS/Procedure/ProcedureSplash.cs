//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Resource;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Game
{
    public class ProcedureSplash : ProcedureBase
    {
        public override bool UseNativeDialog
        {
            get
            {
                return true;
            }
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            // TODO: 这里可以播放一个 Splash 动画
            // ...

            // 编辑器模式
            if (GameEntry.Base.EditorResourceMode)
            {
                Log.Info("Editor resource mode run");
                ChangeState<ProcedurePreload>(procedureOwner);
            }
            // 单机模式
            else if (GameEntry.Resource.ResourceMode == ResourceMode.Package)
            {

                Log.Info("Package resource mode run");
                ChangeState<ProcedureInitResources>(procedureOwner);
            }
            else // 可更新模式
            {
                Log.Info("Updatable resource mode run");
                ChangeState<ProcedureCheckVersion>(procedureOwner);
            }
        }
    }
}
