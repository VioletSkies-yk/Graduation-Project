using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : OsSingletonMono<SceneManager>
{
    public int curSceneIndex
    {
        get;
        private set;
    }
    public int saveSceneIndex
    {
        get;
        private set
        {
            if (saveSceneIndex < value)
                saveSceneIndex = value;
        }
    }

    public bool isInPlayingScene
    {
        get
        {
            return curSceneIndex != -1 || curSceneIndex != 0;
        }
    }


    void Awake()
    {
        curSceneIndex = 0;
        DontDestroyOnLoad(this.gameObject);
    }

    public struct SceneMsg
    {
        public string sceneName;

        public Action callBack;

        public SceneMsg(string name, Action Func)
        {
            sceneName = name;
            callBack = Func;
        }
    }

    private void OnEnable()
    {
        EventManager.Instance.StartListening<SceneMsg>(CONST.SendLoadingScene, LoadSceneAsync);
    }
    private void OnDisable()
    {
        EventManager.Instance.StopListening<SceneMsg>(CONST.SendLoadingScene, LoadSceneAsync);
    }

    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="sceneName"></param>
    public void Load(string sceneName)
    {
        //if(UnityEngine.SceneManagement.SceneManager.sce)
        //{
        //    KaiUtils.Error("非法的参数:加载场景失败，指针超出场景个数");
        //    return;
        //}
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    //同步加载
    public void LoadScene(string sceneName, Action Func)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        Func();
    }

    //异步加载
    public void LoadSceneAsync(SceneMsg msg)
    {
        StartCoroutine(LoadingSceneAsync(msg.sceneName, msg.callBack));
    }

    //异步加载
    public void LoadSceneAsync(string sceneName, Action Func)
    {
        StartCoroutine(LoadingSceneAsync(sceneName, Func));
    }
    public IEnumerator LoadingSceneAsync(string sceneName, Action OnSecenLoaded)
    {
        GameManager.Instance.IsLoadingLevel = true;

        EventManager.Instance.TriggerEvent(CONST.StartLoadingSceneProgress);

        //暂时先等待一秒，作为过场动画
        yield return new WaitForSeconds(1f);

        AsyncOperation async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);

        //判定场景是否加载完成，通过协程实现加载进度的更新，可用于其他事件，外部也可以监听
        while (!async.isDone)
        {
            EventManager.Instance.TriggerEvent(CONST.LoadSceneProgress, async);
            yield return async.progress;
        }
        yield return async;
        OnSecenLoaded?.Invoke();

        GameManager.Instance.IsLoadingLevel = false;
        switch (sceneName)
        {
            case (CONST.SCENE_NAME_CACHE):
                curSceneIndex = -1;
                break;
            case (CONST.SCENE_NAME_START):
                curSceneIndex = 0;
                break;
            case (CONST.SCENE_NAME_LEVEL_01):
                curSceneIndex = 1;
                break;
            case (CONST.SCENE_NAME_LEVEL_02):
                curSceneIndex = 2;
                break;
        }
            saveSceneIndex = curSceneIndex;

        EventManager.Instance.TriggerEvent<int>(CONST.FinishLoadingSceneProgress, curSceneIndex);
    }



    public Scene GetCurrentScene()
    {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene();
    }

    public void StartListening(UnityEngine.Events.UnityAction<Scene, Scene> onChangeScene)
    {
        if (onChangeScene == null)
        {
            KaiUtils.Error(string.Format("非法的参数：切换场景时的监听回调为空!"));
            return;
        }
        UnityEngine.SceneManagement.SceneManager.activeSceneChanged += onChangeScene;
    }

    public void StopListening(UnityEngine.Events.UnityAction<Scene, Scene> onChangeScene)
    {
        UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= onChangeScene;
    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    public void Close()
    {
        Application.Quit();
    }
}

