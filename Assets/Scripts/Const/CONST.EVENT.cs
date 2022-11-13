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

    #region 物体互动相关

    /// <summary>
    /// 安全屋开锁
    /// </summary>
    public const string OnPuttingDownTheKey = "OnPuttingDownTheKey";

    #endregion
}
