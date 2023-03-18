using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.GamePlay.GameLogic
{
    public class DoorInteractable : Interactable
    {
        /// <summary>
        /// 旋转的门
        /// </summary>
        [Space]
        [Header("旋转的门")]
        [SerializeField] private Transform _door;


        public float _angle;
        public float _duration;

        public override void OnFocus()
        {

        }

        public override void OnInteract()
        {
            _door.DORotate(new Vector3(0, _angle, 0), _duration);
        }

        public override void OnLoseFocus()
        {

        }
    }
}
