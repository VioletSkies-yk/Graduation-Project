using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GamePlay.GameLogic.Episode1
{
    public class CorridorPainting : TouchTrigger
    {
        /// <summary>
        /// 画布
        /// </summary>
        [Space]
        [Header("画布")]
        [SerializeField] private MeshRenderer _paintingMesh;

        /// <summary>
        /// 画布
        /// </summary>
        [Space]
        [Header("画布")]
        [SerializeField] private MeshCollider _paintingCol;

        /// <summary>
        /// 不透明材质
        /// </summary>
        [Space]
        [Header("不透明材质")]
        [SerializeField] private Material _blackMat;

        /// <summary>
        /// 透明材质
        /// </summary>
        [Space]
        [Header("透明材质")]
        [SerializeField] private Material _transparentMat;

        /// <summary>
        /// 碰撞体
        /// </summary>
        [Space]
        [Header("碰撞体")]
        [SerializeField] private TouchTrigger _trigger;

        public void SetTransparent()
        {
            _paintingMesh.sharedMaterial = _transparentMat;
            //_paintingCol.enabled = false;
            _paintingCol.isTrigger = true;
        }

        public void SetBlack()
        {
            _paintingMesh.sharedMaterial = _blackMat;
            _paintingCol.enabled = true;
            _paintingCol.isTrigger = false;
        }

        public void SetTriggerCallBack(Action callback)
        {
            //_trigger.TriggerAction = callback;
        }
    }
}
