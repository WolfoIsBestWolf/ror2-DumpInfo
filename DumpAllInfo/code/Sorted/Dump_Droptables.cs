using RoR2;
using RoR2.Skills;
using RoR2.Navigation;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using BepInEx;
using RoR2.ContentManagement;
using System.Runtime.CompilerServices;
using RoR2.ExpansionManagement;
using static UnityEngine.Rendering.DebugUI;
using static RoR2.Console;
using RoR2.Achievements.Artifacts;
using static RoR2.SkinDef;
using static RoR2.CharacterModel;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine.ResourceManagement.AsyncOperations;


namespace DumpAll
{
    public static class Dump_Droptables
    {
        public static void DumpInfo()
        {
            string log = string.Empty;
            BasicPickupDropTable[] all_BasicPickupDropTable = Resources.FindObjectsOfTypeAll<BasicPickupDropTable>();
            ExplicitPickupDropTable[] all_ExplicitPickupDropTable = Resources.FindObjectsOfTypeAll<ExplicitPickupDropTable>();

            List<BasicPickupDropTable> BasicPickupDropTable = all_BasicPickupDropTable.OfType<BasicPickupDropTable>().ToList();
            List<ExplicitPickupDropTable> ExplicitPickupDropTable = all_ExplicitPickupDropTable.OfType<ExplicitPickupDropTable>().ToList();

            BasicPickupDropTable.Sort((x, y) => string.Compare(x.name, y.name));
            ExplicitPickupDropTable.Sort((x, y) => string.Compare(x.name, y.name));



            //DUMP ALL DROP TABLES
            log = "\n\nAll Basic Droptables\n---------------";
            foreach (var dt in BasicPickupDropTable)
            {
                log += "\n\n--" + dt.name + "--\n";
                log += "CanBeReplaced: " + dt.canDropBeReplaced + "\n";
                if (dt.tier1Weight > 0)
                {
                    log += "tier1Weight " + dt.tier1Weight + "\n";
                }
                if (dt.tier2Weight > 0)
                {
                    log += "tier2Weight " + dt.tier2Weight + "\n";
                }
                if (dt.tier3Weight > 0)
                {
                    log += "tier3Weight " + dt.tier3Weight + "\n";
                }
                if (dt.bossWeight > 0)
                {
                    log += "bossWeight " + dt.bossWeight + "\n";
                }
                if (dt.lunarItemWeight > 0)
                {
                    log += "lunarItemWeight " + dt.lunarItemWeight + "\n";
                }
                if (dt.lunarEquipmentWeight > 0)
                {
                    log += "lunarEquipmentWeight " + dt.lunarEquipmentWeight + "\n";
                }
                if (dt.lunarCombinedWeight > 0)
                {
                    log += "lunarCombinedWeight " + dt.lunarCombinedWeight + "\n";
                }
                if (dt.equipmentWeight > 0)
                {
                    log += "equipmentWeight " + dt.equipmentWeight + "\n";
                }
                if (dt.voidTier1Weight > 0)
                {
                    log += "voidTier1Weight " + dt.voidTier1Weight + "\n";
                }
                if (dt.voidTier2Weight > 0)
                {
                    log += "voidTier2Weight " + dt.voidTier2Weight + "\n";
                }
                if (dt.voidTier3Weight > 0)
                {
                    log += "voidTier3Weight " + dt.voidTier3Weight + "\n";
                }
                if (dt.voidBossWeight > 0)
                {
                    log += "voidBossWeight " + dt.voidBossWeight + "\n";
                }
                /*if (dt.foodTierWeight > 0)
                {
                    log += "foodTierWeight " + dt.foodTierWeight + "\n";
                }
                if (dt.powerShapesWeight > 0)
                {
                    log += "powerShapesWeight " + dt.powerShapesWeight + "\n";
                }*/
                if (dt.bannedItemTags.Length > 0)
                {
                    log += "-BannedItemTags:\n";
                    foreach (var flag in dt.bannedItemTags)
                    {
                        log += flag + "\n";
                    }
                }
                if (dt.requiredItemTags.Length > 0)
                {
                    log += "-RequiredItemTags:\n";
                    foreach (var flag in dt.requiredItemTags)
                    {
                        log += flag + "\n";
                    }
                }
            }
            DumpAll.logger.LogMessage(log);

            log = "\n\nAll Explicit Droptables\n---------------";
            foreach (var dt in ExplicitPickupDropTable)
            {
                log += "\n\n--" + dt.name + "--\n";
                log += "CanBeReplaced: " + dt.canDropBeReplaced + "\n";
                if (dt.entries.Length > 0)
                {
                    foreach (var entry in dt.entries)
                    {
                        log += entry.pickupName + " | " + entry.pickupWeight + "\n";
                    }
                }
                if (dt.pickupEntries.Length > 0)
                {
                    foreach (var entry in dt.pickupEntries)
                    {
                        log += entry.pickupDef + " | " + entry.pickupWeight + "\n";
                    }
                }
            }
             DumpAll.logger.LogMessage(log);


        }



    }




}