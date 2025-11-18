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
    public static class Dump_Items
    {
        public static void DumpInfo()
        {
            string log = string.Empty;
            List<ItemDef> itemDefs = ContentManager.itemDefs.ToList();
            List<ItemDef> itemDefsTier = ContentManager.itemDefs.ToList();

            itemDefs.Sort((x, y) => string.Compare(x.name, y.name));
            itemDefsTier.Sort((x, y) => x.tier.CompareTo(y.tier));

            log = "\n\nItemDefs";
            for (int dccs = 0; itemDefs.Count > dccs; dccs++)
            {
                log += "\n--------------------\n\n";
                log += itemDefs[dccs].name + "  (" + Language.GetString(itemDefs[dccs].nameToken) + ")\n";
                if (itemDefs[dccs].hidden)
                {
                    log += "-hiddenItem\n";
                }
                for (int cat = 0; itemDefs[dccs].tags.Length > cat; cat++)
                {
                    log += "-[" + cat + "] " + itemDefs[dccs].tags[cat] + "\n";
                }
            }
            DumpAll.logger.LogMessage(log);

            DumpItemTag(ref itemDefsTier, ItemTag.CanBeTemporary, true, true);
            DumpItemTag(ref itemDefsTier, ItemTag.AIBlacklist, false, false);
            DumpItemTag(ref itemDefsTier, ItemTag.BrotherBlacklist, false, false);
            DumpItemTag(ref itemDefsTier, ItemTag.ExtractorUnitBlacklist, false, false);

            DumpItemTag(ref itemDefsTier, ItemTag.CannotSteal, false, false);
            DumpItemTag(ref itemDefsTier, ItemTag.CannotCopy, false, false);
       
            DumpItemTag(ref itemDefsTier, ItemTag.MobilityRelated, false, false);
            DumpItemTag(ref itemDefsTier, ItemTag.Technology, false, false);
            DumpItemTag(ref itemDefsTier, ItemTag.HiddenForDroneBuffIcon, false, false);

            DumpItemTag(ref itemDefsTier, ItemTag.SacrificeBlacklist, false, false);
            DumpItemTag(ref itemDefsTier, ItemTag.DevotionBlacklist, false, false);
            DumpItemTag(ref itemDefsTier, ItemTag.RebirthBlacklist, false, false);


        }

        public static void DumpItemTag(ref List<ItemDef> defs, ItemTag tag, bool invert, bool noNotier)
        {
            string log = "\n\n--ItemTag: All ";
            if (invert)
            {
                log += "Not ";
            }
            log += tag+"--\n\n";
            
            foreach (ItemDef def in defs)
            {
                if ((!noNotier || def.tier != ItemTier.NoTier) && (invert ? !def.ContainsTag(tag) : def.ContainsTag(tag)))
                {
                    log += def.name + " | " + Language.GetString(def.nameToken) + "\n";
                }
            }
            DumpAll.logger.LogMessage(log);
        }
    }




}