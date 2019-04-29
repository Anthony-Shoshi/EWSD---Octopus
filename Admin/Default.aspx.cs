using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default : System.Web.UI.Page
{
    SqlConnection con;

    protected void Page_Load(object sender, EventArgs e)
    {
        GridView2.DataBind();
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
            Button btnSubmit = (Button)e.Row.FindControl("btnSubmit");
            HiddenField hdnFlag = (HiddenField)e.Row.FindControl("hdnFlag");
            if (hdnFlag.Value == "False")
            {
                e.Row.BackColor = Color.FromName("#d2322d");
                e.Row.ForeColor = Color.White;
                btnSubmit.Visible = false;
                //btnInsert.Visible = true;
            }
            else
                btnInsert.Visible = false;



        }
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        try
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");

            //try
            //{
            //    DateTime sdate = Convert.ToDateTime(txtClosureDate.Text);
            //    DateTime sdate2 = Convert.ToDateTime(txtFinaldate.Text);
            //}
            //catch (Exception ex)
            //{

            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please enter valid date')", true);

            //}

            SqlCommand cmd = new SqlCommand("INSERT INTO ChordTaskPost VALUES(@FieldName, @Flag, @date, @ClosureDate, @FinalClosureDate)", con);
            cmd.Parameters.AddWithValue("@FieldName", txtTaskName.Text);
            cmd.Parameters.AddWithValue("@Flag", true);
            cmd.Parameters.AddWithValue("@date", DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            cmd.Parameters.AddWithValue("@ClosureDate", DateTime.ParseExact(txtClosureDate.Text,"dd/MM/yyyy",CultureInfo.InvariantCulture));
            cmd.Parameters.AddWithValue("@FinalClosureDate", DateTime.ParseExact(txtFinaldate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));



            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            GridView2.DataBind();
            btnInsert.Visible = false;
            txtTaskName.Text = "";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Saved Successfully')", true);

        }
        catch (Exception ex)
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('"+ex.Message+"')", true);

        }
    }

    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.CommandArgument);
            DataKey dk = GridView2.DataKeys[index];
            int id = Convert.ToInt32(dk.Values["TaskId"]);
            GridViewRow row = GridView2.Rows[index];

            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");

            SqlCommand cmd = new SqlCommand("UPDATE [ChordTaskPost] SET [Flag] = @Flag WHERE [TaskId] = " + id + "", con);
            cmd.Parameters.AddWithValue("@Flag", false);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            GridView2.DataBind();
            btnInsert.Visible = true;

            ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", "alert('Success')", true);

        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
    }

    protected void txtStartDate_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime date;
            date = Convert.ToDateTime(txtStartDate.Text).AddDays(14);
            txtClosureDate.Text = date.ToString("dd/MM/yyyy");
            txtFinaldate.Text = date.AddDays(5).ToString("dd/MM/yyyy");
            
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}