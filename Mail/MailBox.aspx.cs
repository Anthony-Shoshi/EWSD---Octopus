using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mail_MailBox : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection con;

        if (Session["UserId"] != null)
        {
            int studentid = 0;
            int cordid = 0;

            if (Session["RoleId"].ToString() == "5")
            {
                cordid = Convert.ToInt32(Session["UserId"]);
                studentid = 0;
            }
            else if (Session["RoleId"].ToString() == "2")
            {
                studentid = Convert.ToInt32(Session["UserId"]);
                cordid = 0;
            }

            if (!IsPostBack)
            {
                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
                SqlCommand com = new SqlCommand("GetMail", con);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 600;
                SqlParameter p1 = new SqlParameter("@studentId", studentid);
                SqlParameter p2 = new SqlParameter("@cordid", cordid);
                com.Parameters.Add(p1);
                com.Parameters.Add(p2);

                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                da.SelectCommand = com;
                con.Open();
                da.Fill(ds);
                con.Close();
                string ul = "";
                DataTable dt = ds.Tables[0];
                DataRow[] filteredRows = null;
                //var filtered = dt.AsEnumerable().Where(r => r.Field<String>("fromMail").Contains(Session["Email"].ToString()));
                //this.Master.SetTitle = filtered.Count().ToString();
                if (Request.QueryString["index"] == null || Request.QueryString["index"].ToString() == "1")
                {
                     filteredRows = dt.Select("toMail = '" + Session["Email"].ToString() + "'");
                    h2.InnerHtml = "Inbox";
                }
                else if (Request.QueryString["index"].ToString() == "2")
                {
                    filteredRows = dt.Select("fromMail = '" + Session["Email"].ToString() + "'");
                    h2.InnerHtml = "Sent";


                }
                foreach (DataRow dr in filteredRows)
                {
                    string frommail = dr["fromMail"].ToString();
                    string tomail = dr["toMail"].ToString();
                    string sendBy = dr["fromMail"].ToString();
                    if (Session["Email"].ToString().Trim() == frommail)
                        sendBy = "You";
                    ul += "<li class='unread'>" +
                                                    "<a href = 'MailDetails.aspx?mid="+ dr["mailId"].ToString() + "'>" +
                                                         "<div class='col-sender'>" +
                                                            "<div class='checkbox-custom checkbox-text-primary ib'>" +
                                                                "<input type = 'checkbox' id='mail1'>" +
                                                                "<label for='mail1'></label>" +
                                                            "</div>" +
                                                            "<p class='m-none ib'>" + sendBy + "</p>" +
                                                        "</div>" +
                                                        "<div class='col-mail'>" +
                                                            "<p class='m-none mail-content'>" +
                                                                "<span class='subject'>" + dr["mailSub"].ToString() + "</span>" +
                                                                " | <span class='mail-partial'>" + dr["mailBody"].ToString() + "</span>" +
                                                            "</p>" +
                                                            "<p class='m-none mail-date'>" + Convert.ToDateTime(dr["entryDate"]).ToString("dd/MM/yyyy") + "</p>" +
                                                        "</div>" +
                                                    "</a>" +
                                                "</li>";
                }
                ulMailList.InnerHtml = ul;
            }
        }
    }
}