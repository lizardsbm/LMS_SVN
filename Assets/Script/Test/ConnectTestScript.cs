using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectTestScript : BaseBehaviour
{
    public GUISkin _skin;

    public static string textContent;

    protected override void Awake() {
        base.Awake();
        textContent = "데이터 입력 대기중";
    }

    private void OnGUI() {
        GUI.skin = _skin;
        GUI.Label(new Rect(0,Screen.height - 200, Screen.width-200, 100), textContent);
    }

    public void saveAllData() {
        DataManager.inst.saveAllData();
        textContent = "데이터 전부 저장";
    }

    public void loadAllData() {
        DataManager.inst.LoadData();
        textContent = "데이터 전부 로드";
    }

    public void saveCharData() {
        DataManager.inst.saveCharacterData();
        textContent = "캐릭터 데이터 세이브";
    }

    public void loadCharacter() {
        DataManager.inst.loadCharacterData();
        textContent = "캐릭터 데이터 로드";
    }

    public void saveMyRoomData() {
        DataManager.inst.saveMyRoomData();
        textContent = "마이룸 데이터 세이브";
    }

    public void loadMyRoom() {
        DataManager.inst.loadMyRoomData();
        textContent = "마이룸 데이터 로드";
    }

    public void saveAchieve() {
        DataManager.inst.saveAchievementsData();
        textContent = "업적 데이터 세이브";
    }

    public void loadAchieve() {
        DataManager.inst.loadAchievementsData();
        textContent = "업적 데이터 로드";
    }

    public void setClientCharData() {

        CharKind _kind = (CharKind)Random.Range(0, 6);

        DataManager.inst.setCharacterType(_kind);

        textContent = string.Format("캐릭터 데이터 입력 {0}", _kind);
    }

    public void setAchieveData() {
        Achieve_Mouse_Pattern pattern = (Achieve_Mouse_Pattern)Random.Range(0, 2);
        bool isOpen = Random.Range(0, 2) == 1 ? true : false;

        DataManager.inst.setPatternData(pattern, isOpen);

        textContent = string.Format("업적 데이터 입력, 패턴 = {0}, {1}", pattern, isOpen);

        Achieve_Illust illust = (Achieve_Illust)Random.Range(0, 2);
        isOpen = Random.Range(0, 2) == 1 ? true : false;

        DataManager.inst.setIllustData(illust, isOpen);

        textContent = string.Format("{0}, 일러스트 = {1},{2}", textContent, illust, isOpen);


        Achieve_Furniture furniture = (Achieve_Furniture)Random.Range(0, 2);
        isOpen = Random.Range(0, 2) == 1 ? true : false;

        DataManager.inst.setFurniture(furniture, isOpen);

        textContent = string.Format("{0}, 가구 = {1},{2}", textContent, furniture, isOpen);
    }

    public void setMyRoomData() {
        Achieve_Furniture furniture = (Achieve_Furniture)Random.Range(0, 2);
        int posIdx = Random.Range(0, 100);

        FurnitureInfo data = new FurnitureInfo(furniture, posIdx);

        DataManager.inst.setMyRoomData(data);

        textContent = string.Format("마이룸 데이터 입력 {0}", furniture);
    }
}
