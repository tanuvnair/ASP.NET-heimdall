<%@ Page Title="User Dashboard" Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="ASP.NET_heimdall.Dashboard" MasterPageFile="/Site.Master" %>

<asp:Content ContentPlaceHolderID="UserBodyContent" runat="server">
    <div class="container-fluid p-5">
        <div class="row">
            <div class="col-sm-12">
                <h1 class="mb-5 fw-bold">Welcome to the User Dashboard!</h1>
                <div class="d-flex gap-3">
                    <div class="card bg-success text-light" style="width: 18rem;">
                        <a href="#" class="card-body d-flex flex-column justify-content-center text-decoration-none gap-2 p-4">
                            <h1><span class="badge text-bg-light">
                                <asp:Label ID="DaysPresent" runat="server" Text="0"></asp:Label>
                            </span></h1>
                            <h3>Days Present</h3>
                        </a>
                    </div>

                    <div class="card bg-warning text-light" style="width: 18rem;">
                        <a href="#" class="card-body d-flex flex-column justify-content-center text-decoration-none gap-2 p-4">
                            <h1><span class="badge text-bg-light">
                                <asp:Label ID="DaysLate" runat="server" Text="0"></asp:Label>
                            </span></h1>
                            <h3>Days Late</h3>
                        </a>
                    </div>

                    <div class="card bg-danger text-light" style="width: 18rem;">
                        <a href="#" class="card-body d-flex flex-column justify-content-center text-decoration-none gap-2 p-4">
                            <h1><span class="badge text-bg-light">
                                <asp:Label ID="DaysMissed" runat="server" Text="0"></asp:Label>
                            </span></h1>
                            <h3>Days Missed</h3>
                        </a>
                    </div>

                    <div class="card bg-primary text-light" style="width: 18rem;">
                        <a href="#" class="card-body d-flex flex-column  justify-content-center text-decoration-none gap-2 p-4">
                            <h1><span class="badge text-bg-light">
                                <asp:Label ID="AttendancePercentage" runat="server"></asp:Label>
                            </span></h1>
                            <h3>Attendance Percentage</h3>
                        </a>
                    </div>
                </div>

                <div class="mt-5">
                    <asp:Panel ID="PunchInWrapper" CssClass="" runat="server">
                        <asp:Button CssClass="btn btn-lg btn-outline-dark d-flex mt-3" ID="PunchIn" Text="Punch In" OnClick="PunchInButtonClick" runat="server" />
                    </asp:Panel>

                    <asp:Panel ID="PunchOutWrapper" CssClass="p-4 w-auto border border-1 rounded-3 border-black" runat="server">
                        <h3>
                            <asp:Label Text="You have punched in at" runat="server"></asp:Label>
                        </h3>

                        <asp:Label CssClass="fw-bold fs-1 ms-1" ID="PunchedInTime" runat="server" Text="21:00"></asp:Label>
                        <asp:Button CssClass="btn btn-lg btn-outline-dark d-flex mt-3" ID="PunchOut" Text="Punch Out" OnClick="PunchOutButtonClick" runat="server" />
                    </asp:Panel>

                    <asp:Panel ID="AlreadyPunchedOutWrapper" CssClass="p-4 w-auto border border-1 rounded-3 border-black" runat="server">
                        <h3>
                            <asp:Label Text="You have punched out at" runat="server"></asp:Label>
                            <asp:Label CssClass="fw-bold fs-1 ms-1" ID="PunchedOutTime" runat="server" Text="9:00"></asp:Label>
                        </h3>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
