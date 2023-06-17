using UnityEngine;
using System.Collections;

/// <summary>
/// 모든 클래스에서 MonoBehaviour 대신 상속받아 사용할 클래스.
/// </summary>
public class BaseBehaviour : MonoBehaviour {

    public const string INIT_VARIABLES = "initMemberVariables";

    //캐시 변수들.
    private GameObject myGo;
    private Transform myTrf;
    private RectTransform myRectTrf;

    public GameObject obj {
        get {
            if (myGo == null)
                myGo = this.gameObject;

            return myGo;
        }
    }

    public Transform trf {
        get {
            if (myTrf == null) {
                myTrf = this.transform;
            }

            return myTrf;
        }
    }

    public RectTransform rectTrf {
        get {

            if(myRectTrf == null)
            {
                myRectTrf = trf.GetComponent<RectTransform>();
            }

            return myRectTrf;
        }
    }

	public bool isActive {
		get {
			return obj.activeInHierarchy;
		}
	}



	//프리팹을 Instantiate 한 후에 메서드를 호출하면 Awake 가 호출되기 전에 내부 변수에 접근하는 상황이 생기기도 한다.
	//그래서 초기화 메서드를 따로 만들어두고 처리하도록 바꿨다.
	protected bool isInitialized = false;
	
	/// destroy가 호출되었는지
	protected bool isDestroyed = false;

	/// <summary>
	/// 내부변수 셋팅.
	/// </summary>
	/// <param name="forced">이미 initialized가 완료되었어도 다시 셋팅할 것인지.</param>
	/// <returns></returns>
	public void initMemberVariables(bool forced = false) {

		if(isInitialized && forced){
			isInitialized = false;
			initVariables();
			isInitialized = true;
			return;
		}

		if(!isInitialized) {
			initVariables();
			isInitialized = true;
			return;
		}
	}



	/// <summary>
	/// 완료여부를 리턴받고 싶은데 여러번 상속되는 구조에서 어떻게 처리할지 아직 미정이다..
	/// </summary>
	/// <returns></returns>
	protected virtual void initVariables() {

		isDestroyed = false;
        isInitialized = true;

    }

    protected virtual void Awake() {
        myGo = this.gameObject;
        myTrf = this.transform;

		if(!isInitialized) {
			initVariables();
		}
    }


    protected virtual void Start(){
    }

    /// <summary>
    /// 상속하지 않고 OnActiveStateChanged(bool)로 대체.
    /// </summary>
    private void OnEnable() {
        this.OnActiveStateChanged(true);
    }

    /// <summary>
    /// 상속하지 않고 OnActiveStateChanged(bool)로 대체.
    /// </summary>
    private void OnDisable() {
        this.OnActiveStateChanged(false);
    }

    protected virtual void OnActiveStateChanged(bool enabled) {
    }

    protected virtual void OnDestroy() {
		isDestroyed = true;
	}

    /// <summary>
    /// 기본값으로 세팅될때 호출. 거의 Editor에서만 사용하는 메서드.
    /// </summary>
    protected virtual void Reset() {
    }

    /// <summary>
    /// 인스펙터에서 값이 바뀌었을때 호출.
    /// </summary>
    protected virtual void OnValidate() {
    }

    public virtual void show()
    {
        Utils.setActive(trf, true);
    }

    public virtual void hide()
    {
        Utils.setActive(trf, false);
    }
}
