using UnityEngine;
using System.Collections;
using UIStack;

/// <summary>
///  스택의 컨트롤을 받는 패널 컨트롤 스크립트의 베이스 클래스.
/// </summary>
/// <typeparam name="K">화면별 고유 식별자</typeparam>
public class BaseStackablePanel<K> : BaseUI, IUIStackable
{
    #region IUIStackable implements

    /// <summary>
    /// topStack에 추가되었을때 호출
    /// </summary>
    public virtual void onAddStack()
    {

    }


    /// <summary>
    /// ui 가 stack에서 빠져나갈 때 호출
    /// </summary>
	public virtual void onPopStack()
    {

    }


    /// <summary>
    /// back으로 빠졌다가 다시 top으로 복귀되었을때 호출
    /// </summary>
	public virtual void onTopRegistered()
    {

    }


    /// <summary>
    /// top이었다가 pop, 혹은 새로운 스택이 추가되어 top스택에서 벗어날때 호출
    /// </summary>
	public virtual void onTopReleased()
    {

    }

    public virtual bool getActiveState()
    {
        return isActive;
    }

    public void setActiveState(bool state)
    {
        Utils.setActive(this.obj, state);
    }
    #endregion
}
