using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mail_ComposeMail : System.Web.UI.Page
{
    SqlConnection con;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {

        }
        else
        {
            Response.Redirect("~/Presentation/frmLogin.aspx");

        }
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");


            SqlCommand com = new SqlCommand("GetAllUsers", con);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandTimeout = 600;
            SqlParameter p1 = new SqlParameter("@email", to.Value);
            com.Parameters.Add(p1);

            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            da.SelectCommand = com;
            con.Open();
            da.Fill(ds);
            con.Close();
            //string email = ds.Tables[0].Rows[0]["CordEmail"].ToString();
            int cid = 0;
            int sid = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Session["RoleId"].ToString() == "5")
                {
                    cid = Convert.ToInt32(Session["UserId"]);
                    sid = (int)ds.Tables[0].Rows[0]["Id"];
                }
                else if (Session["RoleId"].ToString() == "2")
                {
                    sid = Convert.ToInt32(Session["UserId"]);
                    cid = (int)ds.Tables[0].Rows[0]["Id"];
                }

                #region mail Send
                var body = "<p>Email From: {0} ({1})</p><p>Subject: {2}</p><p>Message:</p><p>{3}</p>";
                MailMessage mail = new MailMessage();
                mail.To.Add(to.Value);
                mail.From = new MailAddress("saad.ewsd@gmail.com", "EWSD");
                mail.Body = string.Format(body, "", to.Value, subject.Value, txtMailBody.Text); ;

                mail.Subject = subject.Value;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("saad.ewsd@gmail.com", "1234@A.com");
                smtp.EnableSsl = true;
                smtp.Send(mail);

                #endregion
                #region Save into Mail table
                SqlCommand command = new SqlCommand("InsertMailTable", con);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 600;

                command.Parameters.AddWithValue("@mailSub", mail.Subject.ToString());
                command.Parameters.AddWithValue("@fromMail", Session["Email"].ToString());
                command.Parameters.AddWithValue("@tomail", to.Value);
                command.Parameters.AddWithValue("@mailBody", txtMailBody.Text);
                command.Parameters.AddWithValue("@studentId", sid);
                command.Parameters.AddWithValue("@cordId", cid);
                con.Open();
                command.ExecuteNonQuery();
                con.Close();
                #endregion
                to.Value = "";
                subject.Value = "";
                txtMailBody.Text = "";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Mail Sent')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('This mail id dosent belongs to any users of this system')", true);

            }


        }
        catch (Exception ex)
        {

            throw;
        }
    }
}