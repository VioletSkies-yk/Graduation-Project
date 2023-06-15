using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using System;
using Assets.Scripts.GamePlay.UI;

namespace Assets.Scripts.GamePlay.GameLogic
{
    public class PlayerController : OsSingletonMono<PlayerController>
    {
        #region 可调参数

        /// <summary>
        /// 玩家移动速度
        /// </summary>
        [Space]
        [Range(0, 20)]
        [Header("玩家移动速度")]
        [SerializeField] private float _walkSpeed = 5;

        /// <summary>
        /// 玩家加速移动速度
        /// </summary>
        [Space]
        [Range(0, 50)]
        [Header("玩家加速移动速度")]
        [SerializeField] private float _runSpeed = 10;

        /// <summary>
        /// 玩家跳跃强度
        /// </summary>
        [Space]
        [Range(0, 20)]
        [Header("玩家跳跃强度")]
        [SerializeField] private float _jumpPower = 5;


        /// <summary>
        /// 玩家所受到的重力
        /// </summary>
        [Space]
        [Range(0, 20)]
        [Header("玩家所受到的重力")]
        [SerializeField] private float _gravity = 7f;

        /// <summary>
        /// 鼠标移动速度
        /// </summary>
        [Space]
        [Range(0, 20)]
        [Header("鼠标移动速度")]
        [SerializeField] private float _mouseSpeed = 5f;

        /// <summary>
        /// 最小仰角（默认-60f）
        /// </summary>
        [Space]
        [Range(-90, 90)]
        [Header("最小仰角")]
        [SerializeField] private float _minMouseY = -60f;

        /// <summary>
        /// 最大仰角（默认60f）
        /// </summary>
        [Space]
        [Range(-90, 90)]
        [Header("最大仰角")]
        [SerializeField] private float _maxMouseY = 60f;

        #endregion

        #region 组件
        /// <summary>
        /// 玩家控制器
        /// </summary>
        [SerializeField] private CharacterController _playerController;

        /// <summary>
        /// 玩家第一视角摄像机
        /// </summary>
        [SerializeField] public Camera _playerCamera;

        /// <summary>
        /// 交互射线的起始点
        /// </summary>
        [SerializeField] private Vector3 _interactionRayPoint = default;

        /// <summary>
        /// 用于判断玩家注视的距离长度
        /// </summary>
        [SerializeField] private float _interactionDistance = 100;

        /// <summary>
        /// 交互对象的层级
        /// </summary>
        [SerializeField] private LayerMask _interactionLayer = default;

        /// <summary>
        /// 当前所注视的交互对象
        /// </summary>
        [SerializeField] private Interactable _currentInteractable;

        /// <summary>
        /// 拿起物体时的挂点
        /// </summary>
        [SerializeField] public Transform _anchorPos;

        #endregion

        #region 固定参数

        /// <summary>
        /// 玩家当前注视的方向
        /// </summary>
        private Vector3 direction;

        /// <summary>
        /// Y轴旋转度数
        /// </summary>
        private float RotationY = 0f;

        /// <summary>
        /// X轴旋转度数
        /// </summary>
        private float RotationX = 0f;

        /// <summary>
        /// 是否可以移动
        /// </summary>
        private bool isUnLockPosition;


        /// <summary>
        /// 是否可以旋转
        /// </summary>
        private bool isUnLockRotationn;

        #endregion

        //private void Awake()
        //{
        //    DontDestroyOnLoad(this.gameObject);
        //}

        void Start()
        {
            InitData();
        }

        private void OnEnable()
        {
            EventManager.Instance.StartListening(CONST.LoadingImageAllBlack, OnSceneChangeStart);
            EventManager.Instance.StartListening<int>(CONST.FinishLoadingSceneProgress, OnSceneChange);
        }

        private void OnDisable()
        {
            EventManager.Instance.StopListening(CONST.LoadingImageAllBlack, OnSceneChangeStart);
            EventManager.Instance.StopListening<int>(CONST.FinishLoadingSceneProgress, OnSceneChange);
        }

        private void OnSceneChangeStart()
        {
            LockAll();
            //_playerCamera.gameObject.SetActive(false);
        }

