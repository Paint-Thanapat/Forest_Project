using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeInteractUIManager : SceneSingleton<TreeInteractUIManager>
{
    [SerializeField] GameObject panel;
    [SerializeField] Button buildButton;
    [SerializeField] Button destroyButton;

    public BuildableTree interactTree;

    public override void Awake()
    {
        base.Awake();

        buildButton.onClick.AddListener(() =>
        {
            HangManager.OnCreatePreviewHang?.Invoke(interactTree.buildHangPosition.position, GameManager.Instance.playerCharacter.transform.eulerAngles);

            SetActivePanel(false);
        });

        destroyButton.onClick.AddListener(() =>
        {
            interactTree.CutTheTree();

            SetActivePanel(false);
        });

        SetActivePanel(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetActivePanel(false);
        }
    }

    public void SetActivePanel(bool isActive)
    {
        panel.SetActive(isActive);

        if (isActive)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void SetInteractWindow(BuildableTree tree)
    {
        interactTree = tree;
    }
}
