using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.IO;
using Ionic.Zip;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Drawing;

public partial class Marketing_Manager_Default : System.Web.UI.Page
{
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DownloadFiles(object sender, EventArgs e)
    {
        try
        {
            using (ZipFile zip = new ZipFile())
            {
                int count = 0;
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName("Images");
                foreach (GridViewRow row in GridView1.Rows)
                {
                    if ((row.FindControl("chkSelect") as CheckBox).Checked)
                    {
                        string filePath = Server.MapPath("~/Images/") + "" + (row.FindControl("lblFileName") as Label).Text;
                        zip.AddFile(filePath, "Images");
                        count++;
                    }
                }
                if (count > 0)
                {
                    Response.Clear();
                    Response.BufferOutput = false;
                    string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                    Response.ContentType = "application/zip";
                    Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                    zip.Save(Response.OutputStream);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    count = 0;
                }
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please select at least one')", true);

            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Something Went wrong.')", true);

        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkChecked = (CheckBox)e.Row.FindControl("chkChecked");
            HiddenField hdnIsSent = (HiddenField)e.Row.FindControl("hdnIsSent");
            if (hdnIsSent.Value == "True")
            {
                chkChecked.Checked = true;
                chkChecked.Enabled = false;
            }


        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnFlag = (HiddenField)e.Row.FindControl("hdnFlag");
            if (hdnFlag.Value == "False")
            {
                e.Row.BackColor = Color.FromName("#d2322d");
                e.Row.ForeColor = Color.White;
            }


        }
    }
}