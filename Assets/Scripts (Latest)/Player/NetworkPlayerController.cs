using System.Collections;
using System.Collections.Generic;
using Lightbug.CharacterControllerPro.Core;
using Lightbug.CharacterControllerPro.Demo;
using Lightbug.CharacterControllerPro.Implementation;
using Photon.Pun;
using UnityEngine;

public class NetworkPlayerController : MonoBehaviour
{
    [SerializeField] CharacterActor characterActor;
    [SerializeField] CharacterStateController characterStateController;
    [SerializeField] Camera3D mainCamera;
    public PhotonView PV { get; private set; }

    [SerializeField] LayerMask treeInteractLayer;
    BuildableTree interactingBuildableTree;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        PV = GetComponent<PhotonView>();

        if (PV.IsMine)
        {
            characterActor.enabled = true;
            characterStateController.enabled = true;
            characterStateController.ExternalReference = mainCamera.transform;

            GameManager.Instance.playerCharacter = this.gameObject;

            mainCamera.transform.parent = null;
            mainCamera.Initialize(characterActor.gameObject.transform.GetChild(0));
        }
        else
        {
            characterActor.enabled = false;
            characterStateController.enabled = false;

            DestroyImmediate(mainCamera.gameObject);
        }
    }

    private void Update()
    {
        if (!PV.IsMine) return;

        if (Input.GetMouseButtonDown(1))
        {
            if (interactingBuildableTree != null)
            {
                HangManager.OnCancelPreviewHang?.Invoke();
                interactingBuildableTree = null;
            }

            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out RaycastHit hit, 15f, treeInteractLayer))
            {
                BuildableTree temp_tree = hit.transform.GetComponent<BuildableTree>();
                if (temp_tree != null)
                {
                    if (!temp_tree.isPreviewToBuild)
                    {
                        // * Open UI
                        TreeInteractUIManager.Instance.interactTree = temp_tree;
                        TreeInteractUIManager.Instance.SetActivePanel(true);

                        interactingBuildableTree = temp_tree;
                    }
                    else
                    {
                        // * 
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (interactingBuildableTree != null)
            {
                HangManager.OnCreateHang?.Invoke(interactingBuildableTree, gameObject.transform.eulerAngles);
                interactingBuildableTree = null;
            }
        }
    }
}
