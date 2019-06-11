using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Expansions;
using Kopernicus;
using Kopernicus.Configuration;
using UnityEngine;

namespace MyRocksAreBiggerThanYours
{
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    public class Rock : MonoBehaviour
    {
        [SuppressMessage("ReSharper", "ConvertClosureToMethodGroup")]
        private void Awake()
        {
            Events.OnPQSLoaderPostApply.Add((pqs, config) => OnPQSLoaderPostApply(pqs, config));
        }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private static void OnPQSLoaderPostApply(PQSLoader pqs, ConfigNode node)
        {
            // You need to pay for rocks
            if (!ExpansionsLoader.IsExpansionInstalled("Serenity"))
            {
                return;
            }
            
            PQSROCControl rockController = pqs.Value.GetComponentInChildren<PQSROCControl>();
            if (!rockController)
            {
                GameObject rockControllerObject = new GameObject("My rocks are bigger than yours.");
                rockController = rockControllerObject.AddComponent<PQSROCControl>();
                rockController.sphere = pqs.Value;
                rockController.modEnabled = true;
                rockController.order = 987654321;
                rockController.transform.parent = rockController.sphere.transform;
                rockController.rocs = new List<LandClassROC>();
            }

            // We support modding
            rockController.currentCBName = pqs.Value.name;
        }
    }
}