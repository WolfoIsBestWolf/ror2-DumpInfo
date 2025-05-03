using BepInEx;
using BepInEx.Configuration;
using R2API;
using R2API.Utils;
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

#pragma warning disable CS0618 // Type or member is obsolete
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618 // Type or member is obsolete
[module: UnverifiableCode]

namespace DumpAll
{
    [BepInPlugin("com.Wolfo.DumpAll", "DumpAllDumper", "1.0.0")]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]


    public class DumpAll : BaseUnityPlugin
    {

        public static void DumpInfo()
        {
            foreach (var item in Addressables.ResourceLocators)
            {
                foreach (var strng in item.Keys)
                {
                    Debug.LogWarning(strng);
                    Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: strng).WaitForCompletion();
                    Addressables.LoadAssetAsync<SpawnCard>(key: strng).WaitForCompletion();
                    Addressables.LoadAssetAsync<DccsPool>(key: strng).WaitForCompletion();
                    Addressables.LoadAssetAsync<ItemDef>(key: strng).WaitForCompletion();
                    Addressables.LoadAssetAsync<BuffDef>(key: strng).WaitForCompletion();
                    Addressables.LoadAssetAsync<SkillDef>(key: strng).WaitForCompletion();

                    //Addressables.LoadAssetAsync<SpawnCard>(key: strng).WaitForCompletion();
                }
            }
            DirectorCardCategorySelection[] allDCCS = Resources.FindObjectsOfTypeAll<DirectorCardCategorySelection>();
            DccsPool[] AallDCCSPool = Resources.FindObjectsOfTypeAll<DccsPool>();
            CharacterSpawnCard[] AallCSC = Resources.FindObjectsOfTypeAll<CharacterSpawnCard>();
            InteractableSpawnCard[] AallISC = Resources.FindObjectsOfTypeAll<InteractableSpawnCard>();
            ItemDef[] AallITEMDEF = Resources.FindObjectsOfTypeAll<ItemDef>();
            BuffDef[] Aall_BuffDef = Resources.FindObjectsOfTypeAll<BuffDef>();
            SkillDef[] Aall_SkillDef = Resources.FindObjectsOfTypeAll<SkillDef>();

            List<DccsPool> allDCCSPool = AallDCCSPool.OfType<DccsPool>().ToList();
            List<CharacterSpawnCard> allCSC = AallCSC.OfType<CharacterSpawnCard>().ToList();
            List<InteractableSpawnCard> allISC = AallISC.OfType<InteractableSpawnCard>().ToList();
            List<ItemDef> allItemDef = AallITEMDEF.OfType<ItemDef>().ToList();
            List<BuffDef> all_BuffDef = Aall_BuffDef.OfType<BuffDef>().ToList();
            List<SkillDef> all_SkillDef = Aall_SkillDef.OfType<SkillDef>().ToList();




            List<DirectorCardCategorySelection> allinteractables = new List<DirectorCardCategorySelection>();
            List<DirectorCardCategorySelection> allmonsters = new List<DirectorCardCategorySelection>();
            List<DirectorCardCategorySelection> allMisc = new List<DirectorCardCategorySelection>();

            foreach (DirectorCardCategorySelection dccs in allDCCS)
            {
                if (dccs.name.Contains("Interactables"))
                {
                    allinteractables.Add(dccs);
                }
                else if (dccs.name.Contains("Monsters"))
                {
                    allmonsters.Add(dccs);
                }
                else
                {
                    allMisc.Add(dccs);
                }
            }


            Debug.LogWarning("SortLists");

            allinteractables.Sort((x, y) => string.Compare(x.name, y.name));
            allmonsters.Sort((x, y) => string.Compare(x.name, y.name));
            allMisc.Sort((x, y) => string.Compare(x.name, y.name));
            allDCCSPool.Sort((x, y) => string.Compare(x.name, y.name));
            allCSC.Sort((x, y) => string.Compare(x.name, y.name));
            allISC.Sort((x, y) => string.Compare(x.name, y.name));
            allItemDef.Sort((x, y) => string.Compare(x.name, y.name));
            all_BuffDef.Sort((x, y) => string.Compare(x.name, y.name));
            all_SkillDef.Sort((x, y) => string.Compare(x.skillNameToken, y.skillNameToken));


            Debug.LogWarning("");
            Debug.LogWarning("");
            Debug.LogWarning("All SkillDef");
            for (int dccs = 0; all_SkillDef.Count > dccs; dccs++)
            {
                Debug.LogWarning("");
                Debug.LogWarning("--------------------");
                Debug.LogWarning(all_SkillDef[dccs].name + "  (" + Language.GetString(all_SkillDef[dccs].skillNameToken) + ")");
                Debug.LogWarning("-isCombat       : " + all_SkillDef[dccs].isCombatSkill);
            }


            Debug.LogWarning("");
            Debug.LogWarning("");
            Debug.LogWarning("All BuffDefs");
            for (int dccs = 0; all_BuffDef.Count > dccs; dccs++)
            {
                Debug.LogWarning("");
                Debug.LogWarning("--------------------");
                Debug.LogWarning(all_BuffDef[dccs].name);
                Debug.LogWarning("-Debuff       : " + all_BuffDef[dccs].isDebuff);
                Debug.LogWarning("-Cooldown     : " + all_BuffDef[dccs].isCooldown);
                Debug.LogWarning("-Ignore Nectar: " + all_BuffDef[dccs].ignoreGrowthNectar);
                if (all_BuffDef[dccs].isDebuff && !all_BuffDef[dccs].ignoreGrowthNectar)
                {
                    Debug.LogWarning("--DEBUFF THAT COUNTS FOR GROWTH NECTAR");
                }
                else if (all_BuffDef[dccs].isCooldown && !all_BuffDef[dccs].ignoreGrowthNectar)
                {
                    Debug.LogWarning("--COOLDOWN THAT COUNTS FOR GROWTH NECTAR");
                }
            }

            Debug.LogWarning("-------------------------");
            Debug.LogWarning("NOXIOUS THORN BLACKLIST");
            for (int dccs = 0; all_BuffDef.Count > dccs; dccs++)
            {
                if (all_BuffDef[dccs].flags.HasFlag(BuffDef.Flags.ExcludeFromNoxiousThorns))
                {
                    Debug.LogWarning(all_BuffDef[dccs].name);
                }
            }
            Debug.LogWarning("-------------------------");


            Debug.LogWarning("");
            Debug.LogWarning("");
            Debug.LogWarning("All Item Defs");
            for (int dccs = 0; allItemDef.Count > dccs; dccs++)
            {
                Debug.LogWarning("");
                Debug.LogWarning("--------------------");
                Debug.LogWarning("");
                Debug.LogWarning(allItemDef[dccs].name + "  (" + Language.GetString(allItemDef[dccs].nameToken) + ")");
                for (int cat = 0; allItemDef[dccs].tags.Length > cat; cat++)
                {
                    Debug.LogWarning("-[" + cat + "] " + allItemDef[dccs].tags[cat]);
                }
            }




            Debug.LogWarning("");
            Debug.LogWarning("");
            Debug.LogWarning("DCCS All Interactables");
            for (int dccs = 0; allinteractables.Count > dccs; dccs++)
            {
                Debug.LogWarning("");
                Debug.LogWarning("--------------------");
                Debug.LogWarning("");
                Debug.LogWarning(allinteractables[dccs].name);


                for (int cat = 0; allinteractables[dccs].categories.Length > cat; cat++)
                {
                    var CATEGORY = allinteractables[dccs].categories[cat];
                    Debug.LogWarning("--[" + cat + "]--" + CATEGORY.name + "--" + "  wt:" + allinteractables[dccs].categories[cat].selectionWeight);
                    for (int card = 0; CATEGORY.cards.Length > card; card++)
                    {
                        if(!CATEGORY.cards[card].spawnCard)
                        {
                            Debug.LogWarning("[" + card + "]  NULL Spawn Card  wt:" + CATEGORY.cards[card].selectionWeight + "  minStage:" + CATEGORY.cards[card].minimumStageCompletions);
                        }
                        else
                        {
                            string debug = "[" + card + "] " + CATEGORY.cards[card].spawnCard.name + "  wt:" + CATEGORY.cards[card].selectionWeight + "  minStage:" + CATEGORY.cards[card].minimumStageCompletions;
                            if (CATEGORY.cards[card].forbiddenUnlockableDef || !string.IsNullOrEmpty(CATEGORY.cards[card].forbiddenUnlockable))
                            {
                                debug += "  forbiddenWhen: " + CATEGORY.cards[card].forbiddenUnlockable + CATEGORY.cards[card].forbiddenUnlockableDef;
                            }
                            Debug.LogWarning(debug);
                        }
                    }
                    Debug.LogWarning("");
                }


            }
            Debug.LogWarning("");
            Debug.LogWarning("");
            Debug.LogWarning("DCCS All Monsters");
            for (int dccs = 0; allmonsters.Count > dccs; dccs++)
            {
                Debug.LogWarning("");
                Debug.LogWarning("--------------------");
                Debug.LogWarning("");
                Debug.LogWarning(allmonsters[dccs].name);


                for (int cat = 0; allmonsters[dccs].categories.Length > cat; cat++)
                {
                    Debug.LogWarning("--[" + cat + "]--" + allmonsters[dccs].categories[cat].name + "--" + "  wt:" + allmonsters[dccs].categories[cat].selectionWeight);
                    for (int card = 0; allmonsters[dccs].categories[cat].cards.Length > card; card++)
                    {
                        if (!allmonsters[dccs].categories[cat].cards[card].spawnCard)
                        {
                            Debug.LogWarning("[" + card + "]  NULL Spawn Card  wt:" + allmonsters[dccs].categories[cat].cards[card].selectionWeight + "  minStage:" + allmonsters[dccs].categories[cat].cards[card].minimumStageCompletions);
                        }
                        else
                        {
                            Debug.LogWarning("[" + card + "] " + allmonsters[dccs].categories[cat].cards[card].spawnCard.name + "  wt:" + allmonsters[dccs].categories[cat].cards[card].selectionWeight + "  minStage:" + allmonsters[dccs].categories[cat].cards[card].minimumStageCompletions);
                        }
                    }
                    Debug.LogWarning("");
                }


            }
            Debug.LogWarning("");
            Debug.LogWarning("");
            Debug.LogWarning("DCCS All Misc");
            for (int dccs = 0; allMisc.Count > dccs; dccs++)
            {
                Debug.LogWarning("");
                Debug.LogWarning("--------------------");
                Debug.LogWarning("");
                Debug.LogWarning(allMisc[dccs].name);


                for (int cat = 0; allMisc[dccs].categories.Length > cat; cat++)
                {
                    Debug.LogWarning("--[" + cat + "]--" + allMisc[dccs].categories[cat].name + "--" + "  wt:" + allMisc[dccs].categories[cat].selectionWeight);
                    for (int card = 0; allMisc[dccs].categories[cat].cards.Length > card; card++)
                    {
                        if (!allMisc[dccs].categories[cat].cards[card].spawnCard)
                        {
                            Debug.LogWarning("[" + card + "]  NULL Spawn Card  wt:" + allMisc[dccs].categories[cat].cards[card].selectionWeight + "  minStage:" + allMisc[dccs].categories[cat].cards[card].minimumStageCompletions);
                        }
                        else
                        {
                            Debug.LogWarning("[" + card + "] " + allMisc[dccs].categories[cat].cards[card].spawnCard.name + "  wt:" + allMisc[dccs].categories[cat].cards[card].selectionWeight + "  minStage:" + allMisc[dccs].categories[cat].cards[card].minimumStageCompletions);
                        }
                    }
                    Debug.LogWarning("");
                }


            }
            Debug.LogWarning("");
            Debug.LogWarning("");
            Debug.LogWarning("DccsPools");
            for (int dccs = 0; allDCCSPool.Count > dccs; dccs++)
            {
                Debug.LogWarning("");
                Debug.LogWarning("--------------------");
                Debug.LogWarning("");
                Debug.LogWarning(allDCCSPool[dccs].name);


                for (int cat = 0; allDCCSPool[dccs].poolCategories.Length > cat; cat++)
                {
                    Debug.LogWarning("--[" + cat + "]--" + allDCCSPool[dccs].poolCategories[cat].name);
                    for (int card = 0; allDCCSPool[dccs].poolCategories[cat].alwaysIncluded.Length > card; card++)
                    {
                        Debug.LogWarning("[" + card + "] " + allDCCSPool[dccs].poolCategories[cat].alwaysIncluded[card].dccs.name + "  wt:" + allDCCSPool[dccs].poolCategories[cat].alwaysIncluded[card].weight);
                    }
                    for (int card = 0; allDCCSPool[dccs].poolCategories[cat].includedIfConditionsMet.Length > card; card++)
                    {
                        Debug.LogWarning("[" + card + "] " + allDCCSPool[dccs].poolCategories[cat].includedIfConditionsMet[card].dccs.name + "  wt:" + allDCCSPool[dccs].poolCategories[cat].includedIfConditionsMet[card].weight);
                    }
                    for (int card = 0; allDCCSPool[dccs].poolCategories[cat].includedIfNoConditionsMet.Length > card; card++)
                    {
                        Debug.LogWarning("[" + card + "] " + allDCCSPool[dccs].poolCategories[cat].includedIfNoConditionsMet[card].dccs.name + "  wt:" + allDCCSPool[dccs].poolCategories[cat].includedIfNoConditionsMet[card].weight);
                    }
                    Debug.LogWarning("");
                }


            }
            Debug.LogWarning("");
            Debug.LogWarning("");
            Debug.LogWarning("CharacterSpawnCards");
            Debug.LogWarning("");
            for (int dccs = 0; allCSC.Count > dccs; dccs++)
            {
                Debug.LogWarning(allCSC[dccs].name + " Cost:" + allCSC[dccs].directorCreditCost);
            }
            Debug.LogWarning("");
            Debug.LogWarning("");
            Debug.LogWarning("InteractableSpawnCards");
            Debug.LogWarning("");
            for (int dccs = 0; allISC.Count > dccs; dccs++)
            {
                Debug.LogWarning(allISC[dccs].name + " Cost:" + allISC[dccs].directorCreditCost);
            }
            Debug.LogWarning("");
            Debug.LogWarning("");
            
        }



        public void Awake()
        {
            //DumpInfo();
            On.RoR2.UI.MainMenu.MainMenuController.Start += OneTimeOnlyLateRunner;
            
        }
        private void OneTimeOnlyLateRunner(On.RoR2.UI.MainMenu.MainMenuController.orig_Start orig, RoR2.UI.MainMenu.MainMenuController self)
        {
            orig(self);

            DumpInfo();


            On.RoR2.UI.MainMenu.MainMenuController.Start -= OneTimeOnlyLateRunner;
        }


    }




}