using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Marketing_Coordinator_Default : System.Web.UI.Page
{
    SqlConnection con;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
                if(Session["RoleId"].ToString() == "6" && Request.QueryString["depid"] != null)
                {

                    int deptId = Convert.ToInt32(Request.QueryString["depid"]);

                    BindGrid(0, deptId);
                    if(deptId == 1)
                    {
                        this.Master.SetTitle = "Information Technology";
                    }
                    else if (deptId == 2)
                    {
                        this.Master.SetTitle = "Mathmetics";
                    }
                    else if (deptId == 3)
                    {
                        this.Master.SetTitle = "History";
                    }
                }
                else
                {
                    BindGrid(Convert.ToInt32(Session["UserId"]), 0);

                }
            }
        }
        else
        {
            Response.Redirect("~/Presentation/frmLogin.aspx");

        }
    }

    public void BindGrid(int cordId, int deptId)
    {
        con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
        SqlCommand com = new SqlCommand("GetTaskForCoodinator", con);
        com.CommandType = CommandType.StoredProcedure;
        com.CommandTimeout = 600;
        SqlParameter p1 = new SqlParameter("@CoodId", cordId);
        SqlParameter p2 = new SqlParameter("@deptId", deptId);
        com.Parameters.Add(p1);
        com.Parameters.Add(p2);

        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        da.SelectCommand = com;
        con.Open();
        da.Fill(ds);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        con.Close();
    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        try
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");

            SqlCommand cmd = new SqlCommand("INSERT INTO ChordTaskPost VALUES(@FieldName, @Flag, @date)", con);
            cmd.Parameters.AddWithValue("@FieldName", txtTaskName.Text);
            cmd.Parameters.AddWithValue("@Flag", true);
            cmd.Parameters.AddWithValue("@date", DateTime.Now);



            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            GridView2.DataBind();

        }
        catch (Exception ex)
        {

            throw;
        }
        Literal1.Text = "Data Inserted Successfully";
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
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

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button btnSubmit = (Button)e.Row.FindControl("btnSubmit");
            HiddenField hdnFlag = (HiddenField)e.Row.FindControl("hdnFlag");

            DataKey dk = GridView2.DataKeys[e.Row.RowIndex];
            if (hdnFlag.Value == "False")
            {
                btnSubmit.Visible = false;
            }
            else
            {
                btnInsert.Visible = false;
            }
        }
    }

    protected void chkChecked_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            int indx = 0;
            int error = 0;
            int result;
            CheckBox chk = (CheckBox)sender;

            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
            indx = Convert.ToInt32(chk.Text);
            DataKey dk = GridView1.DataKeys[indx];
            int id = Convert.ToInt32(dk.Values["Id"]);
            SqlCommand com = new SqlCommand("GetUserInfo", con);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandTimeout = 600;
            SqlParameter p1 = new SqlParameter("@Id", id);
            com.Parameters.Add(p1);

            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            da.SelectCommand = com;
            con.Open();
            da.Fill(ds);
            con.Close();
            string email = ds.Tables[0].Rows[0]["email"].ToString();
            int sid = (int)ds.Tables[0].Rows[0]["StudentId"];
            #region mail Send
            if (email != "")
            {
                var body = "<p>Email From: {0} ({1})</p><p>Subject: {2}</p><p>Message:</p><p>{3}</p>";
                MailMessage mail = new MailMessage();
                mail.To.Add(email);
                mail.From = new MailAddress("saad.ewsd@gmail.com", "EWSD");
                mail.Body = string.Format(body, "Hi", email, "EWSD", "Your File has been accepted"); ;

                mail.Subject = "Mail From EWSD";
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("saad.ewsd@gmail.com", "1234@A.com");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Successfully sent mail to selected student')", true);
                SqlCommand cmd = new SqlCommand("UPDATE [tblFile] SET [IsSent] = @IsSent WHERE [Id] = " + id + "", con);
                cmd.Parameters.AddWithValue("@IsSent", true);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                chk.Checked = true;
                chk.Enabled = false; 
                #endregion

            #region Save into Mail table
                SqlCommand command = new SqlCommand("InsertMailTable", con);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 600;

                command.Parameters.AddWithValue("@mailSub", mail.Subject.ToString());
                command.Parameters.AddWithValue("@fromMail", Session["Email"].ToString());
                command.Parameters.AddWithValue("@tomail", email.ToString());
                command.Parameters.AddWithValue("@mailBody", "Your File has been accepted");
                command.Parameters.AddWithValue("@studentId", sid);
                command.Parameters.AddWithValue("@cordId", Session["UserId"].ToString());
                con.Open();
                command.ExecuteNonQuery();
                con.Close();
                #endregion

            }
            
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkChecked = (CheckBox)e.Row.FindControl("chkChecked");
            TextBox txtComment = (TextBox)e.Row.FindControl("txtComment");
            Button btnComment = (Button)e.Row.FindControl("btnComment"); 
             HiddenField hdnIsSent = (HiddenField)e.Row.FindControl("hdnIsSent");
            HiddenField hdndateCreated = (HiddenField)e.Row.FindControl("hdndateCreated");
            DateTime dateCreated = Convert.ToDateTime(hdndateCreated.Value);
            DateTime expiryDate = dateCreated.AddDays(14);
            if (hdnIsSent.Value == "True")
            {
                chkChecked.Checked = true;
                chkChecked.Enabled = false;
            }
            if(DateTime.Now > expiryDate)
            {
                txtComment.Enabled = false;
                btnComment.Enabled = false;
            }

        }
    }

    protected void btnComment_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.CommandArgument);

            DataKey dk = GridView1.DataKeys[index];
            int id = Convert.ToInt32(dk.Values["Id"]);
            GridViewRow row = GridView1.Rows[index];
            TextBox txtComment = row.FindControl("txtComment") as TextBox;

            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");

            SqlCommand cmd = new SqlCommand("UPDATE [tblFile] SET [Comment] = @Comment WHERE [Id] = " + id + "", con);
            cmd.Parameters.AddWithValue("@Comment", txtComment.Text);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", "alert('Success')", true);

        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
    }
}