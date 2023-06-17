using UnityEngine;
using System.Collections;

/// <summary>
/// 로그 출력 제어를 위해 추가한 랩핑 클래스
/// 
/// 안드로이드 로그출력 순서를 따라 v, d, i, w, e 순으로 레벨을 책정.
/// 어플리케이션 최초 진입시 Log.enabled 값을 조절해 로그를 노출 가능하며,
/// 에디터에서 사용시 LogWindow 를 통해 일부 로그만 노출이 가능하다. - 이거 잘 안될랑가
/// 
/// 2018.04.12 추가 : 로그로 전달된 메시지를 콜백받을수 있는 기능을 추가함
///		콜백받아 파일로 저장하거나 뭐 그런 용도
/// </summary>
public static class Log {

	//걍 안드로이드 레벨을 따랐다. ㅋㅋㅋ
	public enum Level {
		Verbose,
		Debug,
		Info,
		Warning,
		Error
	}

	private const string NORMAL_LOG_FORMAT = "[{0:S}] {1:S} : {2:S}";
	private const string COLOR_LOG_FORMAT = "<color=#{1:S}>[{0:S}]  {2:S} : {3:S}</color>";

	//로그출력 사용여부.
	public static bool enabled = true;

	//로그레벨 별 로그노출여부.
	public static bool showLogV = true;
	public static bool showLogD = true;
	public static bool showLogI = true;
	public static bool showLogW = true;
	public static bool showLogE = true;

#if UNITY_EDITOR
	public static bool isProSkin = UnityEditor.EditorGUIUtility.isProSkin;
#else
	public static bool isProSkin = false;
#endif

	//로그 콜백받고싶으면 추가가능
	public static event System.Action<Level, string, string> onLog;


	#region Log.v 

	public static void v(string msg) {
		v("Null", msg);
	}

	public static void v(string format, params object[] param) {
		v("Null", format, param);
	}

	public static void v(Object cls, string msg) {
		if(cls == null) {
			v(msg);
		} else {
			v(cls.GetType().ToString(), msg);
		}
	}

	public static void v(Object cls, string format, params object[] param) {
		if(cls == null) {
			v(format, param);
		} else {
			v(cls.GetType().ToString(), format, param);
		}
	}

	public static void v(string tag, string msg) {

		if((enabled && showLogD)) {
			Print(Level.Verbose, tag, msg);
		}
	}

	public static void v(string tag, string format, params object[] param) {

		if((enabled && showLogD)) {
			Print(Level.Verbose, tag, string.Format(format, param));
		}
	}

	#endregion

	#region Log.d

	public static void d(string msg) {
		d("Null", msg);
	}

	public static void d(string format, params object[] param) {
		d("Null", format, param);
	}

	public static void d(Object cls, string msg) {
		if(cls == null) {
			d(msg);
		} else {
			d(cls.GetType().ToString(), msg);
		}
	}

	public static void d(Object cls, string format, params object[] param) {
		if(cls == null) {
			d(format, param);
		} else {
			d(cls.GetType().ToString(), format, param);
		}
	}

	public static void d(string tag, string msg) {

		if(!(enabled && showLogD))
			return;

		Print(Level.Debug, tag, msg);
	}

	public static void d(string tag, string format, params object[] param) {
		d(tag, string.Format(format, param));
	}

	#endregion

	#region Log.i

	public static void i(string msg) {
		i("Null", msg);
	}

	public static void i(string format, params object[] param) {
		i("Null", format, param);
	}

	public static void i(Object cls, string msg) {
		if(cls == null) {
			i(msg);
		} else {
			i(cls.GetType().ToString(), msg);
		}
	}

	public static void i(Object cls, string format, params object[] param) {
		if(cls == null) {
			i(format, param);
		} else {
			i(cls.GetType().ToString(), format, param);
		}
	}

	public static void i(string tag, string msg) {

		if(!(enabled && showLogD))
			return;

		Print(Level.Info, tag, msg);
	}

	public static void i(string tag, string format, params object[] param) {
		i(tag, string.Format(format, param));
	}

	#endregion

	#region Log.w

	public static void w(string msg) {
		w("Null", msg);
	}

	public static void w(string format, params object[] param) {
		w(string.Format(format, param));
	}

	public static void w(Object cls, string msg) {
		if(cls == null) {
			w("Null", msg);
		} else {
			w(cls.GetType().ToString(), msg);
		}
	}

