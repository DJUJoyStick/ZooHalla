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

public enum MELEEWEAPON             // 근접무기
{
    TOOTH_PICK,                     // 이쑤시개 (normal)
    CLIP,                           // 클립?
    STONE_SPEAR,                    // 돌창
    VINE_WHIP,                      // 넝쿨채찍
    WOOD_SHIELD,                    // 나무방패
    SPIRAL_SWORD,                   // 나선검 (rare)
    SWORD_SHIELD,                   // 소드쉴드
    KOLA,                           // kola
    RAPIER,                         // 레이피어
    FORK,                           // 포크 (unique)
    SEALED_KEY,                     // 봉인된 열쇠
    MASTER_KEY,                     // 마스터키 (legend)
    GAUNTLET,                       // ???건틀릿 (???)
    SMALL_KEY                       // ???작은열쇠
}

public enum RANGEDWEAPON            // 원거리무기
{
    TEST_GUN,                       // 테스트 (normal)
    WOOD_SLINGSHOT,                 // 나무새총 (normal)
    WOOD_BOW,                       // 나무 활
    STING,                          // 독침
    BOOMERANG,                      // 부메랑
    SEEDING,                        // 씨뿌리기
    FIRE_BIRD,                      // 불새 (rare)
    FIRE_LOCK,                      // 화승총
    CHAKRAM,                        // 차크람
    RAILGUN,                        // 레일건
    SHILED_PISTOL,                  // 실드피소톨 (unique)
    GENTLEMAN_UMBRELLA,             // 신사우산
    THREE_SIX_NINE,                 // 369
    FLAXGUN,                        // Flax Gun
    END_OF_THE_CENTURY_GAUNTLET     // 세기말 건틀릿 (legend)
}

public enum THROWWEAPON             // 투척무기
{
    PINECONE_BOMB,                  // 솔방울 폭탄 (normal)
    SEED_BOMB,                      // 씨 폭탄
    TOY_ROBOT,                      // 장난감 로봇 (rare)
    PLASMA_BOMB,                    // 플라즈마 폭탄 (unique)
}

public enum TURRET                  // 포탑
{
    CATAPULT,                       // 투석기 (normal)
    SUN_FLOWER,                     // 해바라기
    TV,                             // Tv (rare)
    DOL_HAREU_BANG,                 // 돌하르방
    MINIGUN_TURRET                  // 미니건포탑 (unique)
}