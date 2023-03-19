using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using Assets.Scripts.GamePlay.GameLogic.Episode1;

namespace Assets.Scripts.GamePlay.GameLogic
{
    public class ElevatorTrigger : TouchTrigger
    {
        /// <summary>
        /// 电梯左门
        /// </summary>
        [Space]
        [Header("电梯左门")]
        [SerializeField] private Transform _elevatorLeftDoor;

        /// <summary>
        /// 电梯右门
        /// </summary>
        [Space]
        [Header("电梯右门")]
        [SerializeField] private Transform _elevatorRightDoor;

        private bool isFirst = true;

        public override void OnTriggerEnterCallBack()
        {
            //_elevatorLeftDoor.DOMove();
            //_elevatorRightDoor.DOMove();
        }
    }
}
