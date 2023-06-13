using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GamePlay.UI
{
    public class SaveMenuItem : MonoBehaviour
    {
        [SerializeField] private ButtonHandle _saveGameBtns;

        [SerializeField] private GameObject _fillBg;

        [SerializeField] private GameObject _emptyBg;

        [SerializeField] private GameObject _selectBg;

        /// <summary>
        /// 该存档是否为空
        /// </summary>
        public bool _isEmpty;

        /// <summary>
        /// 存档指针
        /// </summary>
        private int _index;

        public void Init(int index, Action<int> action)
        {
            _index = index;

            _isEmpty = SaveManager.Instance.isSaveDataEmpty(_index);


            StartListening(action);

            RefreshUI();
        }

        private void StartListening(Action<int> action)
        {
            _saveGameBtns.onClick.AddListener(delegate ()
            {
                action(_index);
            });
            //_saveGameBtns.SetCallBack(() =>
            //{
            //    KaiUtils.SetActive(true, _selectBg);
            //},
            //() =>
            //{
            //    KaiUtils.SetActive(false, _selectBg);
            //}
            //);
        }

        public void StopListening()
        {
            _saveGameBtns.onClick.RemoveAllListeners();
        }

        public void RefreshUI()
        {
            _isEmpty = SaveManager.Instance.isSaveDataEmpty(_index);

            _fillBg.SetActive(!_isEmpty);
            _emptyBg.SetActive(_isEmpty);

            int hour = DateTime.Now.Hour;
            int minute = DateTime.Now.Minute;
            int second = DateTime.Now.Second;
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;

            //_date.text = string.Format("{0}/{1}/{2}/{3}/{4}/{5}", year, month, day, hour, minute, second);

        }
    }
}
