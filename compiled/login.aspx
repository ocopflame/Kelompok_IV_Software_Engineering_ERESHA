<%@ page language="VB" autoeventwireup="false" inherits="login, App_Web_qsfiy5ho" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="icon" type="image/png" sizes="16x16" href="assets/images/pertamedika.png" />
    <link href="assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
	<link href="css/jquery.toast.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    <link href="css/colors/blue.css" id="theme" rel="stylesheet" />
    <style type="text/css">
        .jqstooltip  
        {
            position: absolute;left: 0px;top: 0px;visibility: hidden;
            background: rgb(0, 0, 0) transparent;background-color: rgba(0,0,0,0.6);
            filter:progid:DXImageTransform.Microsoft.gradient(startColorstr=#99000000, endColorstr=#99000000);-ms-filter: "progid:DXImageTransform.Microsoft.gradient(startColorstr=#99000000, endColorstr=#99000000)";
            color: white;font: 10px arial, san serif;text-align: left;white-space: nowrap;padding: 5px;
            border: 1px solid white;z-index: 10000;
        }
            .jqsfield { color: white;font: 10px arial, san serif;text-align: left;}
    </style>

    <script type="text/javascript">
            window.history.forward();
            function noBack() {
                window.history.forward();
            }
    </script>
</head>
<body onload="noBack();" onpageshow="if (event.persisted) noBack();" onunload="">
    <!-- ============================================================== -->
    <!-- Preloader - style you can find in spinners.css -->
    <!-- ============================================================== -->
    <div class="preloader" style="display: none;">
        <svg class="circular" viewBox="25 25 50 50">
            <circle class="path" cx="50" cy="50" r="20" fill="none" stroke-width="2" stroke-miterlimit="10"></circle> </svg>
    </div>
    <!-- ============================================================== -->
    <!-- Main wrapper - style you can find in pages.scss -->
    <!-- ============================================================== -->
    <section id="wrapper" class="login-register login-sidebar" style="background-image:url(images/siti.jpg);">
        <div class="login-box card">
            <div class="card-body">
                <form runat="server" class="form-horizontal form-material" id="loginform" >
                    <a href="javascript:void(0)" class="text-center db"><img src="images/efiling.png" alt="Home"><br><img src="images/logo_text.png" alt="Home"></a>
                    <div style="text-align:center;" ><h2>KELOMPOK 4</h2></div>
                    <div style="text-align:center;" ><h2>Login</h2></div>

                    <div class="form-group m-t-40">
                        <div class="col-xs-12">
                            <asp:Label ID="lblErrMessage" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group m-t-40">
                        <div class="col-xs-12">
                            <asp:TextBox ID="txtUserID" runat="server" class="form-control" required="" placeholder="User Name" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-xs-12">
                            <asp:TextBox ID="txtPassword" runat="server" class="form-control" required="" placeholder="Password" TextMode="Password" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group text-center m-t-20">
                        <div class="col-xs-12">
                            <asp:Button ID="cmdLogin" runat="server" Text="Login" class="btn btn-info btn-lg btn-block text-uppercase" />
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </section>
    <!-- ============================================================== -->
    <!-- End Wrapper -->
    <!-- ============================================================== -->
    <!-- ============================================================== -->
    <!-- All Jquery -->
    <!-- ============================================================== -->
    <script type="text/javascript" src="assets/plugins/jquery/jquery.min.js"></script>
    <!-- Bootstrap tether Core JavaScript -->
	<script type="text/javascript" src="js/download/popper.min.js"></script>	
    <script type="text/javascript" src="assets/plugins/bootstrap/js/tether.min.js"></script>
    <script type="text/javascript" src="assets/plugins/bootstrap/js/bootstrap.min.js"></script>
    <!-- slimscrollbar scrollbar JavaScript -->
    <script type="text/javascript" src="js/jquery.slimscroll.js"></script>
    <!--Wave Effects -->
    <script type="text/javascript" src="js/waves.js"></script>
    <!--Menu sidebar -->
    <script type="text/javascript" src="js/sidebarmenu.js"></script>
    <!--stickey kit -->
    <script type="text/javascript" src="assets/plugins/sticky-kit-master/dist/sticky-kit.min.js"></script>
	<script type="text/javascript" src="js/download/jquery.sparkline.min.js"></script>	
	
    <!--Custom JavaScript -->
    <script type="text/javascript" src="js/custom.min.js"></script>
    <script type="text/javascript" src="js/download/jquery.toast.js"></script>
	<script type="text/javascript" src="js/download/toastr.js"></script>
	
	<!-- Style switcher -->
    <!-- ============================================================== -->
    <script type="text/javascript" src="js/download/jQuery.style.switcher.js"></script>

</body>
</html>
