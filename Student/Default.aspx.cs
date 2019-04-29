using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_Default : System.Web.UI.Page
{

    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");

        if (Session["UserId"] != null)
        {
            BindData();
            ForClousuredate();
            
   
            if (!IsPostBack)
            {

                GridView2.Visible = false;
                //if (GridView2.Rows.Count == 0)
                //{
                //    divtermsAndConditions.Visible = false;
                //    divUpload.Visible = false;
                //}
                //else
                //{
                //    divtermsAndConditions.Visible = true;

                //    //divUpload.Visible = true;
                //}
                GetCoordinator();

            }
        }
        else
        {
            Response.Redirect("~/Presentation/frmLogin.aspx");

        }


    }

    public void GetCoordinator()
    {
        try
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
            SqlCommand com = new SqlCommand("GetAllCordinatior", con);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandTimeout = 600;
            SqlParameter p1 = new SqlParameter("@deptId", Session["DepId"].ToString());
            com.Parameters.Add(p1);

            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            da.SelectCommand = com;
            con.Open();
            da.Fill(ds);
            con.Close();
            ddlCoord.DataSource = ds.Tables[0];
            ddlCoord.DataValueField = "Id";
            ddlCoord.DataTextField = "firstname";
            ddlCoord.DataBind();
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    public void BindData()
    {
        try
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
            SqlCommand com = new SqlCommand("GetTaskForStudent", con);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandTimeout = 600;
            SqlParameter p1 = new SqlParameter("@StudentId", Session["UserId"]);
            //SqlParameter p2 = new SqlParameter("@cordid", "0");
            com.Parameters.Add(p1);
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            da.SelectCommand = com;
            con.Open();
            da.Fill(ds);
            con.Close();
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("sp_InsTblFile", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 600;

            cmd.Parameters.AddWithValue("@ft", DropDownList1.Text);
            cmd.Parameters.AddWithValue("@dc", DateTime.Now);
            if (FileUpload1.HasFile)
            {
                cmd.Parameters.AddWithValue("@fn", FileUpload1.FileName);
                string ext = Path.GetExtension(FileUpload1.FileName);
                if ((DropDownList1.SelectedValue == "Document") && ext != ".doc" && ext != ".docx" && ext != ".pdf" && ext != ".xlsx" && ext != ".xls" && ext != ".txt")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please select document file')", true);
                    return;
                }
                if ((DropDownList1.SelectedValue == "Image") && ext != ".jpg" && ext != ".JPEG" && ext != ".png" && ext != ".gif" && ext != ".jpeg" && ext != ".PNG")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please select image file')", true);
                    return;
                }

                FileUpload1.SaveAs(Server.MapPath("~/Images/") + FileUpload1.FileName);
            }
            else
            {
                cmd.Parameters.AddWithValue("@fn", "");
            }
            cmd.Parameters.AddWithValue("@StudentId", Convert.ToInt32(Session["UserId"].ToString()));
            cmd.Parameters.AddWithValue("@CoordId", Convert.ToInt32(ddlCoord.SelectedValue));

            SqlParameter Id = new SqlParameter("@Id", SqlDbType.Int);
            Id.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(Id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            int tblFileId = (Int32)Id.Value;
            GridView1.DataBind();
            Literal1.Text = "Data Inserted Successfully";


            SendMail(tblFileId);
            BindData();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Data Inserted Successfully')", true);
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Failed to insert data')", true);
        }

    }


    public void SendMail(int tblFileId)
    {
        con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");


        SqlCommand com = new SqlCommand("GetUserInfo", con);
        com.CommandType = CommandType.StoredProcedure;
        com.CommandTimeout = 600;
        SqlParameter p1 = new SqlParameter("@Id", tblFileId);
        com.Parameters.Add(p1);

        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        da.SelectCommand = com;
        con.Open();
        da.Fill(ds);
        con.Close();
        string email = ds.Tables[0].Rows[0]["CordEmail"].ToString();
        int cid = (int)ds.Tables[0].Rows[0]["CordId"];
        #region mail Send
        if (email != "")
        {
            var body = "<p>Email From: {0} ({1})</p><p>Subject: {2}</p><p>Message:</p><p>{3}</p>";
            MailMessage mail = new MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress("saad.ewsd@gmail.com", "EWSD");
            mail.Body = string.Format(body, "Hi", email, "EWSD", "You Have a new submission from Student"); ;

            mail.Subject = "Mail From EWSD";
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("saad.ewsd@gmail.com", "1234@A.com");
            smtp.EnableSsl = true;
            smtp.Send(mail);
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Successfully sent mail to selected coordinator')", true);

            #endregion

            #region Save into Mail table
            SqlCommand command = new SqlCommand("InsertMailTable", con);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 600;

            command.Parameters.AddWithValue("@mailSub", mail.Subject.ToString());
            command.Parameters.AddWithValue("@fromMail", Session["Email"].ToString());
            command.Parameters.AddWithValue("@tomail", email.ToString());
            command.Parameters.AddWithValue("@mailBody", "You Have a new submission from Student");
            command.Parameters.AddWithValue("@studentId", Session["UserId"].ToString());
            command.Parameters.AddWithValue("@cordId", cid);
            con.Open();
            command.ExecuteNonQuery();
            con.Close();
            #endregion
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(hdnId.Value);
        if (id > 0)
        {
            Update(id);
            BindData();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Updated Failed !!!')", true);
        }
    }

    protected void chkAgree_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAgree.Checked == true)
            divUpload.Visible = true;
        else divUpload.Visible = false;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //#0088cc
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblComment = (Label)e.Row.FindControl("lblComment");


            if (lblComment.Text != "")
            {
                e.Row.BackColor = Color.FromName("#0088cc");
                e.Row.ForeColor = Color.White;
            }
        }
    }

    public void ForClousuredate()
    {
        DateTime? CloseDate = null;
        DateTime? FinalDate = null;
        foreach (GridViewRow r in GridView3.Rows)
        {
            Label lblCloseDate = r.FindControl("lblCloseDate") as Label;
            Label lblfinalDate = r.FindControl("lblfinalDate") as Label;
            CloseDate = Convert.ToDateTime(lblCloseDate.Text);
            FinalDate = Convert.ToDateTime(lblfinalDate.Text);


        }
        foreach (GridViewRow r in GridView1.Rows)
        {
            LinkButton btnEdit = r.FindControl("btnEdit") as LinkButton;

        }
        if (CloseDate > DateTime.Now)
        {
            //CloseDate = DateTime.ParseExact(CloseDate.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            btnInsert.Visible = true;
            int daysDiff = ((TimeSpan)(CloseDate - DateTime.Now)).Days;
            hdnduration.Value = CloseDate.Value.ToString("dd/MM/yyyy hh:mm:ss tt");

            divCountdown.InnerHtml = "<strong>Closure date: "+ CloseDate.Value.ToString("dd/MM/yyyy") + "</strong>";
            divAlertCountdown.Visible = true;
        }
        else if (FinalDate > DateTime.Now)
        {
            btnInsert.Visible = false;
            divtermsAndConditions.Visible = false;
            int daysDiff = ((TimeSpan)(CloseDate - DateTime.Now)).Days;
            hdnduration.Value = FinalDate.Value.ToString("dd/MM/yyyy hh:mm:ss tt");
            divCountdown.InnerHtml = "<strong>Final Submission: " + FinalDate.Value.ToString("dd/MM/yyyy") + "</strong>";
            divAlertCountdown.Visible = true;
            foreach (GridViewRow r in GridView1.Rows)
            {
                LinkButton btnEdit = r.FindControl("btnEdit") as LinkButton;
                btnEdit.Visible = true;

            }
        }
        else
        {
            divtermsAndConditions.Visible = false;
        }
    }
    protected void btnComment_Click(object sender, EventArgs e)
    {

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {

    }

    protected void GridView1_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "update")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                DataKey dk = GridView1.DataKeys[index];
                int Id = Convert.ToInt32(dk.Values["Id"]);
                hdnId.Value = Id.ToString();
                GridViewRow row = GridView1.Rows[index];
                Label lblDateCreated = row.FindControl("lblDateCreated") as Label;
                DateTime createDate = Convert.ToDateTime(lblDateCreated.Text);
                divtermsAndConditions.Visible = false;
                foreach (GridViewRow r in GridView3.Rows)
                {
                    Label lblCloseDate = r.FindControl("lblCloseDate") as Label;
                    Label lblfinalDate = r.FindControl("lblfinalDate") as Label;
                    DateTime CloseDate = Convert.ToDateTime(lblCloseDate.Text);
                    DateTime FinalDate = Convert.ToDateTime(lblfinalDate.Text);
                    if (FinalDate > DateTime.Now)
                    {
                        chkAgree.Checked = true;
                        divUpload.Visible = true;
                        btnDelete.Visible = true;
                        btnInsert.Visible = false;

                    }

                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }

    public void Update(int id)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("UpdateTblFile", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 600;

            cmd.Parameters.AddWithValue("@ft", DropDownList1.Text);
            cmd.Parameters.AddWithValue("@dc", DateTime.Now);
            if (FileUpload1.HasFile)
            {
                cmd.Parameters.AddWithValue("@fn", FileUpload1.FileName);
                string ext = Path.GetExtension(FileUpload1.FileName);
                if ((DropDownList1.SelectedValue == "Document") && ext != ".doc" && ext != ".docx" && ext != ".pdf" && ext != ".xlsx" && ext != ".xls" && ext != ".txt")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please select document file')", true);
                    return;
                }
                if ((DropDownList1.SelectedValue == "Image") && ext != ".jpg" && ext != ".JPEG" && ext != ".png" && ext != ".gif" && ext != ".jpeg" && ext != ".PNG")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please select image file')", true);
                    return;
                }

                FileUpload1.SaveAs(Server.MapPath("~/Images/") + FileUpload1.FileName);
            }
            else
            {
                cmd.Parameters.AddWithValue("@fn", "");
            }
            cmd.Parameters.AddWithValue("@StudentId", Convert.ToInt32(Session["UserId"].ToString()));
            cmd.Parameters.AddWithValue("@CoordId", Convert.ToInt32(ddlCoord.SelectedValue));
            cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(id));
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            GridView1.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Updated Successfully')", true);
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    protected void GridView1_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        // throw new NotImplementedException();
    }
}