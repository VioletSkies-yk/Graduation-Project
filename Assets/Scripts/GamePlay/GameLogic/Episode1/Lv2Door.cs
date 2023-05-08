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
    public class Lv2Door : MonoBehaviour
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

        private void OnEnable()
        {
            EventManager.Instance.StartListening(CONST.Lv2Door, CallBack);
        }
        private void OnDisable()
        {

            EventManager.Instance.StopListening(CONST.Lv2Door, CallBack);

        }
        public void CallBack()
        {
            Vector3 temp = _elevatorLeftDoor.position - _elevatorRightDoor.position;
            if (isFirst)
            {
                _elevatorLeftDoor.DOMove(_elevatorLeftDoor.position + temp.normalized * 7, 2f);
                _elevatorRightDoor.DOMove(_elevatorRightDoor.position - temp.normalized * 7, 2f);
                isFirst = false;
            }
        }
    }
}
