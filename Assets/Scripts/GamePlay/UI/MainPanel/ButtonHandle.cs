﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.GamePlay.UI
{
    public class ButtonHandle : Button
    {
        private Action OnMouseEnter;

        private Action OnMouseExit;

        public void SetCallBack(Action enterCallBack = null, Action exitCallBack = null)
        {
            OnMouseEnter = enterCallBack;
            OnMouseExit = exitCallBack;
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            OnMouseEnter?.Invoke();
            base.OnPointerEnter(eventData);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            OnMouseExit?.Invoke();
            base.OnPointerEnter(eventData);
        }
    }
}