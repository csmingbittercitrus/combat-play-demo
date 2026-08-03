namespace BitterCitrus.SRC.Core.BInput;

using Godot;
using System;

// 입력 맵 Action들의 이름을 미리 선언해두는 클래스

public static class InputActionNames
{
    // 이하 UI 관련 입력은 Godot 엔진 자체 Action을 사용
    public static readonly StringName UI_Accept = "ui_accept";
    public static readonly StringName UI_Cancel = "ui_cancel";

    public static readonly StringName UI_Up = "ui_up";
    public static readonly StringName UI_Down = "ui_down";
    public static readonly StringName UI_Left = "ui_left";
    public static readonly StringName UI_Right = "ui_right";

    // 이하 UI 관련 입력은 예외적으로 자체 Input Action을 사용
    public static readonly StringName UI_Tab_Left = "bittercitrus_ui_tab_left";
    public static readonly StringName UI_Tab_Right = "bittercitrus_ui_tab_right";


    // 이하 입력은 게임 내 입력으로, 자체 Action을 사용
    public static readonly StringName Up = "bittercitrus_up";
    public static readonly StringName Down = "bittercitrus_down";
    public static readonly StringName Left = "bittercitrus_left";
    public static readonly StringName Right = "bittercitrus_right";

    public static readonly StringName Camera_Up = "bittercitrus_camera_up";
    public static readonly StringName Camera_Down = "bittercitrus_camera_down";
    public static readonly StringName Camera_Left = "bittercitrus_camera_left";
    public static readonly StringName Camera_Right = "bittercitrus_camera_right";



    public static readonly StringName Pause = "bittercitrus_pause";

    public static readonly StringName Menu = "bittercitrus_menu";
    public static readonly StringName Menu_Map = "bittercitrus_menu_map";
    public static readonly StringName Menu_Equipment = "bittercitrus_menu_equipment";
    public static readonly StringName Menu_Inventory = "bittercitrus_menu_inventory";
    public static readonly StringName Menu_Quest = "bittercitrus_menu_quest";
    public static readonly StringName Menu_Dex = "bittercitrus_menu_dex";
    public static readonly StringName FastMap = "bittercitrus_fast_map";



    public static readonly StringName Jump = "bittercitrus_jump";
    public static readonly StringName Attack = "bittercitrus_attack";
    public static readonly StringName Smash = "bittercitrus_smash";
    public static readonly StringName Dash = "bittercitrus_dash";
    public static readonly StringName Parry = "bittercitrus_parry";
    public static readonly StringName Throw = "bittercitrus_throw";
    public static readonly StringName Potion = "bittercitrus_potion";
}
