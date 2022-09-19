using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GamePlay.UI
{
    public class UIManager : OsSingletonMono<UIManager>
    {
        private static UIManager __inst;

        public UINode[] _uiNodes;

        private Dictionary<string, UINode> _uiNodeDict = new Dictionary<string, UINode>();

        public int _currentLayer = 0;

        private Stack<UINode> panelStack;

        public UINode _uiParent;

        void Awake()
        {
            __inst = this;

            _uiNodeDict.Clear();

            for (int i = 0; i < _uiNodes.Length; ++i)
            {
                _uiNodes[i].Init();
                _uiNodeDict.Add(_uiNodes[i].Name, _uiNodes[i]);

            }

            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(_uiParent.gameObject);
        }

        /*
        /// <summary>
        /// 把指定类型的panel入栈,并显示在场景中
        /// </summary>
        public void PushPanel(UIPanelType type)
        {
            if (panelStack == null)
                panelStack = new Stack<UINode>();

            //判断栈里是否有其他panel,若有,则把原栈顶panel设定其状态为暂停(OnPause)
            if (panelStack.Count > 0)
            {
                UINode topPanel = panelStack.Peek();
                topPanel.OnPause();
            }

            UINode panel = GetPanel(type);

            //把指定类型的panel入栈并设定其状态为进入场景(OnEnter)
            panelStack.Push(panel);
            panel.OnEnter();
        }

        /// <summary>
        /// 把栈顶panel出栈,并从场景中消失
        /// </summary>
        public void PopPanel()
        {
            if (panelStack == null)
                panelStack = new Stack<UINode>();

            //检查栈是否为空，若为空则直接退出方法
            if (panelStack.Count <= 0) return;

            //把栈顶panel出栈，并把该panel状态设为退出场景(OnExit)
            UINode topPanel = panelStack.Pop();
            topPanel.OnExit();

            //再次检查出栈栈顶Panel后栈是否为空
            //若为空，直接退出方法
            //若不为空，则把新的栈顶Panel状态设为继续(OnResume)
            if (panelStack.Count <= 0) return;

            UINode topPanel2 = panelStack.Peek();
            topPanel2.OnResume();
        }

        */
        //private GameObject _uiParent;
        public UINode GetUI(string uiname)
        {
            UINode ui = null;
            //if it not init,load it.
            if (!_uiNodeDict.TryGetValue(uiname, out ui) || ui == null)
            {
                if (ui == null)
                {
                    _uiNodeDict.Remove(uiname);
                }
                //first load use less uiif(assetPath != null)
                // in Editor try to load from actual place
                GameObject prefab = (GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath(string.Format(CONST.UIPrefabAssetPath, uiname), typeof(GameObject));
                if (null != prefab)
                {
                    GameObject go = Instantiate(prefab, _uiParent.transform);

                    ui = go.GetComponent<UINode>();
                    if (null != ui)
                    {
                        ui.Init();
                        //去掉带clone的名字
                        ui.name = uiname;
                        ui.Name = uiname;
                        _uiNodeDict.Add(uiname, ui);
                    }

                }
                else
                {
                    Debug.LogError("missing UI: " + uiname);
                }
            }
            return ui;
        }
        public UINode GetOpenedUI(string uiName)
        {
            UINode ui = null;
            _uiNodeDict.TryGetValue(uiName, out ui);
            if (ui != null && ui.IsOpened)
            {
                return ui;
            }
            return null;
        }
        public UINode OpenUI(string uiName, UINode.OnUIClosed callback = null)
        {
            UINode ui = UIManager.Instance.GetUI(uiName);
            if (ui != null && !ui.IsOpened && ui.Open(_uiParent))
            {
                ui.onUIClosed = (UINode node, int returncode) =>
                {
                    KaiUtils.Log("{0}界面已成功关闭", uiName);
                };
                if (callback != null)
                {
                    ui.onUIClosed += callback;
                }
            }
            var uiNode = ui.OpenUI(uiName, callback);
            return uiNode;
        }
    }
}
