﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GamePlay.UI
{
    public class HelpMenuPanel : UINode
    {
        /// <summary>
        /// 继续游戏按钮
        /// </summary>  
        [SerializeField] private ButtonHandle _closeBtn;

        protected override bool OnOpened()
        {
            AddListener();
            return base.OnOpened();
        }

        protected override void OnClosing()
        {
            RemoveListener();
            base.OnClosing();
        }

        private void AddListener()
        {
            _closeBtn.onClick.AddListener(onButtonClickToClose);
        }
        private void RemoveListener()
        {
            _closeBtn.onClick.RemoveListener(onButtonClickToClose);
        }
    }
}