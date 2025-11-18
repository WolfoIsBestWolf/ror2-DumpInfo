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
    public static class Dump_MiscDefs
    {
        public static void DumpInfo()
        {
            string log = string.Empty;

            List<SkillDef> skillDefs = new List<SkillDef>();
            //List<SkillDef> skillDefs = ContentManager.skillDefs.ToList();
            //skillDefs.Sort((x, y) => string.Compare(x.name, y.name));
            
            List<SceneDef> sceneDefs = ContentManager.sceneDefs.ToList();
            sceneDefs.Sort((x, y) => y.stageOrder.CompareTo(x.stageOrder));


            log = "\n\n---SceneDefs---\n\n";
            foreach (SceneDef scene in sceneDefs)
            {
                log += scene.cachedName + "\n";
                log += scene.sceneType + "\n";
                log += scene.stageOrder + "\n";
                log += scene.environmentColor + "\n";
                log += "\n";
            }
            DumpAll.logger.LogMessage(log);


            log = "\n\n---EliteDefs---\n\n";
            foreach (EliteDef elite in ContentManager.eliteDefs)
            {
                log += elite.name + "\n";
                log += elite.eliteEquipmentDef + "\n";
                log += elite.healthBoostCoefficient + "\n";
                log += elite.damageBoostCoefficient + "\n";
                log += elite.devotionLevel + "\n";
                log += "\n";
            }
            DumpAll.logger.LogMessage(log);


            log = "\n\n---PickupDefs---\n\n";
            foreach (PickupDef pickup in PickupCatalog.allPickups)
            {
                log += (int)pickup.pickupIndex.value + " | ";
                log += pickup.internalName + " | ";
                log += Language.GetString(pickup.nameToken) + "\n";
            }
            DumpAll.logger.LogMessage(log);

            log = "\n\n---SkillDefs--\n\n";
            for (int i = 0; i < ContentManager._skillDefs.Length; i++)
            {
                if (ContentManager._skillDefs[i].skillNameToken != "")
                {
                    skillDefs.Add(ContentManager._skillDefs[i]);
                }
            }
            for (int i = 0; i < ContentManager._skillDefs.Length; i++)
            {
                if (ContentManager._skillDefs[i].skillNameToken == "")
                {
                    skillDefs.Add(ContentManager._skillDefs[i]);
                }
            }
            for (int dccs = 0; skillDefs.Count > dccs; dccs++)
            {
                SkillDef skillDef = skillDefs[dccs];
                log += "\n--------------------\n";
                log += (skillDefs[dccs] as ScriptableObject).name + "  (" + Language.GetString(skillDefs[dccs].skillNameToken) + ")" + "\n";
                log += "-cooldown: " + skillDefs[dccs].baseRechargeInterval + "\n";
                log += "-isCombat: " + skillDefs[dccs].isCombatSkill + "\n";
            }
            DumpAll.logger.LogMessage(log);



            log = "\n\n---GameEndingDefs---\n\n";
            foreach (GameEndingDef def in ContentManager._gameEndingDefs)
            {
                log += def.cachedName + " | " + def.lunarCoinReward;
                log += "\n";
            }
            DumpAll.logger.LogMessage(log);




            if (DumpAll.DLC3) { Dump_DLC3(); };
        
        }
        public static void Dump_DLC3()
        {

            string log = "\n\nDroneDefs\n";
            foreach (DroneDef def in ContentManager.droneDefs)
            {
                log += def.name
                + "  canDrop: " + def.canDrop
                + "  canScrap: " + def.canScrap;
                if (def.droneBrokenSpawnCard)
                {
                    log += "  cost: " + def.droneBrokenSpawnCard.prefab.GetComponent<PurchaseInteraction>().cost;
                }
                if (def.canRemoteOp)
                {
                    log += "  remoteOpCost: " + def.remoteOpCost;
                }
                log += "\n";
            }
            DumpAll.logger.LogMessage(log);
        }


    }




}