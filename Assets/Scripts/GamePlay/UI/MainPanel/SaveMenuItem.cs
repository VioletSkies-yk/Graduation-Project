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
        /// <summary>
        /// 存档按钮
        /// </summary>
        [SerializeField] private Button _saveGameBtns;

        /// <summary>
        /// 存档名字
        /// </summary>
        [SerializeField] private Text _name;

        /// <summary>
        /// 存档名字
        /// </summary>
        [SerializeField] private Text _date;


        /// <summary>
        /// 该存档是否为空
        /// </summary>
        public  bool _isEmpty;

        /// <summary>
        /// 存档指针
        /// </summary>
        private int _index;

        public void Init(int index,Action<int> action)
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
        }

        public  void StopListening()
        {
            _saveGameBtns.onClick.RemoveAllListeners();
        }

        public void RefreshUI()
        {
            _isEmpty = SaveManager.Instance.isSaveDataEmpty(_index);

            _name.text = _isEmpty ? CONST.SAVEMENU_NONE_NAME : string.Format(CONST.SAVEMENU_NAME, _index);

            int hour = DateTime.Now.Hour;
            int minute = DateTime.Now.Minute;
            int second = DateTime.Now.Second;
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;

            _date.text = string.Format("{0}/{1}/{2}/{3}/{4}/{5}", year, month, day, hour, minute, second);

        }
    }
}
