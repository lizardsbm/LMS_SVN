using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 선행조건 체크
/// </summary>
public class PrerequisitesManager : BaseSingleton<PrerequisitesManager> {

    public Dictionary<int, bool> mDicPrerequisites = new Dictionary<int, bool>();

    public List<string> mLstPre = new List<string>();

    public override void init() {
        base.init();
        obj.name = "PrerequisitesManager";
    }

    /// <summary>
    /// 선행 조건을 추가함
    /// </summary>
    /// <param name="idx"></param>
    public void initPrerequisites(int[] index,string[] str) {

        mDicPrerequisites.Clear();
        mLstPre.Clear();

        mLstPre.Add("기본 충족상태");

#if UNITY_EDITOR
        int checkIdx = 0;

        for(int i=0; i < index.Length; ++i) {
            checkIdx = index[i];

            for(int k = i; k < index.Length; ++k) {
                if(k == i) {
                    continue;
                }

                if(index[k] == index[i]) {
                    Log.e(i + "번째 인덱스와 " + k + "번째 인덱스의 값이 똑같습니다.");
                    Log.d(i + "번쨰 인덱스 값  = " + index[i] + ", " + k + "번째 인덱스 값 = " + index[k]);
                }
            }
        }

#endif
        for(int i = 0; i < index.Length; ++i) {
        }

        // 선행 조건 갯수만큼 추가해줌
        for (int i = 0; i < index.Length; ++i) {
            mDicPrerequisites.Add(index[i],false);
        }

        // 선행조건 내용이 무엇인지
        for (int i = 0; i < str.Length; ++i)
        {
            mLstPre.Add(str[i]);
        }

        // 기본적으로 0은 기본상태를 뜻하는것으로
        mDicPrerequisites[0] = true;
    }

    /// <summary>
    /// 선행 조건을 on 하거나 off 시킨다.
    /// </summary>
    /// <param name="idx"></param>
    /// <param name="isEnable"></param>
    public void enablePrerequisites(int idx, bool isEnable) {
        mDicPrerequisites[idx] = isEnable;
    }

    /// <summary>
    /// 외부에서 들어온 선행조건들이 모두 충족하는지 체크
    /// </summary>
    /// <param name="idx"></param>
    /// <returns></returns>
    public bool isSatisfyPre(params int[] idx) {

        for(int i = 0; i < idx.Length; ++i) {
            if(!mDicPrerequisites[idx[i]]) {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 외부로부터 딱 하나의 선행조건이 맞는지 체크
    /// </summary>
    /// <param name="idx"></param>
    /// <returns></returns>
    public bool isSatisfyPre(int idx) {
        return mDicPrerequisites[idx];
    }

    /// <summary>
    /// 게임 도중 꺼진경우 다시 재진입 했을때 ..
    /// </summary>
    /// <param name="prerequis"></param>
    public void setLoadTokenData(int[] prerequis) {
        for(int i=0;i< prerequis.Length; ++i) {
            mDicPrerequisites[prerequis[i]] = prerequis[i] == 0 ? false : true;
        }
    }
}
