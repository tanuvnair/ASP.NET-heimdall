<%@ Page Title="Sign In" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ASP.NET_heimdall.Default" MasterPageFile="~/Auth.Master" %>

<asp:Content ContentPlaceHolderID="signInPlaceHolder" runat="server">
    <%--Signin form--%>
    <div id="signIn">
        <h3 class="mt-5 mb-4">Sign In</h3>

        <asp:TextBox runat="server" CssClass="form-control my-3 px-3 py-3" placeholder="Username"></asp:TextBox>
        <asp:TextBox runat="server" TextMode="Password" CssClass="form-control my-3 px-3 py-3" placeholder="Password"></asp:TextBox>
        <h6><a href="#" class="text-decoration-none">Forgot Password?</a></h6>
        <asp:Button CssClass="btn btn-outline-light mt-3 px-4 py-2" runat="server" Text="Sign In" PostBackUrl="~/Dashboard.aspx"></asp:Button>

        <h6 class="d-flex mt-5">Don't have an account?<a href="#" class="ms-2 text-decoration-none" id="signUpLink">Sign Up</a></h6>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="signUpPlaceHolder" runat="server">
    <%--Sign Up--%>
    <div id="signUp" class="d-none">
        <h3 class="mt-5 mb-4">Sign Up</h3>

        <asp:TextBox runat="server" TextMode="Email" CssClass="form-control my-3 px-3 py-3" placeholder="Email"></asp:TextBox>
        <asp:TextBox runat="server" CssClass="form-control my-3 px-3 py-3" placeholder="Username"></asp:TextBox>
        <asp:TextBox runat="server" TextMode="Phone" CssClass="form-control my-3 px-3 py-3" placeholder="Phone Number"></asp:TextBox>
        <asp:TextBox runat="server" TextMode="Password" CssClass="form-control my-3 px-3 py-3" placeholder="Password"></asp:TextBox>
        <asp:TextBox runat="server" TextMode="Password" CssClass="form-control my-3 px-3 py-3" placeholder="Confirm Password"></asp:TextBox>
        <asp:Button CssClass="btn btn-outline-light mt-3 px-4 py-2" runat="server" Text="Sign Up" PostBackUrl="#"></asp:Button>

        <h6 class="d-flex mt-5">Already have an account?<a href="#" class="ms-2 text-decoration-none" id="signInLink">Sign In</a></h6>
    </div>
</asp:Content>
