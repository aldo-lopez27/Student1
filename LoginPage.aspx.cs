using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment4
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            PagesDataContext dbcon;
            string con = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Riot Games\\VALORANT\\KarateSchool(1).mdf\";Integrated Security=True;Connect Timeout=30";


            dbcon = new PagesDataContext(con);
                string nUserName = Login1.UserName;
                string nPassword = Login1.Password;


                HttpContext.Current.Session["nUserName"] = nUserName;
                HttpContext.Current.Session["uPass"] = nPassword;



                // Search for the current User, validate UserName and Password
                NetUser myUser = (from x in dbcon.NetUsers
                                  where x.UserName == HttpContext.Current.Session["nUserName"].ToString()
                                  && x.UserPassword == HttpContext.Current.Session["uPass"].ToString()
                                  select x).First();

                if (myUser != null)
                {
                    //Add UserID and User type to the Session
                    HttpContext.Current.Session["userID"] = myUser.UserID;
                    HttpContext.Current.Session["userType"] = myUser.UserType;
                    Session["userID"] = myUser.UserID;

                }
                if (myUser != null && HttpContext.Current.Session["userType"].ToString().Trim() == "Member")
                {

                    FormsAuthentication.RedirectFromLoginPage(HttpContext.Current.Session["nUserName"].ToString(), true);

                    Response.Redirect("MemberPage.aspx");
                }
                else if (myUser != null && HttpContext.Current.Session["userType"].ToString().Trim() == "Instructor")
                {

                    FormsAuthentication.RedirectFromLoginPage(HttpContext.Current.Session["nUserName"].ToString(), true);

                    Response.Redirect("InstructorPage.aspx");
                }
                else
                    Response.Redirect("Logon.aspx", true);
            }
        }
    } 