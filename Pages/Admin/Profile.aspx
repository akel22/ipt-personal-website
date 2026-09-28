<%@ Page Title="Profile" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="VelascoPersonalWebsite_IPT.Pages.Admin.Profile" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <meta name="description" content="Edit the public portfolio content." />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section aria-labelledby="profileHeading">
        <div class="mb-4"><h1 id="profileHeading" class="admin-page-title mb-2">Content</h1><p class="text-secondary mb-0">Manage the content shown on your public portfolio.</p></div>
        <asp:ValidationSummary ID="ProfileValidationSummary" runat="server" CssClass="alert alert-danger" />
        <asp:Literal ID="FeedbackLiteral" runat="server" />
        <section class="admin-panel p-4">
            <h2 class="h5 fw-semibold mb-3">Introduction and contact links</h2>
            <div class="row g-3">
                <div class="col-md-6"><label class="form-label" for="GreetingNameTextBox">Greeting name</label><asp:TextBox ID="GreetingNameTextBox" runat="server" CssClass="form-control" MaxLength="200" /></div>
                <div class="col-md-6"><label class="form-label" for="EmailTextBox">Email</label><asp:TextBox ID="EmailTextBox" runat="server" CssClass="form-control" MaxLength="254" /></div>
                <div class="col-12"><label class="form-label" for="IntroductionTextBox">Introduction</label><asp:TextBox ID="IntroductionTextBox" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="2000" /></div>
                <div class="col-md-6"><label class="form-label" for="LinkedInUrlTextBox">LinkedIn URL</label><asp:TextBox ID="LinkedInUrlTextBox" runat="server" CssClass="form-control" MaxLength="500" /></div>
                <div class="col-md-6"><label class="form-label" for="GitHubUrlTextBox">GitHub URL</label><asp:TextBox ID="GitHubUrlTextBox" runat="server" CssClass="form-control" MaxLength="500" /></div>
            </div>
            <hr class="my-4" /><h2 class="h5 fw-semibold mb-3">Education</h2>
            <div class="row g-3">
                <div class="col-md-6"><label class="form-label">Heading</label><asp:TextBox ID="EducationHeadingTextBox" runat="server" CssClass="form-control" /></div><div class="col-md-6"><label class="form-label">Summary</label><asp:TextBox ID="EducationSummaryTextBox" runat="server" CssClass="form-control" /></div>
                <div class="col-md-4"><label class="form-label">Current period</label><asp:TextBox ID="CurrentEducationPeriodTextBox" runat="server" CssClass="form-control" /></div><div class="col-md-4"><label class="form-label">Current title</label><asp:TextBox ID="CurrentEducationTitleTextBox" runat="server" CssClass="form-control" /></div><div class="col-md-4"><label class="form-label">Current description</label><asp:TextBox ID="CurrentEducationDescriptionTextBox" runat="server" CssClass="form-control" /></div>
                <div class="col-md-4"><label class="form-label">Previous period</label><asp:TextBox ID="PreviousEducationPeriodTextBox" runat="server" CssClass="form-control" /></div><div class="col-md-4"><label class="form-label">Previous title</label><asp:TextBox ID="PreviousEducationTitleTextBox" runat="server" CssClass="form-control" /></div><div class="col-md-4"><label class="form-label">Previous description</label><asp:TextBox ID="PreviousEducationDescriptionTextBox" runat="server" CssClass="form-control" /></div>
            </div>
            <hr class="my-4" /><h2 class="h5 fw-semibold mb-3">Interests and skills</h2>
            <div class="row g-3">
                <div class="col-md-6"><label class="form-label">Interests heading</label><asp:TextBox ID="InterestsHeadingTextBox" runat="server" CssClass="form-control" /></div><div class="col-md-6"><label class="form-label">Interests summary</label><asp:TextBox ID="InterestsSummaryTextBox" runat="server" CssClass="form-control" /></div>
                <div class="col-md-6"><label class="form-label">Interest 1 title</label><asp:TextBox ID="InterestOneTitleTextBox" runat="server" CssClass="form-control" /></div><div class="col-md-6"><label class="form-label">Interest 1 description</label><asp:TextBox ID="InterestOneDescriptionTextBox" runat="server" CssClass="form-control" /></div>
                <div class="col-md-6"><label class="form-label">Interest 2 title</label><asp:TextBox ID="InterestTwoTitleTextBox" runat="server" CssClass="form-control" /></div><div class="col-md-6"><label class="form-label">Interest 2 description</label><asp:TextBox ID="InterestTwoDescriptionTextBox" runat="server" CssClass="form-control" /></div>
                <div class="col-md-6"><label class="form-label">Interest 3 title</label><asp:TextBox ID="InterestThreeTitleTextBox" runat="server" CssClass="form-control" /></div><div class="col-md-6"><label class="form-label">Interest 3 description</label><asp:TextBox ID="InterestThreeDescriptionTextBox" runat="server" CssClass="form-control" /></div>
                <div class="col-md-4"><label class="form-label">Skills eyebrow</label><asp:TextBox ID="SkillsEyebrowTextBox" runat="server" CssClass="form-control" /></div><div class="col-md-4"><label class="form-label">Skills heading</label><asp:TextBox ID="SkillsHeadingTextBox" runat="server" CssClass="form-control" /></div><div class="col-md-4"><label class="form-label">Skills summary</label><asp:TextBox ID="SkillsSummaryTextBox" runat="server" CssClass="form-control" /></div>
                <div class="col-12"><label class="form-label">Skills (separate with |)</label><asp:TextBox ID="SkillsTextBox" runat="server" CssClass="form-control" /></div>
            </div>
            <hr class="my-4" /><h2 class="h5 fw-semibold mb-3">Contact and footer</h2>
            <div class="row g-3">
                <div class="col-md-6"><label class="form-label">Contact heading</label><asp:TextBox ID="ContactHeadingTextBox" runat="server" CssClass="form-control" /></div><div class="col-md-6"><label class="form-label">Contact description</label><asp:TextBox ID="ContactDescriptionTextBox" runat="server" CssClass="form-control" /></div>
                <div class="col-md-6"><label class="form-label">Footer statement (use | for line break)</label><asp:TextBox ID="FooterStatementTextBox" runat="server" CssClass="form-control" /></div><div class="col-md-6"><label class="form-label">Footer email</label><asp:TextBox ID="FooterEmailTextBox" runat="server" CssClass="form-control" /></div>
                <div class="col-md-4"><label class="form-label">Footer phone</label><asp:TextBox ID="FooterPhoneTextBox" runat="server" CssClass="form-control" /></div><div class="col-md-4"><label class="form-label">Footer GitHub</label><asp:TextBox ID="FooterGitHubTextBox" runat="server" CssClass="form-control" /></div><div class="col-md-4"><label class="form-label">Footer LinkedIn</label><asp:TextBox ID="FooterLinkedInTextBox" runat="server" CssClass="form-control" /></div>
            </div>
            <asp:Button ID="SaveButton" runat="server" Text="Save profile" CssClass="btn btn-dark mt-4" OnClick="SaveButton_Click" />
        </section>
    </section>
</asp:Content>
