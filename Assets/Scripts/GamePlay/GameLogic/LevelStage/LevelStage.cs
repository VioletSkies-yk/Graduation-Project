using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GamePlay.GameLogic
{
    public enum LevelStageType
    {
        Level_01 = 1,
        Level_02 = 2,
        Level_03 = 3,
        Level_04 = 4,
    }

    public abstract class LevelStage : MonoBehaviour
    {
        public LevelStageType Type { get; private set; }

        public LevelStage(LevelStageType type)
        {
            Type = type;
        }

        public abstract void Init();
        public abstract void ResetStage();
        public virtual void OnEnter()
        {
        }
        public virtual void OnLeave()
        {

        }
    }
}
