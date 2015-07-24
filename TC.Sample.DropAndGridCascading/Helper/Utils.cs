using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace TC.Sample.DropAndGridCascading
{
    public static class Utils
    {
        public static SPList EnsureListCreation(SPWeb web, string listName, string listTitle, string listDescription, SPListTemplateType type)
        {
            EnsureListCleanup(web, listTitle);
            Guid listGuid = web.Lists.Add(listName, listDescription, type);
            SPList createdList = web.Lists.GetList(listGuid, true);
            createdList.Title = listTitle;
            createdList.Update();
            return createdList;
        }
        public static SPList EnsureCustomListCreation(SPWeb web, string listName, string listTitle, string listDescription)
        {
            return EnsureListCreation(web, listName, listTitle, listDescription, SPListTemplateType.GenericList);
        }
        public static SPList EnsureFormLibraryCreation(SPWeb web, string listName, string listTitle, string listDescription)
        {
            //return EnsureListCreation(web, listName, listTitle, listDescription, SPListTemplateType.DocumentLibrary);
            return EnsureListCreation(web, listName, listTitle, listDescription, SPListTemplateType.XMLForm);
            //return EnsureListCreation(web, listName, listTitle, listDescription, SPListTemplateType.GenericList);
        }
        public static void EnsureListCleanup(SPWeb web, string listTitle)
        {
            SPList list = web.Lists.TryGetList(listTitle);
            if (list != null)
                list.Delete();
        }
    }
}
