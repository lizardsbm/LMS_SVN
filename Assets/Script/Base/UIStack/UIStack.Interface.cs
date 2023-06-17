using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace UIStack
{
    /// <summary>
    /// 스택의 상태 변경을 노티 해줌.
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public interface IUIStackChangedHandler<K, V>
    {

        void onAddUI(K key, V value);                   //ui가 스택상에 세팅 되었을때 호출
        void onPopUI(K key, V value);                   //ui가 스택상에서 사라질때 호출
        void onSetClosed(K key, V value, bool isPopStack);          //닫히는 ui에 대한 처리
        void onSetOpened(K key, V value, bool isAddStack);          //열리는 ui에 대한 처리
    }



    /// <summary>
    /// 스택에 적재 가능한 패널은 모두 이 인터페이스를 상속받아야 한다.
    /// </summary>
    /// <typeparam name="T">UIKey</typeparam>
    public interface IUIStackable
    {

        /// <summary>
        /// topStack에 추가되었을때 호출
        /// </summary>
        void onAddStack();


        /// <summary>
        /// ui 가 팝 될때 호출
        /// </summary>
        void onPopStack();


        /// <summary>
        /// back으로 빠졌다가 다시 top으로 복귀되었을때 호출
        /// </summary>
        void onTopRegistered();


        /// <summary>
        /// top이었다가 pop, 혹은 새로운 스택이 추가되어 top스택에서 벗어날때 호출
        /// </summary>
        void onTopReleased();

        //망할.... 라인업 투명배경 때문에 뒤에 UI들이 보여서 추가했당
        bool getActiveState();
        void setActiveState(bool state);

    }


    /// <summary>
    /// 뒤로가기 이벤트를 처리할 함수
    /// </summary>
	public interface IOnBackPressed
    {
        bool onBackPressed();
    }

    /// <summary>
    /// 홈으로가기 버튼/나가기버튼 눌렀을때 이벤트를 처리할 함수
    /// 사실 홈버튼은 홈버튼 이기도 하고 어드민 버튼이기도 하고 그렇긴 함;;
    /// </summary>
    public interface IOnMenuPressed
    {
        bool onHomePressed();
        bool onQuitPressed();
    }

    /// <summary>
    /// 스택에 추가될때의 타입
    /// </summary>
    public enum Option
    {
        ClearStack,             //스택을 비우고 추가
        PopBackStack,           //최상단 스택을 pop 한 뒤 추가
        HideBackStack,      //최상단스택을 hide처리(pop은안함)하고 추가
        AddTo,                   //아무것도안하고 적재만
        HideAllBackStack,		//뒤에 열려있는 모든걸 닫는당..
    }
}