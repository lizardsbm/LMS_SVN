using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 연산관련 메서드
/// </summary>
public partial class Utils{




    /// <summary>
    /// 오차범위 내의 값인지.
    /// </summary>
    /// <param name="left">비교대상 값1</param>
    /// <param name="right">비교대상 값2</param>
    /// <param name="tolerance">오차범위</param>
    /// <returns></returns>
    public static bool isSame(float left, float right, float tolerance=0.0001f)
    {
        //두 값의 차이가 오차범위 내인지.
        return Mathf.Abs(left - right) <= tolerance;
    }

	/// <summary>
	/// 두 값의 절대값이 오차범위 내의 값인지.
	/// </summary>
	/// <param name="left">비교대상 값1</param>
	/// <param name="right">비교대상 값2</param>
	/// <param name="tolerance">오차범위</param>
	/// <returns></returns>
	public static bool isAbsSame(float left, float right, float tolerance = 0.0001f) {
		
		return (Mathf.Abs(left) - Mathf.Abs(right)) <= tolerance;
	}



    /// <summary>
    /// 각도에 해당하는 sin값을 리턴. 라디안 각도가 아닌 호도법의 각도이다.
    /// </summary>
    /// <param name="degree"></param>
    /// <returns></returns>
    public static float sin(float degree)
    {
        return Mathf.Sin(degree * Mathf.Deg2Rad);
    }


    /// <summary>
    /// 각도에 해당하는 cos값을 리턴. 라디안 각도가 아닌 호도법의 각도이다.
    /// </summary>
    /// <param name="degree"></param>
    /// <returns></returns>
    public static float cos(float degree)
    {
        return Mathf.Cos(degree * Mathf.Deg2Rad);
	}



	/// <summary>
	/// 특정 숫자가 범위 내에 있는지 체크한다.
	/// </summary>
	/// <param name="target">체크대상 숫자</param>
	/// <param name="min">최소범위</param>
	/// <param name="max">최대범위</param>
	/// <param name="excludeEdge">최소최대값까지 범위로 포함시킬것인가. true:최소 최대까지 true, false:최소 최대값은 범위에 미포함</param>
	/// <returns></returns>
	public static bool isInRange(int target, int min, int max, bool excludeEdge) {

		if(excludeEdge) {
			return (target >= min && target <= max);
		} else {
			return (target > min && target < max);
		}
	}


	/// <summary>
	/// 특정 숫자가 범위 내에 있는지 체크한다.
	/// </summary>
	/// <param name="target">체크대상 숫자</param>
	/// <param name="min">최소범위</param>
	/// <param name="max">최대범위</param>
	/// <param name="excludeEdge">최소최대값까지 범위로 포함시킬것인가. true:최소 최대까지 true, false:최소 최대값은 범위에 미포함</param>
	/// <returns></returns>
	public static bool isInRange(float target, float min, float max, bool excludeEdge) {

		if(excludeEdge) {
			return (target >= min && target <= max);
		} else {
			return (target > min && target < max);
		}
	}



	#region int32 컨버트

	public static int toInt32(string text, int defValue = 0) {

		int value;
		bool succeed = System.Int32.TryParse(text, out value);

		if(succeed)
			return value;
		else
			return defValue;
	}

	#endregion


	public static bool toFloat(string text, out float output) {

		output = 0;
		bool succeed = System.Single.TryParse(text, out output);

		return succeed;
	}


	//매쓰아닌뒈
	public static string arrayToString(System.Array array, string seperator = ",") {

		string text = "";

		for(int i = 0; i < array.Length; ++i) {

			text += array.GetValue(i).ToString();

			if(i + 1 < array.Length) {
				text += seperator + " ";
			}
		}

		return text;
	}



	/// <summary>
	/// 16진수 형식의 컬러값을 RGBA 로 파싱해서 리턴
	/// </summary>
	/// <param name="hex"></param>
	/// <returns></returns>
	public static Color32 parseRGBA(string hex) {
		
		if(hex == null) {
			throw new System.ArgumentException("empty string parameter");
		}

		//
		if(hex.StartsWith("#")) {
			hex = hex.Substring(1, hex.Length-1);
		}
		
		//
		if(!(hex.Length == 8 || hex.Length == 6)) {
			throw new System.ArgumentException(string.Format("invalid format parameter = {0}", hex));
		}

		//hex 문자열이 맞는지 정규식 체크는 귀찮으니까 일단 패스

		Color32 color = Color.white;
		string strColor = null;
		byte temp = 0;

		//두자리씩 떼어내
		for(int i=0; i<hex.Length; i += 2) {

			strColor = hex.Substring(i, 2);
			temp = System.Convert.ToByte(strColor, 16);

			//순서대로 넣긩
			switch(i) {

				case 0:
					color.r = temp;
					break;

				case 2:
					color.g = temp;
					break;

				case 4:
					color.b = temp;
					break;

				case 6:
					color.a = temp;
					break;
			}
		}
		
		return color;
	}



	/// <summary>
	/// 16진수 형식의 컬러값을 ARGB 로 파싱해서 리턴
	/// </summary>
	/// <param name="hex"></param>
	/// <returns></returns>
	public static Color32 parseARGB(string hex) {


		if(hex == null) {
			throw new System.ArgumentException("empty string parameter");
		}

		//
		if(hex.StartsWith("#")) {
			hex = hex.Substring(1, hex.Length - 1);
		}

		//
		if(!(hex.Length == 8 || hex.Length == 6)) {
			throw new System.ArgumentException(string.Format("invalid format parameter = {0}", hex));
		}

		//hex 문자열이 맞는지 정규식 체크는 귀찮으니까 일단 패스

		Color32 color = Color.white;
		string strColor = null;
		byte temp = 0;

		//두자리씩 떼어내
		for(int i = 0; i < hex.Length; i += 2) {

			strColor = hex.Substring(i, 2);
			temp = System.Convert.ToByte(strColor, 16);

			//순서대로 넣긩
			switch(i) {

				case 0:
					color.a = temp;
					break;

				case 2:
					color.r = temp;
					break;

				case 4:
					color.g = temp;
					break;

				case 6:
					color.b = temp;
					break;
			}
		}

		return color;
	}
}
