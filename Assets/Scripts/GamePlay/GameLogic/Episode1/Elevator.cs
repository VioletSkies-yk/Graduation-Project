using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static SceneManager;

namespace Assets.Scripts.GamePlay.GameLogic.Episode1
{
    public class Elevator : MonoBehaviour
    {
        public float Y;
        public float duration;

        private bool isCompelete;

        private void OnEnable()
        {
            isCompelete = false;
            EventManager.Instance.StartListening(CONST.EnterDianTi, CallBack);
        }
        private void OnDisable()
        {
            EventManager.Instance.StopListening(CONST.EnterDianTi, CallBack);
        }

        private void CallBack()
        {
            if (isCompelete)
                return;
            EventManager.Instance.TriggerEvent(CONST.PlayAudio, "Elevator");
            PlayerController.Instance.SetUnLockPos(false);
            this.transform.DOLocalMoveY(Y, duration).onComplete = delegate ()
            {
                isCompelete = true;
                EventManager.Instance.TriggerEvent(CONST.StopAudio, "Elevator");

                EventManager.Instance.TriggerEvent(CONST.SendLoadingScene, new SceneMsg(KaiUtils.GetSceneName(SceneManager.Instance.curSceneIndex + 1), () =>
                  {
                      EventManager.Instance.TriggerEvent(CONST.SendTypeMsg, KaiUtils.GetSceneSubtitles(SceneManager.Instance.curSceneIndex+1));
                      var pos = GameObject.Find("bornTrigger").transform.position;
                      PlayerController.Instance.SetPlayerPosAndRotation(pos);
                  }));
            };
            PlayerController.Instance.transform.DOLocalMoveY(PlayerController.Instance.transform.position.y - (transform.localPosition.y - Y), duration);
        }
    }
}
