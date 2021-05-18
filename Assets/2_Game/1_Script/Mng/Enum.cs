using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DOORPOS
{
    TOP = 0,
    DOWN,
    LEFT,
    RIGHT
}

//public enum PLAYERDIRECT
//{
//    DONTMOVE,
//    LEFT,
//    RIGHT,
//    UP,
//    DOWN
//}

public enum WEAPONRATING
{
    NORMAL,
    RARE,
    UNIQUE,
    LEGEND,
    UNKNOWN
}

public enum PLAYERTYPE
{
    RAT,
    TURTLE,
    WOLF
}

public enum WEAPONTYPE
{
    MELEE_WEAPON,
    RANGED_WEAPON,
    THROW_WEAPON,
    TURRET
}

public enum MELEEWEAPON             // ��������
{
    TOOTH_PICK,                     // �̾��ð� (normal)
    CLIP,                           // Ŭ��?
    STONE_SPEAR,                    // ��â
    VINE_WHIP,                      // ����ä��
    WOOD_SHIELD,                    // ��������
    SPIRAL_SWORD,                   // ������ (rare)
    SWORD_SHIELD,                   // �ҵ彯��
    KOLA,                           // kola
    RAPIER,                         // �����Ǿ�
    FORK,                           // ��ũ (unique)
    SEALED_KEY,                     // ���ε� ����
    MASTER_KEY,                     // ������Ű (legend)
    GAUNTLET,                       // ???��Ʋ�� (???)
    SMALL_KEY                       // ???��������
}

public enum RANGEDWEAPON            // ���Ÿ�����
{
    TEST_GUN,                       // �׽�Ʈ (normal)
    WOOD_SLINGSHOT,                 // �������� (normal)
    WOOD_BOW,                       // ���� Ȱ
    STING,                          // ��ħ
    BOOMERANG,                      // �θ޶�
    SEEDING,                        // ���Ѹ���
    FIRE_BIRD,                      // �һ� (rare)
    FIRE_LOCK,                      // ȭ����
    CHAKRAM,                        // ��ũ��
    RAILGUN,                        // ���ϰ�
    SHILED_PISTOL,                  // �ǵ��Ǽ��� (unique)
    GENTLEMAN_UMBRELLA,             // �Ż���
    THREE_SIX_NINE,                 // 369
    FLAXGUN,                        // Flax Gun
    END_OF_THE_CENTURY_GAUNTLET     // ���⸻ ��Ʋ�� (legend)
}

public enum THROWWEAPON             // ��ô����
{
    PINECONE_BOMB,                  // �ֹ�� ��ź (normal)
    SEED_BOMB,                      // �� ��ź
    TOY_ROBOT,                      // �峭�� �κ� (rare)
    PLASMA_BOMB,                    // �ö�� ��ź (unique)
}

public enum TURRET                  // ��ž
{
    CATAPULT,                       // ������ (normal)
    SUN_FLOWER,                     // �عٶ��
    TV,                             // Tv (rare)
    DOL_HAREU_BANG,                 // ���ϸ���
    MINIGUN_TURRET                  // �̴ϰ���ž (unique)
}