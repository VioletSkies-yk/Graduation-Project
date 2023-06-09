using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static partial class CONST
{
    #region 存档相关

    /// <summary>
    /// 存档成功
    /// </summary>
    public const string SaveDataSuccess = "SaveDataSuccess";

    #endregion

    #region 场景加载相关


    /// <summary>
    /// 发起加载场景
    /// </summary>
    public const string SendLoadingScene = "SendLoadingScene";

    /// <summary>
    /// 开始加载场景
    /// </summary>
    public const string StartLoadingSceneProgress = "StartLoadingSceneProgress";

    /// <summary>
    /// 正在加载场景（暂定每帧都在Trigger）
    /// </summary>
    public const string LoadSceneProgress = "LoadSceneProgress";

    /// <summary>
    /// 加载场景完成
    /// </summary>
    public const string FinishLoadingSceneProgress = "FinishLoadingSceneProgress";

    /// <summary>
    /// 过场背景全黑
    /// </summary>
    public const string LoadingImageAllBlack = "LoadingImageAllBlack";

    #endregion

    #region 关卡内切换相关

    /// <summary>
    /// 同一关卡内过关
    /// </summary>
    public const string PassLevelOnTheSameEpisode = "PassLevelOnTheSameEpisode";

    #endregion


    #region 模块之间调用相关

    /// <summary>
    /// 放开物体
    /// </summary>
    public const string ReleaseCatchItem = "ReleaseCatchItem";

    /// <summary>
    /// 梯子
    /// </summary>
    public const string LadderComplete = "LadderComplete";


    /// <summary>
    /// 进入电梯
    /// </summary>
    public const string EnterDianTi = "EnterDianTi";


    /// <summary>
    /// 显示器变色
    /// </summary>
    public const string Lv2Door = "Lv2Door";



    /// <summary>
    /// 显示器变色
    /// </summary>
    public const string OpenMuralPaintingDoor = "OpenMuralPaintingDoor";

    #endregion

    #region 音频

    /// <summary>
    /// 播放音频
    /// </summary>
    public const string PlayAudio = "PlayAudio";

    /// <summary>
    /// 关闭音频
    /// </summary>
    public const string StopAudio = "StopAudio";

    #endregion
}
