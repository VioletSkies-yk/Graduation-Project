using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GamePlay.GameLogic.Episode1
{
    public class Episode_1 : Episode
    {
        /// <summary>
        /// 进入走廊Trigger
        /// </summary>
        [Space]
        [Header("进入走廊Trigger")]
        [SerializeField] private TouchTrigger _levelTrigger01;

        /// <summary>
        /// 进入走廊Trigger
        /// </summary>
        [Space]
        [Header("安全屋灯光")]
        [SerializeField] private GameObject _saveRoomLight;


        public Episode_1() : base(EpisodeType.Episode1)
        {
        }

        private void OnEnable()
        {
            EventManager.Instance.TriggerEvent(CONST.PlayAudio, "LV1");
        }
        private void OnDisable()
        {
            EventManager.Instance.TriggerEvent(CONST.PlayAudio, "LV1");
        }

        public override void OnEnter()
        {
            _levelTrigger01.TriggerAction = OnEnterCorridorCallBack;
        }

        public override void OnChangeLevelStage()
        {
            switch (_curLevel)
            {
                case LevelStageType.Level_01:
                    break;
                case LevelStageType.Level_02:
                    break;
                case LevelStageType.Level_03:
                    break;
                case LevelStageType.Level_04:
                    break;
                default:
                    break;
            }
        }

        public void OnEnterCorridorCallBack()
        {
            if (_curLevel== LevelStageType.Level_01)
            {
                base.ChangeLevelStage();
                KaiUtils.SetActive(true, _levelStageDic[LevelStageType.Level_03].gameObject);
                KaiUtils.SetActive(false, _saveRoomLight);
                _levelTrigger01.SetTriggerOnOfOff(false);
            }
        }
    }
}
