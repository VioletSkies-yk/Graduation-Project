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
    public class MuralPaintingDoor : MonoBehaviour
    {
        /// <summary>
        /// 移动
        /// </summary>
        [Space]
        [Header("移动向量")]
        [SerializeField] private Vector3 duration;

        /// <summary>
        /// 移动
        /// </summary>
        [Space]
        [Header("移动时间")]
        [SerializeField] private float time;

        private bool isFirst = true;

        private void OnEnable()
        {
            EventManager.Instance.StartListening(CONST.OpenMuralPaintingDoor, CallBack);
        }
        private void OnDisable()
        {

            EventManager.Instance.StopListening(CONST.OpenMuralPaintingDoor, CallBack);

        }
        public void CallBack()
        {
            if (isFirst)
            {
                transform.DOMove(transform.localPosition + duration, time);
                isFirst = false;
            }
        }
    }
}
