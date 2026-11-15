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
            int previousStack = element.StackSize;
            var refund = access.SingleCopyCost(element, -1);
            if (refund == null)
                return false;

            bool granted = false;
            try
            {
                access.SetStackSize(element, previousStack - 1);
                access.Grant(refund, countAsIncome: false);
                granted = true;
            }
            catch
            {
                if (!granted && access.IsInActiveWorkspace(element))
                {
                    try { access.SetStackSize(element, previousStack); }
                    catch { }
                }

                throw;
            }

            access.RefreshLabel(view);
            ui.PlaySound(GameUiSound.TurnPage);
            access.ShowIncome(view, refund);
            access.ShowMessage(view, "Unstacked (x" + element.StackSize + ")", Color.white);
            return true;
        }
    }
}
