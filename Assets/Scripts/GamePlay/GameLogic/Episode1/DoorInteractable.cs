using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.GamePlay.GameLogic
{
    public class DoorInteractable : TouchTrigger
    {
        /// <summary>
        /// 旋转的门
        /// </summary>
        [Space]
        [Header("旋转的门")]
        [SerializeField] private Transform _door;

        /// <summary>
        /// 门把触发器
        /// </summary>
        [Space]
        [Header("门把触发器")]
        [SerializeField] private BoxCollider _triggerCollider;


        /// <summary>
        /// 钥匙碰撞体
        /// </summary>
        [Space]
        [Header("钥匙碰撞体")]
        [SerializeField] private BoxCollider _inputCollider;

        private void OnEnable()
        {
            SetTriggerOnOfOff(true);
            EventManager.Instance.StartListening(CONST.OnPuttingDownTheKey, TriggerTheKey);
        }

        private void OnDisable()
        {
            EventManager.Instance.StopListening(CONST.OnPuttingDownTheKey, TriggerTheKey);
        }

        private void TriggerTheKey()
        {
            if(isTriggerON)
            {
                if (_triggerCollider.bounds.Intersects(_inputCollider.bounds))
                {
                    KaiUtils.SetActive(false, _inputCollider.gameObject);
                    _door.DORotate(new Vector3(0, 70, 0), 2);
                    EventManager.Instance.StopListening(CONST.OnPuttingDownTheKey, TriggerTheKey);
                    SetTriggerOnOfOff(false);
                }
            }
        }
    }
}
