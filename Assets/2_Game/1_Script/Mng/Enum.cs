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
public enum PLAYERTYPE
{
    RAT,
    TURTLE,
    WOLF
}

public enum WEAPONRATING
{
    NORMAL,
    RARE,
    UNIQUE,
    LEGEND,
    UNKNOWN
}

public enum WEAPONTYPE
{
    MELEE_WEAPON,
    RANGED_WEAPON,
    TURRET
}

public enum MELEEWEAPON             // ��������
{
    TOOTH_PICK = 0,                 // �̾��ð� (normal)
    CLUB,                           // Ŭ��
    STONE_SPEAR,                    // ��â
    WOOD_SHIELD,                    // ��������
    SPIRAL_SWORD,                   // ������ (rare)
    RIGHT_SWORD_LEFT_SHILED,        // �����տ� �� �޼տ� ����
    KOLA,                           // �ݶ�
    RAPIER,                         // �����Ǿ�
    FORK,                           // ��ũ (unique)
    SEALED_KEY,                     // ���ε� ����
    MASTER_KEY,                     // ������Ű (legend)
    GAUNTLET,                       // ???��Ʋ�� (???)
    SMALL_KEY                       // ???��������
}

public enum RANGEDWEAPON            // ���Ÿ�����
{
    TEST_GUN = 0,                   // �׽�Ʈ (normal)
    WOOD_SLINGSHOT,                 // �������� (normal)
    WOOD_BOW,                       // ���� Ȱ
    STING,                          // ��ħ
    FIRE_BIRD,                      // �һ� (rare)
    FIRE_LOCK,                      // ȭ����
    CHAKRAM,                        // ��ũ��
    NAILGUN,                        // ���ϰ�
    SHILED_PISTOL,                  // �ǵ��ǽ��� (unique)
    GENTLEMAN_UMBRELLA,             // �Ż���
    THREE_SIX_NINE,                 // 369
    FLAX_GUN,                       // Flax Gun
    END_OF_THE_CENTURY_GAUNTLET     // ���⸻ ��Ʋ�� (legend)
}

public enum TURRET                  // ��ž
{
    CATAPULT,                       // ������ (normal)
    SUN_FLOWER,                     // �عٶ��
    TV,                             // TV (rare)
    DOL_HAREU_BANG,                 // ���ϸ���
    MINIGUN_TURRET                  // �̴ϰ���ž (unique)
}

public enum MONSTERTYPE
{
    MELEE_MONSTER,                  // �ٰŸ� ����
    RANGED_MONSTER                  // ���Ÿ� ����
}

public enum MONSTERLIST
{
    VIPER,                          // ����                      �������� : ���� �� ����
    INEXPERIENCED_HUNTER,           // �̼��� ��ɲ�
    SLIME,                          // ������
    SPIDER_EGG_HOUST,               // �Ź̾���
    BABY_SPIDER,                    // �����Ź�
    QUEEN_SPIDER,                   // ���հŹ�  (����)
    WILD_MUSHROOM,                  // �߻����� (�Ʒ��� �ش罺������ ���� ���� ����)
    HORNET,                         // ����
    MOSS_GOLEM,                     // �̳��� �� ��
    BIG_SLIME,                      // �Ŵ��� ������ (����)

    BIG_CARNIVOROUS_PLANT,          // �Ŵ����� ����Ĺ�         �������� : ��ο� ��
    METEOR_GOLEM,                   // ��� ���� ��
    STRANGE_SPIRIT,                 // ��� �̻����� ����
    METEOR_HUMAN,                   // ��� Ȧ�� ���
    POISON_SLIME,                   // �� ������
    METEOR_ENT,                     // ��� ���� ��Ʈ (����)
    HUNTER_TRAP,                    // ��ɲ��� �� (�Ʒ��� �ش罺������ ���� ���� ����)
    SKILLED_HUNTER,                 // ���õ� ��ɲ�
    GHOST,                          // ����
    SNIPER_HUNTER,                  // ���ݼ� ��ɲ� (����)

    CAR,                            // �ڵ���                    �������� : ������ �� ����
    SHIELD_GUARD,                   // ���и� �� ����
    DRONE,                          // ���
    POLLUTANTS_DRUMCAN,             // �������� �� �巳��
    HUNTDOG_ROBOT,                  // ��ɰ� �κ�
    FOUR_LANES_TRAFFIC_LIGHT,       // 4���� ��ȣ�� (����)
    SHOTGUN_GUARD,                  // ������ �� ���� (�Ʒ��� �ش罺������ ���� ���� ����)
    STREET_LAMP,                    // ���ε�
    SAFETY_CONE_SOMETHING,          // ������� ���� ����
    RIDING_ROBOT_GUARD,             // �κ��� ź ���� (����)

    MIX_SLIME,                      // ȥ�յ� ������            �������� : ������
    AUTO_GUARD_ROBOT,               // �ڵ� ��� �κ�
    BOMB_FROG,                      // �����ϴ� ������
    BULB,                           // ����
    ANDROID,                        // �ȵ���̵�
    SUPER_COMPUTER,                 // ������ǻ�� (����)
    MUTANT_WILD_DOG,                // �������� �鰳 (�Ʒ��� �ش罺������ ���� ���� ����)
    FLASK_TROW_RESEARCHER,          // �ö�ũ�� ������ ������
    FLYING_BOOK,                    // ���ƴٴϴ� å
    LAB_DIRECTOR,                   // ������ ���� (����)
}