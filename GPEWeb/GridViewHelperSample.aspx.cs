//------------------------------------------------------------------------------------------
// Copyright © 2006 Agrinei Sousa [www.agrinei.com]
//
// Esse código fonte é fornecido sem garantia de qualquer tipo.
// Sinta-se livre para utilizá-lo, modificá-lo e distribuí-lo,
// inclusive em aplicações comerciais.
// É altamente desejável que essa mensagem não seja removida.
//------------------------------------------------------------------------------------------

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Drawing;

public partial class GridViewHelperSample : System.Web.UI.Page
{
    private GridViewHelper helper;

    // To show custom operations...
    private List<int> mQuantities = new List<int>();

    protected void Page_Load(object sender, EventArgs e)
    {
        int val = Convert.ToInt32( rdBtnList.SelectedValue );
        fsGroupBy.Visible = (val > 3 && val < 10);
        rdBtnLstGroup.Visible = fsGroupBy.Visible;
        fsUseFooter.Visible = (val == 7 || (val >= 1 && val <= 3));
        cbxUseFooter.Visible = fsUseFooter.Visible;

        if (val > 0)
        {
            helper = new GridViewHelper(this.GridView1, cbxUseFooter.Checked);
            ConfigSample();
        }
    }

    private void ConfigSample()
    {

        switch (rdBtnList.SelectedValue)
        {
            case "1":
                {
                    helper.RegisterSummary("ItemTotal", SummaryOperation.Sum);
                    break;
                }
            case "2":
                {
                    helper.RegisterSummary("Quantity", SaveQuantity, GetMinQuantity);
                    break;
                }
            case "3":
                {
                    helper.RegisterSummary("ItemTotal", SummaryOperation.Sum);
                    helper.RegisterSummary("Quantity", SaveQuantity, GetMinQuantity);
                    break;
                }
            case "4":
                {
                    helper.RegisterGroup(rdBtnLstGroup.SelectedValue, true, true);
                    helper.ApplyGroupSort();
                    break;
                }
            case "5":
                {
                    helper.RegisterGroup(rdBtnLstGroup.SelectedValue, true, false);
                    helper.ApplyGroupSort();
                    break;
                }
            case "6":
                {
                    GridViewGroup g = helper.RegisterGroup(rdBtnLstGroup.SelectedValue, true, true);
                    //helper.RegisterSummary("Quantity", SummaryOperation.Sum, rdBtnLstGroup.SelectedValue);
                    helper.RegisterSummary("ItemTotal", SummaryOperation.Sum, rdBtnLstGroup.SelectedValue);
                    helper.ApplyGroupSort();
                    break;
                }
            case "7":
                {
                    helper.RegisterGroup(rdBtnLstGroup.SelectedValue, true, true);
                    helper.RegisterSummary("ItemTotal", SummaryOperation.Sum, rdBtnLstGroup.SelectedValue);
                    helper.RegisterSummary("ItemTotal", SummaryOperation.Sum);
                    helper.ApplyGroupSort();
                    break;
                }
            case "8":
                {
                    helper.SetSuppressGroup(rdBtnLstGroup.SelectedValue);
                    helper.RegisterSummary("Quantity", SummaryOperation.Sum, rdBtnLstGroup.SelectedValue);
                    helper.RegisterSummary("ItemTotal", SummaryOperation.Sum, rdBtnLstGroup.SelectedValue);
                    helper.HideColumnsWithoutGroupSummary();
                    helper.ApplyGroupSort();
                    break;
                }
            case "9":
                {
                    helper.SetSuppressGroup(rdBtnLstGroup.SelectedValue);
                    helper.RegisterSummary("Quantity", SummaryOperation.Sum, rdBtnLstGroup.SelectedValue);
                    helper.RegisterSummary("ItemTotal", SummaryOperation.Sum, rdBtnLstGroup.SelectedValue);
                    helper.ApplyGroupSort();
                    break;
                }
            case "10":
                {
                    helper.RegisterGroup("ShipRegion", true, true);
                    helper.RegisterGroup("ShipName", true, true);
                    helper.GroupHeader += new GroupEvent(helper_GroupHeader);
                    helper.ApplyGroupSort();
                    break;
                }
            case "11":
                {
                    string[] cols = new string[2];
                    cols[0] = "ShipRegion";
                    cols[1] = "ShipName";
                    helper.RegisterGroup(cols, true, true);
                    helper.RegisterSummary("ItemTotal", SummaryOperation.Avg, "ShipRegion+ShipName");
                    helper.GroupSummary += new GroupEvent(helper_GroupSummary);
                    helper.ApplyGroupSort();
                    break;
                }
            case "12":
                {
                    helper.RegisterGroup("ShipRegion", true, true);
                    helper.RegisterGroup("ShipName", true, true);
                    helper.RegisterSummary("ItemTotal", SummaryOperation.Sum, "ShipName");
                    helper.RegisterSummary("ItemTotal", SummaryOperation.Sum);
                    helper.GroupSummary += new GroupEvent(helper_Bug);
                    helper.ApplyGroupSort();
                    break;
                }
            case "13":
                {
                    GridViewSummary s = helper.RegisterSummary("ItemTotal", SummaryOperation.Sum);
                    s.Automatic = false;
                    s = helper.RegisterSummary("ProductName", SummaryOperation.Count);
                    s.Automatic = false;
                    helper.GeneralSummary += new FooterEvent(helper_ManualSummary);
                    break;
                }
        }
    }

    private void helper_ManualSummary(GridViewRow row)
    {
        GridViewRow newRow = helper.InsertGridRow(row);
        newRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
        newRow.Cells[0].Text = String.Format("Total: {0} itens, {1:c}", helper.GeneralSummaries["ProductName"].Value, helper.GeneralSummaries["ItemTotal"].Value);
    }

    private void helper_GroupSummary(string groupName, object[] values, GridViewRow row)
    {
        row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
        row.Cells[0].Text = "Média";
    }

    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        if ( groupName == "ShipRegion" )
        {
            row.BackColor = Color.LightGray;
            row.Cells[0].Text = "&nbsp;&nbsp;" + row.Cells[0].Text;
        }
        else if (groupName == "ShipName")
        {
            row.BackColor = Color.FromArgb(236, 236, 236);
            row.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + row.Cells[0].Text;
        }
    }

    private void SaveQuantity(string column, string group, object value)
    {
        mQuantities.Add(Convert.ToInt32(value));
    }

    private object GetMinQuantity(string column, string group)
    {
        int[] qArray = new int[mQuantities.Count];
        mQuantities.CopyTo(qArray);
        Array.Sort(qArray);
        return qArray[0];
    }

    private void helper_Bug(string groupName, object[] values, GridViewRow row)
    {
        if (groupName == null) return;

        row.BackColor = Color.Bisque;
        row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        row.Cells[0].Text = "[ Summary for " + groupName + " " + values[0] + " ]";
    }
}
