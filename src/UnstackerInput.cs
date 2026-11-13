using System;
using KingdomFoundation.Api;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Unstacker
{
    internal static class UnstackerInput
    {
        internal static bool IsUnstackChord => Keyboard.current != null && Keyboard.current.ctrlKey.isPressed && Keyboard.current.shiftKey.isPressed;
    }
}
