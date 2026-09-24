<%@ Page Title="User details" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" Inherits="VelascoPersonalWebsite_IPT.Pages.Admin.UserDetails" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section aria-labelledby="userDetailsHeading"><div class="mb-4"><h1 id="userDetailsHeading" class="admin-page-title mb-2">User details</h1><p class="text-secondary mb-0">View account information.</p></div>
        <asp:Literal ID="FeedbackLiteral" runat="server" />
        <asp:Panel ID="DetailsPanel" runat="server" CssClass="admin-panel p-4" Visible="false">
            <dl class="row mb-0"><dt class="col-sm-4">Username</dt><dd class="col-sm-8"><%: UsernameValue %></dd><dt class="col-sm-4">Email</dt><dd class="col-sm-8"><%: EmailValue %></dd><dt class="col-sm-4">Employment status</dt><dd class="col-sm-8"><%: EmploymentStatusValue %></dd><dt class="col-sm-4">Gender</dt><dd class="col-sm-8"><%: GenderValue %></dd><dt class="col-sm-4">Date of birth</dt><dd class="col-sm-8"><%: DateOfBirthValue %></dd><dt class="col-sm-4">Account state</dt><dd class="col-sm-8"><%: AccountStateValue %></dd><dt class="col-sm-4">Registered (UTC)</dt><dd class="col-sm-8"><%: RegisteredValue %></dd></dl>
        </asp:Panel>
        <a class="btn btn-outline-dark mt-4" href="<%= ResolveUrl("~/admin") %>">Back to dashboard</a>
    </section>
</asp:Content>
