<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="ASP.NET_heimdall.ResetPassword" MasterPageFile="/Auth.Master" %>

<asp:Content ContentPlaceHolderID="resetPasswordPlaceHolder" runat="server">
    <form runat="server">
        <div id="resetPassword">
            <h1 class="fw-bold mt-5 mb-4">Reset Password</h1>

            <div class="d-flex justify-content-center align-items-center gap-3">
                <i class="fa-fw fa-solid fa-unlock"></i>

                <asp:TextBox ID="resetPasswordTextBox" runat="server" TextMode="Password" CssClass="form-control mt-3 mb-2 px-3 py-3" placeholder="Password"></asp:TextBox>
            </div>
            <div class="ms-5">
                <asp:RequiredFieldValidator CssClass="text-danger" ID="signUpRequiredPassword" runat="server" ErrorMessage="*The password field is required." ControlToValidate="resetPasswordTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>

            <div class="d-flex justify-content-center align-items-center gap-3">
                <i class="fa-fw fa-solid fa-lock"></i>

                <asp:TextBox ID="confirmPasswordTextBox" runat="server" TextMode="Password" CssClass="form-control mt-3 mb-2 px-3 py-3" placeholder="Confirm Password"></asp:TextBox>
            </div>
            <div class="ms-5">
                <asp:CompareValidator CssClass="text-danger" ID="ComparePassword" runat="server" ErrorMessage="*Passwords do not match." ControlToValidate="resetPasswordTextBox" ControlToCompare="confirmPasswordTextBox" Display="Dynamic"></asp:CompareValidator>
            </div>

            <asp:Button ID="resetPasswordButton" CssClass="btn btn-outline-light mt-4 px-4 py-2" runat="server" Text="Reset Password" OnClick="resetPasswordButtonClick"></asp:Button>
        </div>
    </form>
</asp:Content>
