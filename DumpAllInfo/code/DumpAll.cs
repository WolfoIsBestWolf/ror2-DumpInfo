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

using Unity.IO.LowLevel.Unsafe;
using UnityEngine.ResourceManagement.AsyncOperations;
using BepInEx.Logging;
using RoR2.ExpansionManagement;


namespace DumpAll
{
    [BepInPlugin("com.Wolfo.DumpAll", "DumpAll", "1.0.0")]
#pragma warning disable CS0618 // Type or member is obsolete
    [assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618 // Type or member is obsolete
    [module: UnverifiableCode]

    public class DumpAll : BepInEx.BaseUnityPlugin
    {
        public static ManualLogSource logger;

        public static bool DLC3 = false;
      

        public static void DumpInfo()
        {
            DumpAll.logger.LogMessage("\n--------------------\n--------------------\n--------------------\n--------------------\n--------------------\n--------------------\n--------------------\n--------------------");
            string log = string.Empty;

            DLC3 = ExpansionCatalog.expansionDefs.Count() >= 3;

            foreach (var item in Addressables.ResourceLocators)
            {
                foreach (var strng in item.Keys)
                {
                    //DumpAll.logger.LogMessage(strng);
                    Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: strng).WaitForCompletion();
                    Addressables.LoadAssetAsync<SpawnCard>(key: strng).WaitForCompletion();
                    Addressables.LoadAssetAsync<DccsPool>(key: strng).WaitForCompletion();
                    Addressables.LoadAssetAsync<NodeGraph>(key: strng).WaitForCompletion();
                    Addressables.LoadAssetAsync<PickupDropTable>(key: strng).WaitForCompletion();;
                    Addressables.LoadAssetAsync<DelayBlast>(key: strng).WaitForCompletion();
                    Addressables.LoadAssetAsync<ItemDisplay>(key: strng).WaitForCompletion();
                    //Addressables.LoadAssetAsync<SpecialObjectAttributes>(key: strng).WaitForCompletion();
                    //Addressables.LoadAssetAsync<GameObject>(key: strng).WaitForCompletion();
                    Addressables.LoadAssetAsync<GameObject>(key: strng).WaitForCompletion();
                    //Addressables.LoadAssetAsync<SphereZone>(key: strng).WaitForCompletion();

                }
            }
            DumpAll.logger.LogMessage("\n--------------------\n--------------------\n--------------------\n--------------------\n--------------------\n--------------------\n--------------------\n--------------------");
 

            Dump_Items.DumpInfo();
            Dump_Equipment.DumpInfo();
  
            Dump_Buffs.DumpInfo();
            Dump_MiscDefs.DumpInfo();

            Dump_DCCS.DumpInfo();
           
            Dump_Droptables.DumpInfo();
            
            Dump_Body.DumpInfo();
            Dump__Other.DumpInfo();
            Dump__Colors.DumpInfo();

            if (DLC3) { Dump__DLC3.DumpInfo(); };
            if (DLC3) { Dump__Specific.DumpInfo(); };

            DumpAll.logger.LogMessage("\n--------------------\n--------------------\n--------------------\n--------------------\n--------------------\n--------------------\n--------------------\n--------------------");
        }




        public void Awake()
        {
            logger = base.Logger;
            //DumpInfo();
            GameModeCatalog.availability.CallWhenAvailable(DumpInfo);
            
        }
        


    }




}