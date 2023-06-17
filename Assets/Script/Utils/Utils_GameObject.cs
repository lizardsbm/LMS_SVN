using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/**
 * 기본적인 GameObject, Transform, Monobehaviour 관련 유틸
 * 
 * loadPrefab : 프리팹을 로드 TODO LoadAsync 추가 해야함 
 * createObject : 오브젝트를  Instantiate
 */
public partial class Utils {


    /**
     * 프리팹 혹은 파일경로를 사용해 게임오브젝트 인스턴스 생성.
     */
    #region createObject


    /*
     * position 파라미터 제외.
     */
    public static GameObject createObject(GameObject prefab, Transform parent = null, string displayName = null) {
        return createObject(prefab, Vector3.zero, parent, displayName);
    }

    /**
     * position 파라미터 제외.
	 */
    public static GameObject createObject(string prefabPath, Transform parent = null, string displayName = null) {
        return createObject(prefabPath, Vector3.zero, parent, displayName);
    }

    /**
     * 파일경로를 사용해 겜오브젝트 인스턴스 생성.
     */
    public static GameObject createObject(string prefabPath, Vector3 localPosition, Transform parent = null, string displayName = null) {
        GameObject prefab = loadPrefab(prefabPath);
        return createObject(prefab, localPosition, parent, displayName);
    }

    /**
     * 프리팹을 사용해 겜오브젝트 인스턴스 생성.
     */
    public static GameObject createObject(GameObject prefab, Vector3 localPosition, Transform parent = null, string displayName = null) {

        if(prefab == null)
            return null;

        if(string.IsNullOrEmpty(displayName) && prefab != null)
            displayName = prefab.name;

        GameObject temp = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        temp.name = displayName;

        attach(parent, temp.transform);
        temp.transform.localPosition = localPosition;

        return temp;
    }


    #endregion



    /**
     * parent 하위로 차일드를 붙여줌.
     */
    #region attach

    public static void attach(Transform parent, Transform child, bool reset = true) {
        child.SetParent(parent);

        if(reset)
            resetTransform(child);
    }

    public static void attach(Transform parent, Transform child, Vector3 pos) {
        child.SetParent(parent);

        resetTransform(child);
        child.localPosition = pos;
    }


    public static void attachSibling(Transform sibling, Transform child, bool reset = true) {
        attach(sibling.parent, child, reset);
    }

    public static void attachSibling(Transform sibling, Transform child, Vector3 pos) {
        attach(sibling.parent, child, pos);
    }

    #endregion




    /// <summary>
    /// 위치를 복사
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    public static void copyPosition(Transform from, Transform to) {

        if(from == null || to == null) {
            return;
        }

        to.transform.position = from.position;
    }


    /**
     * 컴포넌트 추가.
     * @params go : 추가대상 게임오브젝트.
     * @ params add : 해당 컴포넌트가 존재하지 않을 경우 addComponent 여부.
     */
    #region getComponent<T>

    public static T getComponent<T>(GameObject go, bool add = true) where T : Component {
        if(go == null) {
            Log.v("getComponent Failed. gameObject is null");
            return null;
        }

        T component = go.GetComponent<T>();

        if(component == null && add)
            component = addComponent<T>(go, false);

        return component;
    }

    public static T getComponent<T>(Transform trf, bool add = true) where T : Component {
        if(trf == null) {
            Log.v("getComponent Failed. transform is null");
            return null;
        }

        return getComponent<T>(trf.gameObject, add);
    }

    public static T getComponent<T>(MonoBehaviour behaviour, bool add = true) where T : Component {
        if(behaviour == null) {
            Log.v("getComponent Failed. behaviour is null");
            return null;
        }

        return getComponent<T>(behaviour.gameObject, add);
    }

    #endregion



    /**
     * 부모를 탐색하며 컴포넌트를 찾음.
     */
    #region findInParents<T>

