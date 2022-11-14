//using Assets.Scripts.GamePlay.UI;
//using System;
//using UnityEngine;
//public class GameControllerManager : AbstractManager
//{
//    // Fired when the pause button is pressed on a controller
//    public static event Action<GameController> controllerPauseButtonPressedEvent;

//    // Fired when a controller connects
//    public static event Action<GameController> controllerDidConnectEvent;

//    // Fired when a controller disconnects
//    public static event Action<int> controllerDidDisconnectEvent;


//    static GameControllerManager()
//    {
//        //AbstractManager.initialize( typeof( GameControllerManager ) );
//    }


//    public void controllerPauseButtonPressed(string json)
//    {
//        if (controllerPauseButtonPressedEvent != null)
//            controllerPauseButtonPressedEvent(Json.decode<GameController>(json));
//    }


//    public void controllerDidConnect(string json)
//    {
//        if (controllerDidConnectEvent != null)
//            controllerDidConnectEvent(Json.decode<GameController>(json));
//    }


//    public void controllerDidDisconnect(string param)
//    {
//        if (controllerDidDisconnectEvent != null)
//            controllerDidDisconnectEv ent(int.Parse(param));
//    }
//}

//public class InputManager : OsSingletonMono<GameManager>
//{
//    int _connectedControllerId = -1;
//    bool _isExtendedProfileController;

//    void Start()
//    {
//        // we need to listen for any device connections so that we know when we have a controller
//        GameControllerManager.controllerDidConnectEvent += controller =>
//        {
//            _isExtendedProfileController = controller.hasExtendedGamepadProfile;
//            _connectedControllerId = controller.internalControllerId;
//        };

//        GameControllerManager.controllerDidDisconnectEvent += controller =>
//        {
//            Logging.Debug("controller disconnected: " + controller);
//            _connectedControllerId = -1;
//        };

//        GameControllerManager.controllerPauseButtonPressedEvent += controller =>
//        {
//            Logging.Debug("controller pause button pressed: " + controller);
//        };

//        if (IsGameControllerAvailable())
//        {
//            StartWirlessDiscovery();
//        }
//    }

//    bool wasHoldingAny = false;
//    Touch touch;
//    private Vector3 _lastMovement = Vector3.zero;
//    private KeyCode _lastKeyCode = KeyCode.None;

//    private void Update()
//    {
//        if (!_enable)
//            return;

//        if (null == CameraController.Instance)
//        {
//            return;
//        }
//        KeyCode keyCode = KeyCode.None;
//        Vector3 up = new Vector3(0, 20f, 0);
//        Vector3 right = new Vector3(20f, 0, 0);

//        Vector3 dir = Vector3.zero;
//        bool pressed = false;
//        if (Input.GetKey(KeyCode.W))
//        {
//            dir += up;
//            pressed = true;
//            keyCode = KeyCode.W;

//        }
//        if (Input.GetKey(KeyCode.S))
//        {
//            dir += -up;
//            pressed = true;
//            keyCode = KeyCode.S;
//        }
//        if (Input.GetKey(KeyCode.A))
//        {
//            dir += -right;
//            pressed = true;
//            keyCode = KeyCode.A;
//        }
//        if (Input.GetKey(KeyCode.D))
//        {
//            dir += right;
//            pressed = true;
//            keyCode = KeyCode.D;
//        }

//        if (pressed)
//        {
//            _lastKeyCode = keyCode;
//            _lastMovement = dir;

//            SetCurEntityMoveDir(dir, false);

//            wasHoldingAny = true;
//        }
//        else if (_lastMovement != Vector3.zero)
//        {
//            ReleaseCurEntityMoveDir();
//            _lastMovement = Vector3.zero;
//            wasHoldingAny = false;
//        }
//    }
//}
