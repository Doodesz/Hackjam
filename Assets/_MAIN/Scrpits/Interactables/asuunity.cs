using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class asuunity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //EditorUtility.SetDirty(WhateverItWasThatYouChangedDefinesWith);
        AssetDatabase.SaveAssets();

        AssetDatabase.Refresh();
    }
}
