<%@ Page Title="Home" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ASP.NET_heimdall.Default" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Default Page</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <link href="Content/style.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/bootstrap-icons.min.css" rel="stylesheet" />
</head>

<body class="min-vh-100 bg-black vh-100">
    <section class="container vh-100 d-flex flex-column justify-content-center">
        <div class="row">
            <div class="col-sm-6 d-flex align-items-center justify-content-center">
                <img src="Assets/heimdall-300x300-transparent.png" />
            </div>
        <div class="col-sm-6">
            <div class="card bg-black text-bg-dark">
                <div class="card-body p-5">
                    <h1 class="fw-bold">Welcome to Heimdall</h1>

                    <h3 class="mt-5 mb-4">Sign In</h3>
                    <form runat="server">
                        <asp:TextBox runat="server" CssClass="form-control my-3 px-3 py-3" placeholder="Username"></asp:TextBox>
                        <asp:TextBox runat="server" TextMode="Password" CssClass="form-control my-3 px-3 py-3" placeholder="Password"></asp:TextBox>
                        <h6><a href="#" class="text-decoration-none">Forgot Password?</a></h6>
                        <button class="btn btn-outline-light mt-3 px-4 py-2">Login</button>


                        <h6 class="d-flex mt-5">Don't have an account?<a href="#" class="ms-2 text-decoration-none">Sign Up</a></h6>

                    </form>
                </div>
            </div>
        </div>
        </div>
    </section>

    <script src="Scripts/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.7.1.min.js" integrity="sha256-/JqT3SQfawRcv/BIHPThkBvs0OEvtFFmqPF/lYI/Cxo=" crossorigin="anonymous"></script>
    <script src="https://kit.fontawesome.com/8b835e0cdc.js" crossorigin="anonymous"></script>
</body>
</html>
