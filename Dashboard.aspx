<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="ASP.NET_heimdall.Dashboard" MasterPageFile="~/Site.Master" %>

<asp:Content ContentPlaceHolderID="bodyContent" runat="server">
    <div class="container-fluid p-5">
        <div class="row">
            <div class="col-sm-12">
                <div class="d-flex justify-content-center gap-5">
                    <div class="card bg-success text-light" style="width: 18rem;">
                        <a href="#" class="card-body d-flex flex-column justify-content-center text-decoration-none gap-2 p-4">
                            <h1><span class="badge text-bg-light">12</span></h1>
                            <h3>Days Present</h3>
                        </a>
                    </div>

                    <div class="card bg-warning text-light" style="width: 18rem;">
                        <a href="#" class="card-body d-flex flex-column justify-content-center text-decoration-none gap-2 p-4">
                            <h1><span class="badge text-bg-light">1</span></h1>
                            <h3>Days Late</h3>
                        </a>
                    </div>

                    <div class="card bg-danger text-light" style="width: 18rem;">
                        <a href="#" class="card-body d-flex flex-column justify-content-center text-decoration-none gap-2 p-4">
                            <h1><span class="badge text-bg-light">3</span></h1>
                            <h3>Days Missed</h3>
                        </a>
                    </div>

                    <div class="card bg-primary text-light" style="width: 18rem;">
                        <a href="#" class="card-body d-flex flex-column  justify-content-center text-decoration-none gap-2 p-4">
                            <h1><span class="badge text-bg-light">3</span></h1>
                            <h3>Attendance Percentage</h3>
                        </a>
                    </div>
                </div>

                <div class="w-50 mx-auto p-3 mt-5">
                        <asp:Panel ID="AttendanceRecordButtonWrapper" CssClass="mx-auto d-flex justify-content-center w-75 p-5 text-center" runat="server">
                            <asp:Button CssClass="btn btn-lg btn-primary d-flex" ID="RecordAttendanceButton" Text="Record Attendance For Today" OnClick="RecordAttendanceButtonClick" runat="server" />
                        </asp:Panel>

                        <asp:Panel ID="ShowAttendanceDetails" CssClass="d-flex flex-column mx-auto w-75 p-5 border border-1 rounded-3 border-info text-center" runat="server">
                            <asp:Label runat="server" Text="You have punched in at "></asp:Label>
                            <asp:Label CssClass="fw-bold fs-3" ID="PunchedInTime" runat="server" Text="9:00 AM"></asp:Label>
                        </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
