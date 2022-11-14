using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GamePlay.GameLogic
{
    public class PrisonInteractable : Interactable
    {
        /// <summary>
        /// 外面的牢笼
        /// </summary>
        [Space]
        [Header("原本的牢笼")]
        [SerializeField] private GameObject _smallPrison;

        /// <summary>
        /// 小熊MeshRender
        /// </summary>
        [Space]
        [Header("小熊MeshRender")]
        [SerializeField] private MeshRenderer _teddy;

        /// <summary>
        /// 黑色小熊材质
        /// </summary>
        [Space]
        [Header("黑色小熊材质")]
        [SerializeField] private Material _blackMat;

        // 按下交互键时
        public override void OnInteract()
        {

        }
        // 视线瞄准，选中时
        public override void OnFocus()
        {
            KaiUtils.SetActive(false, _smallPrison);
            _teddy.sharedMaterial = _blackMat;
        }
        // 视线脱离后
        public override void OnLoseFocus()
        {

        }
    }
}
