using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GamePlay.GameLogic.Episode1
{
    public class MuralPaintingBall : MonoBehaviour
    {
        /// <summary>
        /// 目标Bound
        /// </summary>
        [Space]
        [Header("目标Bound")]
        [SerializeField] private BoxCollider targetBound;
        /// <summary>
        /// 迷宫
        /// </summary>
        [Space]
        [Header("迷宫")]
        [SerializeField] private GameObject muralPaint;

        private void Start()
        {

        }

        private void Update()
        {
            if (targetBound.bounds.Contains(this.transform.position))
            {
                KaiUtils.SetActive(false, this.gameObject, muralPaint);

                EventManager.Instance.TriggerEvent(CONST.OpenMuralPaintingDoor);
            }
        }
    }
}
