// -----------------------------------------------------------------------
// <copyright file="Plugin.cs" company="Dottore112 and Build">
// Copyright (c) Dottore112 and Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeslaConfiguration
{
    using System;
    using Exiled.API.Features;
    using PlayerEvents = Exiled.Events.Handlers.Player;
    using Scp079Events = Exiled.Events.Handlers.Scp079;
    using ServerEvents = Exiled.Events.Handlers.Server;

    /// <summary>
    /// The main plugin class.
    /// </summary>
    public class Plugin : Plugin<Config>
    {
        private EventHandlers eventHandlers;

        /// <inheritdoc />
        public override string Name { get; } = "TeslaConfiguration";

        /// <inheritdoc />
        public override string Prefix { get; } = "TeslaConfiguration";

        /// <inheritdoc />
        public override Version RequiredExiledVersion { get; } = new Version(4, 1, 5);

        /// <inheritdoc />
        public override Version Version { get; } = new Version(2, 0, 0);

        /// <inheritdoc />
        public override void OnEnabled()
        {
            eventHandlers = new EventHandlers(this);
            PlayerEvents.Dying += eventHandlers.OnDying;
            PlayerEvents.PickingUpItem += eventHandlers.OnPickingUpItem;
            PlayerEvents.TriggeringTesla += eventHandlers.OnTriggeringTesla;
            Scp079Events.InteractingTesla += eventHandlers.OnInteractingTesla;
            ServerEvents.WaitingForPlayers += eventHandlers.OnWaitingForPlayers;
        }

        /// <inheritdoc />
        public override void OnDisabled()
        {
            PlayerEvents.Dying -= eventHandlers.OnDying;
            PlayerEvents.PickingUpItem -= eventHandlers.OnPickingUpItem;
            PlayerEvents.TriggeringTesla -= eventHandlers.OnTriggeringTesla;
            Scp079Events.InteractingTesla -= eventHandlers.OnInteractingTesla;
            ServerEvents.WaitingForPlayers -= eventHandlers.OnWaitingForPlayers;
            eventHandlers = null;
            base.OnDisabled();
        }
    }
}