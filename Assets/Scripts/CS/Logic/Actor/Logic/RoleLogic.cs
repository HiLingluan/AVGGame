﻿using GameFramework.Fsm;
using UnityGameFramework.Runtime;

namespace ZFight.Logic
{
    /// <summary>
    /// 角色逻辑层
    /// </summary>
    public class Role
    {
        public static string FsmName = "LogicActorFsm";
        private IFsm<Role> fsm = null;

        public Role(RoleData data)
        {
            FsmComponent fsmComponent = GameEntry.GetComponent<FsmComponent>();
            fsm = fsmComponent.CreateFsm(Role.FsmName, this, new IdleState(), new MoveState(), new SkillState(), new DeadState());
        }

        public void Run()
        {
            fsm.Start<IdleState>();
        }
    }
}
