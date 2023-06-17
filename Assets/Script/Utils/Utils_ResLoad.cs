using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;


/**
 * 리소스 로드 관련 유틸클래스
 * 
 */
public partial class Utils{
    
	/**
	 * 프리팹 로드.
	 */
	public static GameObject loadPrefab(string fullPath){

		GameObject prefab = Resources.Load (fullPath) as GameObject;

		if(prefab == null){
            Log.e("LoadPrefab failed !! path[" + fullPath + "]");
		}
        
		return prefab;
	}

    /**
	 * 프리팹 로드.
	 */
    public static T loadRes<T>(string fullPath) where T : UnityEngine.Object
    {
        return Resources.Load<T>(fullPath);
    }



	/// <summary>
	/// string > enum 변환.
	/// </summary>
	/// <typeparam name="E">변환할 enum 타입</typeparam>
	/// <param name="str"></param>
	/// <returns></returns>
	public static bool toEnum<E>(string str, ref E type) where E : struct, IConvertible {
		
		if( ! typeof(E).IsEnum) {
			return false;
		}

		try {
			type = (E) Enum.Parse(typeof(E), str);
			return true;
		} catch(Exception e) {
            Log.e(e.ToString());
			return false;
		}
	}



	/// <summary>
	/// 파일 읽기.
	/// </summary>
	/// <param name="path">파일경로</param>
	/// <param name="txt">파일내용을 담을 변수</param>
	/// <returns>리드 성공 여부.</returns>
	public static bool readFile(string path, out string txt) {
		txt = null;

		//
		bool exist = File.Exists("Resources/" + path);
		if(!exist)
			return false;


		try {
			FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
			BinaryReader reader = new BinaryReader(fileStream);

			if(reader == null)
				return false;

			txt = reader.ReadString();
			reader.Close();
			return true;
		} catch(FileNotFoundException e) {
            Log.e(e.ToString());
            return false;
		}
	}



	/// <summary>
	/// 파일 읽기.
	/// </summary>
	/// <param name="path">파일경로</param>
	/// <param name="txt">파일내용을 담을 변수</param>
	/// <returns>리드 성공 여부.</returns>
	public static bool readTextAsset(string path, out string txt) {
		
		txt = null;
		
		try {
			TextAsset asset = (TextAsset) Resources.Load(path) as TextAsset;

			if(asset == null)
				return false;

			txt = asset.text;
			
			return true;
		} catch(FileNotFoundException e) {
            Log.e(e.ToString());
            return false;
		}
	}
}
