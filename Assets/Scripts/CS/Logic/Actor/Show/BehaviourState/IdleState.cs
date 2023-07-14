using GameFramework.Fsm;
using UnityGameFramework.Runtime;
namespace Game.Show
{
    /// <summary>
    /// 站立表现层
    /// </summary>
    public class IdleState : FsmState<Role>
    {
        protected override void OnInit(IFsm<Role> fsm)
        {
            // 创建有限状态机时调用
            base.OnInit(fsm);
            Log.Info("创建站立状态。");
        }

        protected override void OnDestroy(IFsm<Role> fsm)
        {
            // 销毁有限状态机时调用
            base.OnDestroy(fsm);
            Log.Info("销毁站立状态。");
        }

        protected override void OnEnter(IFsm<Role> fsm)
        {
            // 进入本状态时调用
            base.OnEnter(fsm);
            Log.Info("进入站立状态。");
        }

        protected override void OnLeave(IFsm<Role> fsm, bool isShutdown)
        {
            // 离开本状态时调用
            base.OnLeave(fsm, isShutdown);
            Log.Info("离开站立状态。");
        }

        protected override void OnUpdate(IFsm<Role> fsm, float elapseSeconds, float realElapseSeconds)
        {
            // 本状态被轮询时调用
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            Log.Info("轮询站立状态。");
        }
    }
}