    public static T findInParents<T>(Transform trf) where T : Component {
        if(trf == null || trf.parent == null) {
            return null;
        }

        T target = trf.parent.GetComponent<T>();
        if(target == null) {
            return findInParents<T>(trf.parent);
        } else {
            return target;
        }
    }


    public static T findInParents<T>(GameObject obj) where T : Component {
        return findInParents<T>(obj.transform);
    }


    public static T findInParents<T>(MonoBehaviour behaviour) where T : Component {
        return findInParents<T>(behaviour.transform);
    }

    #endregion



    /**
     * 차일드 게임오브젝트를 모두 삭제.
     */
    #region clearChilds

    public static void clearChilds(Transform parent) {
        if(parent == null)
            return;

        //Destroy는 Update루프가 끝나고 렌더링 전에 삭제된다. 그래서 while문으로 돌리면 무한루프에 빠짐.
        //DestroyImmediate 는 바로 삭제되지만, Editor에서 쓰도록 권장. 
        //그래서 아래와 같은 방법으로 변경.
        //while(parent.childCount > 0)
        for(int i = 0; i < parent.childCount; ++i) {

            Transform child = parent.GetChild(i);

            if(child != null)
                GameObject.Destroy(child.gameObject);
        }
    }

    public static void clearChildsImmediate(Transform parent) {
        if(parent == null)
            return;

        //Destroy는 Update루프가 끝나고 렌더링 전에 삭제된다. 그래서 while문으로 돌리면 무한루프에 빠짐.
        //DestroyImmediate 는 바로 삭제되지만, Editor에서 쓰도록 권장. 
        //그래서 아래와 같은 방법으로 변경.
        //while(parent.childCount > 0)
        for(int i = 0; i < parent.childCount; ++i) {

            Transform child = parent.GetChild(i);

            if(child != null) {
                GameObject.DestroyImmediate(child.gameObject);
                --i;
            }
        }
    }


    public static void clearChilds(GameObject parent) {
        if(parent != null)
            clearChilds(parent.transform);
    }


    public static void clearChilds(MonoBehaviour parent) {

        clearChilds(parent.transform);
    }

    #endregion



    /**
     * 빈오브젝트를 생성.
     */
    #region createEmptyObject

    public static GameObject createEmptyObject(string name, Transform parent = null, int layer = 0) {

        GameObject newObject = new GameObject(name);
        newObject.layer = layer;

        if(parent != null)
            attach(parent, newObject.transform);

        return newObject;
    }

    #endregion



    /**
     * 자식 중 이름에 해당하는 게임오브젝트를 찾아 리턴.
     * @params parent : 탐색대상 부모.
     * @params name : 찾을 오브젝트 이름
     * @params searchOnHierachy : false:다이렉트 차일드만 찾음 false:전체 차일드에서 찾음.
     */
    #region getChild

    public static Transform getChild(GameObject parent, string name, bool searchOnHierachy = true) {
        if(parent == null) {
            Log.e("Invalid parent parameter");
            return null;
        }

        return getChild(parent.transform, name, searchOnHierachy);
    }

    public static Transform getChild(Behaviour parent, string name, bool searchOnHierachy = true) {
        if (parent == null) {
            Log.e("Invalid parent parameter");
            return null;
        }

        return getChild(parent.transform, name, searchOnHierachy);
    }

    public static Transform getChild(MonoBehaviour mono, string name, bool searchOnHierachy = true) {
        if(mono == null) {
            Log.e("Invalid parent parameter");
            return null;
        }

        return getChild(mono.transform, name, searchOnHierachy);
    }

    public static Transform getChild(Transform parent, string name, bool searchOnHierachy = true) {
        if(parent == null) {
            Log.e("Invalid parent parameter");
            return null;
        }

        //다이렉트차일드 먼저 체크.
        Transform child = parent.Find(name);

        //다이렉트 차일드만 찾게 설정되었다면 걍리턴.
        if(!searchOnHierachy)
            return child;

        //전체찾기 더라도 발견되었으면 리턴
        if(child != null)
            return child;


        Transform r = null;
        foreach(Transform t in parent) {
            r = getChild(t, name, searchOnHierachy);
            if(r)
                return r;
        }

        return null;
    }



