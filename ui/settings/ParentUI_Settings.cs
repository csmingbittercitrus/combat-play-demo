namespace CombatPlayDemo.UI;

using BitterCitrus.SRC.Core.BInput;
using Godot;
using System;
using System.Collections.Generic;


public partial class ParentUI_Settings : ParentUI
{
    [Export] public UISelection Back { get; set; }

    List<SettingsTab> SettingsTabList { get; set; } 

    [Export] private SettingsTab Tab_Gameplay { get; set; }
    [Export] private SettingsTab Tab_Graphics { get; set; }
    [Export] private SettingsTab Tab_Sounds { get; set; }
    [Export] private SettingsTab Tab_Controls { get; set; }

    List<ParentUI> SettingsList { get; set; }

    [Export] private ParentUI Gameplay { get; set; }
    [Export] private ParentUI Graphics { get; set; }
    [Export] private ParentUI Sounds { get; set; }
    [Export] private ParentUI Controls { get; set; }

    private int CurrentActiveTabIndex { get; set; }

    public override void _Ready()
    {
        Init();
    }

    protected override void Init()
    {
        SettingsTabList = new();
        
        SettingsTabList.Add(Tab_Gameplay);
        SettingsTabList.Add(Tab_Graphics);
        SettingsTabList.Add(Tab_Sounds);
        SettingsTabList.Add(Tab_Controls);

        SettingsList = new();

        SettingsList.Add(Gameplay);
        SettingsList.Add(Graphics);
        SettingsList.Add(Sounds);
        SettingsList.Add(Controls);

        CurrentActiveTabIndex = 0;

        SettingsTabList[CurrentActiveTabIndex].ActivateNow();
        SettingsList[CurrentActiveTabIndex].ActivateNow();

        ConnectSignals();
    }

    private void ConnectSignals()
    {
        foreach(SettingsTab tab in SettingsTabList)
        {
            tab.Index = SettingsTabList.IndexOf(tab);
            tab.SettingsTabSelected += ChangeTab;
        }
    }

    public override void _ExitTree()
    {
        DisconnectSignals();
    }

    private void DisconnectSignals()
    {
        foreach(SettingsTab tab in SettingsTabList)
        {
            tab.SettingsTabSelected -= ChangeTab;
        }
    }


    public override void _UnhandledInput(InputEvent @event)
    {
        if (!IsActive) return;

        if (@event.IsActionPressed(InputActionNames.UI_Tab_Left))
        {
            if (CurrentActiveTabIndex - 1 < 0)
            {
                return;
            }

            ChangeTab(CurrentActiveTabIndex - 1);
        }
        else if (@event.IsActionPressed(InputActionNames.UI_Tab_Right))
        {
            if (CurrentActiveTabIndex + 1 > 3)
            {
                return;
            }

            ChangeTab(CurrentActiveTabIndex + 1);
        }
    }

    private void ChangeTab(int index)
    {
        if (CurrentActiveTabIndex == index) return;

        SettingsTabList[CurrentActiveTabIndex].DeactivateNow();
        SettingsList[CurrentActiveTabIndex].DeactivateNow();

        CurrentActiveTabIndex = index;

        SettingsTabList[CurrentActiveTabIndex].ActivateNow();
        SettingsList[CurrentActiveTabIndex].ActivateNow();
    }
    

}
