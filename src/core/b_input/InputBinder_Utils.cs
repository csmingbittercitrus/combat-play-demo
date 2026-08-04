namespace BitterCitrus.SRC.Core.BInput;

using Godot;
using Godot.Collections;
using System;
using BitterCitrus.SRC.Core.BSettings;


public static class InputBinder_Utils
{
    // 헬퍼 함수들, 초기화 시 값을 불러오는 등을 위한 유틸 클래스
    public static ReadOnlySpan<StringName> GetAllInputActions => new StringName[]
    {
        InputActionNames.UI_Accept,
        InputActionNames.UI_Cancel,

        InputActionNames.UI_Up,
        InputActionNames.UI_Down,
        InputActionNames.UI_Left,
        InputActionNames.UI_Right,

        InputActionNames.UI_Tab_Left,
        InputActionNames.UI_Tab_Right,

        InputActionNames.Up,
        InputActionNames.Down,
        InputActionNames.Left,
        InputActionNames.Right,

        InputActionNames.Camera_Up,
        InputActionNames.Camera_Down,
        InputActionNames.Camera_Left,
        InputActionNames.Camera_Right,

        InputActionNames.Pause,

        InputActionNames.Menu,
        InputActionNames.Menu_Map,
        InputActionNames.Menu_Equipment,
        InputActionNames.Menu_Inventory,
        InputActionNames.Menu_Quest,
        InputActionNames.Menu_Dex,

        InputActionNames.FastMap,

        InputActionNames.Jump,
        InputActionNames.Attack,
        InputActionNames.Smash,
        InputActionNames.Dash,
        InputActionNames.Parry,
        InputActionNames.Throw,
        InputActionNames.Potion,
    };

    public static ReadOnlySpan<StringName> GetAllControllerBindableActions => new StringName[]
    {
        // 컨트롤러 조작 체계에서 플레이어가 임의대로 바인딩 할 수 있는 액션들의 목록.
        // 설정 값 딕셔너리가 유효한지 검사하기 위해 호출됨.
        InputActionNames.FastMap,

        InputActionNames.Jump,
        InputActionNames.Attack,
        InputActionNames.Smash,
        InputActionNames.Dash,
        InputActionNames.Parry,
        InputActionNames.Throw,
        InputActionNames.Potion,
    };
    
    public static System.Collections.Generic.Dictionary<StringName, InputData> GetNewControllerBindings() => new System.Collections.Generic.Dictionary<StringName, InputData>
    {
        // 컨트롤러 조작 체계의 기본 바인딩 값.
        // 설정 값을 초기화할 때 호출됨.
        { InputActionNames.Jump, new InputData(JoyButton.A) },
        { InputActionNames.Smash, new InputData(JoyButton.B)},
        { InputActionNames.Attack, new InputData(JoyButton.X)},
        { InputActionNames.FastMap, new InputData(JoyButton.Y)},

        { InputActionNames.Parry, new InputData(JoyButton.LeftShoulder)},
        { InputActionNames.Potion, new InputData(JoyButton.RightShoulder)},

        { InputActionNames.Throw, new InputData(JoyAxis.TriggerLeft)},
        { InputActionNames.Dash, new InputData(JoyAxis.TriggerRight)},
    };

    public static ReadOnlySpan<StringName> GetAllKeyboardBindableActions => new StringName[]
    {
        // 키보드 조작 체계에서 플레이어가 임의대로 바인딩 할 수 있는 액션들의 목록.
        // 설정 값 딕셔너리가 유효한지 검사하기 위해 호출함.
        InputActionNames.UI_Tab_Left,
        InputActionNames.UI_Tab_Right,

        InputActionNames.Up,
        InputActionNames.Down,
        InputActionNames.Left,
        InputActionNames.Right,

        InputActionNames.Camera_Up,
        InputActionNames.Camera_Down,
        InputActionNames.Camera_Left,
        InputActionNames.Camera_Right,

        InputActionNames.Menu,
        InputActionNames.Menu_Map,
        InputActionNames.Menu_Equipment,
        InputActionNames.Menu_Inventory,
        InputActionNames.Menu_Quest,
        InputActionNames.Menu_Dex,

        InputActionNames.FastMap,

        InputActionNames.Jump,
        InputActionNames.Attack,
        InputActionNames.Smash,
        InputActionNames.Dash,
        InputActionNames.Parry,
        InputActionNames.Throw,
        InputActionNames.Potion,
    };


