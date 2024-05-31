using System;
using System.IO;
using System.Threading.Tasks;
using Code;
using Code.SO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CDNLoader : MonoBehaviour
{
    [SerializeField] private RawImage _image;
    [SerializeField] private Config config;

    // private async void Stop()
    // {
    //     const string url = "http://localhost/test/levels.json";
    //     await ReadJsonUrl(url);
    // }

    // private async Task ReadJsonUrl(string url)
    // {
    //     var request = UnityWebRequest.Get(url);
    //     var response = await request.SendWebRequest();
    //
    //     if (response == UnityWebRequest.Result.Success)
    //     {
    //         var result = JsonUtility.FromJson<Levels>(request.downloadHandler.text);
    //         Debug.Log(request.downloadHandler.text);
    //     }
    // }


    // private void Start()
    // {
    //     var jsonLevels = JsonUtility.ToJson(_levelsConfig.levels, true);
    //
    //     // write file
    //
    //     var fileName = "levels.json";
    //     StreamWriter writer = null;
    //     try
    //     {
    //         writer = File.CreateText(fileName);
    //         writer.Write(jsonLevels);
    //     }
    //     catch (Exception e)
    //     {
    //         Debug.Log(e.Message);
    //     }
    //     finally
    //     {
    //         writer?.Close();
    //         Debug.Log("Completed!");
    //     }
    //
    //     EditorApplication.ExitPlaymode();
    // }

    // private void ReadImage()
    // {
    //     const string url = "https://unitydev17.github.io/assets/img/portfolio/Cutality/icon.png";
    //     ReadUrl(url);
    // }

    // private async void ReadUrl(string url)
    // {
    //     var request = UnityWebRequestTexture.GetTexture(url);
    //     var response = await request.SendWebRequest();
    //
    //     if (response == UnityWebRequest.Result.Success)
    //     {
    //         _image.texture = DownloadHandlerTexture.GetContent(request);
    //     }
    //
    //     Debug.Log(response);
    // }
}