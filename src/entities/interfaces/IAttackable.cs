namespace BitterCitrus.SRC.Entites;

using Godot;
using System;

// Player를 제외한, 모든 공격받을 수 있는 객체가 상속하는 클래스.

// Player에게 피해를 로직은 별개의 체계를 사용한다.
// 공격 간 상쇄 시스템과는 별개의 체계를 사용한다.

public interface IAttackable
{
    // 플레이어 캐릭터의 공격이 호출하는 함수.
    public void HandlePlayerAttack(PlayerAttackType type);

    // 플레이어 캐릭터의 공격이 아닌, 적의 공격 또는 환경 요소 등이 호출하는 함수.
    public void HandleNonPlayerAttack();
}

