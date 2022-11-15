using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GamePlay.GameLogic
{
    public enum EpisodeType
    {
        Episode1 = 1,
        Episode2 = 2,
        Episode3 = 3,
        Episode4 = 4,
        Episode5 = 5,
        Episode6 = 6,
    }
    public abstract class Episode : MonoBehaviour
    {
        [SerializeField]
        private Transform _bornPoint;

        [SerializeField]
        private List<LevelStage> _levelStageList;


        public EpisodeType Type { get; private set; }

        protected Vector3 bornPoint;

        protected Queue<LevelStage> _levelStageQueue;

        protected Dictionary<LevelStageType, LevelStage> _levelStageDic;

        protected LevelStageType _curLevel;

        public Episode(EpisodeType type)
        {
            Type = type;

            _curLevel = LevelStageType.Level_01;

            _levelStageQueue = new Queue<LevelStage>();
            _levelStageDic = new Dictionary<LevelStageType, LevelStage>();

        }

        public void Awake()
        {
            Init();
            Enter();
        }

        private void Init()
        {
            bornPoint = _bornPoint.position;
            for (int i = 0; i < _levelStageList.Count; i++)
            {
                _levelStageQueue.Enqueue(_levelStageList[i]);
                _levelStageDic.Add(_levelStageList[i].Type, _levelStageList[i]);
                _levelStageList[i].Init();
            }
            EventManager.Instance.StartListening<string>(CONST.FinishLoadingSceneProgress, OnLoadSceneCallBack);
        }

        private void Enter()
        {
            OnEnter();
            _levelStageDic[_curLevel].OnEnter();
            PlayerController.Instance.SetPlayerPosAndRotation(bornPoint);
        }

        protected void ChangeLevelStage()
        {
            OnChangeLevelStage();

            _curLevel = _levelStageQueue.Peek().Type;
            _levelStageDic[_curLevel].OnLeave();
            _levelStageQueue.Dequeue();
            _levelStageQueue.Peek().OnEnter();
        }

        private void ResetAll()
        {
            OnReset();

            for (int i = 1; i <= (int)_curLevel; i++)
            {
                _levelStageDic[_curLevel].ResetStage();
            }
            PlayerController.Instance.SetPlayerPosAndRotation(bornPoint);
        }

        public virtual void OnEnter()
        {

        }

        public virtual void OnChangeLevelStage()
        {

        }

        public virtual void OnReset()
        {

        }


        private void OnLoadSceneCallBack(string sceneName)
        {
            switch (Type)
            {
                case EpisodeType.Episode1:
                    if (string.Equals(CONST.SCENE_NAME_LEVEL_01, sceneName))
                    {
                        Debug.Log("成功加载第一关");
                        Enter();
                    }
                    break;
                default:
                    break;
            }

        }
    }
}
