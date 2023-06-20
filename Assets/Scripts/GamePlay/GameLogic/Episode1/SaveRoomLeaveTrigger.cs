using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using Assets.Scripts.GamePlay.GameLogic.Episode1;

namespace Assets.Scripts.GamePlay.GameLogic
{
    public class SaveRoomLeaveTrigger : TouchTrigger
    {
        /// <summary>
        /// 安全屋
        /// </summary>
        [Space]
        [Header("安全屋")]
        [SerializeField] private GameObject _saveRoom;

        /// <summary>
        /// 走廊墙壁
        /// </summary>
        [Space]
        [Header("走廊墙壁")]
        [SerializeField] private GameObject _corridorWall;

        /// <summary>
        /// 画布
        /// </summary>
        [Space]
        [Header("画布")]
        [SerializeField] private CorridorPainting _corridorPainting;


        private bool isFirst = true;

        public override void OnTriggerEnterCallBack()
        {
            if (isFirst)
            {
                //KaiUtils.SetActive(false, _saveRoom);
                KaiUtils.SetActive(true, _corridorWall);
                isFirst = false;
            }
            else
            {
                _corridorPainting.SetTransparent();
            }
        }
    }
}
