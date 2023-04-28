using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;

namespace Assets.Scripts.GamePlay.GameLogic
{
    public class MagicBallInteractable : Interactable
    {
        /// <summary>
        /// 要出现的梯子
        /// </summary>
        [Space]
        [Header("要出现的小球")]
        [SerializeField] private BaseCatchInteractable _catchBall;

        /// <summary>
        /// 爬梯触发器
        /// </summary>
        [Space]
        [Header("大门")]
        [SerializeField] private Transform _bigDoor;

        /// <summary>
        /// 需要对齐的位置数组1
        /// </summary>
        [Space]
        [Header("需要对齐的位置数组1")]
        [SerializeField] public List<Transform> _viewPoints = new List<Transform>();

        /// <summary>
        /// 对齐可以接受的误差（1920X1080下我测试认为10最友好，建议默认为10）
        /// </summary>
        [Space]
        [Range(0, 20)]
        [Header("对齐可以接受的误差")]
        [SerializeField] private float _offsetValue = 10f;

        /// <summary>
        /// 大门上升高度
        /// </summary>
        [Space]
        [Header("大门上升高度Y")]
        [SerializeField] private float _doorHeight = 50f;

        /// <summary>
        /// 大门上升高度
        /// </summary>
        [Space]
        [Header("大门上升时间")]
        [SerializeField] private float _doorDuration = 3f;

        /// <summary>
        /// 是否为首次交互
        /// </summary>
        private bool isFirst = true;


        // 按下交互键时
        public override void OnInteract()
        {

        }
        // 视线瞄准，选中时
        public override void OnFocus()
        {
            if (isFirst && CheckTriggerPosition())
            {
                isFirst = false;
                KaiUtils.SetActive(true, _catchBall.gameObject);
                _catchBall.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                _bigDoor.DOLocalMoveY(_doorHeight, _doorDuration);
            }
        }
        // 视线脱离后
        public override void OnLoseFocus()
        {

        }

        private bool CheckTriggerPosition()
        {
            for (int i = 0; i < _viewPoints.Count; i += 2)
            {
                var pos1_1 = PlayerController.Instance._playerCamera.WorldToScreenPoint(_viewPoints[i].position);
                var pos1_2 = PlayerController.Instance._playerCamera.WorldToScreenPoint(_viewPoints[i + 1].position);

                KaiUtils.Log("{0}  {1} {2} ", new Vector2(pos1_1.x, pos1_1.y), new Vector2(pos1_2.x, pos1_2.y), GeneralCalculate.DistanceOfTowPoint(new Vector2(pos1_1.x, pos1_1.y), new Vector2(pos1_2.x, pos1_2.y)));
                if (GeneralCalculate.DistanceOfTowPoint(new Vector2(pos1_1.x, pos1_1.y), new Vector2(pos1_2.x, pos1_2.y)) <= _offsetValue)
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
