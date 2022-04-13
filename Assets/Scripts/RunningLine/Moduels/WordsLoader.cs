using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class WordsLoader : MonoBehaviour
{
    string textArrayLoaded;

    private void Awake()
    {
        StartLoadTextFileFromAdressable();
    }
    
    async void StartLoadTextFileFromAdressable()
    {
        AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>("RunningLineText");
        await handle.Task;

        if(handle.Status == AsyncOperationStatus.Succeeded)
        {
            textArrayLoaded = handle.Result.text;

            Addressables.Release(handle);
        }
    }

    public string[] GetTextFile()
    {
        return textArrayLoaded.Split(new Char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
    }

    public void ClearMemmory() => textArrayLoaded = "";

}
