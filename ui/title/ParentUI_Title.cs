namespace CombatPlayDemo.UI;

using Godot;
using System;
using System.Threading.Tasks;

public partial class ParentUI_Title : ParentUI
{
    [Export] public UISelection StartGame { get; private set; }
    [Export] public UISelection Settings { get; private set; }
    [Export] public UISelection Quit { get; private set; }
}
