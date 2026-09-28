using System;
using System.Web.UI;
using VelascoPersonalWebsite_IPT.Application.DTOs;
using VelascoPersonalWebsite_IPT.Application.Services;

namespace VelascoPersonalWebsite_IPT.Pages
{
    public partial class Register : Page
    {
        private readonly RegistrationService registrationService = new RegistrationService();
        private readonly AuditService auditService = new AuditService();

        protected void Page_Load(object sender, EventArgs e)
        {
            auditService.RecordPageVisit(null);
        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            RegistrationMessageLabel.Visible = false;

            Page.Validate("Registration");
            if (!Page.IsValid)
            {
                return;
            }

            // Map inputs
            var dto = new RegistrationDto
            {
                Username = UsernameTextBox.Text?.Trim(),
                Email = EmailTextBox.Text?.Trim(),
                Password = PasswordTextBox.Text,
                ConfirmPassword = ConfirmPasswordTextBox.Text,
                DateOfBirth = null
            };

            DateTime dob;
            if (DateTime.TryParse(DateOfBirthTextBox.Text, out dob)) dto.DateOfBirth = dob;

            // Parse enums safely
            VelascoPersonalWebsite_IPT.DataAccess.Enums.EmploymentStatus emp;
            if (Enum.TryParse(EmploymentStatusDropDownList.SelectedValue, true, out emp)) dto.EmploymentStatus = emp;

            VelascoPersonalWebsite_IPT.DataAccess.Enums.Gender gender;
            if (Enum.TryParse(GenderRadioButtonList.SelectedValue, true, out gender)) dto.Gender = gender;

            string error;
            var success = registrationService.Register(dto, out error);

            if (!success)
            {
                RegistrationMessageLabel.CssClass = "alert alert-danger small py-2 mb-4";
                RegistrationMessageLabel.Text = GetRegistrationErrorMessage(error);
                RegistrationMessageLabel.Visible = true;
                return;
            }

            RegistrationMessageLabel.CssClass = "alert alert-success small py-2 mb-4";
            RegistrationMessageLabel.Text = "Account successfully created. Redirecting to sign in...";
            RegistrationMessageLabel.Visible = true;

            var redirectUrl = ResolveUrl("~/login");
            var script = $"setTimeout(function() {{ window.location = '{redirectUrl}'; }}, 2000);";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "redirect", script, true);
        }

        private static string GetRegistrationErrorMessage(string error)
        {
            switch (error)
            {
                case "username-invalid":
                    return "Enter a username with 3–20 letters, numbers, or underscores.";
                case "email-invalid":
                    return "Enter a valid email address.";
                case "password-invalid":
                    return "Password must be at least 8 characters and include uppercase, lowercase, a number, and a special character.";
                case "password-mismatch":
                    return "Passwords must match.";
                case "date-invalid":
                    return "Enter a valid date of birth that is not in the future.";
                case "profile-invalid":
                    return "Select a valid current status and gender.";
                case "conflict":
                    return "Registration failed. Please ensure your details are valid and try again.";
                default:
                    return "We could not create your account. Please try again later.";
            }
        }
    }
}
