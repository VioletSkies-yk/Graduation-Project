using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GamePlay.GameLogic.Episode1
{
    public class SafeRoomLevel : LevelStage
    {
        /// <summary>
        /// 2D交互物体
        /// </summary>
        [Space]
        [Header("2D交互物体")]
        [SerializeField] private TwoDInteractable _cube;

        /// <summary>
        /// 门
        /// </summary>
        [Space]
        [Header("门")]
        //[SerializeField] private DoorInteractable _doorInteractable;

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

        /// <summary>
        /// 门把触发器
        /// </summary>
        [Space]
        [Header("门上方块")]
        [SerializeField] private MeshRenderer _triggerMeshRender;

        /// <summary>
        /// 钥匙碰撞体
        /// </summary>
        [Space]
        [Header("钥匙碰撞体")]
        [SerializeField] private BoxCollider _inputCollider;

        public SafeRoomLevel() : base(LevelStageType.Level_01)
        {

        }
        private void OnEnable()
        {
            EventManager.Instance.StartListening(CONST.Lv2Door1, OpenTheDoor);
        }
        private void OnDisable()
        {
            EventManager.Instance.StopListening(CONST.Lv2Door1, OpenTheDoor);
        }



        private void Update()
        {
            _cube.UpdateFunc(OnPuttingDownTheKey);
        }

        public override void Init()
        {
        }

        public override void OnEnter()
        {
            KaiUtils.SetActive(true, gameObject);
        }

        public override void OnLeave()
        {
            KaiUtils.SetActive(false, gameObject);
        }

        public override void ResetStage()
        {
        }

        public void OnPuttingDownTheKey()
        {
            if (_triggerCollider.bounds.Intersects(_inputCollider.bounds))
            {
                _triggerMeshRender.enabled = true;
                KaiUtils.SetActive(false, _inputCollider.gameObject);
                _door.DORotate(new Vector3(0, 70, 0), 2);
                EventManager.Instance.TriggerEvent(CONST.PlayAudio, "lv1open the door");
            }
        }

        private void OpenTheDoor()
        {
            _door.DORotate(new Vector3(0, -70, 0), 2);
            EventManager.Instance.TriggerEvent(CONST.PlayAudio, "lv1open the door");
        }
    }
}
