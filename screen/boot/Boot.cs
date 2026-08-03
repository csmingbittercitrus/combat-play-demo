namespace UI.Boot;

using BitterCitrus.SRC.Core.BInput;
using BitterCitrus.SRC.Core.BSettings;
using Godot;
using System;

// 게임 시작 지점의 최상위 노드

public partial class Boot : Control
{
    [Export] string TitleScreenPath { get; set; }

    [Export] Label Debug_1 { get; set; }

    private bool IsTitleScreenLoaded { get; set; } = false;
    public override void _Ready()
    {
        IsTitleScreenLoaded = false;
        LoadTitleScreen();
        Input.MouseMode = Input.MouseModeEnum.Hidden;
    }

    private async void LoadTitleScreen()
    {
        ResourceLoader.LoadThreadedRequest(TitleScreenPath);

        Debug_1.Text = "Loading";

        while (ResourceLoader.LoadThreadedGetStatus(TitleScreenPath) == ResourceLoader.ThreadLoadStatus.InProgress)
        {
            await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
        }

        ResourceLoader.ThreadLoadStatus status = ResourceLoader.LoadThreadedGetStatus(TitleScreenPath);

        switch (status)
        {
            case ResourceLoader.ThreadLoadStatus.Loaded:
                Debug_1.Text = "Done";
                IsTitleScreenLoaded = true;
                break;
            default:
                Debug_1.Text = "Failed";
                break;
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (!IsTitleScreenLoaded) return;

        if (@event.IsActionPressed(InputActionNames.UI_Accept))
        {
            PackedScene loadedScene = (PackedScene)ResourceLoader.LoadThreadedGet(TitleScreenPath);

            GetTree().ChangeSceneToPacked(loadedScene);
        }
    }

}
