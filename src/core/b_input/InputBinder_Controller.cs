namespace BitterCitrus.SRC.Core.BInput;

using static InputBinder_Utils;

using Godot;
using System;
using BitterCitrus.SRC.Core.BSettings;


// 컨트롤러 조작 체계의 InputMap을 관리하는 클래스.

// InputManager 초기화 시 호출됨.
// 컨트롤러 조작 체계의 InputMap 관련 설정 변경 시 호출됨.



public static class InputBinder_Controller
{
    public static void Init()
    {
        GD.Print("[InputBinder_Controller] Controller InputMap 초기화.");
        ClearInputMap();

        BindUncustomables();
        ApplyNintendoLayout();
        ApplyCustomInputMap();
    }

    private static void ClearInputMap()
    {
        foreach (StringName name in GetAllInputActions)
        {
           UnbindControllerInputFromAction(name);
        }
    }

    private static void BindUncustomables()
    {
        // 컨트롤러 특성상, 매핑을 허용하면 안 되는 부분을 매핑
        BindInputEventToAction(InputActionNames.UI_Up, JoyButton.DpadUp);
        BindInputEventToAction(InputActionNames.UI_Down, JoyButton.DpadDown);
        BindInputEventToAction(InputActionNames.UI_Left, JoyButton.DpadLeft);
        BindInputEventToAction(InputActionNames.UI_Right, JoyButton.DpadRight);

        BindInputEventToAction(InputActionNames.UI_Up, JoyAxis.LeftY, -1.0f);
        BindInputEventToAction(InputActionNames.UI_Down, JoyAxis.LeftY, 1.0f);
        BindInputEventToAction(InputActionNames.UI_Left, JoyAxis.LeftX, -1.0f);
        BindInputEventToAction(InputActionNames.UI_Right, JoyAxis.LeftX, 1.0f);

        BindInputEventToAction(InputActionNames.UI_Tab_Left, JoyButton.LeftShoulder);
        BindInputEventToAction(InputActionNames.UI_Tab_Right, JoyButton.RightShoulder);



        BindInputEventToAction(InputActionNames.Up, JoyButton.DpadUp);
        BindInputEventToAction(InputActionNames.Down, JoyButton.DpadDown);
        BindInputEventToAction(InputActionNames.Left, JoyButton.DpadLeft);
        BindInputEventToAction(InputActionNames.Right, JoyButton.DpadRight);

        BindInputEventToAction(InputActionNames.Up, JoyAxis.LeftY, -1.0f);
        BindInputEventToAction(InputActionNames.Down, JoyAxis.LeftY, 1.0f);
        BindInputEventToAction(InputActionNames.Left, JoyAxis.LeftX, -1.0f);
        BindInputEventToAction(InputActionNames.Right, JoyAxis.LeftX, 1.0f);

        BindInputEventToAction(InputActionNames.Camera_Up, JoyAxis.RightY, -1.0f);
        BindInputEventToAction(InputActionNames.Camera_Down, JoyAxis.RightY, 1.0f);
        BindInputEventToAction(InputActionNames.Camera_Left, JoyAxis.RightX, -1.0f);
        BindInputEventToAction(InputActionNames.Camera_Right, JoyAxis.RightX, 1.0f);

        BindInputEventToAction(InputActionNames.Pause, JoyButton.Start);
        BindInputEventToAction(InputActionNames.Menu, JoyButton.Back);
    }

    public static void ApplyNintendoLayout()
    {
        // 닌텐도 레이아웃 사용 여부 설정값에 따라 UI 확인/취소 액션에 올바른 입력 할당
        UnbindControllerInputFromAction(InputActionNames.UI_Accept);
        UnbindControllerInputFromAction(InputActionNames.UI_Cancel);

        if (SettingsManager.Instance.CurrentSettingsData.UseNintendoLayout)
        {
            BindInputEventToAction(InputActionNames.UI_Accept, JoyButton.B);
            BindInputEventToAction(InputActionNames.UI_Cancel, JoyButton.A);
        }
        else
        {
            BindInputEventToAction(InputActionNames.UI_Accept, JoyButton.A);
            BindInputEventToAction(InputActionNames.UI_Cancel, JoyButton.B);
        }
    }

    public static void ApplyCustomInputMap()
    {
        // 설정값이 없으면 하나 만들기
        if (!CheckIfInputMapIsValid())
        {
            GD.Print("[InputBinder_Controller] 초기 Controller InputMap값 가져옴.");
            SettingsManager.Instance.ControllerInputMap = GetNewControllerBindings();
        }

        foreach (System.Collections.Generic.KeyValuePair<StringName, InputData> pair in SettingsManager.Instance.ControllerInputMap)
        {
            UnbindKeyboardMouseInputFromAction(pair.Key);

            switch (pair.Value.Type)
            {
                case InputType.JoyButton:
                    BindInputEventToAction(pair.Key, (JoyButton)pair.Value.Id);
                    break;
                case InputType.JoyAxis:
                    BindInputEventToAction(pair.Key, (JoyAxis)pair.Value.Id);
                    break;
                default:
                    break;
            }
        }
    }

    private static bool CheckIfInputMapIsValid()
    {
        if (SettingsManager.Instance.ControllerInputMap == null)
        {
            GD.PrintErr("[InputBinder_Controller] Controller InputMap의 값이 null.");
            return false;
        }
        
        foreach (StringName actionName in GetAllControllerBindableActions)
        {
            if (!SettingsManager.Instance.ControllerInputMap.ContainsKey(actionName))
            {
                GD.PrintErr("[InputBinder_Controller] Controller InputMap 유효하지 않음.", actionName);
                return false;
            }
        }

        return true;
    }
}
