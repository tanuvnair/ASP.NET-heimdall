<%@ Page Title="Sign In" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ASP.NET_heimdall.Default" MasterPageFile="~/Auth.Master" %>

<asp:Content ContentPlaceHolderID="signInPlaceHolder" runat="server">
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

        <asp:Button CssClass="btn btn-outline-light mt-3 px-4 py-2" runat="server" Text="Sign In" OnClick="SignIn"></asp:Button>

        <h6 class="d-flex mt-5">Don't have an account?<a href="#" class="ms-2 text-decoration-none" id="signUpLink">Sign Up</a></h6>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="signUpPlaceHolder" runat="server">
    <%--Sign Up--%>
    <div id="signUp" class="d-none">
        <h3 class="mt-5 mb-4">Sign Up</h3>

        <div class="d-flex justify-content-center align-items-center gap-3">
            <i class="fa-solid fa-user"></i>

            <asp:TextBox ID="signUpUsername" runat="server" CssClass="form-control mt-3 mb-2 px-3 py-3" placeholder="Username" required="true"></asp:TextBox>
        </div>
        <div class="ms-5">
            <asp:RequiredFieldValidator class="card-text text-danger" ID="signUpRequiredUsername" runat="server" ErrorMessage="*The username field is required." ControlToValidate="signUpUsername" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>



        <div class="d-flex justify-content-center align-items-center gap-3">
            <i class="fa-fw fa-solid fa-envelope"></i>

            <asp:TextBox ID="signUpEmail" runat="server" TextMode="Email" CssClass="form-control mt-3 mb-2 px-3 py-3" placeholder="Email"></asp:TextBox>

        </div>
        <div class="ms-5">
            <asp:RequiredFieldValidator class="text-danger" ID="signUpRequiredEmail" runat="server" ErrorMessage="*The email field is required." ControlToValidate="signUpEmail" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator class="text-danger" ID="RegularExpressionEmailValidation" runat="server" ErrorMessage="*The email is invalid." ControlToValidate="signUpEmail" ValidationExpression="^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$" Display="Dynamic"></asp:RegularExpressionValidator>
        </div>


        <div class="d-flex justify-content-center align-items-center gap-3">
            <i class="fa-fw fa-solid fa-phone"></i>

            <asp:TextBox ID="signUpPhoneNumber" runat="server" TextMode="Phone" CssClass="form-control mt-3 mb-2 px-3 py-3" placeholder="Phone Number"></asp:TextBox>
        </div>
        <div class="ms-5">
            <asp:RequiredFieldValidator class="text-danger" ID="signUpRequiredPhoneNumber" runat="server" ErrorMessage="*The phone number field is required." ControlToValidate="signUpPhoneNumber" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator class="text-danger" ID="RegularExpressionPhoneValidation" runat="server" ErrorMessage="*The phone number is invalid." ControlToValidate="signUpPhoneNumber" ValidationExpression="^\+(?:[0-9] ?){6,14}[0-9]$" Display="Dynamic"></asp:RegularExpressionValidator>

        </div>


        <div class="d-flex justify-content-center align-items-center gap-3">
            <i class="fa-fw fa-solid fa-unlock"></i>

            <asp:TextBox ID="signUpPassword" runat="server" TextMode="Password" CssClass="form-control mt-3 mb-2 px-3 py-3" placeholder="Password"></asp:TextBox>
        </div>
        <div class="ms-5">
            <asp:RequiredFieldValidator class="text-danger" ID="signUpRequiredPassword" runat="server" ErrorMessage="*The password field is required." ControlToValidate="signUpPassword" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>

        <div class="d-flex justify-content-center align-items-center gap-3">
            <i class="fa-fw fa-solid fa-lock"></i>

            <asp:TextBox ID="signUpConfirmPassword" runat="server" TextMode="Password" CssClass="form-control mt-3 mb-2 px-3 py-3" placeholder="Confirm Password"></asp:TextBox>
        </div>
        <div class="ms-5">
            <asp:CompareValidator class="text-danger" ID="ComparePassword" runat="server" ErrorMessage="*Passwords do not match." ControlToValidate="signUpPassword" ControlToCompare="signUpConfirmPassword" Display="Dynamic"></asp:CompareValidator>
        </div>

        <asp:Button CssClass="btn btn-outline-light mt-3 px-4 py-2" runat="server" Text="Sign Up" OnClick="SignUp"></asp:Button>

        <h6 class="d-flex mt-4">Already have an account?<a href="#" class="ms-2 text-decoration-none" id="signInLink">Sign In</a></h6>
    </div>
</asp:Content>
