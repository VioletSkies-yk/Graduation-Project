using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.GamePlay.GameLogic
{
    public class DoorInteractable : Interactable
    {
        /// <summary>
        /// 旋转的门
        /// </summary>
        [Space]
        [Header("旋转的门")]
        [SerializeField] private Transform _door;

        /// <summary>
        /// 是否能够触发
        /// </summary>
        //private bool isTrigger = false;

        private void OnEnable()
        {
            SetInteractableActive(false);
            EventManager.Instance.StartListening<float>(CONST.OnPuttingDownTheKey, TriggerTheKey);
        }

        private void OnDisable()
        {
            EventManager.Instance.StopListening<float>(CONST.OnPuttingDownTheKey, TriggerTheKey);
        }


        // 按下交互键时
        public override void OnInteract()
        {
        }
        // 视线瞄准，选中时
        public override void OnFocus()
        {
            //if (isTrigger)
            //{
                _door.DORotate(new Vector3(0, 70, 0), 2);

                //isTrigger = false;
                EventManager.Instance.StopListening<float>(CONST.OnPuttingDownTheKey, TriggerTheKey);
            //}
        }
        // 视线脱离后
        public override void OnLoseFocus()
        {
            KaiUtils.Log("{0}已脱离注视", gameObject.name);
        }


        private void TriggerTheKey(float distance)
        {
            _focusDistance = distance + 5f;
            SetInteractableActive(true);
            Invoke(nameof(SetInteractableFalse), 0.2f);
        }

        private void SetInteractableFalse()
        {
            SetInteractableActive(false);
        }
    }
}
