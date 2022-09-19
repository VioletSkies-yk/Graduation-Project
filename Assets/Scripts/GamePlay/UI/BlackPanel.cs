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

        protected override bool OnOpened()
        {
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
        }
        private void RemoveListener()
        {
            EventManager.Instance.StopListening(CONST.StartLoadingSceneProgress, Fade);
        }
        private void Fade()
        {
            var _beginEvent = _blackImg.DOFade(1, 1f);
            _beginEvent.onComplete = delegate ()
            {
                StartCoroutine(DelayFade());
            };
        }

        IEnumerator DelayFade()
        {
            yield return new WaitForSeconds(1f);
            EventManager.Instance.TriggerEvent(CONST.LoadingImageAllBlack);
            var _finishEvent = _blackImg.DOFade(0, 1f);
            _finishEvent.onComplete = delegate ()
            {
            };
        }
    }
}
