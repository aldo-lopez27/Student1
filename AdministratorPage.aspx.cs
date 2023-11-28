using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment4
{
    public partial class AdministratorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PagesDataContext dbcon;
            string con = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Riot Games\\VALORANT\\KarateSchool(1).mdf\";Integrated Security=True;Connect Timeout=30";

            dbcon = new PagesDataContext(con);
            PopulateSectionsDropDown();
            var queryMember = from x in dbcon.Members
                              select new
                              {
                                  x.MemberFirstName,
                                  x.MemberLastName,
                                  x.MemberPhoneNumber,
                                  x.MemberDateJoined
                              };

            // Bind the query result to the GridView
            gridViewMemberAdmin.DataSource = queryMember.ToList();
            gridViewMemberAdmin.DataBind();

            var queryInstructor = from x in dbcon.Instructors
                                  select new
                                  {
                                      x.InstructorFirstName,
                                      x.InstructorLastName,
                                  };
            gridViewInstructorAdmin.DataSource = queryInstructor.ToList();
            gridViewInstructorAdmin.DataBind();
        }




        private void LoadMembers()
        {

            PagesDataContext dbcon;
            string con = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Riot Games\\VALORANT\\KarateSchool(1).mdf\";Integrated Security=True;Connect Timeout=30";
            dbcon = new PagesDataContext(con);


            var members = from m in dbcon.Members
                          select new
                          {

                              MemberFirstName = m.MemberFirstName,
                              MemberLastName = m.MemberLastName,
                              MemberPhoneNumber = m.MemberPhoneNumber,
                              MemberDateJoined = m.MemberDateJoined,
                          };

            gridViewMemberAdmin.DataSource = members.ToList();
            gridViewMemberAdmin.DataBind();
        }


        private void LoadInstructors()
        {
            PagesDataContext dbcon;
            string con = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Riot Games\\VALORANT\\KarateSchool(1).mdf\";Integrated Security=True;Connect Timeout=30";
            dbcon = new PagesDataContext(con);


            var members = from i in dbcon.Instructors
                          select new
                          {

                              InstructorFirstName = i.InstructorFirstName,
                              InstructorLastName = i.InstructorLastName,

                          };

            gridViewMemberAdmin.DataSource = members.ToList();
            gridViewMemberAdmin.DataBind();
        }

        protected void gvMembers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            PagesDataContext dbcon;
            string con = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Riot Games\\VALORANT\\KarateSchool(1).mdf\";Integrated Security=True;Connect Timeout=30";
            dbcon = new PagesDataContext(con);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlSections = (DropDownList)e.Row.FindControl("ddlSections");


                var sections = from s in dbcon.Sections
                               select new
                               {
                                   SectionID = s.SectionID,
                                   SectionName = s.SectionName,
                                   SectionStartDate = s.SectionStartDate,
                                   Member_ID = s.Member_ID,
                                   Instructor_ID = s.Instructor_ID,
                                   SectionFee = s.SectionFee,
                               };

                ddlSections.DataSource = sections.ToList();
                ddlSections.DataTextField = "SectionName";
                ddlSections.DataValueField = "SectionID";
                ddlSections.DataTextField = "SectionStartDate";
                ddlSections.DataValueField = "Member_ID";
                ddlSections.DataValueField = "Instructor_ID";
                ddlSections.DataValueField = "SectionFee";
                ddlSections.DataBind();
            }
        }



        protected void addMemberInstructor_Click(object sender, EventArgs e)
        {
            PagesDataContext dbcon;
            string con = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Riot Games\\VALORANT\\KarateSchool(1).mdf\";Integrated Security=True;Connect Timeout=30";
            dbcon = new PagesDataContext(con);
            int nextUserID = GetNextUserID();

            // Get values from form controls
            string firstNameMember = firstNameTextBox.Text;
            string lastNameMember = lastNameTextBox.Text;
            string dateJoined = memberJoinDateTextBox.Text;
            string phoneNumber = phoneNumberTextBox.Text;
            string email = studentEmailTextBox.Text;

            string dateFormat = "MM-dd-yyyy";
            DateTime dateJoined1;

            dateJoined1 = DateTime.ParseExact(dateJoined, dateFormat, CultureInfo.InvariantCulture);
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO NetUser ( UserName, UserPassword, UserType) VALUES ( @UserName, @UserPassword, @UserType)", connection))
                {
                    command.Parameters.AddWithValue("@UserName", firstNameMember+"."+lastNameMember);
                    command.Parameters.AddWithValue("@UserPassword", lastNameMember);
                    command.Parameters.AddWithValue("@UserType", "Member");
                    

                    command.ExecuteNonQuery();
                }
            }

            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Member (Member_UserID, MemberFirstName, MemberLastName, MemberDateJoined, MemberPhoneNumber, MemberEmail) VALUES (@Member_UserID, @MemberFirstName, @MemberLastName, @MemberDateJoined, @MemberPhoneNumber, @MemberEmail)", connection))
                {
                    command.Parameters.AddWithValue("@Member_UserID", nextUserID);
                    command.Parameters.AddWithValue("@MemberFirstName", firstNameMember);
                    command.Parameters.AddWithValue("@MemberLastName", lastNameMember);
                    command.Parameters.AddWithValue("@MemberDateJoined", dateJoined);
                    command.Parameters.AddWithValue("@MemberPhoneNumber", phoneNumber);
                    command.Parameters.AddWithValue("@MemberEmail", email);

                    command.ExecuteNonQuery();
                }
                LoadMembers();
            }

            // Clear textboxes after adding member
            firstNameTextBox.Text = string.Empty;
            lastNameTextBox.Text = string.Empty;
            memberJoinDateTextBox.Text = string.Empty;
            phoneNumberTextBox.Text = string.Empty;
            studentEmailTextBox.Text = string.Empty;

        }
        private int GetNextUserID()
        {
            string con = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Riot Games\\VALORANT\\KarateSchool(1).mdf\";Integrated Security=True;Connect Timeout=30";

            int nextUserID = 1; // Default value if no users are in the database

            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();

                // Query to find the maximum existing UserID
                string query = "SELECT MAX(UserID) FROM NetUser";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        nextUserID = Convert.ToInt32(result) + 1;
                    }
                }
            }

            return nextUserID;
        }





        protected void addInstructorButton_Click(object sender, EventArgs e)
        {
            PagesDataContext dbcon;
            string con = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Riot Games\\VALORANT\\KarateSchool(1).mdf\";Integrated Security=True;Connect Timeout=30";
            dbcon = new PagesDataContext(con);
            int nextUserID = GetNextUserID();

            // Get values from form controls
            string firstNameMember = firstNameTextBox.Text;
            string lastNameMember = lastNameTextBox.Text;
            string phoneNumber = phoneNumberTextBox.Text;

            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO NetUser ( UserName, UserPassword, UserType) VALUES ( @UserName, @UserPassword, @UserType)", connection))
                {
                    // command.Parameters.AddWithValue("@UserID", nextUserID);
                    command.Parameters.AddWithValue("@UserName", firstNameMember + "." + lastNameMember);
                    command.Parameters.AddWithValue("@UserPassword", lastNameMember);
                    command.Parameters.AddWithValue("@UserType", "Instructor");


                    command.ExecuteNonQuery();
                }
            }

            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Instructor (InstructorID, InstructorFirstName, InstructorLastName, InstructorPhoneNumber,) VALUES (@InstructorID, @InstructorFirstName, @InstructorLastName, @InstructorPhoneNumber)", connection))
                {
                    command.Parameters.AddWithValue("@InstructorID", nextUserID);
                    command.Parameters.AddWithValue("@InstructorFirstName", firstNameMember);
                    command.Parameters.AddWithValue("@InstructorLastName", lastNameMember);
                    command.Parameters.AddWithValue("@InstructorPhoneNumber", phoneNumber);

                    command.ExecuteNonQuery();
                }
                LoadMembers();
            }

            // Clear textboxes after adding member
            firstNameTextBox.Text = string.Empty;
            lastNameTextBox.Text = string.Empty;
            memberJoinDateTextBox.Text = string.Empty;
            phoneNumberTextBox.Text = string.Empty;
            studentEmailTextBox.Text = string.Empty;
        }

        protected void assignMemberToSecionBtn_Click(object sender, EventArgs e)
        {
            PagesDataContext dbcon;
            string con = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Riot Games\\VALORANT\\KarateSchool(1).mdf\";Integrated Security=True;Connect Timeout=30";
            dbcon = new PagesDataContext(con);
            try
            {
                foreach (GridViewRow row in gridViewMemberAdmin.Rows)
                {
                    // Get the selected member and section values
                    int selectedMemberID = Convert.ToInt32(gridViewMemberAdmin.DataKeys[row.RowIndex]["MemberID"]);
                    DropDownList ddlSections = (DropDownList)row.FindControl("ddlSections");
                    int selectedSectionID = Convert.ToInt32(ddlSections.SelectedValue);

                    if (selectedMemberID > 0 && selectedSectionID > 0)
                    {
                        // Check if the member is already assigned to the section (you may need to modify this logic based on your actual schema)
                        bool isAlreadyAssigned = dbcon.Sections.Any(ms => ms.Member_ID == selectedMemberID && ms.SectionID == selectedSectionID);

                        if (!isAlreadyAssigned)
                        {
                            // Create a new MemberSection record to represent the assignment
                            Section newAssignment = new Section()
                            {
                                Member_ID = selectedMemberID,
                                SectionID = selectedSectionID,
                            };

                            dbcon.Sections.InsertOnSubmit(newAssignment);
                        }
                        else
                        {
                            string script = $"alert('One or more members are already assigned to the selected section.');";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                            return;
                        }
                    }
                    else
                    {
                        // Display an error message if member or section is not selected
                        string script = $"alert('Please select a member and a section for all rows.');";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                        return;
                    }
                }

                // Submit changes to the database
                dbcon.SubmitChanges();

                LoadMembers();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            string firstNameToDelete = firstNameTextBox.Text;
            string lastNameToDelete = lastNameTextBox.Text;
            string memberJoinDateToDelete = memberJoinDateTextBox.Text;
            string emailToDelete = studentEmailTextBox.Text;
            string phoneNumberToDelete = phoneNumberTextBox.Text;

            DeleteMember(firstNameToDelete, lastNameToDelete, memberJoinDateToDelete, emailToDelete, phoneNumberToDelete);
        }
        protected void DeleteMember(string firstName, string lastName, string memberJoinDate, string email, string phoneNumber)
        {
            string con = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Riot Games\\VALORANT\\KarateSchool(1).mdf\";Integrated Security=True;Connect Timeout=30";

            try
            {
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();

                    // Delete the member from Members table
                    string deleteMemberQuery = @"
                DELETE FROM Member 
                WHERE MemberFirstName = @MemberFirstName
                AND MemberLastName = @MemberLastName
                AND MemberDateJoined = @MemberDateJoined
                AND MemberEmail = @MemberEmail
                AND MemberPhoneNumber = @MemberPhoneNumber";

                    using (SqlCommand command = new SqlCommand(deleteMemberQuery, connection))
                    {
                        command.Parameters.AddWithValue("@MemberFirstName", firstName);
                        command.Parameters.AddWithValue("@MemberLastName", lastName);
                        command.Parameters.AddWithValue("@MemberDateJoined", memberJoinDate);
                        command.Parameters.AddWithValue("@MemberEmail", email);
                        command.Parameters.AddWithValue("@MemberPhoneNumber", phoneNumber);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Deletion from Members table successful

                            string deleteNetUserQuery = @"
                        DELETE FROM NetUser 
                        WHERE UserName = @UserName";

                            using (SqlCommand netUserCommand = new SqlCommand(deleteNetUserQuery, connection))
                            {
                                netUserCommand.Parameters.AddWithValue("@UserName", $"{firstName}.{lastName}");

                                int netUserRowsAffected = netUserCommand.ExecuteNonQuery();

                                if (netUserRowsAffected > 0)
                                {
                                    // Deletion from NetUser table successful
                                    Console.WriteLine($"Member deleted successfully.");
                                    LoadMembers();
                                }
                                else
                                {
                                    // No rows were affected in NetUser table, handle accordingly
                                    Console.WriteLine($"Member not found in NetUser table for the provided details.");
                                }
                            }
                        }
                        else
                        {
                            // No rows were affected in Members table, handle accordingly
                            Console.WriteLine($"Member not found in Members table for the provided details.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        protected void Button2_Click(object sender, EventArgs e)
            {
            
            }

        protected void DeleteInstructor(string firstName, string lastName,int phoneNumber)
        {
            string con = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Riot Games\\VALORANT\\KarateSchool(1).mdf\";Integrated Security=True;Connect Timeout=30";

            try
            {
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();

                    // Delete the instructor from Instructors table
                    string deleteInstructorQuery = @"
                DELETE FROM Instructor 
                WHERE InstructorFirstName = @InstructorFirstName
                AND InstructorLastName = @InstructorLastName
                AND  InstructorPhoneNumber = @InstructorPhoneNumber";

                    using (SqlCommand command = new SqlCommand(deleteInstructorQuery, connection))
                    {
                        command.Parameters.AddWithValue("@InstructorFirstName", firstName);
                        command.Parameters.AddWithValue("@InstructorLastName", lastName);
                        command.Parameters.AddWithValue("@InstructorPhoneNumber", phoneNumber);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Deletion from Instructors table successful

                            string deleteNetUserQuery = @"
                        DELETE FROM NetUser 
                        WHERE UserName = @UserName";

                            using (SqlCommand netUserCommand = new SqlCommand(deleteNetUserQuery, connection))
                            {
                                netUserCommand.Parameters.AddWithValue("@UserName", $"{firstName}.{lastName}");

                                int netUserRowsAffected = netUserCommand.ExecuteNonQuery();

                                if (netUserRowsAffected > 0)
                                {
                                    // Deletion from NetUser table successful
                                    Console.WriteLine($"Instructor deleted successfully.");
                                    LoadInstructors();
                                }
                                else
                                {
                                    // No rows were affected in NetUser table, handle accordingly
                                    Console.WriteLine($"Instructor not found in NetUser table for the provided details.");
                                }
                            }
                        }
                        else
                        {
                            // No rows were affected in Instructors table, handle accordingly
                            Console.WriteLine($"Instructor not found in Instructors table for the provided details.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void PopulateSectionsDropDown()
        {
            string con = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Riot Games\\VALORANT\\KarateSchool(1).mdf\";Integrated Security=True;Connect Timeout=30";

            try
            {
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();

                    string query = "SELECT SectionID, SectionName, SectionStartDate, Instructor_ID, SectionFee FROM Section";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            ddlSections.DataSource = reader;
                            ddlSections.DataTextField = "SectionName";
                            ddlSections.DataValueField = "SectionID";
                            ddlSections.DataTextField = "SectionStartDate";
                            ddlSections.DataValueField = "InstructorID";
                            ddlSections.DataValueField = "SectionFee";
                            ddlSections.DataBind();

                            ddlSections.Items.Insert(0, new ListItem("Select Section", ""));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void ddlSections_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void assignSectionButton_Click(object sender, EventArgs e)
        {
            // Validate and get member details from textboxes
            string firstName = firstNameTextBox.Text;
            string lastName = lastNameTextBox.Text;
            string memberJoinDate = memberJoinDateTextBox.Text;
            string email = studentEmailTextBox.Text;
            string phoneNumber = phoneNumberTextBox.Text;

            AssignMemberToSection(firstName, lastName, memberJoinDate, email, phoneNumber);
        
        }

        protected void AssignMemberToSection(string firstName, string lastName, string memberJoinDate, string email, string phoneNumber)
        {
            string con = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Riot Games\\VALORANT\\KarateSchool(1).mdf\";Integrated Security=True;Connect Timeout=30";

            try
            {
                int sectionId = Convert.ToInt32(ddlSections.SelectedValue);

                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();

                    string updateQuery = @"
                UPDATE Member 
                SET SectionID = @SectionID
                WHERE FirstName = @FirstName
                AND LastName = @LastName
                AND MemberJoinDate = @MemberJoinDate
                AND Email = @Email
                AND PhoneNumber = @PhoneNumber";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@SectionID", sectionId);
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@MemberJoinDate", memberJoinDate);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Member assigned to Section with ID {sectionId}");
                        }
                        else
                        {
                            Console.WriteLine($"Member or Section not found for the provided details.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

    }
}