        private void OnSceneChange(int index)
        {
            if (index == -1 || index == 0)
            {
                LockAll();
                _playerCamera.gameObject.SetActive(false);
            }
            else
            {
                if(index==2)
                {
                    gameObject.transform.localScale = new Vector3(6f, 6f, 6f);
                }
                else if(index == 3)
                {
                    gameObject.transform.localScale = new Vector3(10f, 10f, 10f);
                }
                else
                {
                    gameObject.transform.localScale = new Vector3(5f, 5f, 5f);
                }
                UnpackLockAll();
                _playerCamera.gameObject.SetActive(true);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!_playerCamera.gameObject.activeInHierarchy)
                return;
            if (GameManager.instance._pauseStatus)
            {
                LockAll();
                return;
            }
            UnpackLockAll();

            ActionControl();

            HandleInteractionCheck();
            HandleInteractionInput();


            if (Input.GetKeyDown(KeyCode.E))
            {
                UIManager.Instance.OpenUI(CONST.UI_DetailTipsPanel);
            }
            GamePause();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            isUnLockPosition = true;
            isUnLockRotationn = true;
        }

        #region 人物控制

        /// <summary>
        /// 人物控制器
        /// </summary>
        private void ActionControl()
        {
            Screen.lockCursor = true;

            if (isUnLockPosition)
            {
                float _horizontal = Input.GetAxis("Horizontal");
                float _vertical = Input.GetAxis("Vertical");

                if (_playerController.isGrounded)
                {
                    direction = new Vector3(_horizontal, 0, _vertical);
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        direction.y = _jumpPower;
                    }
                }
                direction.y -= _gravity * Time.deltaTime;
                Vector3 transfromDir = new Vector3((direction * Time.deltaTime * (Input.GetKey(KeyCode.LeftShift) ? _runSpeed : _walkSpeed)).x, (direction * Time.deltaTime * _walkSpeed).y, (direction * Time.deltaTime * (Input.GetKey(KeyCode.LeftShift) ? _runSpeed : _walkSpeed)).z);
                _playerController.Move(_playerController.transform.TransformDirection(transfromDir));

                #region Audio
                if (direction.x != 0 || direction.z != 0)
                {
                    EventManager.Instance.TriggerEvent(CONST.PlayAudio, "lv1walk");

                    if(Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        EventManager.Instance.TriggerEvent(CONST.StopAudio, "lv1walk");
                        EventManager.Instance.TriggerEvent(CONST.PlayAudio, "lv1run");
                    }
                    else
                    {
                        EventManager.Instance.TriggerEvent(CONST.StopAudio, "lv1run");
                        EventManager.Instance.TriggerEvent(CONST.PlayAudio, "lv1walk");
                    }
                }
                else
                {
                    EventManager.Instance.TriggerEvent(CONST.StopAudio, "lv1walk");
                    EventManager.Instance.TriggerEvent(CONST.StopAudio, "lv1run");
                }

                #endregion

                if (_playerController.isGrounded)
                {
                    if (Input.GetKeyDown(KeyCode.C))
                    {
                        GetComponent<CapsuleCollider>().height = GetComponent<CapsuleCollider>().height == 2 ? 1 : 2f;
                        _playerController.height = _playerController.height == 2 ? 1 : 2f;
                    }
                }
            }

            if (isUnLockRotationn)
            {
                RotationX += _playerCamera.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * _mouseSpeed;
                RotationY -= Input.GetAxis("Mouse Y") * _mouseSpeed;
                RotationY = Mathf.Clamp(RotationY, _minMouseY, _maxMouseY);
                this.transform.eulerAngles = new Vector3(0, RotationX, 0);
                _playerCamera.transform.eulerAngles = new Vector3(RotationY, RotationX, 0);
            }

        }

        #endregion


        #region 交互

