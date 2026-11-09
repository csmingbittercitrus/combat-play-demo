namespace BitterCitrus.SRC.Entites;

using Game;
using Godot;
using System;

// 모든 공격받을 수 있는 객체가 상속해야 하는 인터페이스.

// 플레이어 캐릭터 역시 자신의 일부 공격에 피해를 받을 수 있기에 해당 인터페이스를 상속해야 한다.
// 피해를 계산하지 않더라도 해당 인터페이스를 상속해야 한다. ex) 때리면 소리나는 단순한 벽

public interface IAttackable
{
    // 플레이어 캐릭터의 공격이 호출하는 함수.
    public void HandlePlayerAttack(PlayerAttackType type);

    // 플레이어 캐릭터의 공격을 제외한, 데미지 요소가 호출하는 함수.
    public void HandleDamage(DamageType type);
}

public enum PlayerAttackType
{
    Melee,
    Smash,
    Parry,
    Magic,
}

public enum DamageType
{
    Normal,
    Strong,
    Spike,
}