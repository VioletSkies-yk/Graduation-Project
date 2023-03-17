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

        /// <summary>
        /// 钥匙碰撞体
        /// </summary>
        [Space]
        [Header("钥匙碰撞体")]
        [SerializeField] private BoxCollider _inputCollider;

        /// <summary>
        /// 旋转的门
        /// </summary>
        [Space]
        [Header("旋转的门")]
        [SerializeField] private Transform _door;

        /// <summary>
        /// 门把触发器
        /// </summary>
        [Space]
        [Header("门把触发器")]
        [SerializeField] private BoxCollider _triggerCollider;

        public IllusionRoomLevel() : base(LevelStageType.Level_03)
        {

        }

        private void Update()
        {
            for (int i = 0; i < _cubeList.Count; i++)
            {
                _cubeList[i].UpdateFunc();
            }
            _key.UpdateFunc(OnPuttingDownTheKey);
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


        public void OnPuttingDownTheKey()
        {
            if (_triggerCollider.bounds.Intersects(_inputCollider.bounds))
            {
                KaiUtils.SetActive(false, _inputCollider.gameObject);
                _door.DORotate(new Vector3(0, 70, 0), 2);
            }
        }
    }
}
