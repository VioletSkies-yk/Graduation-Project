using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager:OsSingletonMono<SceneManager>
{
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
    public void LoadSceneAsync(string sceneName, Action Func)
    {
        StartCoroutine(LoadingSceneAsync(sceneName, Func));
    }
    public IEnumerator LoadingSceneAsync(string sceneName, Action Func)
    {
        EventManager.Instance.TriggerEvent(CONST.StartLoadingSceneProgress);

        //暂时先等待一秒，作为过场动画
        yield return new WaitForSeconds(1f);

        AsyncOperation AO = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);

        //判定场景是否加载完成，通过协程实现加载进度的更新，可用于其他事件，外部也可以监听
        while (!AO.isDone)
        {
            EventManager.Instance.TriggerEvent(CONST.LoadSceneProgress, AO);
            yield return AO.progress;
        }
        yield return AO;
        Func();
        EventManager.Instance.TriggerEvent(CONST.FinishLoadingSceneProgress);
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

