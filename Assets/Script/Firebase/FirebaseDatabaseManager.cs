using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;

public class FirebaseDatabaseManager
{
    // 데이터베이스 레퍼런스 
    private DatabaseReference mReference;
    private FirebaseDatabase mDatabase;

    public Text[] testData;

    public void init() {

        mReference = FirebaseDatabase.DefaultInstance.RootReference;
        mReference.ChildAdded += MReference_ChildAdded;
    }

    private void MReference_ChildAdded(object sender, ChildChangedEventArgs e) {
        Debug.Log("Child Added");
    }

    /// <summary>
    /// 이거 형식보고 결정해야함..
    /// </summary>
    public void SaveData(string datas) {

        PlayerPrefs.SetString(FirebaseManager.USER_TOKEN, datas);
        mReference.Child(FirebaseManager.USER_TOKEN).SetRawJsonValueAsync(datas);
    }

    public void saveChildData(string datas, string childKey) {
        PlayerPrefs.SetString(string.Format("{0}/{1}",FirebaseManager.USER_TOKEN,childKey), datas);
        mReference.Child(FirebaseManager.USER_TOKEN).Child(childKey).SetRawJsonValueAsync(datas);
    }

    public void LoadData(DataManager dManager) {

        string data;

        FirebaseDatabase db = FirebaseDatabase.DefaultInstance;

        db.GetReference(FirebaseManager.USER_TOKEN)
            .GetValueAsync().ContinueWith(task => {
                if (task.IsFaulted) {
                    //
                } else if (task.IsCompleted) {
                    DataSnapshot snapshot = task.Result;

                    data = snapshot.GetRawJsonValue();
                    Debug.Log(data);

                    dManager = LoadDataFromJson<DataManager>(data);
                    ConnectTestScript.textContent = string.Format("데이터 로드, 값 = {0}",data);
                }
            });
    }

    /// <summary>
    /// 하위 자식에 있는 데이터만 가져올때
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="charData"></param>
    /// <param name="childKey"></param>
    public void LoadChildData<T>(T charData,string childKey) where T : class {

        string data;

        FirebaseDatabase db = FirebaseDatabase.DefaultInstance;

        db.GetReference(FirebaseManager.USER_TOKEN).Child(childKey)
            .GetValueAsync().ContinueWith(task => {
                if (task.IsFaulted) {
                    //
                } else if (task.IsCompleted) {
                    DataSnapshot snapshot = task.Result;

                    data = snapshot.GetRawJsonValue();
                    Debug.Log(data);
                    charData = LoadDataFromJson<T>(data);
                    ConnectTestScript.textContent = string.Format("데이터 로드, 값 = {0}", data);
                }
            });
    }


    private T LoadDataFromJson<T>(string loadData) where T : class {
        Log.d(loadData);
        return JsonUtility.FromJson<T>(loadData);
    }
}


