namespace BitterCitrus.SRC.Entites;

using Godot;
using System;

// 플레이어에게 피해를 입힐 수 있는 공격 판정이 상속하는 추상 클래스.
// (액션이나 공격이 아닌, 공격 판정.)

// 아직 작성하지 않음

public abstract partial class HurtBox_Hostile : Area2D
{
    public bool TryClash(PlayerAttackType type)
    {
        return false;
    }
}
