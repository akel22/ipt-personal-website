<%@ Page Title="Audit Logs" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AuditLogs.aspx.cs" Inherits="VelascoPersonalWebsite_IPT.Pages.Admin.AuditLogs" %>
<%@ Import Namespace="VelascoPersonalWebsite_IPT.Application.DTOs" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <meta name="description" content="Registration and sign-in audit logs." />
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section aria-labelledby="auditLogsHeading">
        <div class="mb-3">
            <h1 id="auditLogsHeading" class="admin-page-title mb-2">Audit logs</h1>
            <p class="text-secondary mb-0">Review registration and sign-in activity.</p>
        </div>
        <section class="admin-panel" aria-labelledby="auditTableHeading">
            <div class="admin-panel-header border-bottom p-4">
                <h2 id="auditTableHeading" class="h5 fw-semibold mb-1">Recent events</h2>
                <p class="small text-secondary mb-0">Search by username or email and filter by event type.</p>
            </div>
            <div class="admin-toolbar p-4">
                <div class="row g-3 align-items-end">
                    <div class="col-12 col-lg-7">
                        <label class="form-label small fw-semibold" for="UserSearchTextBox">Search user</label><div class="input-group">
                            <asp:TextBox ID="UserSearchTextBox" runat="server" CssClass="form-control" MaxLength="254" placeholder="Search by username or email" /><asp:Button ID="SearchButton" runat="server" Text="Search" CssClass="btn btn-dark" OnClick="SearchButton_Click" /></div>
                    </div>
                    <div class="col-12 col-lg-5">
                        <label class="form-label small fw-semibold" for="EventTypeDropDownList">Filter by event type</label><div class="input-group">
                            <asp:DropDownList ID="EventTypeDropDownList" runat="server" CssClass="form-select">
                                <asp:ListItem Text="All event types" Value="All" />
                                <asp:ListItem Text="Registration" Value="Registration" />
                                <asp:ListItem Text="Sign in" Value="SignIn" />
                                <asp:ListItem Text="Sign-in failed" Value="SignInFailed" />
                                <asp:ListItem Text="Sign out" Value="SignOut" />
                                <asp:ListItem Text="Page visit" Value="PageVisit" />
                                <asp:ListItem Text="Admin dashboard access" Value="AdminDashboardAccess" />
                                <asp:ListItem Text="Admin user details access" Value="AdminUserDetailsAccess" />
                                <asp:ListItem Text="Admin profile save" Value="AdminProfileSave" />
                                <asp:ListItem Text="Admin user status changed" Value="AdminUserStatusChanged" />
                                <asp:ListItem Text="Admin user deleted" Value="AdminUserDeleted" />
                                <asp:ListItem Text="Audit logs access" Value="AuditLogsAccess" />
                                <asp:ListItem Text="Audit logs search" Value="AuditLogsSearch" />
                                <asp:ListItem Text="Audit logs filter" Value="AuditLogsFilter" />
                                <asp:ListItem Text="Audit logs clear" Value="AuditLogsClear" />
                                <asp:ListItem Text="Unauthorized access" Value="UnauthorizedAccess" />
                            </asp:DropDownList><asp:Button ID="FilterButton" runat="server" Text="Apply" CssClass="btn btn-outline-dark" OnClick="FilterButton_Click" /><asp:Button ID="ClearButton" runat="server" Text="Clear" CssClass="btn btn-outline-secondary" OnClick="ClearButton_Click" CausesValidation="false" /></div>
                    </div>
                </div>
            </div>
            <asp:Literal ID="FeedbackLiteral" runat="server" />
            <div class="table-responsive">
                <table class="table admin-table align-middle mb-0">
                    <thead>
                        <tr>
                            <th scope="col">User</th>
                            <th scope="col">Event type</th>
                            <th scope="col">Occurred (UTC)</th>
                            <th scope="col">Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="AuditLogsRepeater" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><strong><%#: FormatAuditUser(Eval("User")) %></strong><br />
                                        <small><%#: FormatAuditEmail(Eval("User")) %></small></td>
                                    <td><%# FormatEventType(Eval("EventType")) %></td>
                                    <td><%# ((DateTime)Eval("OccurredUtc")).ToString("yyyy-MM-dd HH:mm") %></td>
                                    <td>
                                        <button type="button" class="admin-table-button">View details</button></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
            <% if (AuditLogsTotalPages > 1) { %>
            <nav class="p-3 border-top" aria-label="Audit log pages">
                <ul class="pagination justify-content-center mb-0">
                    <li class="page-item<%: AuditLogsCurrentPage == 1 ? " disabled" : string.Empty %>">
                        <asp:LinkButton ID="AuditLogsPreviousButton" runat="server" CssClass="page-link" OnClick="AuditLogsPreviousButton_Click" Enabled='<%# AuditLogsCurrentPage > 1 %>'>Previous</asp:LinkButton>
                    </li>
                    <asp:Repeater ID="AuditLogsPaginationRepeater" runat="server" OnItemCommand="AuditLogsPaginationRepeater_ItemCommand">
                        <ItemTemplate>
                            <li class="page-item<%# GetPaginationCss((VelascoPersonalWebsite_IPT.Pages.Admin.PaginationItem)Container.DataItem) %>">
                                <asp:LinkButton ID="AuditLogsPageButton" runat="server" CssClass="page-link" CommandName="Page" CommandArgument='<%# Eval("PageNumber") %>' Enabled='<%# !((VelascoPersonalWebsite_IPT.Pages.Admin.PaginationItem)Container.DataItem).IsEllipsis && !((VelascoPersonalWebsite_IPT.Pages.Admin.PaginationItem)Container.DataItem).IsCurrent %>'><%# Eval("Text") %></asp:LinkButton>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                    <li class="page-item<%: AuditLogsCurrentPage == AuditLogsTotalPages ? " disabled" : string.Empty %>">
                        <asp:LinkButton ID="AuditLogsNextButton" runat="server" CssClass="page-link" OnClick="AuditLogsNextButton_Click" Enabled='<%# AuditLogsCurrentPage < AuditLogsTotalPages %>'>Next</asp:LinkButton>
                    </li>
                </ul>
            </nav>
            <% } %>
        </section>
    </section>
</asp:Content>
