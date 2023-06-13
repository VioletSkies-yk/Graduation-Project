using Assets.Scripts.GamePlay.GameLogic;
using UnityEngine;
using DG.Tweening;
using System.Text;
using UnityEngine.UI;
using System.Collections;

namespace Assets.Scripts.GamePlay.UI
{
    public class SubtitlesPanel : UINode
    {
        /// <summary>
        /// 展示图片
        /// </summary>
        [SerializeField] private Text textMeshPro;
        public string fullText;
        public float speed = 1f;

        private string currentText = "";

        private Coroutine coroutine;

        protected override bool OnOpened()
        {
            textMeshPro.transform.parent.gameObject.SetActive(false);
            EventManager.Instance.StartListening<string[]>(CONST.SendTypeMsg, DoType);
            //EventManager.Instance.StartListening(CONST.SendLoadingScene, StopAllCoroutines);
            return base.OnOpened();
        }

        protected override void OnClosing()
        {
            EventManager.Instance.StopListening<string[]>(CONST.SendTypeMsg, DoType);
            //EventManager.Instance.StopListening(CONST.SendLoadingScene, StopAllCoroutines);
            base.OnClosing();
        }

        private void Update()
        {
            if (IsOpened && Input.GetKeyDown(KeyCode.E))
            {
                onButtonClickToClose();
            }
        }

        private void DoType(string[] content)
        {
            if (textMeshPro.transform.parent.gameObject.activeInHierarchy)
                return;
            textMeshPro.transform.parent.gameObject.SetActive(true);
            //return;
            coroutine=StartCoroutine(TypeText(content));
        }

        private IEnumerator TypeText(string[] content)
        {
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < content.Length; i++)
            {
                fullText = content[i];
                // 字符串转为字符数组
                byte[] bytes = Encoding.Unicode.GetBytes(fullText);
                int length = bytes.Length / 2;

                // 从字节数组转换回字符数组
                char[] chars = new char[length];
                for (int j = 0; j < length; j++)
                {
                    chars[j] = (char)(bytes[j * 2] | bytes[j * 2 + 1] << 8);
                }

                StringBuilder sb = new StringBuilder();

                // 逐个字打出
                for (int j = 0; j < chars.Length; j++)
                {
                    sb.Append(chars[j]);
                    currentText = sb.ToString(); // 记录当前打印出来的文本

                    // 这里就是让每个字逐个打出的效果了
                    textMeshPro.text = currentText;
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(1f);
            textMeshPro.transform.parent.gameObject.SetActive(false);
            textMeshPro.text = "";
        }
    }
}
