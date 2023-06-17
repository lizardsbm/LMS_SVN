/// <summary>
/// 가구 종류
/// </summary>
public enum PropType
{
    Normal,
    CyberPunk,
}

/// <summary>
/// 캐릭터 종류
/// </summary>
public enum CharKind
{
    A,
    B,
    C,
    D,
    E,
    F,
}

// 씬 이동후 실행할 함수 종류
public enum FunKind
{
    None,
    MoveMap,
    ExistPlayingGame,
}

//UI 관련 사운드
public enum UISound
{
    UI_Click_Button,
    SceneChangeEffect,
    None,
}

// 배경음 사운드
public enum BGMSound {
    Main_BGM,
    Stage1_FirstScene_MeetABR_BGM,
    Stage1_SecondScene_Marry_BGM,
    Stage1_ingame_BGM,
    Stage2_ingame_BGM,
    None,
}

// 환경음 사운드
public enum EAXSound {
    Stage1_FirstScene_Alone_EAX,
    None,
}

// 업적 - 쥐 패턴 종류
public enum Achieve_Mouse_Pattern {
    Pattenr1,
    Pattenr2,
    NONE
}

// 업적- 일러스트 종류
public enum Achieve_Illust {
    Illust1,
    Illust2,
    NONE
}

// 업적 - 가구 리스트
public enum Achieve_Furniture {
    Furn1,
    Furn2,
    NONE,
}

public enum Achieve_Panel{
    Pattern,
    Illust,
    Furniture
}

public enum SaveType {
    Character,
    Achieve,
    MyRoom,
}

public enum GamePlayStates {
    Attenstion, // 스토리 아예 진입 전
    GiveUp,     // 진입 후 탐험을 포기했을떄,
    Clear,      // 게임 클리어 후
}

/// <summary>
/// 캐릭터 포커싱 관련 옵션
/// </summary>
public enum StoryCharFocus
{
    None, // 둘다 끔
    FIrst, // 첫번쨰 세팅되어있는 캐릭터를 켬
    Second, // 두번째 세팅되어 있는 캐릭터를 켬
    Both // 양쪽다 켬
}

public enum UIKey
{

}

