using GameFramework.Fsm;
using UnityGameFramework.Runtime;
using Game.Show.Move;

namespace Game.Show
{
    /// <summary>
    /// 角色表现层
    /// </summary>
    public class Role
    {
        public static string FsmName = "ShowRoleFsm";
        public static string MoveFsmName = "MoveShowRoleFsm";

        /// <summary>
        /// 行为层状态机
        /// </summary>
        public IFsm<Role> fsm = null;
        /// <summary>
        /// 移动层状态机
        /// </summary>
        public IFsm<Role> moveFsm = null;
        /// <summary>
        /// 动画播放器
        /// </summary>
        AnimationPlayer animationPlayer;

        public Role(RoleData data)
        {
            fsm = GameEntry.Fsm.CreateFsm(Role.FsmName, this, new IdleState(), new MoveState(),new SkillState(),new DeadState());
            moveFsm = GameEntry.Fsm.CreateFsm(Role.MoveFsmName, this, new AnyState(), new MoveState(), new JumpState(), new FollowState(),new RotationToState());
        }

        public void Run()
        {
            fsm.Start<IdleState>();

        }
    }
}
