﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GamePlay.GameLogic
{
    public abstract class Interactable : MonoBehaviour
    {
        #region 参数

        /// <summary>
        /// 可注视交互的距离长度
        /// </summary>
        [Space]
        [Range(0, 100)]
        [Header("可注视交互的距离长度")]
        [SerializeField] public float _focusDistance=20;

        /// <summary>
        /// 可点击交互的距离长度
        /// </summary>
        [Space]
        [Range(0, 100)]
        [Header("可点击交互的距离长度")]
        [SerializeField] public float _clickDistance=20;

        #endregion

        public Action OnInteractCallBack;
        public Action OnFocusCallBack;

        private Transform initTransform;

        private Vector3 initPos;
        private Quaternion initRot;
        private Vector3 initScale;

        public virtual void Awake()
        {
            // 继承Interactable类的，都划分到“Interactable”层
            gameObject.layer = LayerMask.NameToLayer("Interactable");
            initPos = gameObject.transform.position;
            initRot = gameObject.transform.rotation;
            initScale = gameObject.transform.localScale;
        }
        // 按下交互键时
        public abstract void OnInteract();
        // 视线瞄准，选中时
        public abstract void OnFocus();
        // 视线脱离后
        public abstract void OnLoseFocus();

        protected void SetInteractableActive(bool active)
        {
            gameObject.layer = LayerMask.NameToLayer(active ? "Interactable" : "Default");
        }

        public virtual void ResetInteractable()
        {
            gameObject.transform.SetPositionAndRotation(initPos,initRot);
            gameObject.transform.localScale = initScale;
        }
    }
}
