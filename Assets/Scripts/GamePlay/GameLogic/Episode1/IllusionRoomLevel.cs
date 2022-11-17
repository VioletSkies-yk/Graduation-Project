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
        /// <summary>
        /// 2D交互物体
        /// </summary>
        [Space]
        [Header("2D交互物体")]
        [SerializeField] private List<TwoDInteractable> _cubeList;

        /// <summary>
        /// 2D交互物体
        /// </summary>
        [Space]
        [Header("钥匙")]
        [SerializeField] private TwoDInteractable _key;

        public IllusionRoomLevel() : base(LevelStageType.Level_03)
        {

        }

        private void Update()
        {
            for (int i = 0; i < _cubeList.Count; i++)
            {
                _cubeList[i].UpdateFunc();
                _cubeList[i].LockRotation();
            }
            _key.UpdateFunc();
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
            for (int i = 0; i < _cubeList.Count; i++)
            {
                _cubeList[i].LockRotation();
            }
        }
    }
}
