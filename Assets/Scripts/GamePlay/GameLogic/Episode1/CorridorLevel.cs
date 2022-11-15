using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GamePlay.GameLogic.Episode1
{
    public class CorridorLevel : LevelStage
    {
        /// <summary>
        /// 假画
        /// </summary>
        [Space]
        [Header("假画")]
        [SerializeField] private CorridorPainting _fakeCorridorPainting;

        /// <summary>
        /// 真画
        /// </summary>
        [Space]
        [Header("真画")]
        [SerializeField] private CorridorPainting _trueCorridorPainting;

        /// <summary>
        /// Trigger
        /// </summary>
        [Space]
        [Header("Trigger")]
        [SerializeField] private TouchTrigger _passTrigger;

        public CorridorLevel() : base(LevelStageType.Level_02)
        {

        }

        public override void Init()
        {
            _passTrigger.SetTriggerOnOfOff(false);

            _fakeCorridorPainting.SetTriggerCallBack(Transition);
            _passTrigger.TriggerAction = LoopCallBack;
        }

        public override void OnEnter()
        {
            KaiUtils.SetActive(true, _trueCorridorPainting.gameObject);
            _trueCorridorPainting.SetTransparent();
            _passTrigger.SetTriggerOnOfOff(true);
        }

        public override void OnLeave()
        {
            throw new NotImplementedException();
        }

        public override void ResetStage()
        {
            throw new NotImplementedException();
        }


        public void Transition()
        {
            var _startPos = _fakeCorridorPainting.transform.position;
            var _endPos = _trueCorridorPainting.transform.position;
            //KaiUtils.SetActive(true, _fakeCorridorPainting.gameObject);
            _trueCorridorPainting.SetBlack();
            var offset = PlayerController.Instance.transform.position - _startPos;
            PlayerController.Instance.SetPlayerPosAndRotation(_endPos + offset, -90f,
                delegate ()
                {
                    PlayerController.Instance.transform.DOMoveZ(PlayerController.Instance.transform.position.z + 5f, 0.5f);
                });
        }

        public void LoopCallBack()
        {
            _trueCorridorPainting.SetTransparent();
        }
    }
}
