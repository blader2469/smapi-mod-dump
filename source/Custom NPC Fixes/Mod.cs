﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI;
using StardewValley;

namespace CustomNPCFixes
{
    public class Mod : StardewModdingAPI.Mod
    {
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.SaveCreated += doNpcFixes;
            helper.Events.GameLoop.SaveLoaded += doNpcFixes;
        }

        public void doNpcFixes(object sender, EventArgs args)
        {
            // This needs to be called again so that custom NPCs spawn in locations added after the original call
            Game1.fixProblems();

            // Similarly, this needs to be called again so that pathing works.
            NPC.populateRoutesFromLocationToLocationList();
            
            // Schedules for new NPCs don't work the first time.
            foreach ( var npc in Utility.getAllCharacters() )
            {
                if ( npc.Schedule == null )
                {
                    npc.Schedule = npc.getSchedule(Game1.dayOfMonth);
                    npc.checkSchedule(Game1.timeOfDay);
                }
            }
        }
    }
}
