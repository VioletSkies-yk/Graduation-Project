using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GamePlay.GameLogic.Episode1
{
    public class QRmonitor : MonoBehaviour
    {
        private bool isCompelete;

        private void OnEnable()
        {
            isCompelete = false;
            EventManager.Instance.StartListening(CONST.ReleaseCatchItem, delegate (GameObject obj) { ReleaseCallBack(obj); });
        }
        private void OnDisable()
        {
            EventManager.Instance.StopListening(CONST.ReleaseCatchItem, delegate (GameObject obj) { ReleaseCallBack(obj); });
        }

        private void ReleaseCallBack(GameObject obj)
        {
            if (isCompelete || (!string.Equals(obj.name, "card-1") && !string.Equals(obj.name, "card-2")))
                return;
            if (this.GetComponent<Collider>().bounds.Contains(obj.transform.position))
            {
                var mats = GetComponent<MeshRenderer>().materials;
                mats[1].color = Color.green;
                //Debug.LogError(GetComponent<MeshRenderer>().materials[1]);
                isCompelete = true;
                obj.SetActive(false);
                if (string.Equals(obj.name, "card-1"))
                    EventManager.Instance.TriggerEvent(CONST.Lv2Door1);
                else if (string.Equals(obj.name, "card-2"))
                    EventManager.Instance.TriggerEvent(CONST.Lv2Door2);
            }
        }
    }
}
