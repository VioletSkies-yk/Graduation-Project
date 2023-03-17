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
        [SerializeField] private Collider _boxCollider;

        /// <summary>
        /// 房间边界
        /// </summary>
        [Space]
        [Header("房间边界")]
        [SerializeField] private BoxCollider _roomCollider;

        /// <summary>
        /// 初始旋转
        /// </summary>
        private Vector3 initAngles;

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
            initAngles = transform.eulerAngles;
        }

        public void UpdateFunc(Action OnPuttingDownTheKey = null)
        {
            if (isOwnToPlayer && Input.GetMouseButtonUp(0))
            {
                if (isOutOfLevel())
                {
                    ResetInteractable();
                }
                else
                {
                    LockRotation();
                    OnPuttingDownTheKey?.Invoke();
                    isOwnToPlayer = false;
                    _gameObject3D.transform.SetParent(null);
                    _gameObject3D.constraints = RigidbodyConstraints.None;
                    _boxCollider.enabled = true;
                }

            }
        }

        // 按下交互键时
        public override void OnInteract()
        {
            base.OnInteractCallBack?.Invoke();
            LockRotation();
            if (isFirst)
            {
                //KaiUtils.SetActive(false, _gameObject2D);
                //if (_meshRender != null)
                //    _meshRender.enabled = true;
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

        public override void ResetInteractable()
        {
            _gameObject3D.transform.SetParent(null);
            base.ResetInteractable();
            isFirst = true;
            isOwnToPlayer = false;
            _gameObject3D.constraints = RigidbodyConstraints.FreezeAll;
            _boxCollider.enabled = true;
        }

        public void LockRotation()
        {
            transform.eulerAngles = initAngles;
        }

        public void SetRigidbodyConstraints(RigidbodyConstraints state)
        {
            _gameObject3D.constraints = state;
        }


        private bool isOutOfLevel()
        {
            if (_roomCollider != null)
                return !_roomCollider.bounds.Contains(gameObject.transform.position);
            else
                return false;
        }

    }
}
