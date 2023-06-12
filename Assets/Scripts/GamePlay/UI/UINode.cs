using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GamePlay.UI
{
    public class UINode : MonoBehaviour
    {
        public enum ReturnCode
        {
            OK = 0,
            ShutDown = 10,
            ParentClose = 20,
            UserClick = 30,
        }
        public string Name { get; set; }

        public bool IsOpened { get { return mGo.activeSelf; } }

        public delegate void OnUIClosed(UINode ui, int returncode);
        public OnUIClosed onUIClosed;

        public void AddUIClosedEvent(OnUIClosed onUIClosedEvent)
        {
            onUIClosed += onUIClosedEvent;
        }


        public Action OnInvokeCallback;

        protected GameObject mGo;


        protected UINode _parent;
        protected List<UINode> _openedUIs = new List<UINode>();

        public int _nodeLayer = -1;

        public virtual void Init()
        {
            mGo = gameObject;
            mGo.SetActive(false);
            Name = gameObject.name;
        }

        protected virtual bool OnOpened()
        {
            return true;
        }
        protected virtual void OnClosing() { }

        public bool Open(UINode parent)
        {
            // parent is not allowed to be null.
            _parent = parent;
            gameObject.SetActive(true);
            // if open process is failed, close the game object immediately.
            if (_parent != null && OnOpened())
            {
                if (UIManager.Instance)
                {
                    _nodeLayer = UIManager.Instance._currentLayer++;
                }
            }
            else
            {
                _parent = null;
                gameObject.SetActive(false);
            }
            return IsOpened;
        }
        public virtual UINode OpenUI(string uiName, OnUIClosed callback = null)
        {
            UINode ui = UIManager.Instance.GetUI(uiName);
            if (ui != null && !ui.IsOpened && ui.Open(this))
            {
                _openedUIs.Add(ui);
                ui.onUIClosed = (UINode node, int returncode) =>
                {
                    // remove ui from opened UI list if the UI is closed by itself.
                    _openedUIs.Remove(node);
                };
                if (callback != null)
                {
                    ui.onUIClosed += callback;
                }
                return ui;
            }
            return null;
        }


        protected void Close(int returncode, UINode caller)
        {
            string callerName = caller != null ? caller.gameObject.name : "NULL";
            if (caller == null || (caller != this && caller != _parent))
            {
                Debug.LogError(string.Format("{0} is closed by {1}, but the parent is {2}", Name, callerName,
                    _parent ? _parent.Name : "null"));
            }

            OnClosing();

            gameObject.SetActive(false);

            if (onUIClosed != null)
            {
                onUIClosed(this, returncode);
            }
            Destroy(gameObject);
            onUIClosed = null;
            _parent = null;
        }

        public void CloseUI(string name)
        {
            UINode ui = UIManager.Instance.GetOpenedUI(name);
            if (ui != null)
            {
                ui.Close((int)ReturnCode.ParentClose, this);
                //UIManager.Instance.StopCountUIFPS(name);
            }

        }
        public virtual void onButtonClickToClose()
        {
            Close((int)ReturnCode.UserClick, this);
        }
    }
}
