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
    public static class Dump_DCCS
    {
        public static void DumpInfo()
        {
            string log = string.Empty;
            DirectorCardCategorySelection[] allDCCS = Resources.FindObjectsOfTypeAll<DirectorCardCategorySelection>();
            DccsPool[] AallDCCSPool = Resources.FindObjectsOfTypeAll<DccsPool>();
            //SpawnCard[] allSpawnCards = Resources.FindObjectsOfTypeAll<SpawnCard>();
            CharacterSpawnCard[] allCSC_ = Resources.FindObjectsOfTypeAll<CharacterSpawnCard>();
            InteractableSpawnCard[] allISC_ = Resources.FindObjectsOfTypeAll<InteractableSpawnCard>();

            //List<CharacterSpawnCard> allCSC = allSpawnCards.OfType<CharacterSpawnCard>().ToList();
            //List<InteractableSpawnCard> allISC = allSpawnCards.OfType<InteractableSpawnCard>().ToList();
            List<DirectorCardCategorySelection> allInteractables = new List<DirectorCardCategorySelection>();
            List<DirectorCardCategorySelection> allMonsters = new List<DirectorCardCategorySelection>();
            List<DirectorCardCategorySelection> allFamily = new List<DirectorCardCategorySelection>();
            List<DirectorCardCategorySelection> allMisc = new List<DirectorCardCategorySelection>();
            List<DccsPool> allDCCSPool = AallDCCSPool.OfType<DccsPool>().ToList();
            List<CharacterSpawnCard> allCSC = allCSC_.ToList();
            List<InteractableSpawnCard> allISC = allISC_.ToList();

            foreach (DirectorCardCategorySelection dccs in allDCCS)
            {
                if (dccs is FamilyDirectorCardCategorySelection)
                {
                    allFamily.Add(dccs);
                }
                else if (dccs.name.Contains("Interactable"))
                {
                    allInteractables.Add(dccs);
                }
                else if (dccs.name.Contains("Monster"))
                {
                    allMonsters.Add(dccs);
                }
                else
                {
                    allMisc.Add(dccs);
                }
            }

            allFamily.Sort((x, y) => string.Compare(x.name, y.name));
            allInteractables.Sort((x, y) => string.Compare(x.name, y.name));
            allMonsters.Sort((x, y) => string.Compare(x.name, y.name));
            allMisc.Sort((x, y) => string.Compare(x.name, y.name));
            allDCCSPool.Sort((x, y) => string.Compare(x.name, y.name));
            allCSC.Sort((x, y) => string.Compare(x.name, y.name));
            allISC.Sort((x, y) => string.Compare(x.name, y.name));


            DumpAll.logger.LogMessage("\n\nDirectorCardCategorySelections DCCS Interactables");
            DumpDCCS(allInteractables, false);
            DumpAll.logger.LogMessage("\n\nDirectorCardCategorySelections DCCS Monsters");
            DumpDCCS(allMonsters, true);
            DumpAll.logger.LogMessage("\n\nDirectorCardCategorySelections DCCS Family");
            DumpDCCS(allFamily, true);
            DumpAll.logger.LogMessage("n\nDirectorCardCategorySelections DCCS Misc");
            DumpDCCS(allMisc, false);

            DumpAll.logger.LogMessage("\n\nDccsPools");
            for (int dccs = 0; allDCCSPool.Count > dccs; dccs++)
            {
                log = "\n--------------\n\n" + allDCCSPool[dccs].name;
                for (int cat = 0; allDCCSPool[dccs].poolCategories.Length > cat; cat++)
                {
                    var category = allDCCSPool[dccs].poolCategories[cat];
                    log += "\n--[" + cat + "]--" + category.name + " wt:" + category.categoryWeight;
                    for (int card = 0; category.alwaysIncluded.Length > card; card++)
                    {
                        log += "\n[" + card + "] " + category.alwaysIncluded[card].dccs.name + "  wt:" + allDCCSPool[dccs].poolCategories[cat].alwaysIncluded[card].weight;
                    }
                    for (int card = 0; category.includedIfConditionsMet.Length > card; card++)
                    {
                        log += "\n[" + card + "] " + category.includedIfConditionsMet[card].dccs.name + "  wt:" + allDCCSPool[dccs].poolCategories[cat].includedIfConditionsMet[card].weight;
                    }
                    for (int card = 0; category.includedIfNoConditionsMet.Length > card; card++)
                    {
                        log += "\n[" + card + "] " + category.includedIfNoConditionsMet[card].dccs.name + "  wt:" + allDCCSPool[dccs].poolCategories[cat].includedIfNoConditionsMet[card].weight;
                    }
                    log += "\n";
                }
                DumpAll.logger.LogMessage(log);

            }


            log = "\n\nCharacterSpawnCards\n\n";
            for (int i = 0; allCSC.Count > i; i++)
            {
                log += allCSC[i].name;
                log += " Cost:" + allCSC[i].directorCreditCost;
                if (allCSC[i].requiredFlags.HasFlag(NodeFlags.NoCeiling))
                {
                    log += " -RequiresNoCeiling";
                }
                if (allCSC[i].forbiddenFlags.HasFlag(NodeFlags.NoCeiling))
                {
                    log += " -RequiresCeiling";
                }
                if (allCSC[i].noElites)
                {
                    log += " -noElites";
                }
                if (allCSC[i].forbiddenAsBoss)
                {
                    log += " -forbiddenAsBoss";
                }
                log += "\n";
            }
            DumpAll.logger.LogMessage(log);
            log = "\n\nInteractableSpawnCards\n\n";
            for (int i = 0; allISC.Count > i; i++)
            {
                log += allISC[i].name;
                log += " Cost:" + allISC[i].directorCreditCost;
                if (allISC[i].requiredFlags.HasFlag(NodeFlags.NoCeiling))
                {
                    log += " -RequiresNoCeiling";
                }
                if (allISC[i].maxSpawnsPerStage != -1)
                {
                    log += " -MaxSpawns:"+ allISC[i].maxSpawnsPerStage;
                }
                log += "\n";
 
            }
            DumpAll.logger.LogMessage(log);

        }


        public static void DumpDCCS(List<DirectorCardCategorySelection> allDCCS, bool distance)
        {

            for (int dccs = 0; allDCCS.Count > dccs; dccs++)
            {
                string log = string.Empty;

                log += "\n--------------------\n\n";
                log += allDCCS[dccs].name + "\n";
                if (allDCCS[dccs] is FamilyDirectorCardCategorySelection)
                {
                    //(allDCCS[dccs] as FamilyDirectorCardCategorySelection).minimumStageCompletion
                }
                for (int cat = 0; allDCCS[dccs].categories.Length > cat; cat++)
                {
                    var CATEGORY = allDCCS[dccs].categories[cat];
                    log += "--[" + cat + "]--" + CATEGORY.name + "--" + "  wt:" + allDCCS[dccs].categories[cat].selectionWeight + "\n";
                    for (int card = 0; CATEGORY.cards.Length > card; card++)
                    {
                        if (!CATEGORY.cards[card].spawnCard)
                        {
                            DumpAll.logger.LogMessage("[" + card + "]  NULL Spawn Card  wt:" + CATEGORY.cards[card].selectionWeight + "  minStage:" + CATEGORY.cards[card].minimumStageCompletions);
                        }
                        else
                        {
                            log += "[" + card + "] " + CATEGORY.cards[card].spawnCard.name + "  wt:" + CATEGORY.cards[card].selectionWeight + "  minStage:" + CATEGORY.cards[card].minimumStageCompletions;
                            if (CATEGORY.cards[card].forbiddenUnlockableDef)
                            {
                                log += "  forbiddenWhen: " + CATEGORY.cards[card].forbiddenUnlockableDef.cachedName;
                            }
                            else if (!string.IsNullOrEmpty(CATEGORY.cards[card].forbiddenUnlockable))
                            {
                                log += "  forbiddenWhen: " + CATEGORY.cards[card].forbiddenUnlockable;
                            }
                            if (distance)
                            {
                                log += "  distance: " + CATEGORY.cards[card].spawnDistance;
                            }
                            log += "\n";
                        }
                    }
                    log += "\n";
                }
                log += "\n";
                 DumpAll.logger.LogMessage(log);

            }

        }

    }




}