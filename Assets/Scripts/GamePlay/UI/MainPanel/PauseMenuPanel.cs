using Assets.Scripts.GamePlay.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static SceneManager;

namespace Assets.Scripts.GamePlay.UI
{
    public class PauseMenuPanel : UINode
    {
        [SerializeField] private ButtonHandle _continueBtn;

        [SerializeField] private ButtonHandle _returnMainBtn;

        [SerializeField] private ButtonHandle _saveBtn;

        [SerializeField] private ButtonHandle _helpBtn;



        [SerializeField] private GameObject _continueBtnBg;

        [SerializeField] private GameObject _returnMainBtnBg;

        [SerializeField] private GameObject _saveBtnBg;

        [SerializeField] private GameObject _helpBtnBg;

        protected override bool OnOpened()
        {
            GameManager.Instance.GamePause();
            AddListener();
            return base.OnOpened();
        }

        protected override void OnClosing()
        {
            GameManager.Instance.GameContinue();
            RemoveListener();
            base.OnClosing();
        }

        private void AddListener()
        {

            _continueBtn.onClick.AddListener(onButtonClickToClose);

            _continueBtn.SetCallBack(() =>
            {
                KaiUtils.SetActive(true, _continueBtnBg);
            },
            () =>
            {
                KaiUtils.SetActive(false, _continueBtnBg);
            }
            );

            _returnMainBtn.onClick.AddListener(OnClickReturnMenuPanel);
            _returnMainBtn.SetCallBack(() =>
            {
                KaiUtils.SetActive(true, _returnMainBtnBg);
            },
            () =>
            {
                KaiUtils.SetActive(false, _returnMainBtnBg);
            }
            );

            _saveBtn.onClick.AddListener(OnClickSave);

            _saveBtn.SetCallBack(() =>
            {
                KaiUtils.SetActive(true, _saveBtnBg);
            },
            () =>
            {
                KaiUtils.SetActive(false, _saveBtnBg);
            }
            );

            _helpBtn.onClick.AddListener(OnClickHelp);

            _helpBtn.SetCallBack(() =>
            {
                KaiUtils.SetActive(true, _helpBtnBg);
            },
            () =>
            {
                KaiUtils.SetActive(false, _helpBtnBg);
            }
            );
        }
        private void RemoveListener()
        {
            _continueBtn.onClick.RemoveListener(onButtonClickToClose);

            _returnMainBtn.onClick.RemoveListener(onButtonClickToClose);

            _saveBtn.onClick.RemoveListener(onButtonClickToClose);

            _helpBtn.onClick.RemoveListener(onButtonClickToClose);

        }


        private void ReturnMainPanelCallBack()
        {

        }

        #region Click

        private void OnClickReturnMenuPanel()
        {
            GameManager.Instance.GameContinue();
            EventManager.Instance.TriggerEvent(CONST.SendLoadingScene, new SceneMsg(CONST.SCENE_NAME_CACHE, delegate ()
            {
                onButtonClickToClose();

                //PlayerController.Instance.SetUnLockPos(false);
                //PlayerController.Instance.SetUnLockRot(false);
                //PlayerController.Instance.gameObject.SetActive(false);

                UIManager.Instance.OpenUI(CONST.UI_MainMenuPanel);
            })
            );
        }

        private void OnClickSave()
        {
            UIManager.Instance.OpenUI(CONST.UI_SaveMenuPanel);
        }

        private void OnClickHelp()
        {
            UIManager.Instance.OpenUI(CONST.UI_HelpPanel);
        }

        #endregion
    }
}
