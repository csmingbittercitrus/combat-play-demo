namespace BitterCitrus.SRC.Core.BSettings;

using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;

// 설정값들을 json 파일의 형태로 읽고 쓰는 레퍼지토리 클래스

// SettingsData 클래스 형태의 설정값을 settings_data.json 파일에 읽고 씀
// 사용자 지정 KeyboardInputMap을 keyboard_input_map.json 파일에 읽고 씀
// 사용자 지정 ControllerInputMap을 controller_input_map.json 파일에 읽고 씀

// 에디터에서 편하게 파일 확인 : 프로젝트 - 사용자 데이터 폴더 열기

public static class SettingsDataRepository
{
    #region Vars
    private const string SettingsDataPath = "user://settings_data.json";
    private const string KeyboardInputMapPath = "user://keyboard_input_map.json";
    private const string ControllerInputMapPath = "user://controller_input_map.json";

    private static readonly JsonSerializerOptions JsonOption = new JsonSerializerOptions
    {
        // 보기 편한 형태로 직렬화
        WriteIndented = true,
        //StringName <-> string 자동으로 변환해주는 컨버터
        Converters = { new JsonStringNameConverter() }
    };
    #endregion



    #region Funcs
    public static void SaveSettingsData()
    {
        SaveToFile<SettingsData>(SettingsDataPath, SettingsManager.Instance.CurrentSettingsData);
    }

    public static SettingsData LoadSettingsData()
    {
        return LoadFromFile<SettingsData>(SettingsDataPath);
    }

    public static void SaveKeyboardInputMap()
    {
        SaveToFile<Dictionary<StringName, Key>>(KeyboardInputMapPath, SettingsManager.Instance.KeyboardInputMap);
    }

    public static Dictionary<StringName, Key> LoadKeyboardInputMap()
    {
        return LoadFromFile<Dictionary<StringName, Key>>(KeyboardInputMapPath);
    }

    public static void SaveControllerInputMap()
    {
        SaveToFile<Dictionary<StringName, InputData>>(ControllerInputMapPath, SettingsManager.Instance.ControllerInputMap);
    }


    public static Dictionary<StringName, InputData> LoadControllerInputMap()
    {
        return LoadFromFile<Dictionary<StringName, InputData>>(ControllerInputMapPath);
    }
    #endregion



    #region Util
    private static bool SaveToFile<T>(string path, T data)
    {
        // T data를 직렬화하여 json 형식으로 path에 저장하는 유틸 함수

        string jsonString = JsonSerializer.Serialize(data, JsonOption); 
        using var file = FileAccess.Open(path, FileAccess.ModeFlags.Write);

        try
        {
            if (file == null)
            {
                GD.PrintErr($"[SettingsDataRepository] {path} : 파일 존재하지 않음.", FileAccess.GetOpenError());
                return false;
            }
            else
            {
                file.StoreString(jsonString);
                GD.Print($"[SettingsDataRepository] {path} : 파일 저장 성공.");
                return true;
            }
        }
        catch (JsonException ex)
        {
            GD.PrintErr($"[SettingsDataRepository] {path} : Json 파일 저장 중 오류 발생.", ex);
            return false;
        }
        catch (Exception ex)
        {
            GD.PrintErr($"[SettingsDataRepository] {path} : 알 수 없는 오류 발생.", ex);
            return false;
        }
    }

    private static T LoadFromFile<T> (string path) where T : class
    {
        // path의 json 파일을 읽어 T 형식의 데이터로 역직렬화하여 return하는 유틸 함수

        if (!FileAccess.FileExists(path))
        {
            GD.Print($"[SettingsDataRepository] {path} : 파일을 찾을 수 없음.");
            return null;
        }

        using var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);

        if (file == null)
        {
            GD.PrintErr($"[SettingsDataRepository] {path} : 파일이 존재하지만 비어 있음.");
            return null;
        }

        string jsonString = file.GetAsText();

        try
        {
            T loadedData = JsonSerializer.Deserialize<T>(jsonString, JsonOption);

            if (loadedData == null)
            {
                GD.PrintErr($"[SettingsDataRepository] {path} : 역직렬화 결과가 null.");
                return null;
            }

            GD.Print($"[SettingsDataRepository] {path} : 불러오기 성공.");
            return loadedData;
        }
        catch (JsonException ex)
        {
            GD.PrintErr($"[SettingsDataRepository] {path} : Json 파일 로드 중 오류 발생.", ex);
            return null;
        }
        catch (Exception ex)
        {
            GD.PrintErr($"[SettingsDataRepository] {path} : Json 파일 로드 중 알 수 없는 오류 발생.", ex);
            return null;
        }
    }
    #endregion
}
