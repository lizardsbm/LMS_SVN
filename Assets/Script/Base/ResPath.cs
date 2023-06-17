/// <summary>
/// 프리팹 경로를 미리 정해둘곳
/// </summary>
public class ResPath
{
    private const string PREFAB = "Prefab/";
    public const string INGAME = PREFAB + "Ingame/";
    public const string PREFAB_UI = PREFAB + "UI/";
    public const string COMMON_UI = PREFAB_UI + "CommonUI/";
    public const string UI_ITEM = PREFAB_UI + "Item/";
    public const string PANEL = PREFAB_UI + "Panel/";
    public const string POPUP = PREFAB_UI + "Popup/";
    public const string INGAME_SPRITE = SPRITE + "Ingame/";
    public const string INGAME_ITEM = SPRITE + "IngameItem/";
    public const string INGAME_COMMON_ITEM = INGAME_ITEM + "Common/";
    public const string COMBINE_ITEM_ICON = INGAME_ITEM + "Icon/";
    public const string ACHIEVEMENT = SPRITE + "Achievement/";

    private const string SPRITE = "Sprite/";
    public const string COMMON_SPRITE = SPRITE + "Common/";
    public const string CHARACTER = SPRITE + "Character/";
    public const string MENU = SPRITE + "Menu/";
    public const string MENU_COMMON_UI = MENU + "Common/";
    public const string MENU_BUTTON = MENU + "Button/";
    public const string STORY = SPRITE+"Story/";
    public const string STORY_CUT_SCENE = STORY + "CutScene/";

    private const string SOUND = "Sound/";
    public const string UI_SOUND = SOUND + "UI/";
    public const string BGM_SOUND = SOUND + "BGM/";

    public const string EAX_SOUND = SOUND + "EAX/";

    public const string SCENA_GUIDE = MENU + "ScenarioGuide/Stage{0}/{1}";

    public const string INGAME_SUBPOPUP = SPRITE + "SubPopup/";

    public const string BASE_CSV = "csv/";
}
