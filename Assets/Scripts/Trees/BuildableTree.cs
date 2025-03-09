using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableTree : Tree
{
    public Transform buildHangPosition;
    public bool isPreviewToBuild;

    public void CreatePreview(Vector3 rotation)
    {
        isPreviewToBuild = true;
        HangManager.OnCreatePreviewHang?.Invoke(buildHangPosition.position, rotation);
    }

    public void CreateHang(Vector3 rotation)
    {
        isPreviewToBuild = false;
        HangManager.OnCreateHang?.Invoke(this, rotation);
    }

    public void CancelPreview()
    {
        isPreviewToBuild = false;
    }
}
