using GameFramework.Fsm;
using UnityGameFramework.Runtime;
namespace Game.Show
{
    /// <summary>
    /// 死亡状态显示层
    /// </summary>
    public class DeadState : FsmState<Role>
    {
        protected override void OnInit(IFsm<Role> fsm)
        {
            base.OnInit(fsm);
            Log.Info("创建死亡状态。");
        }

        protected override void OnDestroy(IFsm<Role> fsm)
        {
            base.OnDestroy(fsm);
            Log.Info("销毁死亡状态。");
        }

        protected override void OnEnter(IFsm<Role> fsm)
        {
            base.OnEnter(fsm);
            Log.Info("进入死亡状态。");
        }

        protected override void OnLeave(IFsm<Role> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            Log.Info("离开死亡状态。");
        }

        protected override void OnUpdate(IFsm<Role> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            Log.Info("轮询死亡状态。");
        }
    }
}