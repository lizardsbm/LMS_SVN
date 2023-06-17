using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// 파일 읽기 쓰기 관련
/// </summary>
public partial class Utils{



	/// <summary>
	/// 파일이 존재하는지 리턴
	/// </summary>
	/// <param name="path"></param>
	/// <returns></returns>
	public static bool ExistFile(string path) {
		return File.Exists(path);
	}



	/// <summary>
	/// 지정경로의 파일을 읽어 내용을 리턴
	/// </summary>
	/// <param name="path"></param>
	/// <param name="txt"></param>
	/// <returns></returns>
	public static bool Read(string path, out string txt) {
		
		txt = null;

		//
		bool exist = File.Exists(path);
		if(!exist)
			return false;


		try {
			FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
			BinaryReader reader = new BinaryReader(fileStream);

			txt = reader.ReadString();
			reader.Close();
			fileStream.Close();
			return true;

		} catch {
			return false;
		}
	}



	/// <summary>
	/// 지정 저장소에 텍스트파일을 저장
	/// </summary>
	/// <param name="path">풀패스. 경로+파일명+확장자</param>
	/// <param name="text">저장할 문자열</param>
	/// <param name="deleteOnExist">이미 파일이 존재할 경우 지우고 쓸것인가. false인데 파일이 존재하면 처리를 중지한다.</param>
	/// <returns>성공여부</returns>
	public static bool WriteTextFile(string path, string text, bool deleteOnExist = true) {

		bool exist = File.Exists(path);

		//파일이 존재하는데 delete 하지 않도록 설정되었다면 처리 중지.
		if(exist) {

			if(deleteOnExist) {
				DeleteFile(path);

			} else {
				return false;
			}
		}

		try {
			
			FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
			BinaryWriter writer = new BinaryWriter(fileStream);

			writer.Write(text);
			writer.Close();

			return true;
		} catch(UnityEngine.UnityException e) {
			Log.e("Utils.IO.WriteTextFile", e.Message);
			return false;
		}
	}



	/// <summary>
	/// 지정 저장소에 텍스트파일을 저장
	/// </summary>
	/// <param name="path">풀패스. 경로+파일명+확장자</param>
	/// <param name="text">저장할 문자열</param>
	/// <param name="deleteOnExist">이미 파일이 존재할 경우 지우고 쓸것인가. false인데 파일이 존재하면 처리를 중지한다.</param>
	/// <returns>성공여부</returns>
	public static bool WriteTextFileStream(string path, string text, bool deleteOnExist = true) {

		bool exist = File.Exists(path);

		//파일이 존재하는데 delete 하지 않도록 설정되었다면 처리 중지.
		if(exist) {

			if(deleteOnExist) {
				DeleteFile(path);

			} else {
				return false;
			}
		}

		try {
			
			FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
			StreamWriter writer = new StreamWriter(fileStream, System.Text.Encoding.UTF8);

			writer.Write(text);
			writer.Flush();
			writer.Close();

			/*
			int length = text.Length;
			writer.Write((byte)length);

			byte[] temp = System.Text.Encoding.Unicode.GetBytes(text);
			writer.Write(temp, 0, length);
			writer.Close();
			*/

			return true;
		} catch(UnityEngine.UnityException e) {
			Log.e("Utils.IO.WriteTextFile", e.Message);
			return false;
		}
	}



	/// <summary>
	/// 특정 경로의 파일 삭제
	/// </summary>
	/// <param name="path"></param>
	/// <returns>삭제 성공 여부</returns>
	public static bool DeleteFile(string path) {

		try {
			File.Delete(path);
			return true;

		} catch(IOException e) {
			Log.e("Utils.IO.DeleteQuickStartData", e.Message);
			return false;
		}
	}




	
	public static bool isEmail(string text) {
		return Regex.IsMatch(text, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
	}





	/// <summary>
	/// 텍스처를 특정 배율로 ㅇㅇ
	/// </summary>
	/// <param name="source"></param>
	/// <param name="resized"></param>
	/// <param name="multi"></param>
	/// <returns></returns>
	public static bool resizeTexture(Texture2D source, out Texture2D resized, float multi) {

		return resizeTexture(source, out resized, (int)(source.width * multi), (int)(source.height * multi));
	}
	

	/// <summary>
	/// 지정된 사이즈로 텍스처를 리사이징 한다
	/// </summary>
	/// <param name="source"></param>
	/// <param name="resized"></param>
	/// <param name="newX"></param>
	/// <param name="newY"></param>
	/// <returns></returns>
	public static bool resizeTexture(Texture2D source, out Texture2D resized, int newX, int newY) {

		//http://lhh3520.tistory.com/300
		
		//전체 픽셀을 가져옴
		Color[] aSourceColor = source.GetPixels(0);
		Vector2 vSourceSize = new Vector2(source.width, source.height);
		
		//
		float fNewX = newX;
		float fNewY = newY;

		int xLength = newX * newY;
		Color[] aColor = new Color[xLength];
		Vector2 vPixelSize = new Vector2(vSourceSize.x / fNewX, vSourceSize.y / fNewY);

		//
		Vector2 vCenter = new Vector2();

		for(int ii = 0; ii < xLength; ++ii) {

			float xX = ii % fNewX;
			float xY = Mathf.Floor(ii / fNewX);

			vCenter.x = (xX / fNewX) * vSourceSize.x;
			vCenter.y = (xY / fNewY) * vSourceSize.y;

			int xXFrom = (int)Mathf.Max(Mathf.Floor(vCenter.x - (vPixelSize.x * 0.5f)), 0);
			int xXTo = (int)Mathf.Min(Mathf.Ceil(vCenter.x + (vPixelSize.x * 0.5f)), vSourceSize.x);
			int xYFrom = (int)Mathf.Max(Mathf.Floor(vCenter.y - (vPixelSize.y * 0.5f)), 0);
			int xYTo = (int)Mathf.Min(Mathf.Ceil(vCenter.y + (vPixelSize.y * 0.5f)), vSourceSize.y);

			//
			float xGridCount = 0;

			for(int iy = xYFrom; iy < xYTo; ++iy) {
				for(int ix = xXFrom; ix < xXTo; ++ix) {

					//get color
					aColor[ii] += aSourceColor[(int)((iy * vSourceSize.x) + ix)];
					//sum
					++xGridCount;
				}
			}// eo for

			aColor[ii] /= xGridCount;

		}//eo outer for

		//
		resized = new Texture2D(newX, newY, TextureFormat.RGBA32, false);
		resized.SetPixels(aColor);
		resized.Apply();
		return true;
	}

}
