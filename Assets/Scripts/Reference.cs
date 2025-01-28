using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Reference
{
    // internal static Camera MainCamera => _mainCamera != null ? _mainCamera : _mainCamera = GameObject.FindObjectOfType<TagMainCamera>().GetComponent<Camera>();
    // private static Camera _mainCamera;
    //
    internal static GameModel GameModel;
    internal static List<Fetus> AllFetus = new List<Fetus>();
}
