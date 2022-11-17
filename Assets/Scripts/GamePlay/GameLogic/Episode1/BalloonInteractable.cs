using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GamePlay.GameLogic
{
    public class BalloonInteractable : Interactable
    {
        /// <summary>
        /// 钥匙
        /// </summary>
        [Space]
        [Header("钥匙")]
        [SerializeField] private TwoDInteractable _key;

        private int count = 0;

        private float size = 1.5f;

        // 按下交互键时
        public override void OnInteract()
        {
            if (count < 4)
            {
                transform.DOScale(size, 0.5f);
                size += 0.5f;
                count++;
            }
            else
            {
                KaiUtils.SetActive(false, gameObject);
                KaiUtils.SetActive(true, _key.gameObject);
                _key.SetRigidbodyConstraints(RigidbodyConstraints.None);
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
