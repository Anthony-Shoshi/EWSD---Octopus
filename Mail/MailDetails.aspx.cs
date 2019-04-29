using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mail_MailDetails : System.Web.UI.Page
{
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (Request.QueryString["mid"] != "")
            {
                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
                SqlCommand com = new SqlCommand("GetMailDetails", con);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 600;
                SqlParameter p1 = new SqlParameter("@mailId", Request.QueryString["mid"].ToString());
                com.Parameters.Add(p1);

                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                da.SelectCommand = com;
                con.Open();
                da.Fill(ds);
                con.Close();

                string mailBody = "";
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string frommail = dr["fromMail"].ToString();
                    string tomail = dr["toMail"].ToString();
                    string sendBy = dr["fromMail"].ToString();
                    if (Session["Email"].ToString().Trim() == frommail)
                        sendBy = "You";
                    if (Session["Email"].ToString().Trim() == tomail)
                        tomail = "You";

                    mailBody += "<div class='mailbox-email-screen'>" +
                         "<div class='panel'>" +
                        "<div class='panel-heading'>" +
                            "<div class='panel-actions'>" +
                                "<a href = '#' class='fa fa-caret-down'></a>" +
                                "<a href = '#' class='fa fa-mail-reply'></a>" +
                                "<a href = '#' class='fa fa-mail-reply-all'></a>" +
                                "<a href = '#' class='fa fa-star-o'></a>" +
                            "</div>" +
                           "<p class='panel-title'>" + sendBy + "<i class='fa fa-angle-right fa-fw'></i>" + dr["toMail"].ToString() + "</p>" +
                           "<br/><p class='panel-title'>" + dr["mailSub"].ToString() + "</p>" +

                        "</div>" +
                        "<div class='panel-body'>" +
                            "<p>" + dr["mailBody"].ToString() + "</p>" +
                            "</div>" +
                        "<div class='panel-footer'>" +
                           " <p class='m-none'><small>" + dr["entryDate"].ToString() + "</small></p>" +
                        "</div>" +
                    "</div>" +
                "</div>";
                }
                divBody.InnerHtml = mailBody;

            }
        }

    }
}