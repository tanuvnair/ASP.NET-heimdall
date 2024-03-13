<%@ Page Title="Forgot Password" Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="ASP.NET_heimdall.ForgotPassword" MasterPageFile="/Auth.Master" %>

<asp:Content ContentPlaceHolderID="forgotPasswordPlaceHolder" runat="server">
    <form runat="server">
        <%--Forgot Password Form--%>
        <div id="forgotPassword">
            <h1 class="fw-bold mt-4 mb-4"><a href="Default.aspx" class="link-light link-offset-2 link-underline-opacity-25 link-underline-opacity-100-hover"><i class="fa-solid fa-arrow-left-long me-3"></i></a>Forgot Password</h1>

            <div class="d-flex justify-content-center align-items-center gap-3">
                <i class="fa-fw fa-solid fa-envelope"></i>

                <asp:TextBox ID="forgotPasswordEmail" runat="server" CssClass="form-control mt-3 mb-2 px-3 py-3" placeholder="Email"></asp:TextBox>
            </div>

            <div class="ms-5">
                <asp:RequiredFieldValidator class="card-text text-danger" ID="forgotPasswordRequiredEmail" runat="server" ErrorMessage="*The email field is required." ControlToValidate="forgotPasswordEmail" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="text-danger" ID="RegularExpressionEmailValidation" runat="server" ErrorMessage="*The email is invalid." ControlToValidate="forgotPasswordEmail" ValidationExpression="^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$" Display="Dynamic"></asp:RegularExpressionValidator>
            </div>

            <div class="mt-2">
                <asp:Label ID="ForgotPasswordEmailSuccessLabel" CssClass="fw-bold text-success" runat="server" Text=""></asp:Label>
            </div>

            <asp:Button ID="resetPasswordButton" CssClass="btn btn-outline-light mt-3 px-4 py-2" runat="server" Text="Reset Password" OnClick="resetPasswordButtonClick"></asp:Button>
    </form>
</asp:Content>
