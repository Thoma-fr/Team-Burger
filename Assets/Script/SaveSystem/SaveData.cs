using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public float masterLevel;
    public float musicLevel;
    public float sfxLevel;
    public float ambienceLevel;

    public SaveData() { }

    public SaveData(float masterlv, float musiclv, float sfxlv, float ambiencelv)
    {
        this.masterLevel = masterlv;
        this.musicLevel = musiclv;
        this.sfxLevel = sfxlv;
        this.ambienceLevel = ambiencelv;
    }
}
