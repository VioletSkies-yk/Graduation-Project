using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GamePlay.GameLogic.Episode1
{
    public class MuralPaintingInteractable : Interactable
    {
        /// <summary>
        /// 旋转的门
        /// </summary>
        [Space]
        [Header("旋转的门")]
        [SerializeField] private Transform _muralPainting;

        /// <summary>
        /// 旋转的门
        /// </summary>
        [Space]
        [Header("一次旋转的度数")]
        public float value = 10f;

        /// <summary>
        /// 旋转的门
        /// </summary>
        [Space]
        [Header("一次旋转的时间")]
        public float duration = 1f;

        bool isRotAble = true;

        public override void OnFocus()
        {
            //throw new NotImplementedException();
        }

        public override void OnInteract()
        {
            if (isRotAble)
            {
                isRotAble = false;
                Quaternion endv = Quaternion.Euler(_muralPainting.localEulerAngles + new Vector3(0, 0, value));
                _muralPainting.DOLocalRotateQuaternion(endv, duration).OnComplete(() =>
                {
                    isRotAble = true;
                });

            }
        }

        public override void OnLoseFocus()
        {
            //throw new NotImplementedException();
        }
    }
}
