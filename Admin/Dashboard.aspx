<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="ASP.NET_heimdall.Admin.Dashboard" MasterPageFile="/Site.Master" %>

<asp:Content ContentPlaceHolderID="AdminBodyContent" runat="server">
    <div class="container-fluid p-5">
        <div class="row">
            <div class="col-sm-12">
                <h1 class="mb-5">Welcome to Admin dashboard</h1>
                <div class="d-flex gap-5">
                    <div class="card bg-success text-light" style="width: 18rem;">
                        <a href="/Admin/UserManagement.aspx" class="card-body d-flex flex-column justify-content-center text-decoration-none gap-2 p-4">
                            <h1><span class="badge text-bg-light">
                                <asp:Label ID="totalUsers" runat="server" Text="0"></asp:Label>
                            </span></h1>
                            <h3>Total Users</h3>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
