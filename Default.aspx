<%@ Page Title="Sign In" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ASP.NET_heimdall.Default" MasterPageFile="~/Auth.Master" %>

<asp:Content ContentPlaceHolderID="signInPlaceHolder" runat="server">
    <%--Signin form--%>
    <div id="signIn" class="d-none">
        <h3 class="mt-5 mb-4 d-none">Sign In</h3>

        <asp:TextBox ID="signInUsername" runat="server" CssClass="form-control my-3 px-3 py-3" placeholder="Username"></asp:TextBox>
        <asp:TextBox ID="signInPassword" runat="server" TextMode="Password" CssClass="form-control my-3 px-3 py-3" placeholder="Password"></asp:TextBox>
        <h6><a href="#" class="text-decoration-none">Forgot Password?</a></h6>
        <asp:Button CssClass="btn btn-outline-light mt-3 px-4 py-2" runat="server" Text="Sign In" OnClick="SignIn"></asp:Button>

        <h6 class="d-flex mt-5">Don't have an account?<a href="#" class="ms-2 text-decoration-none" id="signUpLink">Sign Up</a></h6>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="signUpPlaceHolder" runat="server">
    <%--Sign Up--%>
    <div id="signUp" class="">
        <h3 class="mt-5 mb-4">Sign Up</h3>

        <asp:TextBox ID="signUpUsername" runat="server" CssClass="form-control my-3 px-3 py-3" placeholder="Username" required="true"></asp:TextBox>
        <asp:RequiredFieldValidator class="form-text" style="" ID="RequiredUsername" runat="server" ErrorMessage="Please enter a username" ControlToValidate="signUpUsername"></asp:RequiredFieldValidator>

        <asp:TextBox ID="signUpEmail" runat="server" TextMode="Email" CssClass="form-control my-3 px-3 py-3" placeholder="Email"></asp:TextBox>


        <asp:TextBox ID="signUpPhoneNumber" runat="server" TextMode="Phone" CssClass="form-control my-3 px-3 py-3" placeholder="Phone Number"></asp:TextBox>


        <asp:TextBox ID="signUpPassword" runat="server" TextMode="Password" CssClass="form-control my-3 px-3 py-3" placeholder="Password"></asp:TextBox>


        <asp:TextBox ID="signUpConfirmPassword" runat="server" TextMode="Password" CssClass="form-control my-3 px-3 py-3" placeholder="Confirm Password"></asp:TextBox>


        <asp:Button CssClass="btn btn-outline-light mt-3 px-4 py-2" runat="server" Text="Sign Up" OnClick="SignUp"></asp:Button>

        <h6 class="d-flex mt-5">Already have an account?<a href="#" class="ms-2 text-decoration-none" id="signInLink">Sign In</a></h6>
    </div>
</asp:Content>
