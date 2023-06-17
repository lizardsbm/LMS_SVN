using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUIController : BaseBehaviour, UIStack.IOnBackPressed
{
    // Base 로까지 올라온 BackPressed 이벤트는 해당 UI를 끄는 역할을 한다.
    public virtual bool onBackPressed()
    {
        hide();
        return false;
    }
}
