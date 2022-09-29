using Assets.Scripts.GamePlay.UI;
using System;
using UnityEngine;

/// <summary>
/// 游戏入口。
/// </summary>
public class GameManager : OsSingletonMono<GameManager>
{
    /// <summary>
    /// 是否正在加载关卡
    /// </summary>
    public bool IsLoadingLevel
    {
        get { return _isLoadingLevel; }

        set { _isLoadingLevel = value; }
    }
    private bool _isLoadingLevel = false;


    bool _pauseStatus = false;
    public Action GameResumeListener;
    public bool GamePaused
    {
        set
        {
            _pauseStatus = value;
            if (value)
            {
                TimeScaleManager.Instance.AddTimeScale(TimeScaleEnum.GamePause, 0);
            }
            else
            {
                TimeScaleManager.Instance.StopTimeScale(TimeScaleEnum.GamePause);
                if (GameResumeListener != null)
                {
                    GameResumeListener();
                    GameResumeListener = null;
                }
            }
            //			Time.timeScale = value ? 0f : 1f;
        }
        get
        {
            return _pauseStatus;
        }
    }


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        var node = UIManager.Instance.OpenUI(CONST.UI_BlackPanel);
        UIManager.Instance.OpenUI(CONST.UI_MainMenuPanel);
    }

    public void GameEntry(SaveData _data = null)
    {
        if (_data == null)
        {
            KaiUtils.Log("当前无本地数据，正在开始新游戏");
            SceneManager.Instance.LoadSceneAsync(CONST.SCENE_NAME_LEVEL_01, delegate ()
             {
                 UIManager.Instance.OpenUI(CONST.UI_BlackPanel);
             });
        }
        else
        {
            KaiUtils.Log("正在加载数据（测试  玩家拥有金币数量：{0}，玩家当前所在位置：{1}", _data.coins, new Vector3(_data.playerPositionX, _data.playerPositionY, _data.playerPositionZ));
            SceneManager.Instance.LoadSceneAsync(CONST.SCENE_NAME_LEVEL_01, null);
        }
    }
}
