using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : BaseSingleton<SoundManager>
{
    private const float FADE_TIME = 0.5f;

    private Dictionary<UISound, AudioClip> mDicUISound = new Dictionary<UISound, AudioClip>();

    private Dictionary<BGMSound, AudioClip> mDicBGMSound = new Dictionary<BGMSound, AudioClip>();

    private Dictionary<EAXSound, AudioClip> mDicEAXSound = new Dictionary<EAXSound, AudioClip>();

    public AudioSource _bgmSource;

    public AudioSource _eaxSource;

    public AudioSource _source;

    public override void init() {
        base.init();

        obj.name = "SoundManager";

        GameObject go = new GameObject("BGMSource");
        go.transform.parent = this.transform;

        _bgmSource = Utils.addComponent<AudioSource>(go);
        _bgmSource.enabled = false;
        _bgmSource.volume = 1;
        _bgmSource.playOnAwake = false;

        go = new GameObject("EAXSource");
        go.transform.parent = this.transform;

        _eaxSource = Utils.addComponent<AudioSource>(go);
        _eaxSource.enabled = false;
        _eaxSource.volume = 1;
        _eaxSource.playOnAwake = false;

        go = new GameObject("AudioSource");
        go.transform.parent = this.transform;

        _source = Utils.addComponent<AudioSource>(go);
        _source.enabled = false;
        _source.volume = 1;
        _source.playOnAwake = false;

        loadUISound();
        loadBGMSound();
        loadEAXSound();
    }

    private void loadUISound()
    {
        AudioClip clips;
        UISound uiSound = UISound.None;

        string path;
        string fileName;

        int enumCount = Enum.GetNames(typeof(UISound)).Length;

        for (int i = 0; i < enumCount; ++i)
        {
            fileName = ((UISound)i).ToString();

            path = string.Format("{0}{1}", ResPath.UI_SOUND, fileName);
            Utils.toEnum(fileName, ref uiSound);

            clips = Utils.loadRes<AudioClip>(path);

            mDicUISound.Add(uiSound, clips);
        }
    }

    private void loadBGMSound() {

        AudioClip clips;
        BGMSound bgmSound = BGMSound.None;

        string path;
        string fileName;

        int enumCount = Enum.GetNames(typeof(BGMSound)).Length;

        for (int i = 0; i < enumCount; ++i) {

            fileName = ((BGMSound)i).ToString();

            path = string.Format("{0}{1}", ResPath.BGM_SOUND, fileName);
            Utils.toEnum(fileName, ref bgmSound);

            clips = Utils.loadRes<AudioClip>(path);

            mDicBGMSound.Add(bgmSound, clips);
        }
    }


    private void loadEAXSound() {

        AudioClip clips;
        EAXSound eaxSound = EAXSound.None;

        string path;
        string fileName;

        int enumCount = Enum.GetNames(typeof(EAXSound)).Length;

        for (int i = 0; i < enumCount; ++i) {

            fileName = ((EAXSound)i).ToString();

            path = string.Format("{0}{1}", ResPath.EAX_SOUND, fileName);
            Utils.toEnum(fileName, ref eaxSound);

            clips = Utils.loadRes<AudioClip>(path);

            mDicEAXSound.Add(eaxSound, clips);
        }
    }

    public void playEAXSound(string exa) {

        if(_eaxSource == null) {
            return;
        }
        _eaxSource.volume = 1;

        _eaxSource.enabled = true;
        _eaxSource.clip = mDicEAXSound[getEAXEnumSound(exa)];
        _eaxSource.Play();
    }

    public void playBGM(string bgm)
    {
        if(_bgmSource == null) {
            return;
        }

        _bgmSource.volume = 1;

        _bgmSource.enabled = true;
        _bgmSource.clip = mDicBGMSound[getBGMEnumSound(bgm)];
        _bgmSource.Play();
    }

    public void fadeOutEAX() {
        StartCoroutine(startEAXFadeOut());
    }

    private IEnumerator startEAXFadeOut() {
        float time = 0;

        while (time < FADE_TIME) {

            time += Time.deltaTime;

            _eaxSource.volume = Mathf.Lerp(1, 0, time / FADE_TIME);
            yield return null;
        }

        _eaxSource.enabled = false;
    }


    public void fadeOutBGM() {
        StartCoroutine(startBGMFadeOut());
    }

    private IEnumerator startBGMFadeOut() {
        float time = 0;

        while(time < FADE_TIME) {

            time += Time.deltaTime;

            _bgmSource.volume = Mathf.Lerp(1, 0, time / FADE_TIME);
            yield return null;
        }

        _bgmSource.enabled = false;
    }

    public void playUISound(string _uiSound)
    {
        if(_source == null) {
            return;
        }

        _source.enabled = true;
        _source.clip = mDicUISound[getUIEnumSound(_uiSound)];
        _source.Play();
    }

    private UISound getUIEnumSound(string name) {

        UISound enums = UISound.None;

        Utils.toEnum(name, ref enums);

        return enums;
    }

    private BGMSound getBGMEnumSound(string name) {

        BGMSound enums = BGMSound.None;

        Utils.toEnum(name, ref enums);

        return enums;
    }

    private EAXSound getEAXEnumSound(string name) {

        EAXSound enums = EAXSound.None;

        Utils.toEnum(name, ref enums);

        return enums;
    }

    public void setUIMute(bool isMute) {
        _source.mute = isMute;
    }

    public void setBGMMute(bool isMute) {
        _bgmSource.mute = isMute;
        _eaxSource.mute = isMute;
    }

}
