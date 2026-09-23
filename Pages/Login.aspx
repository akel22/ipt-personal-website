<%@ Page Title="Sign in" Language="C#" MasterPageFile="~/Unauthenticated.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="VelascoPersonalWebsite_IPT.Pages.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main class="container py-4 py-lg-5">
        <div class="row justify-content-center">
            <div class="col-12 col-xl-10">
                <section class="row g-1 overflow-hidden bg-white border rounded-4 shadow-sm">

                    <div class="col-lg-6 d-flex flex-column justify-content-center align-items-center text-center p-4 p-md-5">
                        <img src="<%= ResolveUrl("~/assets/img/waving_owl.jpeg") %>" alt="Owl wearing a red cap" class="img-fluid login-owl-image mb-4" />    
                    </div>

                    <div class="col-lg-6 border-top p-4 p-md-5 login-form-column">
                        <h1 class="h4 fw-semibold text-dark mb-1">Hoot! Sign in to your account</h1>
                        <p class="text-secondary small mb-4">Enter your details to continue.</p>
                        <asp:ValidationSummary ID="LoginValidationSummary" runat="server" ValidationGroup="Login" CssClass="alert alert-danger small py-2 mb-4" HeaderText="Please correct the following:" />
                        <div class="mb-3">
                            <asp:Label ID="UsernameLabel" runat="server" AssociatedControlID="UsernameTextBox" CssClass="form-label small fw-semibold text-dark" Text="Username" />
                            <asp:TextBox ID="UsernameTextBox" runat="server" CssClass="form-control mb-1" MaxLength="20" autocomplete="username" placeholder="axelot123" />
                            <asp:RequiredFieldValidator ID="UsernameRequiredValidator" runat="server" ControlToValidate="UsernameTextBox" ValidationGroup="Login" CssClass="text-danger small d-block" Display="Dynamic" EnableClientScript="false" ErrorMessage="Username is required." />
                        </div>

                        <div class="mb-3">
                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="PasswordTextBox" CssClass="form-label small fw-semibold text-dark" Text="Password" />
                            <asp:TextBox ID="PasswordTextBox" runat="server" CssClass="form-control mb-1" TextMode="Password" autocomplete="current-password" placeholder="Enter your password" />
                            <asp:RequiredFieldValidator ID="PasswordRequiredValidator" runat="server" ControlToValidate="PasswordTextBox" ValidationGroup="Login" CssClass="text-danger small d-block" Display="Dynamic" EnableClientScript="false" ErrorMessage="Password is required." />
                        </div>

                        <div class="form-check my-4">
                            <asp:CheckBox ID="RememberMeCheckBox" runat="server" />
                            <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMeCheckBox" CssClass="form-check-label small text-secondary" Text="Remember me" />
                        </div>
                        <asp:Button ID="LoginButton" runat="server" Text="Sign in" CssClass="btn btn-dark w-100 py-2" ValidationGroup="Login" />
                        <p class="text-center text-secondary small mt-4 mb-0">Don't have an account?
                            <asp:HyperLink ID="RegisterLink" runat="server" NavigateUrl="~/register" CssClass="link-dark fw-semibold" Text="Register here" /></p>
                    </div>
                </section>
            </div>
        </div>
    </main>
</asp:Content>
