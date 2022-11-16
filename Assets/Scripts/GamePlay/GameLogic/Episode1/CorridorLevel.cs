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

        private bool isCanPass;

        public CorridorLevel() : base(LevelStageType.Level_02)
        {

        }

        public override void Init()
        {
            _passTrigger.SetTriggerOnOfOff(false);
            _trueCorridorPainting.SetTriggerOnOfOff(false);

            _fakeCorridorPainting.SetTriggerCallBack(Transition);
            _passTrigger.TriggerAction = LoopCallBack;
            _trueCorridorPainting.SetTriggerCallBack(() => { EventManager.Instance.TriggerEvent(CONST.PassLevelOnTheSameEpisode, EpisodeType.Episode1); });

        }

        public override void OnEnter()
        {
            KaiUtils.SetActive(true, _trueCorridorPainting.gameObject);
            _trueCorridorPainting.SetTransparent();
            _passTrigger.SetTriggerOnOfOff(true);
        }

        public override void OnLeave()
        {
            _trueCorridorPainting.SetBlack();
        }

        public override void ResetStage()
        {

        }


        public void Transition()
        {
            var _startPos = _fakeCorridorPainting.transform.position;
            var _endPos = _trueCorridorPainting.transform.position;
            //KaiUtils.SetActive(true, _fakeCorridorPainting.gameObject);
            _trueCorridorPainting.SetBlack();
            var offset = PlayerController.Instance.transform.position - _startPos;
            PlayerController.Instance.SetPlayerPosAndRotation(_endPos + offset, -90f,
                    PlayerController.Instance.transform.DOMoveZ(40f, 0.1f));


            _trueCorridorPainting.SetTriggerOnOfOff(false);
        }

        public void LoopCallBack()
        {
            _trueCorridorPainting.SetTransparent();
            _trueCorridorPainting.SetTriggerOnOfOff(true);
            isCanPass = true;
        }
    }
}
