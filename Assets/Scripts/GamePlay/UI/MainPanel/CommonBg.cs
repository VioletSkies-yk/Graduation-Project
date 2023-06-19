using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.GamePlay.UI
{
    public class CommonBg : MonoBehaviour
    {
        /// <summary>
        /// 背景图
        /// </summary>  
        [SerializeField] private Image _bg;

        [SerializeField] private Sprite _lv1;
        [SerializeField] private Sprite _lv2;
        [SerializeField] private Sprite _lv3;

        public void SetBg()
        {
            StartCoroutine(DelaySetBg());
        }

        IEnumerator DelaySetBg()
        {
            yield return null;

            if (SceneManager.Instance.isInPlayingScene)
                _bg.gameObject.SetActive(false);
            else
            {
                _bg.gameObject.SetActive(true);
                int index = SaveManager.Instance.farSceneIndex;
                switch (index)
                {
                    case 1:
                        _bg.sprite = _lv1;
                        break;
                    case 2:
                        _bg.sprite = _lv2;
                        break;
                    case 3:
                        _bg.sprite = _lv3;
                        break;
                    default:
                        _bg.sprite = _lv1;
                        break;
                }

            }
        }
    }
}