    public static Transform getChild(GameObject parent, int index) {

        if(parent == null) {
            Log.e("Invalid parent parameter");
            return null;
        }

        return getChild(parent.transform, index);
    }

    public static Transform getChild(MonoBehaviour mono, int index) {

        if(mono == null) {
            Log.e("Invalid parent parameter");
            return null;
        }

        return getChild(mono.transform, index);
    }

    public static Transform getChild(Transform parent, int index) {
        if(parent == null) {
            Log.e("Invalid parent parameter");
            return null;
        }

        if(parent.childCount <= index)
            return null;
        else
            return parent.GetChild(index);
    }


    #endregion



    /**
     * 자식 중 이름에 해당하는 게임오브젝트를 찾고 지정된 컴포넌트를 리턴.
     * @params parent : 탐색대상 부모.
     * @params name : 찾을 오브젝트 이름
     * @params searchOnHierachy : false:다이렉트 차일드만 찾음 false:전체 차일드에서 찾음.
     */
    #region getChild<T>

    public static T getChild<T>(GameObject parent, string name, bool searchOnHierachy = true, bool includeInactive = true) where T : Component {
        if(parent == null) {
            Log.e("Invalid parent parameter");
            return null;
        }

        return getChild<T>(parent.transform, name, searchOnHierachy);
    }

    public static T getChild<T>(Behaviour parent, string name, bool searchOnHierachy = true, bool includeInactive = true) where T : Component {
        if (parent == null) {
            Log.e("Invalid parent parameter");
            return null;
        }

        return getChild<T>(parent.transform, name, searchOnHierachy);
    }

    public static T getChild<T>(MonoBehaviour parent, string name, bool searchOnHierachy = true, bool includeInactive = true) where T : Component {
        if(parent == null) {
            Log.e("Invalid parent parameter");
            return null;
        }

        return getChild<T>(parent.transform, name, searchOnHierachy);
    }

    public static T getChild<T>(Transform parent, string name, bool searchOnHierachy = true, bool includeInactive = true) where T : Component {
        if(parent == null) {
            Log.e("Invalid parent parameter");
            return null;
        }


        Transform child = getChild(parent, name, searchOnHierachy);

        if(child != null) {
            return child.GetComponent<T>();

        } else {
            return null;
        }
    }


    public static T getChild<T>(GameObject parent, int index) where T : Component {
        if(parent == null) {
            Log.e("Invalid parent parameter");
            return null;
        }

        return getChild<T>(parent.transform, index);
    }

    public static T getChild<T>(MonoBehaviour parent, int index) where T : Component {
        if(parent == null) {
            Log.e("Invalid parent parameter");
            return null;
        }

        return getChild<T>(parent.transform, index);
    }

    public static T getChild<T>(Transform parent, int index) where T : Component {
        if(parent == null) {
            Log.e("Invalid parent parameter");
            return null;
        }


        Transform child = getChild(parent, index);

        if(child != null) {
            return child.GetComponent<T>();

        } else {
            return null;
        }
    }


    #endregion




    /**
     * 오브젝트 켜기/끄기.
     */
    #region setActive

    public static void setActive(GameObject obj, bool state) {
        if(obj == null)
            return;

        if(obj.activeSelf != state)
            obj.SetActive(state);
    }

    public static void setActive(Behaviour obj, bool state) {
        if (obj == null)
            return;

        if (obj.gameObject.activeSelf != state)
            obj.gameObject.SetActive(state);
    }

    public static void setActive(Transform trf, bool state) {
        if(trf == null)
            return;

        setActive(trf.gameObject, state);
    }

    public static void setActive(MonoBehaviour behaviour, bool state) {
        if(behaviour == null)
            return;

        setActive(behaviour.gameObject, state);
    }


    public static void toggleActive(GameObject obj) {
        if(obj == null)
            return;

        bool state = obj.activeInHierarchy;
        obj.SetActive(!state);
    }

