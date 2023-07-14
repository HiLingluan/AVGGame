using GameFramework.Fsm;
using UnityGameFramework.Runtime;
namespace Game.Logic
{
    /// <summary>
    /// 移动逻辑层
    /// </summary>
    public class MoveState : FsmState<Role>
    {
        protected override void OnInit(IFsm<Role> fsm)
        {
            base.OnInit(fsm);
            Log.Info("创建移动状态。");
        }

        protected override void OnDestroy(IFsm<Role> fsm)
        {
            base.OnDestroy(fsm);
            Log.Info("销毁移动状态。");
        }

        protected override void OnEnter(IFsm<Role> fsm)
        {
 
            base.OnEnter(fsm);
            Log.Info("进入移动状态。");
        }

        protected override void OnLeave(IFsm<Role> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            Log.Info("离开移动状态。");
        }

        protected override void OnUpdate(IFsm<Role> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            Log.Info("轮询移动状态。");
        }
    }
}