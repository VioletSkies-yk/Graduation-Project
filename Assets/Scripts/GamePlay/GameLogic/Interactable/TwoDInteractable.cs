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
    public class TwoDInteractable : Interactable
    {
        /// <summary>
        /// 被拿起的3D物体
        /// </summary>
        [Space]
        [Header("被拿起的物体")]
        [SerializeField] private Rigidbody _gameObject3D;

        /// <summary>
        /// 3D物体渲染组件
        /// </summary>
        [Space]
        [Header("渲染组件")]
        [SerializeField] private MeshRenderer _meshRender;

        /// <summary>
        /// 3D物体碰撞体
        /// </summary>
        [Space]
        [Header("3D物体碰撞体")]
        [SerializeField] private BoxCollider _boxCollider;

        /// <summary>
        /// 消失的2D物体
        /// </summary>
        [Space]
        [Header("门锁")]
        [SerializeField] private DoorInteractable _doorLock;

        /// <summary>
        /// 是否为首次交互
        /// </summary>
        private bool isFirst = true;

        /// <summary>
        /// 是否正在持有
        /// </summary>
        private bool isOwnToPlayer = false;

        private void Start()
        {
            _gameObject3D.constraints = RigidbodyConstraints.FreezeAll;
            //_meshRender.enabled = false;
        }

        private void Update()
        {
            if (isOwnToPlayer && Input.GetMouseButtonUp(0))
            {
                EventManager.Instance.TriggerEvent<float>(CONST.OnPuttingDownTheKey, _gameObject3D.transform.parent.localPosition.magnitude);
                isOwnToPlayer = false;
                _gameObject3D.transform.SetParent(null);
                _gameObject3D.constraints = RigidbodyConstraints.None;
                _boxCollider.enabled = true;
            }
        }

        // 按下交互键时
        public override void OnInteract()
        {
            if (isFirst)
            {
                //KaiUtils.SetActive(false, _gameObject2D);
                _meshRender.enabled = true;
                isOwnToPlayer = true;
                PlayerController.Instance.SetAnchorPos(_gameObject3D.transform.position);
                _gameObject3D.transform.SetParent(PlayerController.Instance._anchorPos);
                isFirst = false;
            }
            else
            {
                isOwnToPlayer = true;
                _gameObject3D.transform.SetParent(PlayerController.Instance._anchorPos);
                _gameObject3D.constraints = RigidbodyConstraints.FreezeAll;
                _boxCollider.enabled = false;
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

    }
}