    #endregion



    /**
     * 컴포넌트 추가.
     * @params checkduplicate true:먼저 컴포넌트가 붙어있는지 체크.
     */
    #region addComponent<T>

    public static T addComponent<T>(GameObject go, bool checkDuplicate = true) where T : Component {

        if(!checkDuplicate)
            return go.AddComponent<T>() as T;


        T component = go.GetComponent<T>();

        if(component == null) {
            component = go.AddComponent<T>() as T;
        }

        return component;
    }


    public static T addComponent<T>(Transform trf, bool checkDuplicate = true) where T : Component {
        return addComponent<T>(trf.gameObject, checkDuplicate);
    }


    public static T addComponent<T>(Behaviour behaviour, bool checkDuplicate = true) where T : Component {
        return addComponent<T>(behaviour.gameObject, checkDuplicate);
    }

    #endregion




    /**
     * transform 값들을 초기값으로 셋팅
     */
    #region resetTransform

    public static void resetTransform(GameObject go) {
        if(go == null)
            return;

        resetTransform(go.transform);
    }
    public static void resetTransform(MonoBehaviour behaviour) {
        if(behaviour == null)
            return;

        resetTransform(behaviour.transform);
    }
    public static void resetTransform(Transform t) {
        if(t == null)
            return;

        t.localPosition = Vector3.zero;
        t.localScale = Vector3.one;
        t.rotation = Quaternion.identity;
    }

    #endregion



    /**
     * 모든 차일드의 레이어 설정.
     */
    #region 

    public static void setLayerAllChildren(Transform parent, int layer) {

        parent.gameObject.layer = layer;

        for(int i = 0; i < parent.childCount; i++) {
            Transform child = parent.GetChild(i);
            setLayerAllChildren(child, layer);
        }
    }

    public static void setLayerAllChildren(GameObject parent, int layer) {
        if(parent != null)
            setLayerAllChildren(parent.transform, layer);
    }

    public static void setLayerAllChildren(MonoBehaviour parent, int layer) {
        if(parent != null)
            setLayerAllChildren(parent.transform, layer);
    }

    #endregion

    #region Transfrom  

    public static void setPosScale(Transform t, Vector3 pos, Vector3 scale) {
        t.localPosition = pos;
        t.localScale = scale;
    }

    #endregion 

    /**
     * 로컬포지션 변경
     */

    #region SetLocalPosX
    public static void setLocalPosX(MonoBehaviour behaviour, float posX) {

        if(behaviour != null)
            setLocalPosX(behaviour.transform, posX);
    }

    public static void setLocalPosX(GameObject go, float posX) {

        if(go != null)
            setLocalPosX(go.transform, posX);
    }

    public static void setLocalPosX(Transform tf, float posX) {

        if(tf == null)
            return;

        Vector3 pos = tf.localPosition;
        pos.x = posX;
        tf.localPosition = pos;
    }

    #endregion




    /// <summary>
    /// Destroy 에 관한 예외처리를 포함한 destroy 함수
    /// </summary>
    /// <param name="go"></param>
    public static void safeDestroy(GameObject go) {

        if(go == null) {
            return;
        }

        //플레이중이면 Destroy, 플레이중이 아니면 DestroyImmediate
        //또뭐가있더라
        if(Application.isPlaying) {
            GameObject.Destroy(go);
        } else {
            GameObject.DestroyImmediate(go);
        }
    }

    public static void safeDestroy(Transform tf) {

        if(tf != null) {
            safeDestroy(tf.gameObject);
        }
    }

    public static void safeDestroy(Component comp) {

        if(comp != null) {
            safeDestroy(comp.gameObject);
        }
    }



    public static bool isActive(BaseBehaviour behaviour) {

        if(behaviour == null) {
            return false;
        } else {
            return behaviour.isActive;
        }
    }

    public static bool isActive(GameObject behaviour) {

        if(behaviour == null) {
            return false;
        } else {
            return behaviour.activeInHierarchy;
        }
    }
}
