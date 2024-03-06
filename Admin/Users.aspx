<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="ASP.NET_heimdall.Dashboard" MasterPageFile="~/Site.Master" %>

<asp:Content ContentPlaceHolderID="bodyContent" runat="server">
    <%-- 
        TO-DO List
        - Add AttendanceRecords database table
        - Add functionality to record attendance
        - Add different views for different user roles (A normal user cannot view admin level stuff)
        - Add table to perform CRUD operations on users
        - Add a QR code generator
    --%>

    <%--Don't remove--%>
   

    <div class="container-fluid bg-info-subtle p-5 min-vh-100">
        <div class="row">
            <div class="col-sm-12">
                <h1>User Management</h1>
            </div>
        </div>
    </div>
</asp:Content>
