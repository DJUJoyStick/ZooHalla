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

public enum PLAYERTYPE
{
    RAT,
    TURTLE,
    WOLF
}

public enum MELEEWEAPON             // ��������
{
    TOOTHPICK,                      // �̾��ð� (normal)
    CLIP,                           // Ŭ��?
    STONESPEAR,                     // ��â
    VINEWHIP,                       // ����ä��
    WOODSHIELD,                     // ��������
    SPIRALSWORD,                    // ������ (rare)
    SWORDSHIELD,                    // �ҵ彯��
    KOLA,                           // kola
    RAPIER,                         // �����Ǿ�
    FORK,                           // ��ũ (unique)
    SEALEDKEY,                      // ���ε� ����
    KEYBLADE,                       // Ű���̵� (legend)
    GAUNTLET,                       // ???��Ʋ�� (???)
    SMALLKEY                        // ???��������
}

public enum RANGEDWEAPON            // ���Ÿ�����
{
    TESTGUN,                        // �׽�Ʈ (normal)
    WOODSLINGSHOT,                  // �������� (normal)
    WOODBOW,                        // ���� Ȱ
    STING,                          // ��ħ
    BOOMERANG,                      // �θ޶�
    SEEDING,                        // ���Ѹ���
    FIREBIRD,                       // �һ� (rare)
    FIRELOCK,                       // ȭ����
    CHAKRAM,                        // ��ũ��
    RAILGUN,                        // ���ϰ�
    SHILEDPISTOL,                   // �ǵ��Ǽ��� (unique)
    GENTLEMANUMBRELLA,              // �Ż���
    THREESIXNINE,                   // 369
    FLAXGUN,                        // Flax Gun
    ENDOFTHECENTURYGAUNTLET         // ���⸻ ��Ʋ�� (legend)
}

public enum THROWWEAPON             // ��ô����
{
    PINECONEBOMB,                   // �ֹ�� ��ź (normal)
    SEEDBOMB,                       // �� ��ź
    TOYROBOT,                       // �峭�� �κ� (rare)
    PLASMABOMB,                     // �ö�� ��ź (unique)
}

public enum TURRET                  // ��ž
{
    CATAPULT,                       // ������ (normal)
    SUNFLOWER,                      // �عٶ��
    TV,                             // Tv (rare)
    DOLHAREUBANG,                   // ���ϸ���
    MINIGUNTURRET                   // �̴ϰ���ž (unique)
}