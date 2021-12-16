// -----------------------------------------------------------------------
// <copyright file="EventHandlers.cs" company="Dottore112 and Build">
// Copyright (c) Dottore112 and Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeslaConfiguration
{
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;

    /// <summary>
    /// Handles events derived from <see cref="Exiled.Events.Handlers"/>.
    /// </summary>
    public class EventHandlers
    {
        private readonly Plugin plugin;
        private int teslaActivations;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlers"/> class.
        /// </summary>
        /// <param name="plugin">An instance of the <see cref="Plugin"/> class.</param>
        public EventHandlers(Plugin plugin) => this.plugin = plugin;

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnDying(DyingEventArgs)"/>
        public void OnDying(DyingEventArgs ev)
        {
            if (ev.Handler.Type == DamageType.Tesla && plugin.Config.EraseInventory)
            {
                ev.Target.ClearInventory();
            }
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnPickingUpItem(PickingUpItemEventArgs)"/>
        public void OnPickingUpItem(PickingUpItemEventArgs ev)
        {
            if (plugin.Config.BypassTeslaItems != null &&
                plugin.Config.BypassTeslaItems.Contains(ev.Pickup.Type))
            {
                Broadcast broadcast = plugin.Config.PickItemBC;
                broadcast.Content = broadcast.Content.Replace("{type}", ev.Pickup.Type.ToString());
                ev.Player.Broadcast(broadcast);
            }
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnTriggeringTesla(TriggeringTeslaEventArgs)"/>
        public void OnTriggeringTesla(TriggeringTeslaEventArgs ev)
        {
            if (plugin.Config.TeslaDisableAtNoScp && Player.Get(Team.SCP).IsEmpty())
            {
                ev.IsTriggerable = false;
                return;
            }

            if (ev.Player.CurrentItem != null &&
                plugin.Config.BypassTeslaItems != null &&
                plugin.Config.BypassTeslaItems.Contains(ev.Player.CurrentItem.Type))
            {
                ev.IsTriggerable = false;
                return;
            }

            if (plugin.Config.DisabledRoles != null &&
                plugin.Config.DisabledRoles.TryGetValue(ev.Player.Role, out string broadcast))
            {
                ev.IsTriggerable = false;
                ev.Player.Broadcast(plugin.Config.DisabledBroadcastTime, broadcast);
            }
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Scp079.OnInteractingTesla(InteractingTeslaEventArgs)"/>
        public void OnInteractingTesla(InteractingTeslaEventArgs ev)
        {
            ev.AuxiliaryPowerCost = plugin.Config.TeslaCost079;

            if (!plugin.Config.IsTesla079LimitEnabled)
                return;

            if (teslaActivations >= plugin.Config.Tesla079Limit)
            {
                ev.IsAllowed = false;
                return;
            }

            ev.IsAllowed = true;
            teslaActivations++;
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Server.OnWaitingForPlayers()"/>
        public void OnWaitingForPlayers()
        {
            teslaActivations = 0;
        }
    }
}