<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="VelascoPersonalWebsite_IPT.Pages.Admin.Dashboard" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <meta name="description" content="Velasco Personal Website administration dashboard." />
    <script src="https://cdn.jsdelivr.net/npm/chart.js@4.4.4/dist/chart.umd.min.js"></script>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section aria-labelledby="dashboardHeading">
        <div class="admin-welcome-row mb-3">
            <div>
                <h1 id="dashboardHeading" class="admin-page-title mb-2">Hello, Administrator</h1>
                <p class="text-secondary mb-0">Here is what is happening with your personal website today.</p>
            </div>
        </div>

        <div class="row g-3 mb-4">
            <div class="col-12 col-md-4">
                <article class="admin-stat-card admin-stat-card-purple h-100">
                    <div class="admin-stat-card-top"><span class="admin-stat-label">Total registered</span></div>
                    <p class="admin-stat-value mb-1"><%: Statistics.TotalRegistered %></p>
                </article>
            </div>
            <div class="col-12 col-md-4">
                <article class="admin-stat-card admin-stat-card-mint h-100">
                    <div class="admin-stat-card-top"><span class="admin-stat-label">Active members</span></div>
                    <p class="admin-stat-value mb-1"><%: Statistics.ActiveMembers %></p>
                </article>
            </div>
            <div class="col-12 col-md-4">
                <article class="admin-stat-card admin-stat-card-blue h-100">
                    <div class="admin-stat-card-top"><span class="admin-stat-label">Active now</span></div>
                    <p class="admin-stat-value mb-1"><%: Statistics.ActiveNow %></p>
                </article>
            </div>
        </div>

        <div class="row g-4 mb-4">
            <div class="col-12 col-xl-8">
                <section class="admin-panel h-100" aria-labelledby="trendHeading">
                    <div class="admin-panel-header p-4">
                        <div class="d-flex flex-column flex-sm-row justify-content-between gap-2">
                            <div><h2 id="trendHeading" class="h5 fw-semibold mb-1">Registration overview</h2><p class="small text-secondary mb-0">New accounts registered over the last six months.</p></div>
                       
                        </div>
                    </div>
                    <div class="admin-chart-wrap" id="registrationChartData" runat="server"><canvas id="registrationChart" aria-label="Registration overview chart" role="img"></canvas></div>
                </section>
            </div>
            <div class="col-12 col-xl-4">
                <section class="admin-panel h-100" aria-labelledby="breakdownHeading">
                    <div class="admin-panel-header p-4"><h2 id="breakdownHeading" class="h5 fw-semibold mb-1">Account breakdown</h2><p class="small text-secondary mb-0">Current database distribution.</p></div>
                    <div class="p-4">
                        <div class="admin-breakdown-row"><span>Student</span><strong><%: Statistics.StudentPercentage %>%</strong></div>
                        <div class="admin-breakdown-bar"><span class="admin-bar-purple" style="width: <%: Statistics.StudentPercentage %>%;"></span></div>
                        <div class="admin-breakdown-row"><span>Employed</span><strong><%: Statistics.EmployedPercentage %>%</strong></div>
                        <div class="admin-breakdown-bar"><span class="admin-bar-blue" style="width: <%: Statistics.EmployedPercentage %>%;"></span></div>
                        <div class="admin-breakdown-row"><span>Unemployed</span><strong><%: Statistics.UnemployedPercentage %>%</strong></div>
                        <div class="admin-breakdown-bar"><span class="admin-bar-green" style="width: <%: Statistics.UnemployedPercentage %>%;"></span></div>
                    </div>
                </section>
            </div>
        </div>

        <section class="admin-panel" aria-labelledby="usersHeading">
            <div class="admin-panel-header p-4">
                <div class="d-flex flex-column flex-lg-row align-items-lg-center justify-content-between gap-3">
                    <div><h2 id="usersHeading" class="h5 fw-semibold mb-1">All customers</h2><p class="small text-secondary mb-0">Manage registered users in your community.</p></div>
                </div>
            </div>
            <div class="admin-toolbar p-4">
                <div class="row g-3 align-items-end">
                    <div class="col-12 col-lg-7">
                        <label class="form-label small fw-semibold" for="SearchTextBox">Search user</label>
                        <div class="input-group admin-search-group">
                            <asp:TextBox ID="SearchTextBox" runat="server" CssClass="form-control"
                                ClientIDMode="Static" MaxLength="254" placeholder="Search by username or email" />
                            <asp:Button ID="SearchButton" runat="server" Text="Search"
                                CssClass="btn btn-dark" OnClick="SearchButton_Click" />
                        </div>
                        <asp:Literal ID="SearchFeedbackLiteral" runat="server"></asp:Literal>
                    </div>
                    <div class="col-12 col-lg-5">
                        <label class="form-label small fw-semibold" for="StatusFilterDropDownList">Filter by status</label>
                        <div class="input-group">
                            <asp:DropDownList ID="StatusFilterDropDownList" runat="server" ClientIDMode="Static" CssClass="form-select">
                                <asp:ListItem Text="All statuses" Value="All" />
                                <asp:ListItem Text="Student" Value="Student" />
                                <asp:ListItem Text="Employed" Value="Employed" />
                                <asp:ListItem Text="Unemployed" Value="Unemployed" />
                            </asp:DropDownList>
                            <asp:Button ID="FilterButton" runat="server" Text="Apply"
                                CssClass="btn btn-outline-dark" OnClick="FilterButton_Click" />
                        </div>
                        <asp:Literal ID="FilterFeedbackLiteral" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
            <div class="table-responsive">
                <table class="table admin-table align-middle mb-0"><thead><tr><th scope="col">Username</th><th scope="col">Email</th><th scope="col">Status</th><th scope="col">Account state</th><th scope="col">Registered (UTC)</th><th scope="col">Actions</th></tr></thead>
                    <tbody>
                        <asp:Repeater ID="UsersRepeater" runat="server" OnItemCommand="UsersRepeater_ItemCommand">
                            <ItemTemplate>
                                <tr><td><strong><%#: Eval("Username") %></strong></td><td><%#: Eval("Email") %></td><td><%#: Eval("EmploymentStatus") %></td><td><span class="<%# GetStateCss((bool)Eval("IsActive")) %>"><%# GetStateText((bool)Eval("IsActive")) %></span></td><td><%# ((DateTime)Eval("RegisteredUtc")).ToString("yyyy-MM-dd HH:mm") %></td><td><a class="admin-table-button" href="<%# ResolveUrl("~/admin/user-details?id=" + Eval("UserId")) %>">View</a> <asp:LinkButton ID="DeleteButton" runat="server" CommandName="DeleteUser" CommandArgument='<%# Eval("UserId") %>' CssClass="admin-table-button" OnClientClick="return confirm('Are you sure you want to permanently delete this user?');">Delete</asp:LinkButton></td></tr>
                            </ItemTemplate>
                            <FooterTemplate><asp:Literal ID="EmptyUsersLiteral" runat="server" Visible="false" Text="<tr><td colspan='6' class='text-center py-4'>No users matched your search.</td></tr>" /></FooterTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </section>
    </section>

    <script>
        (function () {
            var chartElement = document.getElementById('registrationChart');
            if (!chartElement || typeof Chart === 'undefined') return;
            var chartData = document.getElementById('registrationChartData');
            new Chart(chartElement, {
                type: 'line',
                data: {
                    labels: JSON.parse(chartData.getAttribute('data-labels')),
                    datasets: [{ label: 'New registrations', data: JSON.parse(chartData.getAttribute('data-values')), borderColor: '#6c4ce6', backgroundColor: 'rgba(108, 76, 230, 0.12)', fill: true, tension: 0.4, pointRadius: 4, pointBackgroundColor: '#fff', pointBorderWidth: 2 }]
                },
                options: { responsive: true, maintainAspectRatio: false, plugins: { legend: { display: false } }, scales: { y: { beginAtZero: true, grid: { color: '#eef0f5' }, ticks: { precision: 0 } }, x: { grid: { display: false } } } }
            });
        }());
    </script>
</asp:Content>
