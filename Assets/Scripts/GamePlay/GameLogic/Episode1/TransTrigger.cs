using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GamePlay.GameLogic.Episode1
{
    public class TransTrigger : TouchTrigger
    {
        public Transform _startPos;
        public Transform _endPos;

        /// <summary>
        /// 画布
        /// </summary>
        [Space]
        [Header("画布")]
        [SerializeField] private CorridorPainting _corridorPainting;

        public override void OnTriggerEnterCallBack()
        {
            KaiUtils.SetActive(true, _endPos.gameObject);
            _corridorPainting.SetBlack();
            var offset = PlayerController.Instance.transform.position - _startPos.position;
            PlayerController.Instance.SetPlayerPos(_endPos.position + offset);
        }
    }
}
