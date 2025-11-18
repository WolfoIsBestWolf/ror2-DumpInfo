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
    public static class Dump_Buffs
    {
        public static void DumpInfo()
        {
            string log = string.Empty;

            List<BuffDef> buffDefs = ContentManager.buffDefs.ToList();
            buffDefs.Sort((x, y) => string.Compare(x.name, y.name));

            log = "\n\nAll BuffDefs";
            for (int dccs = 0; buffDefs.Count > dccs; dccs++)
            {
                log += "\n--------------------\n\n";
                BuffDef buff = buffDefs[dccs];
                log += buff.name + "\n";
                if (buff.isHidden)
                {
                    log += "-isHidden\n";
                }
                if (buff.isDebuff)
                {
                    log += "-isDebuff\n";
                }
                if (buff.isDOT)
                {
                    log += "-isDOT\n";
                }
                if (buff.isCooldown)
                {
                    log += "-isCooldown\n";
                }
                if (buff.isElite)
                {
                    log += "-isElite\n";
                }
                if (buff.ignoreGrowthNectar)
                {
                    log += "-ignoreGrowthNectar\n";
                }
                /*if (buffDefs[dccs].isDebuff && !buffDefs[dccs].ignoreGrowthNectar)
                {
                    DumpAll.logger.LogMessage("--DEBUFF THAT COUNTS FOR GROWTH NECTAR");
                }
                else if (buffDefs[dccs].isCooldown && !buffDefs[dccs].ignoreGrowthNectar)
                {
                    DumpAll.logger.LogMessage("--COOLDOWN THAT COUNTS FOR GROWTH NECTAR");
                }*/
            }
             DumpAll.logger.LogMessage(log);

            log = "-------------------------";
            log += "\n\nNOXIOUS THORN BLACKLIST\n\n";
            for (int dccs = 0; buffDefs.Count > dccs; dccs++)
            {
                if (buffDefs[dccs].flags.HasFlag(BuffDef.Flags.ExcludeFromNoxiousThorns))
                {
                    log += buffDefs[dccs].name + "\n";
                }
            }
            log += "-------------------------";
             DumpAll.logger.LogMessage(log);


            if (DumpAll.DLC3) { Dump_DLC3(); };
             
        }
        public static void Dump_DLC3()
        {
            string log = "-------------------------";
            log += "\n\nDRIFTER RANDOM DEBUFF\n\n";
            for (int dccs = 0; BuffCatalog.debuffIndiciesRandomEligible.Length > dccs; dccs++)
            {
                log += BuffCatalog.GetBuffDef(BuffCatalog.debuffIndiciesRandomEligible[dccs]).name + "\n";
            }
            log += "-------------------------";
            DumpAll.logger.LogMessage(log);
        }


    }




}