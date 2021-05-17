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

public enum MELEEWEAPON             // 근접무기
{
    TOOTHPICK,                      // 이쑤시개 (normal)
    CLIP,                           // 클립?
    STONESPEAR,                     // 돌창
    VINEWHIP,                       // 넝쿨채찍
    WOODSHIELD,                     // 나무방패
    SPIRALSWORD,                    // 나선검 (rare)
    SWORDSHIELD,                    // 소드쉴드
    KOLA,                           // kola
    RAPIER,                         // 레이피어
    FORK,                           // 포크 (unique)
    SEALEDKEY,                      // 봉인된 열쇠
    KEYBLADE,                       // 키블레이드 (legend)
    GAUNTLET,                       // ???건틀릿 (???)
    SMALLKEY                        // ???작은열쇠
}

public enum RANGEDWEAPON            // 원거리무기
{
    TESTGUN,                        // 테스트 (normal)
    WOODSLINGSHOT,                  // 나무새총 (normal)
    WOODBOW,                        // 나무 활
    STING,                          // 독침
    BOOMERANG,                      // 부메랑
    SEEDING,                        // 씨뿌리기
    FIREBIRD,                       // 불새 (rare)
    FIRELOCK,                       // 화승총
    CHAKRAM,                        // 차크람
    RAILGUN,                        // 레일건
    SHILEDPISTOL,                   // 실드피소톨 (unique)
    GENTLEMANUMBRELLA,              // 신사우산
    THREESIXNINE,                   // 369
    FLAXGUN,                        // Flax Gun
    ENDOFTHECENTURYGAUNTLET         // 세기말 건틀릿 (legend)
}

public enum THROWWEAPON             // 투척무기
{
    PINECONEBOMB,                   // 솔방울 폭탄 (normal)
    SEEDBOMB,                       // 씨 폭탄
    TOYROBOT,                       // 장난감 로봇 (rare)
    PLASMABOMB,                     // 플라즈마 폭탄 (unique)
}

public enum TURRET                  // 포탑
{
    CATAPULT,                       // 투석기 (normal)
    SUNFLOWER,                      // 해바라기
    TV,                             // Tv (rare)
    DOLHAREUBANG,                   // 돌하르방
    MINIGUNTURRET                   // 미니건포탑 (unique)
}