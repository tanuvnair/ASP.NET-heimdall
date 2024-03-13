<%@ Page Title="User List" Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="ASP.NET_heimdall.Admin.UserList" MasterPageFile="/Site.Master" %>

<asp:Content ContentPlaceHolderID="AdminBodyContent" runat="server">
    <div class="m-5">
        <h1>User List</h1>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PC %>" SelectCommand="SELECT [UserID], [Username], [Email], [PhoneNumber], [Role], [CreatedAt] FROM [Users]"></asp:SqlDataSource>

        <div class="table-responsive mt-4" style="height: calc(100vh - 300px)">

            <asp:GridView CssClass="table" ID="AllUserList" runat="server" DataSourceID="SqlDataSource1" AllowSorting="True" OnRowDataBound="AllUserList_RowDataBound">
            </asp:GridView>
        </div>
    </div>
</asp:Content>
