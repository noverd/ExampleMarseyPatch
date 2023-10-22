﻿using System.Reflection;
using HarmonyLib;

public static class MarseyPatch
{
    public static string ReqAsm = "CC"; // This defines which assembly is required for this patch.
    // RC=Robust.Client,CS=Content.Shared,CC=Content.Client
    // You get the idea
    public static Assembly TargetAssembly = null; // This is defined by the MarseyPatcher, as it has access to the assemblies. 
    public static string Name = "Flash Overlay disabler";
    public static string Description = "Disables flash overlay.";
}

[HarmonyPatch]
public static class FlashOverlayPatch
{
    // This must return a MethodInfo for the function you're patching. In this case it's the function called "DrawOcclusionDepth" located in the "Robust.Client.Graphics.Clyde.Clyde" type.
    private static MethodBase TargetMethod() 
    {
        var FlashOverlay = MarseyPatch.TargetAssembly.GetType("Content.Client.Flash.FlashOverlay")!;
        return FlashOverlay.GetMethod("Draw", BindingFlags.NonPublic | BindingFlags.Instance);
    }

    // Prefix that will be executed before the original method
    // This is a simple "don't run this function at all" prefix, which returns false.
    [HarmonyPrefix]
    private static bool PrefSkip()
    {
        return false;
    }
}