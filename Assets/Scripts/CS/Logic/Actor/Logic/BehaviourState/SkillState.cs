using GameFramework.Fsm;
using UnityGameFramework.Runtime;
namespace Game.Logic
{
    /// <summary>
    /// 技能逻辑层
    /// </summary>
    public class SkillState : FsmState<Role>
    {
        protected override void OnInit(IFsm<Role> fsm)
        {
            base.OnInit(fsm);
            Log.Info("创建技能状态。");
        }

        protected override void OnDestroy(IFsm<Role> fsm)
        {
            base.OnDestroy(fsm);
            Log.Info("销毁技能状态。");
        }

        protected override void OnEnter(IFsm<Role> fsm)
        {
            base.OnEnter(fsm);
            Log.Info("进入技能状态。");
        }

        protected override void OnLeave(IFsm<Role> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            Log.Info("离开技能状态。");
        }

        protected override void OnUpdate(IFsm<Role> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            Log.Info("轮询技能状态。");
        }
    }
}