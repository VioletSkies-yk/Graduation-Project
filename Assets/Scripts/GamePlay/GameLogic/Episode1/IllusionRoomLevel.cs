using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GamePlay.GameLogic.Episode1
{
    public class IllusionRoomLevel : LevelStage
    {
        public IllusionRoomLevel() : base(LevelStageType.Level_03)
        {

        }
        public override void Init()
        {
        }


        public override void ResetStage()
        {
        }

        public override void OnEnter()
        {
            KaiUtils.SetActive(true,gameObject);
        }
    }
}
