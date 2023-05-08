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
    public class LadderInteractable : Interactable
    {
        /// <summary>
        /// 要消失的梯子
        /// </summary>
        [Space]
        [Header("要消失的梯子")]
        [SerializeField] private GameObject _fakeLadder;

        /// <summary>
        /// 要出现的梯子
        /// </summary>
        [Space]
        [Header("要出现的梯子")]
        [SerializeField] private GameObject _trueLadder;

        /// <summary>
        /// 爬梯触发器
        /// </summary>
        [Space]
        [Header("爬梯触发器")]
        [SerializeField] private TouchTrigger _ladderTrigger;

        /// <summary>
        /// 与小球在同一平面的collider
        /// </summary>
        [Space]
        [Header("小球运动的终点位置（只关乎X与Z，Y由物理碰撞决定）")]
        [SerializeField] private Transform _ballEndPos;

        /// <summary>
        /// 玩家运动时间Y
        /// </summary>
        [Space]
        [Range(0, 20)]
        [Header("玩家运动时间Y")]
        [SerializeField] private float _ballDurationY = 5f;

        /// <summary>
        /// 小球运动时间
        /// </summary>
        [Space]
        [Range(0, 20)]
        [Header("小球运动时间")]
        [SerializeField] private float _ballDuration = 1f;

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


        [Space]
        [Range(0, 20)]
        [Header("上下浮动高度")]
        [SerializeField] private float _floatingHeight = 30f;


        [Space]
        [Range(0, 20)]
        [Header("上下浮动速度")]
        [SerializeField] private float _floatingSpeed = 15f;

        /// <summary>
        /// 是否为首次交互
        /// </summary>
        private bool isFirst = true;

        private void OnEnable()
        {
            _ladderTrigger.SetTriggerOnOfOff(false);
            EventManager.Instance.StartListening(CONST.LadderComplete, () => { _ladderTrigger.SetTriggerOnOfOff(true); });
        }

        // 按下交互键时
        public override void OnInteract()
        {
            EventManager.Instance.StopListening(CONST.LadderComplete, () => { _ladderTrigger.SetTriggerOnOfOff(true); });
        }
        // 视线瞄准，选中时
        public override void OnFocus()
        {
            if (isFirst && CheckTriggerPosition())
            {
                isFirst = false;
                KaiUtils.SetActive(true, _trueLadder);
                KaiUtils.SetActive(false, _fakeLadder);
                _ladderTrigger.TriggerAction = delegate ()
                {
                    PlayerController.Instance.SetUnLockPos(false);

                    PlayerController.Instance.transform.DOMoveX(_ballEndPos.transform.position.x, _ballDuration).onComplete = delegate ()
                      {
                          PlayerController.Instance.transform.DOMoveY(_ballEndPos.transform.position.y, _ballDurationY).onComplete = delegate ()
                            {
                                var move = PlayerController.Instance.transform.DOMoveZ(_ballEndPos.transform.position.z, _ballDuration);
                                move.onComplete = delegate ()
                                {
                                    PlayerController.Instance.SetUnLockPos(true);
                                    PlayerController.Instance.SetFloating( _floatingHeight, _floatingSpeed);
                                    StopAllCoroutines();
                                };
                            };
                      };
                    //StartCoroutine(BallMove());
                };
            }
        }
        // 视线脱离后
        public override void OnLoseFocus()
        {

        }

        //IEnumerator BallMove()
        //{
        //    PlayerController.Instance.transform.DOMoveX(_ballEndPos.transform.position.x, _ballDuration);
        //    yield return new WaitForSeconds(_ballDuration);
        //    PlayerController.Instance.transform.DOMoveY(_ballEndPos.transform.position.y, _ballDuration);
        //    yield return new WaitForSeconds(_ballDuration);
        //    var move = PlayerController.Instance.transform.DOMoveZ(_ballEndPos.transform.position.z, _ballDuration);
        //    move.onComplete = delegate ()
        //    {
        //        PlayerController.Instance.SetUnLockPos(true);
        //        StopAllCoroutines();
        //    };
        //}
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
            //var pos1_1 = PlayerController.Instance._playerCamera.WorldToScreenPoint(_viewPoints[0][0].position);
            //var pos1_2 = PlayerController.Instance._playerCamera.WorldToScreenPoint(_viewPoints1[1].position);

            //var pos2_1 = PlayerController.Instance._playerCamera.WorldToScreenPoint(_viewPoints2[0].position);
            //var pos2_2 = PlayerController.Instance._playerCamera.WorldToScreenPoint(_viewPoints2[1].position);

            //KaiUtils.Log("{0}  {1}  {2}  {3} {4}  {5}", new Vector2(pos1_1.x, pos1_1.y), new Vector2(pos1_2.x, pos1_2.y), GeneralCalculate.DistanceOfTowPoint(new Vector2(pos1_1.x, pos1_1.y), new Vector2(pos1_2.x, pos1_2.y))
            //    , new Vector2(pos2_1.x, pos2_1.y), new Vector2(pos2_2.x, pos2_2.y), GeneralCalculate.DistanceOfTowPoint(new Vector2(pos2_1.x, pos2_1.y), new Vector2(pos2_2.x, pos2_2.y)));

            //return GeneralCalculate.DistanceOfTowPoint(new Vector2(pos1_1.x, pos1_1.y), new Vector2(pos1_2.x, pos1_2.y)) <= _offsetValue
            //    && GeneralCalculate.DistanceOfTowPoint(new Vector2(pos2_1.x, pos2_1.y), new Vector2(pos2_2.x, pos2_2.y)) <= _offsetValue;
        }
    }
}
