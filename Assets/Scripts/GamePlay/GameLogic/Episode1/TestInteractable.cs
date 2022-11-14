using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GamePlay.GameLogic
{
    public class TestInteractable : Interactable
    {
        // 按下交互键时
        public override void OnInteract()
        {
            KaiUtils.Log("{0}已被点击", gameObject.name);
        }
        // 视线瞄准，选中时
        public override void OnFocus()
        {
            KaiUtils.Log("{0}已被注视", gameObject.name);
        }
        // 视线脱离后
        public override void OnLoseFocus()
        {
            KaiUtils.Log("{0}已脱离注视", gameObject.name);
        }
    }
}
