using System;
using KingdomFoundation.Api;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Unstacker
{
    internal static class BuildingClickPatch
    {
        internal static void Before(GameHookCall call)
        {
            var eventData = call.Argument<PointerEventData>(0);
            if (call.IsCancelled || eventData == null || eventData.button != PointerEventData.InputButton.Left || !UnstackerInput.IsUnstackChord)
                return;
            try
            {
                UnstackOperation.TryUnstack(call.View);
            }
            catch (Exception error)
            {
                UnstackerPlugin.Log?.LogError("Could not unstack building: " + error);
            }

            // Consume the chord even at size one, where native Ctrl-click would buy a level.
            call.Cancel();
        }
    }
}
