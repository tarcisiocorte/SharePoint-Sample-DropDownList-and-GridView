using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Data;
using TC.Sample.DropAndGridCascading.Helper;
using Microsoft.SharePoint.WebControls;

namespace TC.Sample.DropAndGridCascading.VisualWebPart1
{
    public partial class VisualWebPart1UserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetInitialRow();
            }
        }

        public DataTable LoadCounties(string county)
        {
            DataTable dtCounties = null;
            using (SPSite site = new SPSite(SPContext.Current.Site.Url))
            {
                using (SPWeb web = site.OpenWeb())
                {

                    SPList listCounty = web.Lists.TryGetList("County");
                    if (listCounty == null) return new DataTable();

                    SPQuery qry = new SPQuery();
                    if (!string.IsNullOrEmpty(county))
                        qry.Query = "<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + county + "</Value></Eq></Where>";

                    dtCounties = listCounty.GetItems(qry).GetDataTable();
                }
            }
            return dtCounties;
        }


        private void AddNewRowToGrid()
        {

            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox txtCounty = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtCounty");
                        TextBox txtCountry = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtCountry");
                        TextBox txtDate = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtDate");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;
                        drCurrentRow["Title"] = txtCounty.Text;
                        drCurrentRow["Country"] = txtCountry.Text;
                        drCurrentRow["Holidays"] = txtDate.Text;

                        rowIndex++;
                    }

                    //add new row to DataTable
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    //Store the current data to ViewState
                    ViewState["CurrentTable"] = dtCurrentTable;

                    //Rebind the Grid with the current data
                    Gridview1.DataSource = dtCurrentTable;
                    Gridview1.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreviousData();
        }

        private void SetPreviousData()
        {

            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtCounty");
                        TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtCountry");
                        TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtDate");


                        box1.Text = dt.Rows[i]["Title"].ToString();
                        box2.Text = dt.Rows[i]["Country"].ToString();
                        box3.Text = dt.Rows[i]["Holidays"].ToString();

                        rowIndex++;

                    }
                }
            }
        }

        private void SetInitialRow()
        {

            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Title", typeof(string)));
            dt.Columns.Add(new DataColumn("Country", typeof(string)));
            dt.Columns.Add(new DataColumn("Holidays", typeof(string)));

            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Title"] = string.Empty;
            dr["Country"] = string.Empty;
            dr["Holidays"] = string.Empty;
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;

            Gridview1.DataSource = dt;
            Gridview1.DataBind();
        }

        protected void lkb_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid();
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow itemRow in this.Gridview1.Rows)
            {
                TextBox txtCount = itemRow.FindControl("txtCounty") as TextBox;
                TextBox txtCountry = itemRow.FindControl("txtCountry") as TextBox;
                TextBox txtDate = itemRow.FindControl("txtDate") as TextBox;

                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList list = web.Lists.TryGetList(Consts.Lists.ScheduleHoliday.ListName);
                        if (list == null) continue;

                        SPListItem itemAdd = list.AddItem();
                        itemAdd[Consts.Lists.ScheduleHoliday.Fields.Title] = txtCount.Text;
                        itemAdd[Consts.Lists.ScheduleHoliday.Fields.Country] = txtCountry.Text;
                        itemAdd[Consts.Lists.ScheduleHoliday.Fields.Holidays] = Convert.ToDateTime(txtDate.Text);
                        itemAdd.Update();
                    }
                }
            }

            SetInitialRow();

        }

    }
}
