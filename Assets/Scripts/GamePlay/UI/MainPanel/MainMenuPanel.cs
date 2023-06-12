using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.GamePlay.UI
{
    public class MainMenuPanel : UINode
    {
        /// <summary>
        /// 开始游戏按钮
        /// </summary>
        [SerializeField] private ButtonHandle _beginGame;

        /// <summary>
        /// 开始游戏按钮
        /// </summary>
        [SerializeField] private ButtonHandle _continueGame;

        /// <summary>
        /// 退出游戏按钮
        /// </summary>
        [SerializeField] private ButtonHandle _helpGame;

        /// <summary>
        /// 退出游戏按钮
        /// </summary>
        [SerializeField] private ButtonHandle _quitGame;


        /// <summary>
        /// 开始游戏按钮
        /// </summary>
        [SerializeField] private GameObject _beginBtnBg;

        /// <summary>
        /// 开始游戏按钮
        /// </summary>
        [SerializeField] private GameObject _continueBtnBg;

        /// <summary>
        /// 开始游戏按钮
        /// </summary>
        [SerializeField] private GameObject _helpBtnBg;

        /// <summary>
        /// 退出游戏按钮
        /// </summary>
        [SerializeField] private GameObject _quitBtnBg;


        /// <summary>
        /// 背景图
        /// </summary>  
        [SerializeField] private Image _bg;


        protected override bool OnOpened()
        {
            Screen.lockCursor = false;
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
            _bg.sprite.name = KaiUtils.GetBgName();
            _beginGame.onClick.AddListener(OnClickStart);
            _beginGame.SetCallBack(() =>
            {
                KaiUtils.SetActive(true, _beginBtnBg);
            },
            () =>
            {
                KaiUtils.SetActive(false, _beginBtnBg);
            }
            );

            _continueGame.onClick.AddListener(OnClickContinue);

            _continueGame.SetCallBack(() =>
            {
                KaiUtils.SetActive(true, _continueBtnBg);
            },
            () =>
            {
                KaiUtils.SetActive(false, _continueBtnBg);
            }
            );

            _helpGame.onClick.AddListener(OnClickHelp);

            _helpGame.SetCallBack(() =>
            {
                KaiUtils.SetActive(true, _helpBtnBg);
            },
            () =>
            {
                KaiUtils.SetActive(false, _helpBtnBg);
            }
            );

            _quitGame.onClick.AddListener(OnClickQuit);

            _quitGame.SetCallBack(() =>
            {
                KaiUtils.SetActive(true, _quitBtnBg);
            },
            () =>
            {
                KaiUtils.SetActive(false, _quitBtnBg);
            }
            );


            EventManager.Instance.StartListening(CONST.LoadingImageAllBlack, onButtonClickToClose);
        }
        private void RemoveListener()
        {
            _beginGame.onClick.RemoveAllListeners();

            _helpGame.onClick.RemoveAllListeners();

            _continueGame.onClick.RemoveAllListeners();

            _quitGame.onClick.RemoveAllListeners();

            EventManager.Instance.StopListening(CONST.LoadingImageAllBlack, onButtonClickToClose);
        }

        #region Click

        private void OnClickStart()
        {
            GameManager.Instance.GameEntry();
        }

        private void OnClickContinue()
        {
            UIManager.Instance.OpenUI(CONST.UI_SaveMenuPanel);
        }

        private void OnClickHelp()
        {
            UIManager.Instance.OpenUI(CONST.UI_HelpPanel);
        }


        private void OnClickQuit()
        {
            //base.Close((int)ReturnCode.UserClick, this);
            GameManager.Instance.GameQuit();
        }

        #endregion
    }
}
