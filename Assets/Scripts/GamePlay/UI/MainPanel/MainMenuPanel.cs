using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GamePlay.UI
{
    public class MainMenuPanel : UINode
    {
        /// <summary>
        /// 开始游戏按钮
        /// </summary>
        [SerializeField] private Button _beginGame;

        /// <summary>
        /// 开始游戏按钮
        /// </summary>
        [SerializeField] private Button _continueGame;

        /// <summary>
        /// 退出游戏按钮
        /// </summary>
        [SerializeField] private Button _quitGame;


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
            _beginGame.onClick.AddListener(delegate () { GameManager.Instance.GameEntry(); });

            _continueGame.onClick.AddListener(delegate ()
            {
                UIManager.Instance.OpenUI(CONST.UI_SaveMenuPanel);
            });

            _quitGame.onClick.AddListener(delegate () { base.Close((int)ReturnCode.UserClick, this); });


            EventManager.Instance.StartListening(CONST.LoadingImageAllBlack, onButtonClickToClose);
        }
        private void RemoveListener()
        {
            _beginGame.onClick.RemoveAllListeners();

            _continueGame.onClick.RemoveAllListeners();

            _quitGame.onClick.RemoveAllListeners();

            EventManager.Instance.StopListening(CONST.LoadingImageAllBlack, onButtonClickToClose);
        }
    }
}
