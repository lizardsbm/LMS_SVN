using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;

public class ShortKey
{
       #region 씬이동 단축키

    [MenuItem("C_Owl/Scene/Prepare Go &1")]
    static void openPrepareScene()
    {
		EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
		EditorSceneManager.OpenScene("Assets/Scene/Prepare.unity");
    }

    [MenuItem("C_Owl/Scene/Prepare Play #&1")]
    static void playPrepareScene()
    {
        openPrepareScene();
        EditorApplication.isPlaying = true;
    }


	[MenuItem("C_Owl/Scene/Menu Go &2")]
	static void openMenuScene()
    {
		EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
		EditorSceneManager.OpenScene("Assets/Scene/Menu.unity");
	}

    [MenuItem("C_Owl/Scene/Menu Play #&2")]
    static void playMenuScene()
    {
        openMenuScene();
		EditorApplication.isPlaying = true;
	}


	[MenuItem("C_Owl/Scene/Story Go &3")]
	static void openStoryScene()
    {
		EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
		EditorSceneManager.OpenScene("Assets/Scene/Story.unity");
	}
	
	[MenuItem("C_Owl/Scene/Story Play #&3")]
    static void playStoryScene()
    {
        openLocalPlay();
		EditorApplication.isPlaying = true;
    }

    [MenuItem("C_Owl/Scene/Ingame Go &4")]
    static void openLocalPlay()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scene/Ingame.unity");
    }

    [MenuItem("C_Owl/Scene/Ingame Play #&4")]
    static void playLocalPlayScene()
    {
        openLocalPlay();
        EditorApplication.isPlaying = true;
    }

    [MenuItem("C_Owl/Scene/Server Go &6")]
    static void openGoogleTestPlay() {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scene/GooglePlayConnect.unity");
    }

    [MenuItem("C_Owl/Scene/Server Play #&6")]
    static void playGoogleTestScene() {
        openGoogleTestPlay();
        EditorApplication.isPlaying = true;
    }

    [MenuItem("C_Owl/Scene/Menu_Backup Go &7")]
    static void openMenu_BackupScene()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scene/Menu_Backup.unity");
    }

    [MenuItem("C_Owl/Scene/Menu_Backup Play #&7")]
    static void playMenu_BackupScene()
    {
        openMenu_BackupScene();
        EditorApplication.isPlaying = true;
    }
    #endregion
}
