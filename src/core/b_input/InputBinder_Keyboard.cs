namespace BitterCitrus.SRC.Core.BInput;

using static InputBinder_Utils;

using Godot;
using System;
using BitterCitrus.SRC.Core.BSettings;


public static class InputBinder_Keyboard
{
    public static void Init()
    {
        GD.Print("[InputBinder_Keyboard] Keyboard InputMap 초기화.");
        ClearInputMap();

        BindUncustomables();
        ApplyCustomInputMap();
    }

    private static void ClearInputMap()
    {
        foreach (StringName name in InputBinder_Utils.GetAllInputActions)
        {
           UnbindKeyboardMouseInputFromAction(name);
        }
    }

    private static void BindUncustomables()
    {
        // 키보드와 마우스 조작 체계에서 관성적으로 사용되는 버튼들을 우선적으로 배정
        BindInputEventToAction(InputActionNames.Pause, Key.Escape);

        BindInputEventToAction(InputActionNames.UI_Cancel, Key.Escape);

        BindInputEventToAction(InputActionNames.UI_Accept, Key.Enter);
        BindInputEventToAction(InputActionNames.UI_Accept, Key.KpEnter);
        BindInputEventToAction(InputActionNames.UI_Accept, MouseButton.Left);
    }

    public static void ApplyCustomInputMap()
    {
        // 설정값이 없으면 하나 만들기
        if (!CheckIfInputMapIsValid())
        {
            GD.Print("[InputBinder_Keyboard] 초기 Keyboard InputMap값 가져옴.");
            SettingsManager.Instance.KeyboardInputMap = GetNewKeyboardBindings();
        }

        foreach (System.Collections.Generic.KeyValuePair<StringName, Key> pair in SettingsManager.Instance.KeyboardInputMap)
        {
            UnbindKeyboardMouseInputFromAction(pair.Key);

            BindInputEventToAction(pair.Key, pair.Value);
        }

        ApplyUIInputMapByCustomInputMap();
    }

    private static void ApplyUIInputMapByCustomInputMap()
    {
        // 커스텀에 따라 UI 관련 인풋 맵 생성
        BindInputEventToAction(InputActionNames.UI_Up, SettingsManager.Instance.KeyboardInputMap[InputActionNames.Up]);
        BindInputEventToAction(InputActionNames.UI_Down, SettingsManager.Instance.KeyboardInputMap[InputActionNames.Down]);
        BindInputEventToAction(InputActionNames.UI_Left, SettingsManager.Instance.KeyboardInputMap[InputActionNames.Left]);
        BindInputEventToAction(InputActionNames.UI_Right, SettingsManager.Instance.KeyboardInputMap[InputActionNames.Right]);

        BindInputEventToAction(InputActionNames.UI_Up, SettingsManager.Instance.KeyboardInputMap[InputActionNames.Camera_Up]);
        BindInputEventToAction(InputActionNames.UI_Down, SettingsManager.Instance.KeyboardInputMap[InputActionNames.Camera_Down]);
        BindInputEventToAction(InputActionNames.UI_Left, SettingsManager.Instance.KeyboardInputMap[InputActionNames.Camera_Left]);
        BindInputEventToAction(InputActionNames.UI_Right, SettingsManager.Instance.KeyboardInputMap[InputActionNames.Camera_Right]);

        BindInputEventToAction(InputActionNames.UI_Accept, SettingsManager.Instance.KeyboardInputMap[InputActionNames.Jump]);
        BindInputEventToAction(InputActionNames.UI_Accept, SettingsManager.Instance.KeyboardInputMap[InputActionNames.Attack]);
    }



    private static bool CheckIfInputMapIsValid()
    {
        if (SettingsManager.Instance.KeyboardInputMap == null)
        {
            GD.PrintErr("[InputBinder_Keyboard] Keyboard InputMap의 값이 null.");
            return false;
        }
        
        foreach (StringName actionName in GetAllKeyboardBindableActions)
        {
            if (!SettingsManager.Instance.KeyboardInputMap.ContainsKey(actionName))
            {
                GD.PrintErr("[InputBinder_Keyboard] Keyboard InputMap 유효하지 않음.", actionName);
                return false;
            }
        }

        return true;
    }
}
