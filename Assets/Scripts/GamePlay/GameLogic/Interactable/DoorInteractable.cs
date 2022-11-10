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

        // 按下交互键时
        public override void OnInteract()
        {
            Quaternion endv = Quaternion.Euler(transform.localEulerAngles + new Vector3(0, 90, 0));
            _door.DORotate(new Vector3(0, 70, 0), 2);
        }
        // 视线瞄准，选中时
        public override void OnFocus()
        {
            KaiUtils.Log("{0}已被注视", gameObject.name);
        }
        // 视线脱离后
        public override void OnLoseFocus()
        {
            KaiUtils.Log("{0}已脱离注视", gameObject.name);
        }
    }
}
