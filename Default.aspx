<%@ Page Title="Sign In" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ASP.NET_heimdall.Default" MasterPageFile="~/Auth.Master" %>

<asp:Content ContentPlaceHolderID="signInPlaceHolder" runat="server">
    <form action="/" method="post" runat="server">
        <%--Signin form--%>
        <div id="signIn" class="">
            <h3 class="mt-5 mb-4 d-none">Sign In</h3>

            <div class="d-flex justify-content-center align-items-center gap-3">
                <i class="fa-fw fa-solid fa-user"></i>
                <asp:TextBox ID="signInUsername" runat="server" CssClass="form-control mt-3 mb-2 px-3 py-3" placeholder="Username"></asp:TextBox>
            </div>
            <div class="ms-5">
                <asp:RequiredFieldValidator class="card-text text-danger" ID="signInRequiredUsername" runat="server" ErrorMessage="*The username field is required." ControlToValidate="signInUsername" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>

            <div class="d-flex justify-content-center align-items-center gap-3">
                <i class="fa-fw fa-solid fa-lock"></i>
                <asp:TextBox ID="signInPassword" runat="server" TextMode="Password" CssClass="form-control mt-3 mb-2 px-3 py-3" placeholder="Password"></asp:TextBox>
            </div>
            <div class="ms-5">
                <asp:RequiredFieldValidator class="text-danger" ID="signInRequiredPassword" runat="server" ErrorMessage="*The password field is required." ControlToValidate="signInPassword" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>

            <h6 class="mt-3"><a href="#" class="text-decoration-none">Forgot Password?</a></h6>

            <asp:Button ID="SignInButton" CssClass="btn btn-outline-light mt-3 px-4 py-2" runat="server" Text="Sign In" CausesValidation="True" OnClick="SignInButtonClick"></asp:Button>

            <h6 class="d-flex mt-5">Don't have an account?<a href="SignUp.aspx" class="ms-2 text-decoration-none" id="signUpLink">Sign Up</a></h6>
        </div>
    </form>
</asp:Content>
