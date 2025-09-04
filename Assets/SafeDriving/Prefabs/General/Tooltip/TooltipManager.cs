//========= Copyright 2016-2024, HTC Corporation. All rights reserved. ===========

using UnityEngine;
using HTC.UnityPlugin.Vive;

public class TooltipManager : MonoBehaviour
{
    public enum Mode
    {
        Default,
        Teleport,
        Menu,
    }

    public Mode initMode = Mode.Teleport;
    public DefaultTooltipRenderer tooltipRenderer_Right;
    public DefaultTooltipRenderDataAsset teleportTooltip_Right;
    public DefaultTooltipRenderDataAsset menuTooltip_Right;
    public DefaultTooltipRenderer tooltipRenderer_Left;
    public DefaultTooltipRenderDataAsset teleportTooltip_Left;
    public DefaultTooltipRenderDataAsset menuTooltip_Left;
    //public GameObject teleportPointers;
    //public GameObject selectColorUI;
    private Mode currentMode;

    private void Awake()
    {
        //EnterMode(initMode);
        tooltipRenderer_Right.SetTooltipData(menuTooltip_Right);
        tooltipRenderer_Left.SetTooltipData(menuTooltip_Right);
    }

    public void SwitchToTeleportMode() { SwitchMode(Mode.Teleport); }

    public void SwitchToMenuMode() { SwitchMode(Mode.Menu); }

    private void SwitchMode(Mode mode)
    {
        if (currentMode != mode)
        {
            ExitMode(currentMode);
            currentMode = mode;
            EnterMode(mode);
        }
    }

    private void EnterMode(Mode mode)
    {
        switch (mode)
        {
            case Mode.Default: break;
            case Mode.Teleport: EnterTeleportMode(); break;
            case Mode.Menu: EnterMenuMode(); break;
        }
    }

    private void ExitMode(Mode mode)
    {
        switch (mode)
        {
            case Mode.Default: break;
            case Mode.Teleport: ExitTeleportMode(); break;
            case Mode.Menu: ExitMenuMode(); break;
        }
    }

    private void EnterTeleportMode()
    {
        ViveInput.AddListenerEx(HandRole.RightHand, ControllerButton.Menu, ButtonEventType.Down, SwitchToMenuMode);
        //teleportPointers.SetActive(true);
        tooltipRenderer_Right.SetTooltipData(teleportTooltip_Right);

        ViveInput.AddListenerEx(HandRole.LeftHand, ControllerButton.Menu, ButtonEventType.Down, SwitchToMenuMode);
        //teleportPointers.SetActive(true);
        tooltipRenderer_Left.SetTooltipData(teleportTooltip_Left);
    }

    private void ExitTeleportMode()
    {
        ViveInput.RemoveListenerEx(HandRole.RightHand, ControllerButton.Menu, ButtonEventType.Down, SwitchToMenuMode);
        //teleportPointers.SetActive(false);
        tooltipRenderer_Right.ClearTooltipData();

        ViveInput.RemoveListenerEx(HandRole.LeftHand, ControllerButton.Menu, ButtonEventType.Down, SwitchToMenuMode);
        //teleportPointers.SetActive(false);
        tooltipRenderer_Left.ClearTooltipData();
    }

    private void EnterMenuMode()
    {
        ViveInput.AddListenerEx(HandRole.RightHand, ControllerButton.Menu, ButtonEventType.Down, SwitchToTeleportMode);
        //selectColorUI.SetActive(true);
        tooltipRenderer_Right.SetTooltipData(menuTooltip_Right);

        ViveInput.AddListenerEx(HandRole.LeftHand, ControllerButton.Menu, ButtonEventType.Down, SwitchToTeleportMode);
        //selectColorUI.SetActive(true);
        tooltipRenderer_Left.SetTooltipData(menuTooltip_Right);
    }

    private void ExitMenuMode()
    {
        ViveInput.RemoveListenerEx(HandRole.RightHand, ControllerButton.Menu, ButtonEventType.Down, SwitchToTeleportMode);
        //selectColorUI.SetActive(false);
        tooltipRenderer_Right.ClearTooltipData();

        ViveInput.RemoveListenerEx(HandRole.LeftHand, ControllerButton.Menu, ButtonEventType.Down, SwitchToTeleportMode);
        //selectColorUI.SetActive(false);
        tooltipRenderer_Left.ClearTooltipData();
    }
}
