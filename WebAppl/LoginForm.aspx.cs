using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WebAppl
{
    public partial class LoginForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        PagesDataContext dbcon;
        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Riot Games\\VALORANT\\KarateSchool(1).mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=True";

        protected void Login_Authenticate(object sender, AuthenticateEventArgs e)
        {
            dbcon = new PagesDataContext(connectionString);
            string nUserName = Login.UserName;
            string nPassword = Login.Password;


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

            }
            if (myUser != null && HttpContext.Current.Session["userType"].ToString().Trim() == "Member")
            {

                FormsAuthentication.RedirectFromLoginPage(HttpContext.Current.Session["nUserName"].ToString(), true);

                Response.Redirect("~/MemberInfo/MemberForm.aspx");
            }
            else if (myUser != null && HttpContext.Current.Session["userType"].ToString().Trim() == "Instructor")
            {

                FormsAuthentication.RedirectFromLoginPage(HttpContext.Current.Session["nUserName"].ToString(), true);

                Response.Redirect("~/InstructorInfo/InstructorForm.aspx");
            }
            else
                Response.Redirect("Logon.aspx", true);


        }

    



    private string GetUserType(string username)
        {
            // Connection string

            // SQL query to retrieve UserType based on username
            string query = "SELECT UserType FROM NetUser WHERE UserName = @UserName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", username);

                    connection.Open();

                    // Execute the query and retrieve UserType
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        return result.ToString();
                    }
                }
            }

            return string.Empty; // Handle unknown UserType or non-existent user
        }

        private void RedirectBasedOnUserType(string userType)
        {
            switch (userType)
            {
                case "Member":
                    Response.Redirect("~/MemberForm.aspx");
                    break;

                case "Instructor":
                    Response.Redirect("~/InstructorForm.aspx");
                    break;

                default:
                    Login.FailureText = "Invalid user type.";
                    break;
            }
        }
    }
}
    



    
