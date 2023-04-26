using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using Assets.Scripts.GamePlay.UI;

namespace Assets.Scripts.GamePlay.GameLogic
{
    public class TipsInteractable : Interactable
    {
        /// <summary>
        /// 被拿起的3D物体
        /// </summary>
        [Space]
        [Header("详情图片")]
        [SerializeField] private Sprite _gameObject3D;

        /// <summary>
        /// 是否正在持有
        /// </summary>
        private bool isOpen = false;

        // 按下交互键时
        public override void OnInteract()
        {
        }
        // 视线瞄准，选中时
        public override void OnFocus()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //var node=UIManager.Instance.GetOpenedUI(CONST.UI_DetailTipsPanel) as DetailTipsPanel;
                Debug.Log("1111122");
                UIManager.Instance.OpenUI(CONST.UI_DetailTipsPanel);
                //if (node != null)
                //return;
                var node = UIManager.Instance.GetOpenedUI(CONST.UI_DetailTipsPanel) as DetailTipsPanel;
                node.SetImage(_gameObject3D);
            }
        }
        // 视线脱离后
        public override void OnLoseFocus()
        {

        }

    }
}
