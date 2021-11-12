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
    RAT = 0,
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

public enum ItemType
{
    MATERIAL,
    CONSUME,
    MELEE_WEAPON,
    RANGED_WEAPON,
    TURRET
}

public enum BOXTYPE
{
    ENDROOM,
    TREASUREBOX
    
}

public enum ItemPrivateNum
{
    BIGGER_POTION,
    WOOD,
    STONE,
    FLINT,
    VINE,
    SEED,
    SCRAP_METAL,
    GUN_POWDER,
    ELECTRIC_WIRE,
    STEEL,
    GOLD,
    METEORITE_DEBRIS,
    RUBY,
    SAPPHIRE,
    EMERALD,
    AMETHYST,
    AMBER,
    TOPAZ,
    TOOTH_PICK,
    CLUB,
    STONE_SPEAR,
    VINE_WHIPPING,
    WOOD_SHIELD,
    SPIRAL_SWORD,
    RSHIELD_LSWORD,
    KOLA,
    LAPPER,
    FORK,
    SEALED_KEY,
    MASTER_KEY,
    GAUNTLET,
    SMALL_KEY,
    WOOD_SLINGSHOT,
    WOOD_BOW,
    POISON_NEEDLE,
    BOOMERANG,
    SEEDING,
    FIRE_BIRD,
    HWASEUNGGUN,
    CHAKRAM,
    NAILGUN,
    SHIELD_PISTOL,
    GENTLEBRELLA,
    TFFGUN,
    FLAXGUN,
    ENDOFCENTURYGAUNTLET,
    PINECON_BOMB,
    SEED_BOMB,
    ROBOTTOY,
    PLASMA_GRENADE,
    CATAPULT,
    SUNFLOWER,
    TV,
    STONE_HARUBANG,
    MINIGUN_TURRET,
    APPLE,
    BANANA,
    CARROT,
    STRAWBERRY,
    ORANGE,
    BLUEBERRY,
    GREEN_APPLE,
    MANGO,
    DRAGON_FRUIT,
    RED_POTION,
    YELLOW_POTION,
    BLUE_POTION,
    GIANT_POTION,
    WOOD_WALL,
    STONE_WALL,
    RUGGED_IRONWALL,
    IRON_WALL,
    CARROTAPPLE_JUICE,
    BLUEBERRYMANGO_SMOOTHIE,
    DOUBLEBERRY,
    STRAWBERRYBANANA_SHAKE,
    SORRY_POTION,
    MAMALADE,
    ORANGE_POTION,
    PURPLE_POTION,
    BRIGHT_REDPOTION

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
    SPIDER_EGG_HOUSE,               // �Ź̾���
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

public enum STATE_ELIMET            // �����̻�
{
    NONE,                           // X
    BURN,                           // ȭ��
    FASCINATION,                    // ��Ȥ
    POISON,                         // ��
    SLOW,                           // ��ȭ
    THUNDER                         // ����
}