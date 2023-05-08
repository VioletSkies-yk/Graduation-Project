using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GamePlay.GameLogic.Episode1
{
    public class LifeRuoy : TouchTrigger
    {

        /// <summary>
        /// 目标位置
        /// </summary>
        [Space]
        [Header("目标位置")]
        [SerializeField] private Transform _target;

        /// <summary>
        /// 玩家运动时间Y
        /// </summary>
        [Space]
        [Range(0, 20)]
        [Header("玩家运动时间Y")]
        [SerializeField] private float _ballDurationY = 2f;

        private void OnEnable()
        {
            TriggerAction = delegate ()
            {
                PlayerController.Instance.StopFloating();

                  PlayerController.Instance.SetUnLockPos(false);

                  PlayerController.Instance.transform.DOMoveX(_target.transform.position.x, 1f).onComplete = delegate ()
                  {
                      PlayerController.Instance.transform.DOMoveY(_target.transform.position.y, _ballDurationY).onComplete = delegate ()
                      {
                          var move = PlayerController.Instance.transform.DOMoveZ(_target.transform.position.z, 1f);
                          move.onComplete = delegate ()
                          {
                              PlayerController.Instance.SetUnLockPos(true);
                          };
                      };
                  };
              };
        }
    }
}
