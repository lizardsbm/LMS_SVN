using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSingleton<T> : BaseBehaviour where T : Component 
{
    public static T t;

    public static T inst {
        get {

        if(t == null) {
            GameObject obj = new GameObject();
            t = obj.AddComponent<T>();
        }

        return t;
        }
    }

    public virtual void init() {

    }
}
