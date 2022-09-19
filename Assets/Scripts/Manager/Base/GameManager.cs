using Assets.Scripts.GamePlay.UI;
using UnityEngine;

/// <summary>
/// 游戏入口。
/// </summary>
public class GameManager : OsSingletonMono<GameManager>
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        var node=UIManager.Instance.OpenUI(CONST.UI_BlackPanel);
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
