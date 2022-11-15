using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static partial class CONST
{
    #region 场景命名

    /// <summary>
    /// 开始界面场景
    /// </summary>
    public const string SCENE_NAME_START = "SampleScene";

    /// <summary>
    /// 第一关场景
    /// </summary>
    public const string SCENE_NAME_LEVEL_01 = "GameScene_01";

    #endregion

    #region 主界面相关

    /// <summary>
    /// 默认存档名显示
    /// </summary>
    public const string SAVEMENU_NAME = "存档{0}";

    /// <summary>
    /// 空存档名显示
    /// </summary>
    public const string SAVEMENU_NONE_NAME = "空";

    /// <summary>
    /// 创建新存档提示
    /// </summary>
    public const string SAVEMENU_CREATE_TIP = "是否创建一个新的存档？";

    /// <summary>
    /// 覆盖存档提示
    /// </summary>
    public const string SAVEMENU_RECOVER_TIP = "是否覆盖这个存档？";

    #endregion
}
