using Assets.Scripts;
using Assets.Scripts.GamePlay.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TouchTrigger : MonoBehaviour
{
    protected bool isTriggerON;

    public Action TriggerAction;
    //public bool isTouchTrigger;

    public TouchTrigger()
    {
        isTriggerON = true;
        TriggerAction = OnTriggerEnterCallBack;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTriggerON)
        {
            if (other.gameObject == PlayerController.Instance.gameObject)
            {
                TriggerAction?.Invoke();
            }
        }
    }

    public virtual void OnTriggerEnterCallBack()
    {

    }

    public void SetTriggerOnOfOff(bool isOn)
    {
        isTriggerON = isOn;
    }

    //private void Update()
    //{
    //    if (!isTouchTrigger)
    //    {
    //        Detection();
    //    }
    //}
    //private void Detection()
    //{
    //    if (GetComponent<BoxCollider2D>().bounds.Contains(Player.m_Instance.transform.position - new Vector3(0, 1.5f, 0)))
    //    {
    //        if (Input.GetKeyDown(KeyCode.E))
    //            TriggerAction();
    //    }
    //}
}
