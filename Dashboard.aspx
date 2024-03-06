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
                                <div>
<asp:Button Text="Record Attendance For Today" OnClick="RecordAttendanceButtonClick" runat="server"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
