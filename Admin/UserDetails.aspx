<%@ Page Title="User Details" Language="C#" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" Inherits="ASP.NET_heimdall.Admin.UserDetails" MasterPageFile="/Site.Master" %>

<asp:Content ContentPlaceHolderID="UserBodyContent" runat="server">
    <div class="container-fluid p-5">
        <div class="row">
            <div class="col-sm-12">
                <h1 class="mb-5">User Details</h1>

                <div class="mb-2">
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PC %>" SelectCommand="SELECT [Username], [Email], [PhoneNumber], [Role] FROM [Users] WHERE ([UserID] = @UserID)">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="UserID" QueryStringField="UserID" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                    <asp:DetailsView ID="DetailsView1" runat="server" DataSourceID="SqlDataSource1" CssClass="table">
                    </asp:DetailsView>
                    <br />
                </div>

                <div class="d-flex gap-5">
                    <div class="card bg-success text-light" style="width: 18rem;">
                        <a href="#" class="card-body d-flex flex-column justify-content-center text-decoration-none gap-2 p-4">
                            <h1><span class="badge text-bg-light">
                                <asp:Label ID="DaysPresent" runat="server" Text="0"></asp:Label></span></h1>
                            <h3>Days Present</h3>
                        </a>
                    </div>

                    <div class="card bg-warning text-light" style="width: 18rem;">
                        <a href="#" class="card-body d-flex flex-column justify-content-center text-decoration-none gap-2 p-4">
                            <h1><span class="badge text-bg-light">
                                <asp:Label ID="DaysLate" runat="server" Text="0"></asp:Label></span></h1>
                            <h3>Days Late</h3>
                        </a>
                    </div>

                    <div class="card bg-danger text-light" style="width: 18rem;">
                        <a href="#" class="card-body d-flex flex-column justify-content-center text-decoration-none gap-2 p-4">
                            <h1><span class="badge text-bg-light">
                                <asp:Label ID="DaysMissed" runat="server" Text="0"></asp:Label></span></h1>
                            <h3>Days Missed</h3>
                        </a>
                    </div>

                    <div class="card bg-primary text-light" style="width: 18rem;">
                        <a href="#" class="card-body d-flex flex-column  justify-content-center text-decoration-none gap-2 p-4">
                            <h1><span class="badge text-bg-light">
                                <asp:Label ID="AttendancePercentage" runat="server"></asp:Label></span></h1>
                            <h3>Attendance Percentage</h3>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>