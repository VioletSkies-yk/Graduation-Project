using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
            PlayerController.Instance.transform.DOLocalMoveY(PlayerController.Instance.transform.position.y - (transform.position.y - Y), duration);
            this.transform.DOLocalMoveY(Y, duration).onComplete = delegate ()
            {
                isCompelete = true;
                PlayerController.Instance.SetUnLockPos(true);
                EventManager.Instance.TriggerEvent(CONST.StopAudio, "Elevator");
            };
        }
    }
}
