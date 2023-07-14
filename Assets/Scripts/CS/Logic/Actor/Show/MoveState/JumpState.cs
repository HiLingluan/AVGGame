using GameFramework.Fsm;
using UnityGameFramework.Runtime;
using UnityEngine;

namespace Game.Show.Move
{
    /// <summary>
    /// 跳跃状态
    /// </summary>
    public class JumpState : FsmState<Role>
    {
        protected override void OnInit(IFsm<Role> fsm)
        {
            base.OnInit(fsm);

        }

        protected override void OnDestroy(IFsm<Role> fsm)
        {
            base.OnDestroy(fsm);
        }

        protected override void OnEnter(IFsm<Role> fsm)
        {
            base.OnEnter(fsm);

        }

        protected override void OnLeave(IFsm<Role> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);

        }

        protected override void OnUpdate(IFsm<Role> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        }
    }
}
