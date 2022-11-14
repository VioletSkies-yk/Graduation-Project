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
    public class SphereInteractable : Interactable
    {
        /// <summary>
        /// 运动的小球
        /// </summary>
        [Space]
        [Header("运动的小球")]
        [SerializeField] private Rigidbody _ball;

        /// <summary>
        /// 与小球在同一平面的collider
        /// </summary>
        [Space]
        [Header("与小球在同一平面的collider")]
        [SerializeField] private GameObject _trueCubes;

        /// <summary>
        /// 与小球在同一平面的collider
        /// </summary>
        [Space]
        [Header("小球运动的终点位置（只关乎X与Z，Y由物理碰撞决定）")]
        [SerializeField] private Transform _ballEndPos;

        /// <summary>
        /// 小球运动时间
        /// </summary>
        [Space]
        [Range(0, 20)]
        [Header("小球运动时间")]
        [SerializeField] private float _ballDuration = 10f;

        /// <summary>
        /// 需要对齐的位置数组1
        /// </summary>
        [Space]
        [Header("需要对齐的位置数组1")]
        [SerializeField] private Transform[] _viewPoints1 = new Transform[2];

        /// <summary>
        /// 需要对齐的位置数组2
        /// </summary>
        [Space]
        [Header("需要对齐的位置数组2")]
        [SerializeField] private Transform[] _viewPoints2 = new Transform[2];

        /// <summary>
        /// 对齐可以接受的误差（1920X1080下我测试认为10最友好，建议默认为10）
        /// </summary>
        [Space]
        [Range(0, 20)]
        [Header("对齐可以接受的误差")]
        [SerializeField] private float _offsetValue = 10f;

        /// <summary>
        /// 是否为首次交互
        /// </summary>
        private bool isFirst = true;

        private void Start()
        {
            _ball.constraints = RigidbodyConstraints.FreezeAll;
        }

        // 按下交互键时
        public override void OnInteract()
        {
            if (isFirst && CheckTriggerPosition())
            {
                isFirst = false;
                PlayerController.Instance.SetUnLockPos(false);
                KaiUtils.SetActive(true, _trueCubes);
                gameObject.GetComponent<BoxCollider>().enabled = false;
                _ball.constraints = RigidbodyConstraints.None;
                StartCoroutine(BallMove());
            }

        }
        // 视线瞄准，选中时
        public override void OnFocus()
        {

        }
        // 视线脱离后
        public override void OnLoseFocus()
        {

        }

        IEnumerator BallMove()
        {
            yield return new WaitForSeconds(1f);
            var move = _ball.DOMoveX(_ballEndPos.transform.position.x, _ballDuration);
            _ball.DOMoveZ(_ballEndPos.transform.position.z, _ballDuration);
            move.onComplete = delegate ()
            {
                PlayerController.Instance.SetUnLockPos(true);
                _ball.constraints = RigidbodyConstraints.FreezeAll;
                StopAllCoroutines();
            };
        }
        private bool CheckTriggerPosition()
        {
            var pos1_1 = PlayerController.Instance._playerCamera.WorldToScreenPoint(_viewPoints1[0].position);
            var pos1_2 = PlayerController.Instance._playerCamera.WorldToScreenPoint(_viewPoints1[1].position);

            var pos2_1 = PlayerController.Instance._playerCamera.WorldToScreenPoint(_viewPoints2[0].position);
            var pos2_2 = PlayerController.Instance._playerCamera.WorldToScreenPoint(_viewPoints2[1].position);

            KaiUtils.Log("{0}  {1}  {2}  {3} {4}  {5}", new Vector2(pos1_1.x, pos1_1.y), new Vector2(pos1_2.x, pos1_2.y), GeneralCalculate.DistanceOfTowPoint(new Vector2(pos1_1.x, pos1_1.y), new Vector2(pos1_2.x, pos1_2.y))
                , new Vector2(pos2_1.x, pos2_1.y), new Vector2(pos2_2.x, pos2_2.y), GeneralCalculate.DistanceOfTowPoint(new Vector2(pos2_1.x, pos2_1.y), new Vector2(pos2_2.x, pos2_2.y)));

            return GeneralCalculate.DistanceOfTowPoint(new Vector2(pos1_1.x, pos1_1.y), new Vector2(pos1_2.x, pos1_2.y)) <= _offsetValue
                && GeneralCalculate.DistanceOfTowPoint(new Vector2(pos2_1.x, pos2_1.y), new Vector2(pos2_2.x, pos2_2.y)) <= _offsetValue;
        }
    }
}
