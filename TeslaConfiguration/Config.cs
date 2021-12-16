// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Dottore112 and Build">
// Copyright (c) Dottore112 and Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeslaConfiguration
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Exiled.API.Features;
    using Exiled.API.Interfaces;

    /// <inheritdoc />
    public class Config : IConfig
    {
        /// <inheritdoc />
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets a collection of roles and the broadcasts to play when they approach a tesla gate when it's disabled for them.
        /// </summary>
        [Description("A collection of roles and the broadcasts to play when they approach a tesla gate when it's disabled for them.")]
        public Dictionary<RoleType, string> DisabledRoles { get; set; } = new Dictionary<RoleType, string>
        {
            { RoleType.ChaosConscript, "<b><color=green> TESLA GATE DISABLED FOR CHI </color></b>" },
        };

        /// <summary>
        /// Gets or sets the amount of time a broadcast for a role in <see cref="DisabledRoles"/> will play.
        /// </summary>
        [Description("The amount of time a broadcast for a role in disabled_roles will play.")]
        public ushort DisabledBroadcastTime { get; set; } = 3;

        /// <summary>
        /// Gets or sets the items that will disable a tesla gate when held.
        /// </summary>
        [Description("The items that will disable a tesla gate when held.")]
        public ItemType[] BypassTeslaItems { get; set; } =
        {
            ItemType.Coin,
        };

        /// <summary>
        /// Gets or sets the broadcast to display to a player when they pick up an item included in <see cref="BypassTeslaItems"/>.
        /// </summary>
        [Description("The broadcast to display to a player when they pick up an item included in bypass_tesla_items.")]
        public Broadcast PickItemBC { get; set; } = new Broadcast("A {type} will disable tesla gates when held!", 5);

        /// <summary>
        /// Gets or sets the energy requirement for triggering a tesla gate as Scp079.
        /// </summary>
        [Description("The energy requirement for triggering a tesla gate as Scp079 (default = 50)")]
        public int TeslaCost079 { get; set; } = 50;

        /// <summary>
        /// Gets or sets a value indicating whether a player's inventory should be wiped when they die to a tesla gate.
        /// </summary>
        [Description("Whether a player's inventory should be wiped when they die to a tesla gate (default = false)")]
        public bool EraseInventory { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="Tesla079Limit"/> is active.
        /// </summary>
        [Description("Whether the tesla079_limit is active (default = false)")]
        public bool IsTesla079LimitEnabled { get; set; } = false;

        /// <summary>
        /// Gets or sets the amount of times tesla gates can be activated by Scp079 when <see cref="IsTesla079LimitEnabled"/> is enabled.
        /// </summary>
        [Description("If Teslas079Limit is true, set the limit for 079 (default = 15)")]
        public int Tesla079Limit { get; set; } = 15;

        /// <summary>
        /// Gets or sets a value indicating whether tesla gates should no longer trigger when there are no remaining scps.
        /// </summary>
        [Description("If there isn't any SCP left, should be the teslas disabled? (default = false)")]
        public bool TeslaDisableAtNoScp { get; set; } = false;
    }
}