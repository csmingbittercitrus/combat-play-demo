namespace CombatPlayDemo.UI;

using BitterCitrus.SRC.Core.BSettings;
using Godot;
using System;
using System.Collections.Generic;


public partial class LanguageOptionButton : OptionButton
{
    private List<string> Languages { get; set; } = new List<string>
    {
        "English",
        "한국어"
    };
    
    private List<string> LocaleKeys { get; set; } = new List<string>
    {
        "en",
        "kr"
    };


    public override void _Ready()
    {
        AddItems();

        Selected = LocaleKeys.IndexOf(SettingsManager.Instance.CurrentSettingsData.LanguageLocaleCode);
    }

    private void AddItems()
    {
        foreach(string lang in Languages)
        {
            AddItem(lang, Languages.IndexOf(lang));
        }
    }

    private void _on_item_selected(int index)
    {
        SettingsManager.Instance.ApplyLanguage(LocaleKeys[index]);
    }
}
