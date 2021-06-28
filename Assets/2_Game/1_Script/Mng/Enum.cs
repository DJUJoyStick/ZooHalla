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

public enum MELEEWEAPON             // 근접무기
{
    TOOTH_PICK = 0,                 // 이쑤시개 (normal)
    CLUB,                           // 클럽
    STONE_SPEAR,                    // 돌창
    WOOD_SHIELD,                    // 나무방패
    SPIRAL_SWORD,                   // 나선검 (rare)
    RIGHT_SWORD_LEFT_SHILED,        // 오른손엔 검 왼손엔 방패
    KOLA,                           // 콜라
    RAPIER,                         // 레이피어
    FORK,                           // 포크 (unique)
    SEALED_KEY,                     // 봉인된 열쇠
    MASTER_KEY,                     // 마스터키 (legend)
    GAUNTLET,                       // ???건틀릿 (???)
    SMALL_KEY                       // ???작은열쇠
}

public enum RANGEDWEAPON            // 원거리무기
{
    TEST_GUN = 0,                   // 테스트 (normal)
    WOOD_SLINGSHOT,                 // 나무새총 (normal)
    WOOD_BOW,                       // 나무 활
    STING,                          // 독침
    FIRE_BIRD,                      // 불새 (rare)
    FIRE_LOCK,                      // 화승총
    CHAKRAM,                        // 차크람
    NAILGUN,                        // 네일건
    SHILED_PISTOL,                  // 실드피스톨 (unique)
    GENTLEMAN_UMBRELLA,             // 신사우산
    THREE_SIX_NINE,                 // 369
    FLAX_GUN,                       // Flax Gun
    END_OF_THE_CENTURY_GAUNTLET     // 세기말 건틀릿 (legend)
}

public enum TURRET                  // 포탑
{
    CATAPULT,                       // 투석기 (normal)
    SUN_FLOWER,                     // 해바라기
    TV,                             // TV (rare)
    DOL_HAREU_BANG,                 // 돌하르방
    MINIGUN_TURRET                  // 미니건포탑 (unique)
}

public enum MONSTERTYPE
{
    MELEE_MONSTER,                  // 근거리 몬스터
    RANGED_MONSTER                  // 원거리 몬스터
}

public enum MONSTERLIST
{
    VIPER,                          // 독사                      스테이지 : 밝은 숲 몬스터
    INEXPERIENCED_HUNTER,           // 미숙한 사냥꾼
    SLIME,                          // 슬라임
    SPIDER_EGG_HOUST,               // 거미알집
    BABY_SPIDER,                    // 새끼거미
    QUEEN_SPIDER,                   // 여왕거미  (보스)
    WILD_MUSHROOM,                  // 야생버섯 (아래로 해당스테이지 몬스터 전부 보류)
    HORNET,                         // 말벌
    MOSS_GOLEM,                     // 이끼가 낀 골렘
    BIG_SLIME,                      // 거대한 슬라임 (보스)

    BIG_CARNIVOROUS_PLANT,          // 거대해진 식충식물         스테이지 : 어두운 숲
    METEOR_GOLEM,                   // 운석이 박힌 골렘
    STRANGE_SPIRIT,                 // 어딘가 이상해진 정령
    METEOR_HUMAN,                   // 운석에 홀린 사람
    POISON_SLIME,                   // 독 슬라임
    METEOR_ENT,                     // 운석이 박힌 엔트 (보스)
    HUNTER_TRAP,                    // 사냥꾼의 덫 (아래로 해당스테이지 몬스터 전부 보류)
    SKILLED_HUNTER,                 // 숙련된 사냥꾼
    GHOST,                          // 유령
    SNIPER_HUNTER,                  // 저격수 사냥꾼 (보스)

    CAR,                            // 자동차                    스테이지 : 연구실 앞 마당
    SHIELD_GUARD,                   // 방패를 든 경비원
    DRONE,                          // 드론
    POLLUTANTS_DRUMCAN,             // 오염물이 든 드럼통
    HUNTDOG_ROBOT,                  // 사냥개 로봇
    FOUR_LANES_TRAFFIC_LIGHT,       // 4차선 신호등 (보스)
    SHOTGUN_GUARD,                  // 샷건을 든 경비원 (아래로 해당스테이지 몬스터 전부 보류)
    STREET_LAMP,                    // 가로등
    SAFETY_CONE_SOMETHING,          // 안전고깔에 숨은 무언가
    RIDING_ROBOT_GUARD,             // 로봇을 탄 경비원 (보스)

    MIX_SLIME,                      // 혼합된 슬라임            스테이지 : 연구실
    AUTO_GUARD_ROBOT,               // 자동 경비 로봇
    BOMB_FROG,                      // 폭발하는 개구리
    BULB,                           // 전구
    ANDROID,                        // 안드로이드
    SUPER_COMPUTER,                 // 슈퍼컴퓨터 (보스)
    MUTANT_WILD_DOG,                // 돌연변이 들개 (아래로 해당스테이지 몬스터 전부 보류)
    FLASK_TROW_RESEARCHER,          // 플라스크를 던지는 연구원
    FLYING_BOOK,                    // 날아다니는 책
    LAB_DIRECTOR,                   // 연구실 소장 (보스)
}