        /// <summary>
        /// 判断指向哪个可交互对象
        /// </summary>
        private void HandleInteractionCheck()
        {
            // ViewportPointToRay 把你的电脑屏幕/摄像机看到的画面，看做一个坐标系，一个平面
            // 左下角为(0, 0)，右上角为(1, 1)
            // 从你眼睛里，也就是屏幕中心(0.5, 0.5)，发射一条射线
            // 距离是interactionDistance,用于判断是否注视，触发物体被注视事件的参数用Interactable组件中的_focusDistance
            Debug.DrawRay(_playerCamera.ViewportPointToRay(_interactionRayPoint).origin, _playerCamera.ViewportPointToRay(_interactionRayPoint).direction * _interactionDistance, Color.blue);
            if (Physics.Raycast(_playerCamera.ViewportPointToRay(_interactionRayPoint), out RaycastHit hit, _interactionDistance, _interactionLayer))
            {
                // 这里用collider，可交互的物品一般都是实体，或者说要求有实体
                // 射线击中可交互物品，且当前没有与其它物品交互，或者从一个可交互物品移动到另一个

                // if ((1 << hit.collider.gameObject.layer) == interactionLayer && (currentInteractable == null || currentInteractable.GetInstanceID() != hit.collider.gameObject.GetInstanceID()))
                if (_currentInteractable == null || _currentInteractable.GetInstanceID() != hit.collider.gameObject.GetInstanceID())
                {
                    // 不为空，那就是换物品了，先失焦
                    if (_currentInteractable != null)
                    {
                        _currentInteractable.OnLoseFocus();
                    }

                    // 获取Component<Interactable>， 给currentInteractable变量
                    hit.collider.TryGetComponent<Interactable>(out _currentInteractable);

                    // 如果存在<Interactable>组件
                    if (_currentInteractable && hit.distance <= _currentInteractable._focusDistance)
                    {
                        // 执行聚焦方法
                        _currentInteractable.OnFocus();
                    }
                }
            }
            // 如果没有击中可交互物品，但是仍绑定了某个可交互物品
            else if (_currentInteractable)
            {
                // 执行失焦方法
                _currentInteractable.OnLoseFocus();
                _currentInteractable = null;
            }
        }

        /// <summary>
        /// 判断交互操作
        /// </summary>
        private void HandleInteractionInput()
        {
            // 一、按下交互键
            // 二、存在可交互对象
            // 三、击中某个可交互物品
            // Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance, interactionLayer)
            if (Input.GetMouseButtonDown(0) && _currentInteractable != null && Physics.Raycast(_playerCamera.ViewportPointToRay(_interactionRayPoint), out RaycastHit hit, _currentInteractable._clickDistance, _interactionLayer))
            {
                _currentInteractable.OnInteract();
            }
        }

        #endregion

        #region public

        public void SetPlayerPosAndRotation(Vector3 pos, float anglesY = 0f, Tween CallBack = null)
        {
            isUnLockPosition = false;
            _playerController.enabled = false;
            transform.position = pos;
            RotationX += _playerCamera.transform.localEulerAngles.y + anglesY;
            transform.eulerAngles = new Vector3(0, RotationX, 0);
            _playerCamera.transform.eulerAngles = new Vector3(0, RotationX, 0);
            if (CallBack != null)
            {
                GetComponent<CapsuleCollider>().enabled = false;
                CallBack?.Play();
                CallBack.onComplete = () =>
                {
                    _playerController.enabled = true;
                    isUnLockPosition = true;
                    GetComponent<CapsuleCollider>().enabled = true;
                };
            }
            else
            {
                _playerController.enabled = true;
                isUnLockPosition = true;
            }
        }

        public void SetAnchorPos(Vector3 pos)
        {
            _anchorPos.position = pos;
        }

        public void SetUnLockPos(bool isUnLockPos)
        {
            isUnLockPosition = isUnLockPos;
        }

        public void SetUnLockRot(bool isUnLockRot)
        {
            isUnLockRotationn = isUnLockRot;
        }


        public void LockAll()
        {
            isUnLockPosition = false;
            isUnLockRotationn = false;
            Screen.lockCursor = false;
        }

        public void UnpackLockAll()
        {
            isUnLockPosition = true;
            isUnLockRotationn = true;
            Screen.lockCursor = true;
        }

        Tween floating;

        public void SetFloating(float duration, float speed)
        {
            floating = PlayerController.instance.transform.DOMoveY(PlayerController.instance.transform.position.y + duration, duration / speed);
            //itm.onlo = delegate () { PlayerController.instance.transform.DOMoveY(Bottom, Math.Abs(Bottom - PlayerController.instance.transform.position.y) / speed); };
            floating.SetEase(Ease.InOutFlash).SetLoops(-1, LoopType.Yoyo);
        }
        public void StopFloating()
        {
            floating.Kill();
        }

        #endregion

        void GamePause()
        {
            if (SceneManager.instance.isInPlayingScene && Input.GetKeyDown(KeyCode.Escape))
            {
                UIManager.instance.OpenUI(CONST.UI_PausePanel);
            }
        }
    }
}