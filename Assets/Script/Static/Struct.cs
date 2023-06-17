/// <summary>
/// 쓰일 구조체를 여기에 정의해놓습니다.
/// </summary>

/// <summary>
/// 캐릭터 대사를 로드해서 사용하는데 필요한 정보
/// </summary>
public struct ScriptInfomation {

    // 인물 대사
    public string script;

    // 캐릭터 인덱스 (기본으로 2명을 생성해두고, 3이상부턴 새로 생성해서 넣어줄 예정)
    public int charIdx;

    // 어떤 캐릭터가 말하는지
    public string character;

    // 1번은 기본표정, 2번은 웃는표정, 3번은 화난표정 등등 따로 있으면 좋을것 같아 넣어둡니다.
    public int emotion;

    // 캐릭터 스탠딩 포지션
    public float charPos;

    // 현재 말하는 캐릭터를 제외하고는 다른 캐릭터들을 암전 시킬것인지. 0 이면 그대로 두고, 1이면 암전
    public int anotherCharDisable;


    // 추후에 해당 스크립트를 그냥 넘길지 안넘길지, 혹은 캐릭터 표정이나 애니메이션이 존재하는 경우 해당 구조체에 적용을해서 작동되도록 합니다.
}
