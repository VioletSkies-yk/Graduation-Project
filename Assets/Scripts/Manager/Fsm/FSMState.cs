using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 转换条件
/// </summary>
public enum Transition
{
    NullTransition = 0,//空的转换条件

    PutAorD,
    BackToStand,
    Assassin,
    JumpToRun,
    DashToRun,
    PutSpace,
    FallToGround,
    PutLeftShift,
    DodgeOver,
    PutF,
    HookTrans,
    BeingAttacked,
    ATD,


    SeePlayer,//看到玩家
    AttackPlayer,//攻击玩家
    ATC,
    LostPlayer,//追赶过程中遗失目标玩家回到巡逻
    BTS,//追赶过程中遗失目标玩家回到站立
    Die,

    BossGTF,
    BossFTA,
    BossFTG,
    BossATF,
    BossTD
}

/// <summary>
/// 当前状态
/// </summary>
public enum StateID
{
    Stand,
    Run,
    Jump,
    Dash,
    Hook1,
    Hook2,
    BackStab,
    Attacked,
    PlayerDie,

    NullState,//空的状态
    Patrol,//巡逻状态
    Chase,//追赶状态
    Die,//死亡状态
    EnemyStand,
    EnemyAttack,

    BossGround,
    BossFly,
    BossAttack,
    BossDie
}

public abstract class FSMState
{
    protected StateID stateID;
    public StateID StateID { get { return stateID; } }
    protected Dictionary<Transition, StateID> transitionStateDic = new Dictionary<Transition, StateID>();
    protected FSMSystem fSMSystem;

    public FSMState(FSMSystem fSMSystem)
    {
        this.fSMSystem = fSMSystem;
    }

    /// <summary>
    /// 添加转换条件
    /// </summary>
    /// <param name="trans">转换条件</param>
    /// <param name="id">转换条件满足时执行的状态</param>
    public void AddTransition(Transition trans, StateID id)
    {
        if (trans == Transition.NullTransition)
        {
            Debug.LogError("不允许NullTransition");
            return;
        }
        if (id == StateID.NullState)
        {
            Debug.LogError("不允许NullStateID");
            return;
        }
        if (transitionStateDic.ContainsKey(trans))
        {
            Debug.LogError("添加转换条件的时候" + trans + "已经存在于transitionStateDic中");
            return;
        }
        transitionStateDic.Add(trans, id);
    }
    /// <summary>
    /// 删除转换条件
    /// </summary>
    public void DeleteTransition(Transition trans)
    {
        if (trans == Transition.NullTransition)
        {
            Debug.LogError("不允许NullTransition");
            return;
        }
        if (!transitionStateDic.ContainsKey(trans))
        {
            Debug.LogError("删除转换条件的时候" + trans + "不存在于transitionStateDic中");
            return;
        }
        transitionStateDic.Remove(trans);
    }

    /// <summary>
    /// 获取当前转换条件下的状态
    /// </summary>
    public StateID GetOutputState(Transition trans)
    {
        if (transitionStateDic.ContainsKey(trans))
        {
            return transitionStateDic[trans];
        }
        return StateID.NullState;
    }
    /// <summary>
    /// 进入新状态之前做的事
    /// </summary>
    public virtual void DoBeforeEnter() { }
    /// <summary>
    /// 离开当前状态时做的事
    /// </summary>
    public virtual void DoAfterLeave() { }
    /// <summary>
    /// 当前状态所做的事
    /// </summary>
    public abstract void Act(GameObject npc);
    /// <summary>
    /// 当前状态所做的物理
    /// </summary>
    public virtual void FixedAct(GameObject npc) { }
    /// <summary>
    /// 在某一状态执行过程中，新的转换条件满足时做的事
    /// </summary>
    public abstract void Reason(GameObject npc);//判断转换条件

    /// <summary>
    /// 当前状态所做的物理
    /// </summary>
    public virtual void FixedReason(GameObject npc) { }
}


