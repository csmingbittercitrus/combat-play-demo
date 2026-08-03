namespace Screen.Title;

using Godot;
using System;
using System.Threading.Tasks;
using CombatPlayDemo.UI;


// Title 화면의 최상위 노드, Screen.

// 하위 노드의 FocusMode, MouseMode 등을 관리함.





public partial class TitleScreen : Control
{
    // FocusMode로 전환 시 Focus를 갖게 될 노드.
    [Export] private ParentUI InitialTopLevelUI { get; set; }

    [Export] private ParentUI_Title TitleUI { get; set; }
    [Export] private ParentUI_SaveFiles SaveFilesUI { get; set; }
    [Export] private ParentUI_Settings SettingsUI { get; set; }

    [Export] private string PlayTest1Path { get; set; }
    public ParentUI ActiveTopLevelUI { get; set; }

    private bool IsMouseActive;

    public override async void _Ready()
    {
        await init();
    }

    private async Task init()
    {
        IsMouseActive = false;

        ActiveTopLevelUI = InitialTopLevelUI;
        await ActiveTopLevelUI.Activate();

        SwitchToFocusMode();
        ConnectSignal();
    }

    #region manageMouseMode

    public override void _Input(InputEvent @event)
    {
        TryToChangeInputMode(@event);
    }

    private void TryToChangeInputMode(InputEvent @event)
    {
        switch (@event)
        {
            case InputEventMouseButton button when !button.IsEcho() && button.IsPressed():
                if (IsMouseActive) return;
                else
                {
                    GetViewport().SetInputAsHandled();
                    SwitchToMouseMode();
                    return;
                }
            case InputEventMouseMotion motion when motion.Velocity.LengthSquared() > 2000:
                if (IsMouseActive) return;
                else
                {
                    SwitchToMouseMode();
                    return;
                }
            case InputEventKey key when !key.IsEcho() && key.IsPressed():
            case InputEventJoypadButton button when !button.IsEcho() && button.IsPressed():
            case InputEventJoypadMotion motion when Math.Abs(motion.AxisValue) > 0.2f:
                if (!IsMouseActive) return;
                else
                {
                    GetViewport().SetInputAsHandled();
                    SwitchToFocusMode();
                    return;
                }
            default:
                return;
        }
    }

    private void SwitchToFocusMode()
    {
        IsMouseActive = false;

        this.FocusBehaviorRecursive = FocusBehaviorRecursiveEnum.Enabled;
        this.MouseBehaviorRecursive = MouseBehaviorRecursiveEnum.Disabled;

        Input.MouseMode = Input.MouseModeEnum.Hidden;

        GetViewport().GuiGetFocusOwner()?.ReleaseFocus();
        ActiveTopLevelUI.TryToGiveFocus();
    }

    private void SwitchToMouseMode()
    {
        IsMouseActive = true;

        this.FocusBehaviorRecursive = FocusBehaviorRecursiveEnum.Disabled;
        this.MouseBehaviorRecursive = MouseBehaviorRecursiveEnum.Enabled;

        Input.MouseMode = Input.MouseModeEnum.Visible;

        GetViewport().GuiGetFocusOwner()?.ReleaseFocus();
    }

    #endregion

    private void ConnectSignal()
    {
        TitleUI.StartGame.UISelectionSelected += SwitchUI_TitleToSaveFiles;
        TitleUI.Settings.UISelectionSelected += SwitchUI_TitleToSettings;
        TitleUI.Quit.UISelectionSelected += QuitGame;

        SaveFilesUI.PlayTest1.UISelectionSelected += SwitchSceneToGamePlayTest1;
        SaveFilesUI.Back.UISelectionSelected += SwitchUI_SaveFilesToTitle;

        SettingsUI.Back.UISelectionSelected += SwitchUI_SettingsToTitle;
    }

    private void DisconnectSignal()
    {
        TitleUI.StartGame.UISelectionSelected -= SwitchUI_TitleToSaveFiles;
        TitleUI.Settings.UISelectionSelected -= SwitchUI_TitleToSettings;
        TitleUI.Quit.UISelectionSelected -= QuitGame;

        SaveFilesUI.PlayTest1.UISelectionSelected -= SwitchSceneToGamePlayTest1;
        SaveFilesUI.Back.UISelectionSelected -= SwitchUI_SaveFilesToTitle;

        SettingsUI.Back.UISelectionSelected -= SwitchUI_SettingsToTitle;
    }

    private async void SwitchUI_TitleToSaveFiles()
    {
        if (ActiveTopLevelUI != TitleUI)
        {
            GD.Print("[TitleScreen] 잘못된 변경 시도");
        }
        await ActiveTopLevelUI.Deactivate();
        ActiveTopLevelUI = SaveFilesUI;
        await ActiveTopLevelUI.Activate();
    }

    private async void SwitchUI_TitleToSettings()
    {
        if (ActiveTopLevelUI != TitleUI)
        {
            GD.Print("[TitleScreen] 잘못된 변경 시도");
        }
        await ActiveTopLevelUI.Deactivate();
        ActiveTopLevelUI = SettingsUI;
        await ActiveTopLevelUI.Activate();
    }

    private void QuitGame()
    {
        GetTree().Quit();
    }

    private async void SwitchUI_SaveFilesToTitle()
    {
        if (ActiveTopLevelUI != SaveFilesUI)
        {
            GD.Print("[TitleScreen] 잘못된 변경 시도");
        }
        await ActiveTopLevelUI.Deactivate();
        ActiveTopLevelUI = TitleUI;
        await ActiveTopLevelUI.Activate();
    }

    private async void SwitchUI_SettingsToTitle()
    {
        if (ActiveTopLevelUI != SettingsUI)
        {
            GD.Print("[TitleScreen] 잘못된 변경 시도");
        }
        await ActiveTopLevelUI.Deactivate();
        ActiveTopLevelUI = TitleUI;
        await ActiveTopLevelUI.Activate();
    }

    private void SwitchSceneToGamePlayTest1()
    {
        PackedScene scene = (PackedScene)ResourceLoader.Load<PackedScene>(PlayTest1Path);
        GetTree().ChangeSceneToPacked(scene);
    }

}
