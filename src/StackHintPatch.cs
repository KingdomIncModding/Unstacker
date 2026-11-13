using System;
using KingdomFoundation.Api;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Unstacker
{
    internal static class StackHintPatch
    {
        internal static void After(GameHookCall call)
        {
            if (UnstackerInput.IsUnstackChord)
                call.SetResult(false);
        }
    }
}