    public static System.Collections.Generic.Dictionary<StringName, Key> GetNewKeyboardBindings() => new System.Collections.Generic.Dictionary<StringName, Key>
    {
        // 키보드 조작 체계의 기본 바인딩 값.
        // 설정 값을 초기화할 때 호출
        { InputActionNames.UI_Tab_Left, Key.U},
        { InputActionNames.UI_Tab_Right, Key.O},

        { InputActionNames.Up, Key.W},
        { InputActionNames.Down, Key.S},
        { InputActionNames.Left, Key.A},
        { InputActionNames.Right, Key.D},

        { InputActionNames.Camera_Up, Key.Up},
        { InputActionNames.Camera_Down, Key.Down},
        { InputActionNames.Camera_Left, Key.Left},
        { InputActionNames.Camera_Right, Key.Right},

        { InputActionNames.Jump, Key.Space},
        { InputActionNames.Dash, Key.K},
        { InputActionNames.Attack, Key.J},
        { InputActionNames.Smash, Key.U},
        { InputActionNames.Parry, Key.L},
        { InputActionNames.Throw, Key.O},
        { InputActionNames.Potion, Key.I},

        { InputActionNames.Menu, Key.Quoteleft},
        { InputActionNames.Menu_Equipment, Key.Key1},
        { InputActionNames.Menu_Inventory, Key.Key2},
        { InputActionNames.Menu_Map, Key.Key3},
        { InputActionNames.Menu_Quest, Key.Key4},
        { InputActionNames.Menu_Dex, Key.Key5},

        { InputActionNames.FastMap, Key.Tab},
    };


    public static void UnbindControllerInputFromAction(StringName actionName)
    {
        // 특정 action에 할당된 컨트롤러 입력 이벤트를 전부 제거하는 헬퍼 함수
        Array<InputEvent> events = InputMap.ActionGetEvents(actionName);

        foreach (InputEvent @event in events)
        {
            switch (@event)
            {
                case InputEventJoypadButton:
                case InputEventJoypadMotion:
                    InputMap.ActionEraseEvent(actionName, @event);
                    break;
            }
        }
    }

    public static void UnbindKeyboardMouseInputFromAction(StringName actionName)
    {
        // 특정 action에 할당된 컨트롤러 입력 이벤트를 전부 제거하는 헬퍼 함수
        Array<InputEvent> events = InputMap.ActionGetEvents(actionName);

        foreach (InputEvent @event in events)
        {
            switch (@event)
            {
                case InputEventMouseButton:
                case InputEventKey:
                    InputMap.ActionEraseEvent(actionName, @event);
                    break;
            }
        }
    }


    // 이하, 특정 action에 입력 이벤트를 할당하는 헬퍼 함수 및 그 오버로드
    public static void BindInputEventToAction(StringName actionName, JoyButton joyButton)
    {
        InputEventJoypadButton @event = new();
        @event.ButtonIndex = joyButton;

        InputMap.ActionAddEvent(actionName, @event);
    }

    public static void BindInputEventToAction(StringName actionName, JoyAxis axis)
    {
        InputEventJoypadMotion @event = new();
        @event.Axis = axis;
        @event.AxisValue = 1.0f;

        InputMap.ActionAddEvent(actionName, @event);
    }

    public static void BindInputEventToAction(StringName actionName, JoyAxis axis, float axisValue)
    {
        InputEventJoypadMotion @event = new();
        @event.Axis = axis;
        @event.AxisValue = axisValue;

        InputMap.ActionAddEvent(actionName, @event);
    }

    public static void BindInputEventToAction(StringName actionName, Key key)
    {
        InputEventKey @event = new();
        @event.Keycode = key;

        InputMap.ActionAddEvent(actionName, @event);
    }

    public static void BindInputEventToAction(StringName actionName, MouseButton mouse)
    {
        InputEventMouseButton @event = new();
        @event.ButtonIndex = mouse;

        InputMap.ActionAddEvent(actionName, @event);
    }
}
