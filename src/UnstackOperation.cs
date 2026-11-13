using KingdomFoundation.Api;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Unstacker
{
    internal static class UnstackOperation
    {
        internal static bool TryUnstack(Component view)
        {
            var access = FoundationApi.WorkspaceAccess;
            var ui = FoundationApi.UiAccess;
            var element = access.FromView(view);
            if (element == null || !element.Stackable || element.StackSize <= 1
                || !access.IsInActiveWorkspace(element) || !FoundationApi.View.IsReady
                || ui.PointerTraverseDistance > 50f || !EventSystem.current
                || !ui.Root || ui.IsDeconstructionVisible || !ui.IsDragDropReady || ui.IsDragDropBusy
                || ui.IsPointerOverButton || ui.IsPointerOverPort) return false;

            // Feature policy stays in this mod: reverse one purchase, keep upgrades,
            // grant the refund and present feedback through Foundation primitives.
            var refund = access.SingleCopyCost(element, -1);
            access.SetStackSize(element, element.StackSize - 1);
            access.Grant(refund, countAsIncome: false);
            access.RefreshLabel(view);
            ui.PlaySound(GameUiSound.TurnPage);
            access.ShowIncome(view, refund);
            access.ShowMessage(view, "Unstacked (x" + element.StackSize + ")", Color.white);
            return true;
        }

        [System.Obsolete("Deprecated: last standalone implementation worked with game build 1.0.14. Stack visuals are maintained by Foundation WorkspaceAccess.SetStackSize. Scheduled for removal in the next Unstacker release.", false)]
        private static void RefreshStackVisuals(Component view)
        {
            var access = FoundationApi.WorkspaceAccess;
            var element = access.FromView(view);
            if (element != null) access.SetStackSize(element, element.StackSize);
        }
    }
}
