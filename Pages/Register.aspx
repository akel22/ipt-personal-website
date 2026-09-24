<%@ Page Title="Register" Language="C#" MasterPageFile="~/Unauthenticated.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="VelascoPersonalWebsite_IPT.Pages.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main class="container-fluid px-3 px-lg-4 py-3 registration-page">
        <div class="row justify-content-center align-items-center g-4 registration-layout">
            <div class="col-12 col-lg-4 col-xl-3 registration-persuasion-panel">
                <div class="text-center text-lg-start">
                        <img src="<%= ResolveUrl("~/assets/img/pointing_owl.png") %>" alt="Owl pointing toward the registration form" class="img-fluid registration-owl-image mb-4" />
                       
                </div>
            </div>

            <div class="col-12 col-lg-8 col-xl-7">
                <section class="card border bg-white shadow-sm registration-form-card">
                    <div class="card-body p-3 p-md-4 registration-form-column">
                        <header class="mb-3">
                            <h2 class="h4 fw-semibold text-dark mb-1">Create your account</h2>
                          <p class="text-secondary small mb-0">Fill in your details and view Ej's portfolio.</p>
                        </header>

                        <asp:ValidationSummary ID="RegistrationValidationSummary" runat="server"
                            ValidationGroup="Registration" CssClass="alert alert-danger small py-2 mb-4"
                            HeaderText="Please correct the following:" />

                        <asp:Label ID="RegistrationMessageLabel" runat="server" CssClass="d-none" Visible="false" />

                        <div class="row g-3 g-lg-4">

                            <%-- Left column: Account --%>
                            <div class="col-lg-6">
                                <h2 class="h6 fw-semibold text-dark border-bottom pb-2 mb-3">Account</h2>

                                <div class="mb-3">
                                    <asp:Label ID="UsernameLabel" runat="server" AssociatedControlID="UsernameTextBox"
                                        CssClass="form-label small fw-semibold text-dark mb-1" Text="Username" />
                                    <asp:TextBox ID="UsernameTextBox" runat="server" CssClass="form-control"
                                        MaxLength="20" autocomplete="username" placeholder="axelot123" />
                                    <asp:RequiredFieldValidator ID="UsernameRequiredValidator" runat="server"
                                        ControlToValidate="UsernameTextBox" ValidationGroup="Registration"
                                        CssClass="text-danger small" Display="Dynamic" ErrorMessage="Username is required."
                                        Text="Username is required." />
                                    <asp:RegularExpressionValidator ID="UsernameFormatValidator" runat="server"
                                        ControlToValidate="UsernameTextBox" ValidationGroup="Registration"
                                        ValidationExpression="^[A-Za-z0-9_]{3,20}$"
                                        CssClass="text-danger small"
                                        Display="Dynamic" ErrorMessage="Username must be 3–20 characters and contain only letters, numbers, or underscores."
                                        Text="Username must be 3–20 letters, numbers, or underscores." />
                                </div>

                                <div class="mb-3">
                                    <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="EmailTextBox"
                                        CssClass="form-label small fw-semibold text-dark mb-1" Text="Email address" />
                                    <asp:TextBox ID="EmailTextBox" runat="server" CssClass="form-control"
                                        TextMode="Email" MaxLength="254" autocomplete="email" placeholder="user@example.com" />
                                    <asp:RequiredFieldValidator ID="EmailRequiredValidator" runat="server"
                                        ControlToValidate="EmailTextBox" ValidationGroup="Registration"
                                        CssClass="text-danger small" Display="Dynamic" ErrorMessage="Email address is required."
                                        Text="Email address is required." />
                                    <asp:RegularExpressionValidator ID="EmailFormatValidator" runat="server"
                                        ControlToValidate="EmailTextBox" ValidationGroup="Registration"
                                        ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" CssClass="text-danger small"
                                        Display="Dynamic" ErrorMessage="Enter a valid email address."
                                        Text="Enter a valid email address." />
                                </div>

                                <div class="mb-3">
                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="PasswordTextBox"
                                        CssClass="form-label small fw-semibold text-dark mb-1" Text="Password" />
                                    <asp:TextBox ID="PasswordTextBox" runat="server" CssClass="form-control"
                                        TextMode="Password" autocomplete="new-password" placeholder="Include uppercase, lowercase, numbers, etc." />
                                    <asp:RequiredFieldValidator ID="PasswordRequiredValidator" runat="server"
                                        ControlToValidate="PasswordTextBox" ValidationGroup="Registration"
                                        CssClass="text-danger small" Display="Dynamic" ErrorMessage="Password is required."
                                        Text="Password is required." />
                                    <asp:RegularExpressionValidator ID="PasswordFormatValidator" runat="server"
                                        ControlToValidate="PasswordTextBox" ValidationGroup="Registration"
                                        ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d]).{8,}$"
                                        CssClass="text-danger small" Display="Dynamic"
                                        ErrorMessage="Password must be at least 8 characters and include uppercase, lowercase, a number, and a special character."
                                        Text="Use at least 8 characters with uppercase, lowercase, a number, and a special character." />
                                </div>

                                <div class="mb-0">
                                    <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPasswordTextBox"
                                        CssClass="form-label small fw-semibold text-dark mb-1" Text="Confirm password" />
                                    <asp:TextBox ID="ConfirmPasswordTextBox" runat="server" CssClass="form-control"
                                        TextMode="Password" autocomplete="new-password" placeholder="Re-enter your password" />
                                    <asp:RequiredFieldValidator ID="ConfirmPasswordRequiredValidator" runat="server"
                                        ControlToValidate="ConfirmPasswordTextBox" ValidationGroup="Registration"
                                        CssClass="text-danger small" Display="Dynamic" ErrorMessage="Password confirmation is required."
                                        Text="Password confirmation is required." />
                                    <asp:CompareValidator ID="PasswordMatchValidator" runat="server"
                                        ControlToValidate="ConfirmPasswordTextBox" ControlToCompare="PasswordTextBox"
                                        ValidationGroup="Registration" CssClass="text-danger small" Display="Dynamic"
                                        ErrorMessage="Passwords must match." Text="Passwords must match." />
                                </div>
                            </div>

                            <%-- Right column: Profile --%>
                            <div class="col-lg-6">
                                <h2 class="h6 fw-semibold text-dark border-bottom pb-2 mb-3">Profile</h2>

                                <div class="mb-3">
                                    <asp:Label ID="DateOfBirthLabel" runat="server" AssociatedControlID="DateOfBirthTextBox"
                                        CssClass="form-label small fw-semibold text-dark mb-1" Text="Date of birth" />
                                    <asp:TextBox ID="DateOfBirthTextBox" runat="server" CssClass="form-control"
                                        TextMode="Date" />
                                    <asp:RequiredFieldValidator ID="DateOfBirthRequiredValidator" runat="server"
                                        ControlToValidate="DateOfBirthTextBox" ValidationGroup="Registration"
                                        CssClass="text-danger small" Display="Dynamic" ErrorMessage="Date of birth is required."
                                        Text="Date of birth is required." />
                                    <asp:CompareValidator ID="DateOfBirthFormatValidator" runat="server"
                                        ControlToValidate="DateOfBirthTextBox" Operator="DataTypeCheck" Type="Date"
                                        ValidationGroup="Registration" CssClass="text-danger small" Display="Dynamic"
                                        ErrorMessage="Enter a valid date of birth." Text="Enter a valid date of birth." />
                                </div>

                                <div class="mb-3">
                                    <asp:Label ID="EmploymentStatusLabel" runat="server" AssociatedControlID="EmploymentStatusDropDownList"
                                        CssClass="form-label small fw-semibold text-dark mb-1" Text="Current status" />
                                    <asp:DropDownList ID="EmploymentStatusDropDownList" runat="server" CssClass="form-select">
                                        <asp:ListItem Text="Select your current status" Value="" />
                                        <asp:ListItem Text="Student" Value="Student" />
                                        <asp:ListItem Text="Unemployed" Value="Unemployed" />
                                        <asp:ListItem Text="Employed" Value="Employed" />
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="EmploymentStatusRequiredValidator" runat="server"
                                        ControlToValidate="EmploymentStatusDropDownList" InitialValue="" ValidationGroup="Registration"
                                        CssClass="text-danger small" Display="Dynamic" ErrorMessage="Current status is required."
                                        Text="Current status is required." />
                                </div>

                                <div class="mb-0">
                                    <asp:Label ID="GenderLabel" runat="server"
                                        CssClass="form-label small fw-semibold text-dark d-block mb-1" Text="Gender" />
                                    <asp:RadioButtonList ID="GenderRadioButtonList" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" CssClass="d-flex flex-wrap gap-3 text-dark" aria-label="Gender">
                                        <asp:ListItem Text="Female" Value="Female" />
                                        <asp:ListItem Text="Male" Value="Male" />
                                        <asp:ListItem Text="Prefer not to say" Value="NotSpecified" />
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="GenderRequiredValidator" runat="server"
                                        ControlToValidate="GenderRadioButtonList" ValidationGroup="Registration"
                                        CssClass="text-danger small" Display="Dynamic" ErrorMessage="Gender is required."
                                        Text="Gender is required." />
                                </div>
                            </div>
                        </div>

                        <hr class="text-secondary my-3" />

                        <asp:Button ID="RegisterButton" runat="server" Text="Create account"
                            CssClass="btn btn-dark w-100 py-2" ValidationGroup="Registration" OnClick="RegisterButton_Click" />

                        <p class="text-center text-secondary small mt-4 mb-0">
                            Already have an account?
                            <asp:HyperLink ID="SignInLink" runat="server" NavigateUrl="~/login"
                                CssClass="link-dark fw-semibold" Text="Sign in here" />
                        </p>
                    </div>
                    </div>
                </section>
            </div>
        </div>
    </main>
</asp:Content>