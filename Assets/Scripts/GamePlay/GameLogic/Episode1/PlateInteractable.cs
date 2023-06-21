using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace Assets.Scripts.GamePlay.GameLogic
{
    public class PlateInteractable : Interactable
    {
        [SerializeField] private Transform _pos;
        [SerializeField] private Transform _parent;
        [SerializeField] private Transform _sliceObject;
        [SerializeField] private Transform _nextPos;

        public bool isMain;

        private bool isCanClick;

        private void OnEnable()
        {
            isCanClick = false;
            EventManager.Instance.StartListening(CONST.PlateCanClick, CallBack);
            EventManager.Instance.StartListening(CONST.ResetPlate, ResetPlate);
            if (isMain)
            {
                TriggerBlink();
            }
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
                        if (_pos.childCount == 4 && isMain)
                        {
                            if (_sliceObject != null)
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

        public float blinkDuration = 50f;
        public float minBrightness = 0.1f;
        public float maxBrightness = 1f;
        public Color originalColor = Color.white;

        private Material material;
        private Sequence blinkSequence;

        private void TriggerBlink()
        {
            // 获取物体的材质
            material = GetComponent<MeshRenderer>().material;

            // 创建闪烁序列
            blinkSequence = DOTween.Sequence()
                .Join(material.DOColor(Color.white * minBrightness, "_EmissiveColor", blinkDuration / 2f))
                .Append(material.DOColor(originalColor * maxBrightness, "_EmissiveColor", blinkDuration / 2f))
                .SetLoops(-1)
                .SetEase(Ease.InOutQuad)
                .SetAutoKill(false)
                .Pause();

            // 开始闪烁
            StartCoroutine(StartBlinking());
        }

        IEnumerator StartBlinking()
        {
            // 设置自发光颜色和纹理
            material.SetColor("_EmissiveColor", originalColor * maxBrightness);
            material.SetTexture("_EmissiveColorMap", null);

            // 播放闪烁序列
            blinkSequence.Play();

            // 等待一段时间后停止闪烁
            yield return new WaitForSeconds(blinkDuration);

            // 停止闪烁并恢复原始颜色
            blinkSequence.Pause();
            material.SetColor("_EmissiveColor", originalColor * maxBrightness);
            material.SetTexture("_EmissiveColorMap", null);

            // 等待一段时间后停止闪烁
            yield return new WaitForSeconds(blinkDuration);

            // 再次开始闪烁
            StartCoroutine(StartBlinking());
        }
    }
}
