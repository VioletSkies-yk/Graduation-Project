using Assets.Scripts.GamePlay.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GamePlay.UI
{
    public class DetailTipsPanel : UINode
    {
        /// <summary>
        /// 展示图片
        /// </summary>
        [SerializeField] private Image _tipsImage;

        /// <summary>
        /// 展示图片
        /// </summary>
        //[SerializeField] private Button _closeBtn;


        protected override bool OnOpened()
        {
            //_closeBtn.onClick.AddListener(onButtonClickToClose);
            PlayerController.Instance.LockAll();
            return base.OnOpened();
        }

        protected override void OnClosing()
        {
            //_closeBtn.onClick.RemoveAllListeners();
            SetImage(null);
            PlayerController.Instance.UnpackLockAll();
            base.OnClosing();
        }

        private void Update()
        {
            if(IsOpened&& Input.GetKeyDown(KeyCode.E))
            {
                onButtonClickToClose();
            }
        }

        public void SetImage(Sprite name)
        {
            _tipsImage.sprite = name;
        }

    }
}
