using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;

namespace Assets.Scripts.GamePlay.UI
{
    public class BlackPanel : UINode
    {
        /// <summary>
        /// 场景加载背景
        /// </summary>
        [SerializeField] private RawImage _blackImg;

        /// <summary>
        /// 中心点
        /// </summary>
        [SerializeField] private GameObject _node;

        protected override bool OnOpened()
        {
            _node.SetActive(false);
            AddListener();
            return base.OnOpened();
        }

        protected override void OnClosing()
        {
            RemoveListener();
            base.OnClosing();
        }

        private void AddListener()
        {
            EventManager.Instance.StartListening(CONST.StartLoadingSceneProgress, Fade);
            EventManager.Instance.StartListening<int>(CONST.FinishLoadingSceneProgress, FadeCallBack);
        }
        private void RemoveListener()
        {
            EventManager.Instance.StopListening(CONST.StartLoadingSceneProgress, Fade);
            EventManager.Instance.StartListening<int>(CONST.FinishLoadingSceneProgress, FadeCallBack);
        }
        private void Fade()
        {
            KaiUtils.SetActive(true, _blackImg.gameObject);
            /*var _beginEvent = */
            _blackImg.DOFade(1, 1f).onComplete = () =>
            {
                EventManager.Instance.TriggerEvent(CONST.LoadingImageAllBlack);
            };
            //_beginEvent.onComplete = delegate ()
            //{
            //    StartCoroutine(DelayFade());
            //};
        }

        private void FadeCallBack(int sceneName)
        {
            _node.SetActive(sceneName != -1 || sceneName != 0);
            StartCoroutine(DelayFade());
        }

        IEnumerator DelayFade()
        {
            yield return new WaitForSeconds(1f);
            var _finishEvent = _blackImg.DOFade(0, 1f);
            _finishEvent.onComplete = delegate ()
            {
                KaiUtils.SetActive(false, _blackImg.gameObject);
                //onButtonClickToClose();
            };
        }
    }
}
