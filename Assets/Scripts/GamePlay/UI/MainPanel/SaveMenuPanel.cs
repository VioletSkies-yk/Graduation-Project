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
    public class SaveMenuPanel : UINode
    {
        /// <summary>
        /// 存档按钮列表
        /// </summary>
        [SerializeField] private SaveMenuItem[] _saveGameItems;

        /// <summary>
        /// 关闭按钮
        /// </summary>  
        [SerializeField] private ButtonHandle _closeBtn;

        /*
        /// <summary>
        /// 创建存档界面
        /// </summary>
        [SerializeField] private GameObject _createSavePanel;

        /// <summary>
        /// 创建存档界面描述
        /// </summary>
        [SerializeField] private Text _createSavePanelDescription;

        /// <summary>
        /// 创建存档确认按钮
        /// </summary>
        [SerializeField] private Button _createSaveBtn;

        /// <summary>
        /// 取消创建存档按钮
        /// </summary>
        [SerializeField] private Button _cancelSaveBtn;

        /// <summary>
        /// 取消创建存档按钮
        /// </summary>
        [SerializeField] private Button _closeBtn;

        */

        private int _index;

        private bool isGaming = false;

        protected override bool OnOpened()
        {
            StartListening();
            HideConfirmPanel();
            return base.OnOpened();
        }
        protected override void OnClosing()
        {
            StopListening();
            base.OnClosing();
        }

        public override void Init()
        {

            base.Init();
        }

        public void StartListening()
        {
            for (int i = 0; i < _saveGameItems.Length; i++)
            {
                _saveGameItems[i].Init(i, OnClickSaveButton);
            }

            //_createSaveBtn.onClick.AddListener(delegate ()
            //{
            //    OnClickSaveConfirm();
            //});

            //_cancelSaveBtn.onClick.AddListener(delegate ()
            //{
            //    KaiUtils.SetActive(false, _createSavePanel.gameObject);
            //});

            //_closeBtn.onClick.AddListener(delegate ()
            //{
            //    base.Close((int)ReturnCode.UserClick, this);
            //});

            EventManager.Instance.StartListening(CONST.SaveDataSuccess, RefreshUI);

            EventManager.Instance.StartListening(CONST.LoadingImageAllBlack, onButtonClickToClose);

            _closeBtn.onClick.AddListener(onButtonClickToClose);
        }

        public void StopListening()
        {
            for (int i = 0; i < _saveGameItems.Length; i++)
            {
                _saveGameItems[i].StopListening();
            }
            //_createSaveBtn.onClick.RemoveAllListeners();

            //_cancelSaveBtn.onClick.RemoveAllListeners();

            EventManager.Instance.StopListening(CONST.SaveDataSuccess, RefreshUI);

            EventManager.Instance.StopListening(CONST.LoadingImageAllBlack, onButtonClickToClose);

            _closeBtn.onClick.RemoveListener(onButtonClickToClose);
        }

        /// <summary>
        /// 保存成功回调
        /// </summary>
        private void RefreshUI()
        {
            _saveGameItems[_index].RefreshUI();
        }

        private void HideConfirmPanel()
        {
            //KaiUtils.SetActive(false, _createSavePanel);
        }

        /// <summary>
        /// 点击存档列表后的回调
        /// </summary>
        /// <param name="index"></param>
        private void OnClickSaveButton(int index)
        {
            _index = index;

            KaiUtils.Log("当前存档指针为{0}", _index);

            if (!_saveGameItems[_index]._isEmpty)
            {
                if (SceneManager.Instance.isInPlayingScene)
                {
                    SaveData _data = new SaveData
                    {
                        saveSceneIndex = SceneManager.Instance.saveSceneIndex,
                        currentSceneIndex = SceneManager.Instance.curSceneIndex,
                        position = PlayerController.Instance.savePos
                    };
                    SaveManager.Instance.Save(_data, _index);
                }
                else
                {
                    if (SaveManager.Instance.InitLoadData(_index))
                    {
                        GameManager.Instance.GameEntry(SaveManager.Instance.GameData);
                    }
                    else
                    {
                        KaiUtils.Error($"存档{_index}为空，请检查本地文件");
                    }
                }
            }
            else
            {
                SaveData _data = new SaveData
                {
                    saveSceneIndex = SceneManager.Instance.saveSceneIndex,
                    currentSceneIndex = SceneManager.Instance.curSceneIndex,
                    position = PlayerController.Instance.transform.position
                };
                SaveManager.Instance.Save(_data, _index);
            }
        }

        /// <summary>
        /// 点击确认后的回调（包括创建新存档、覆盖旧存档）
        /// </summary>
        //private void OnClickSaveConfirm()
        //{
        //    SaveData _data = new SaveData
        //    {
        //        saveSceneIndex = SceneManager.Instance.saveSceneIndex,
        //        currentSceneIndex = SceneManager.Instance.curSceneIndex,
        //        position = PlayerController.Instance.transform.position
        //    };
        //    SaveManager.Instance.Save(_data, _index);
        //    //KaiUtils.SetActive(false, _createSavePanel);
        //}
    }
}
