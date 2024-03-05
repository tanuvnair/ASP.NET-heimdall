<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerifyEmail.aspx.cs" Inherits="ASP.NET_heimdall.VerifyEmail" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Email Verification</title>
    <link href="Content/style.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/bootstrap-icons.min.css" rel="stylesheet" />
    <link runat="server" rel="icon" href="Assets/heimdall-300x300-transparent.png" type="image/ico" />
</head>

<body class="min-vh-100 bg-black vh-100">
    <section class="container vh-100 d-flex flex-column justify-content-center align-items-center flex">

        <a href="Default.aspx" class="w-25 mb-5">
            <img src="Assets/heimdall-300x300-transparent.png"  />
        </a>
        <h1 class="fw-bold text-white">
            <asp:Label ID="VerifedOrNotLabel" runat="server" Text=""></asp:Label><h1>
    </section>

    <script src="Scripts/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.7.1.min.js" integrity="sha256-/JqT3SQfawRcv/BIHPThkBvs0OEvtFFmqPF/lYI/Cxo=" crossorigin="anonymous"></script>
    <script src="https://kit.fontawesome.com/8b835e0cdc.js" crossorigin="anonymous"></script>
</body>
</html>
