using GameFramework.Fsm;
using UnityGameFramework.Runtime;
namespace Game.Logic
{
    /// <summary>
    /// 站立逻辑层
    /// </summary>
    public class IdleState : FsmState<Role>
    {
        protected override void OnInit(IFsm<Role> fsm)
        {
            base.OnInit(fsm);
            Log.Info("创建站立状态。");
        }

        protected override void OnDestroy(IFsm<Role> fsm)
        {
            base.OnDestroy(fsm);
            Log.Info("销毁站立状态。");
        }

        protected override void OnEnter(IFsm<Role> fsm)
        {
            base.OnEnter(fsm);
            Log.Info("进入站立状态。");
        }

        protected override void OnLeave(IFsm<Role> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            Log.Info("离开站立状态。");
        }

        protected override void OnUpdate(IFsm<Role> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            Log.Info("轮询站立状态。");
        }
    }
}