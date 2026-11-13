using BepInEx;
using BepInEx.Logging;
using System.Collections.Generic;
using KingdomFoundation.Api;

namespace Unstacker
{
    [BepInPlugin(PluginId, "Unstacker", PluginVersion)]
    [BepInDependency("urakka.kingdominc.foundation", "0.14.0")]
    public sealed class UnstackerPlugin : BaseUnityPlugin
    {
        public const string PluginId = "urakka.kingdominc.unstacker";
        public const string PluginVersion = "0.1.1";

        internal static ManualLogSource Log;
        private FoundationModContext context;
        private readonly List<System.IDisposable> hooks = new List<System.IDisposable>();

        private void Awake()
        {
            Log = Logger;
            context = FoundationApi.ForMod(PluginId);
            var settings = context.Settings("Unstacker", "Remove a level from a stacked building.");
            settings.AddText("Hold Ctrl + Shift and left-click a stacked building to remove one level.");
            settings.AddText("Refunds the current price of the removed building. Upgrades and connections stay in place.");
            hooks.Add(FoundationApi.GameHooks.Subscribe(PluginId, GameHookPoints.BuildingClick, before: BuildingClickPatch.Before));
            hooks.Add(FoundationApi.GameHooks.Subscribe(PluginId, GameHookPoints.BuildingCanStack, after: StackHintPatch.After));
            Logger.LogInfo("Unstacker loaded. Ctrl+Shift+left-click removes one stack level and refunds its building cost.");
        }

        private void OnDestroy()
        {
            foreach (var hook in hooks) hook.Dispose();
            hooks.Clear();
            context?.Dispose();
            Log = null;
        }
    }
}
