using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangManager : SceneSingleton<HangManager>
{
    [SerializeField] GameObject hangPreviewObj;
    [SerializeField] GameObject hangPrefab;
    public static System.Action<Vector3, Vector3> OnCreatePreviewHang = delegate { };
    public static System.Action<Vector3> OnUpdatePreviewHang = delegate { };
    public static System.Action OnCancelPreviewHang = delegate { };
    public static System.Action<BuildableTree, Vector3> OnCreateHang = delegate { };

    public override void Awake()
    {
        base.Awake();

        OnCreatePreviewHang += OnCreatePreviewHangResp;
        OnUpdatePreviewHang += OnUpdatePreviewHangResp;
        OnCancelPreviewHang += OnCancelPreviewHangResp;

        OnCreateHang += OnCreateHangResp;

        hangPreviewObj.SetActive(false);
    }

    private void OnDestroy()
    {
        OnCreatePreviewHang -= OnCreatePreviewHangResp;
        OnUpdatePreviewHang -= OnUpdatePreviewHangResp;
        OnCancelPreviewHang -= OnCancelPreviewHangResp;

        OnCreateHang -= OnCreateHangResp;
    }

    private void OnCreatePreviewHangResp(Vector3 position, Vector3 rotation)
    {
        hangPreviewObj.transform.position = position;
        hangPreviewObj.transform.rotation = Quaternion.Euler(rotation) * Quaternion.Euler(0, 180, 0); ;
        hangPreviewObj.SetActive(true);
    }

    private void OnUpdatePreviewHangResp(Vector3 rotation)
    {
        hangPreviewObj.transform.rotation = Quaternion.Euler(rotation) * Quaternion.Euler(0, 180, 0); ;
    }

    private void OnCancelPreviewHangResp()
    {
        hangPreviewObj.SetActive(false);
    }

    private void OnCreateHangResp(BuildableTree tree, Vector3 rotation)
    {
        hangPreviewObj.SetActive(false);
        GameObject temp_hang = Instantiate(hangPrefab, tree.buildHangPosition);
        temp_hang.transform.rotation = Quaternion.Euler(rotation) * Quaternion.Euler(0, 180, 0); ;
    }
}
