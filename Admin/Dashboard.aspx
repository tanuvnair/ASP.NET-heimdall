<%@ Page Title="Admin Dashboard" Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="ASP.NET_heimdall.Admin.Dashboard" MasterPageFile="/Site.Master" %>

<asp:Content ContentPlaceHolderID="AdminBodyContent" runat="server">
    <div class="container-fluid p-5">
        <div class="row">
            <div class="col-sm-12">
                <h1 class="mb-5 fw-bold">Welcome to Admin Dashboard!</h1>
                <div class="d-flex gap-3">
                    <div class="card bg-success text-light" style="width: 18rem;">
                        <a href="#" class="card-body d-flex flex-column justify-content-center text-decoration-none gap-2 p-4">
                            <h1><span class="badge text-bg-light">
                                <asp:Label ID="totalUsers" runat="server" Text="0"></asp:Label>
                            </span></h1>
                            <h3>Total Users</h3>
                        </a>
                    </div>

                    <div class="card bg-warning text-light" style="width: 18rem;">
                        <a href="#" class="card-body d-flex flex-column justify-content-center text-decoration-none gap-2 p-4">
                            <h1><span class="badge text-bg-light">
                                <asp:Label ID="totalAdmin" runat="server" Text="0"></asp:Label>
                            </span></h1>
                            <h3>Total Admins</h3>
                        </a>
                    </div>
                </div>
            </div>
        </div>

        <div class="mt-5">
                <h2 class="fw-bold">Generate a new account</h2>

                <div class="d-flex justify-content-center align-items-center gap-3">
                    <i class="fa-fw fa-solid fa-envelope"></i>

                    <asp:TextBox ID="generateAccountEmailTextBox" runat="server" TextMode="Email" CssClass="form-control mt-3 mb-2 px-3 py-3" placeholder="Email"></asp:TextBox>

                </div>

                <div>
                    <asp:Label ID="GenerateAccountEmailSuccessLabel" CssClass="fw-bold text-success" runat="server" Text="Success!"></asp:Label>
                </div>

                <asp:Button CssClass="btn btn-outline-dark mt-3 px-4 py-2" ID="GenerateAccountButton" runat="server" OnClick="GenerateAccountButtonClick" Text="Generate Account" />
        </div>
    </div>
</asp:Content>
