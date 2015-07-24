using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using System.Collections.Generic;
using TC.Sample.DropAndGridCascading.Helper;

namespace TC.Sample.DropAndGridCascading.Features.ConfigSample
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("e70af80a-b625-46f9-a180-966f839261ab")]
    public class ConfigSampleEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            using (SPSite site = properties.Feature.Parent as SPSite)
            {
                //get a reference to the root site / web of the site collection
                using (SPWeb web = site.RootWeb)
                {
                    CreateCountyList(web);
                    CreateScheduleHolidayList(web);

                }
            }
        }


        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {

        }
        private static void CreateCountyList(SPWeb web)
        {
            SPList listCounty = Utils.EnsureListCreation(web, Consts.Lists.County.ListName, Consts.Lists.County.ListName, "", SPListTemplateType.GenericList);
            listCounty.Fields.Add(Consts.Lists.County.Fields.Country, SPFieldType.Text, false);
            listCounty.Update();

            if (listCounty == null) return;

            List<string> countyItems = new List<string>();
            countyItems.Add("Louth");
            countyItems.Add("Mayo");
            countyItems.Add("Meath");
            countyItems.Add("Monaghan");
            countyItems.Add("Offaly");
            countyItems.Add("Roscommon");
            countyItems.Add("Sligo");
            countyItems.Add("Tipperary");
            countyItems.Add("Waterford");
            countyItems.Add("Westmeath");
            countyItems.Add("Wexford");
            countyItems.Add("Wicklow");


            foreach (var item in countyItems)
            {
                SPListItem newItem = listCounty.AddItem();
                newItem["Title"] = item.ToString();
                newItem["Country"] = "Ireland";
                newItem.Update();
            }

            SPListItem rioItem = listCounty.AddItem();
            rioItem["Title"] = "Rio de Janeiro";
            rioItem["Country"] = "Brazil";
            rioItem.Update();

        }

        private static void CreateScheduleHolidayList(SPWeb web)
        {
            SPList listCounty = Utils.EnsureListCreation(web, Consts.Lists.ScheduleHoliday.ListName, Consts.Lists.ScheduleHoliday.ListName, "", SPListTemplateType.GenericList);
            listCounty.Fields.Add(Consts.Lists.ScheduleHoliday.Fields.Country, SPFieldType.Text, false);
            listCounty.Fields.Add(Consts.Lists.ScheduleHoliday.Fields.Holidays, SPFieldType.DateTime, true);
            listCounty.Update();
        }

        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
