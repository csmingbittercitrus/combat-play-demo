namespace BitterCitrus.SRC.Core.BInput;

using Godot;
using System;

// 입력을 중앙 제어하는 싱글톤
// 컨트롤러 입력과 키보드 입력 중 하나만을 받도록 함

public partial class InputManager : Node
{
    public static InputManager Instance;


    // 현 InputMode에 따라 UI를 변경하기 위해, 각 UI가 구독하는 시그널
    [Signal]
    public delegate void InputModeChangedEventHandler();
    public InputDeviceType CurrentInputMode = InputDeviceType.Controller;





    public override void _EnterTree()
    {
        if (Instance == null)
        {
            Instance = this;
            this.ProcessMode = ProcessModeEnum.Always;
        }
    }

    public override void _Ready()
    {
        Init();
    }

    private void Init()
    {
        GD.Print("[InputManager] 초기화.");

        InitInputMode();

        InputBinder_Keyboard.Init();
        InputBinder_Controller.Init();
    }

    private void InitInputMode()
    {
        if (Input.GetConnectedJoypads().Count > 0)
        {
            CurrentInputMode = InputDeviceType.Controller;
        }
        else
        {
            CurrentInputMode = InputDeviceType.Controller;
        }
    }




    public override void _Input(InputEvent @event)
    {
        TryToChangeInputMode(@event);
    }

    private void TryToChangeInputMode(InputEvent @event)
    {
        InputDeviceType targetInputMode;

        switch(@event)
        {
            case InputEventJoypadButton:
            case InputEventJoypadMotion motion when Math.Abs(motion.AxisValue) > 0.2f:
                targetInputMode = InputDeviceType.Controller;
                break;
            
            case InputEventKey:
                targetInputMode = InputDeviceType.Keyboard;
                break;
            
            case InputEventMouseButton:
            case InputEventMouseMotion motion when motion.Velocity.LengthSquared() > 2000:
                targetInputMode = InputDeviceType.Keyboard;
                break;
            
            default:
                targetInputMode = CurrentInputMode;
                break;
        }

        if (CurrentInputMode != targetInputMode)
        {
            CurrentInputMode = targetInputMode;
            EmitSignalInputModeChanged();
            GetViewport().SetInputAsHandled();
        }
    }
}

public enum InputDeviceType
{
    Controller = 0,
    Keyboard = 1,
    Steam = 2
}
