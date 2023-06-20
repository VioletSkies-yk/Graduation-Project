using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.GamePlay.GameLogic
{
    public class PlateInteractable : Interactable
    {
        [SerializeField] private Transform _pos;
        [SerializeField] private Transform _parent;
        [SerializeField] private Transform _sliceObject;
        [SerializeField] private Transform _nextPos;

        private bool isCanClick;

        private void OnEnable()
        {
            isCanClick = false;
            EventManager.Instance.StartListening(CONST.PlateCanClick, CallBack);
            EventManager.Instance.StartListening(CONST.ResetPlate, ResetPlate);
        }

        private void OnDisable()
        {
            EventManager.Instance.StopListening(CONST.PlateCanClick, CallBack);
            EventManager.Instance.StopListening(CONST.ResetPlate, ResetPlate);
        }
        public override void OnFocus()
        {

        }

        public override void OnInteract()
        {
            if (!isCanClick)
                return;
            if (_parent.childCount > 0)
            {
                var temp = _parent.GetChild(0);
                if (temp != null /*&& temp.GetComponent<BaseCatchInteractable>() != null*/)
                {
                    temp.SetParent(_pos);
                    temp.localPosition = Vector3.zero;
                    //temp.GetComponent<BaseCatchInteractable>().enabled = false;
                    if (_parent.childCount == 0)
                    {
                        if (_pos.childCount == 4)
                        {
                            if(_sliceObject!=null)
                                _sliceObject.SetParent(_parent);
                            PlayerController.Instance.SetPlayerPosAndRotation(_nextPos.position);
                            Debug.Log("lv3r1通关");
                        }
                        else
                        {
                            EventManager.Instance.TriggerEvent(CONST.ResetPlate);
                        }
                    }
                }
            }
        }

        public override void OnLoseFocus()
        {

        }

        private void CallBack()
        {
            isCanClick = true;
        }

        private void ResetPlate()
        {
            foreach (var item in _pos.GetComponentsInChildren<Rigidbody>())
            {
                item.transform.SetParent(_parent);
                //item.GetComponent<BaseCatchInteractable>().enabled = true;
            }
        }
    }
}
