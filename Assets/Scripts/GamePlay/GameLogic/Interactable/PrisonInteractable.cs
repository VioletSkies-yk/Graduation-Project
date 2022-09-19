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

        // 按下交互键时
        public override void OnInteract()
        {

        }
        // 视线瞄准，选中时
        public override void OnFocus()
        {
            KaiUtils.SetActive(false, _smallPrison);
        }
        // 视线脱离后
        public override void OnLoseFocus()
        {

        }
    }
}