	public static void w(Object cls, string format, params object[] param) {
		if(cls == null) {
			w(format, param);
		} else {
			w(cls.GetType().ToString(), format, param);
		}
	}

	public static void w(string tag, string msg) {

		if(!(enabled && showLogD))
			return;

		Print(Level.Warning, tag, msg);
	}

	public static void w(string tag, string format, params object[] param) {
		w(tag, string.Format(format, param));
	}

	#endregion

	public static void e(string msg) {
		e("Null", msg);
	}
	public static void e(Object cls, string msg) {
		if(cls == null) {
			e("Null", msg);
		} else {
			e(cls.GetType().ToString(), msg);
		}
	}
	public static void e(string tag, string msg) {

		if(!(enabled && showLogD))
			return;

		Print(Level.Error, tag, msg);
	}
    
    public static void e(object message, Object cls) {

		if(cls == null) {
			e(message.ToString(), "null");
		} else {
			e(message.ToString(), cls.ToString());
		}
	}

	public static void warning(string tag, string msg) {

		//if(!(enabled && showLogD))
		//	return;

		Debug.LogWarningFormat(NORMAL_LOG_FORMAT, NOWTIME, tag, msg);
	}

	public static void error(string msg, bool isBreak = false) {

		//if(!(enabled && showLogD))
		//	return;

		Debug.LogErrorFormat(NORMAL_LOG_FORMAT, NOWTIME, "Exception", msg);

		if(isBreak) {
			Debug.Break();
		}
	}

	public static void error(string tag, string msg, bool isBreak = false) {

		//if(!(enabled && showLogD))
		//	return;

		Debug.LogErrorFormat(NORMAL_LOG_FORMAT, NOWTIME, tag, msg);

		if(isBreak) {
			Debug.Break();
		}
	}


    public static void exception(System.Exception e, string msg = null) {

        Print(Level.Error, null, string.Format("Message : {0}\nException Message : {1}\nStacktrace : {2}", msg, e.Message, e.StackTrace));
    }



	private static void Print(Level level, string tag, string msg) {

		if(onLog != null) {
			onLog(level, tag, msg);
		}

#if UNITY_EDITOR

		//컬러찾긔
		string hexColor = getLogColor(level);

		if(string.IsNullOrEmpty(hexColor)) {
			Debug.Log(string.Format(NORMAL_LOG_FORMAT, NOWTIME, tag, msg));
		} else {
			Debug.Log(string.Format(COLOR_LOG_FORMAT, NOWTIME, hexColor, tag, msg));
		}

#else
		Debug.Log (string.Format (NORMAL_LOG_FORMAT, NOWTIME, tag, msg));
#endif

	}


	/// <summary>
	/// 에디터 상황에 따른로그컬러 리턴
	/// </summary>
	/// <param name="level"></param>
	/// <returns></returns>
	private static string getLogColor(Level level) {

		if(isProSkin) {

			switch(level) {

				case Level.Verbose:
					//컬러없음
					break;

				case Level.Debug:
					return Colors.D_DARTSKIN;

				case Level.Info:
					return Colors.I_DARTSKIN;

				case Level.Warning:
					return Colors.W_DARTSKIN;

				case Level.Error:
					return Colors.E_DARTSKIN;
			}

		} else {

			switch(level) {

				case Level.Verbose:
					//컬러없음
					break;

				case Level.Debug:
					return Colors.D_LIGHTSKIN;

				case Level.Info:
					return Colors.I_LIGHTSKIN;

				case Level.Warning:
					return Colors.W_LIGHTSKIN;

				case Level.Error:
					return Colors.E_LIGHTSKIN;
			}
		}//eo if

		return null;
	}


	public static string NOWTIME {
		get {
			//return InfoManager.NowTime.ToString("yyyy.MM.dd HH:mm:ss");
			return System.DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.fff");
		}
	}


	//컬러들
	internal struct Colors {

		public const string D_LIGHTSKIN = "151591";
		public const string D_DARTSKIN = "8383ff";

		public const string I_LIGHTSKIN = "116011";
		public const string I_DARTSKIN = "22cc22";

		public const string W_LIGHTSKIN = "cc6622";
		public const string W_DARTSKIN = "cc6622";

		public const string E_LIGHTSKIN = "cc2222";
		public const string E_DARTSKIN = "cc2222";
	}
}
