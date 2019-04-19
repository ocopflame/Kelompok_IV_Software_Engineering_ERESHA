<%@ page language="VB" autoeventwireup="false" inherits="tingkat, App_Web_qsfiy5ho" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head id="Head1" runat="server">
    <title>e-Filing</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="icon" type="image/png" sizes="16x16" href="assets/images/pertamedika.png" />
    <link href="assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/plugins/bootstrap-material-datetimepicker/css/bootstrap-material-datetimepicker.css" rel="stylesheet" />
    <!-- Page plugins css -->
    <link href="assets/plugins/clockpicker/dist/jquery-clockpicker.min.css" rel="stylesheet" />
    <!-- Color picker plugins css -->
    <link href="assets/plugins/jquery-asColorPicker-master/dist/css/asColorPicker.css" rel="stylesheet" />
    <!-- Date picker plugins css -->
    <link href="assets/plugins/bootstrap-datepicker/bootstrap-datepicker.min.css" rel="stylesheet" type="text/css" />
    <!-- Daterange picker plugins css -->
    <link href="assets/plugins/timepicker/bootstrap-timepicker.min.css" rel="stylesheet" />
    <link href="assets/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />

	<link href="css/jquery.toast.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    <link href="css/colors/blue.css" id="theme" rel="stylesheet" />

    <script type="text/javascript">
        function alertMe() {
            alert('Hello');
        }
    </script>

</head>
<body class="fix-header fix-sidebar card-no-border">

    <div class="preloader">
        <svg class="circular" viewBox="25 25 50 50">
            <circle class="path" cx="50" cy="50" r="20" fill="none" stroke-width="2" stroke-miterlimit="10" /> 
        </svg>
    </div>

    <div id="main-wrapper">
        <header class="topbar">
            <nav class="navbar top-navbar navbar-toggleable-sm navbar-light">
                <div class="navbar-header">
                    <a class="navbar-brand" href="dashboard.aspx">
                        <b>
						    <img src="images/logo_lite.png" alt="homepage" class="light-logo" height="40px" />
                        </b>
                        <span>
                         <img src="images/logo_text.png" class="light-logo" alt="homepage" />
                         </span> 
                    </a>
                </div>

                <div class="navbar-collapse">
                    <ul class="navbar-nav mr-auto mt-md-0">
                        <!-- This is  -->
                          <li class="nav-item"> <a class="nav-link nav-toggler hidden-md-up text-muted waves-effect waves-dark" href="javascript:void(0)"><i class="mdi mdi-menu"></i></a> </li>
                        <li class="nav-item hidden-sm-down search-box"> <a class="nav-link hidden-sm-down text-muted waves-effect waves-dark" href="javascript:void(0)"><i class="ti-search"></i></a>
                          <form class="app-search">
                                <input type="text" class="form-control" placeholder="Search & enter"> <a class="srh-btn"><i class="ti-close"></i></a> </form>
                       </li> 
                    </ul>
                    <ul class="navbar-nav my-lg-0">
                        <li class="nav-item dropdown">
                            <a class="nav-link dropdown-toggle text-muted waves-effect waves-dark" href="" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><!-- <img src="../assets/images/users/1.jpg" alt="user" class="profile-pic m-r-10"  />--><asp:Label ID="lblNamaUser" runat="server" Text=""></asp:Label></a>
                        </li>
                    </ul>
                </div>
            </nav>
        </header>

        <aside class="left-sidebar">
            <!-- Sidebar scroll-->
            <div class="scroll-sidebar">
                <!-- Sidebar navigation-->
                <nav class="sidebar-nav">
                    <ul id="sidebarnav">
                        <%Select GetCookies("e_filing_OtorisasiID").ToString%>
                            <%Case "1"%> <!-- #include file ="m_admin.htm" -->
                            <%Case "2"%> <!-- #include file ="m_user.htm" -->
                        <%End Select%>
                    </ul>
                </nav>
                <!-- End Sidebar navigation -->
            </div>
            <!-- End Sidebar scroll-->
            <!-- Bottom points-->
            <div class="sidebar-footer">
                <!-- item--><a href="" class="link" data-toggle="tooltip" title="Settings"><i class="ti-settings"></i></a>
                <!-- item--><a href="" class="link" data-toggle="tooltip" title="Email"><i class="mdi mdi-gmail"></i></a>
                <!-- item--><a href="" class="link" data-toggle="tooltip" title="Logout"><i class="mdi mdi-power"></i></a> 
            </div>
            <!-- End Bottom points-->
        </aside>

        <div class="page-wrapper">
            <div class="container-fluid">
<%--                <div class="row page-titles">
                    <div class="col-md-5 col-8 align-self-center">
                        <h3 class="text-themecolor m-b-0 m-t-0">Tingkat</h3>
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="javascript:void(0)">Home</a></li>
                            <li class="breadcrumb-item active">Tingkat</li>
                        </ol>
                    </div>
                </div>--%>

                <div class="row">
                    <div class="col-lg-12 col-xlg-12 col-md-12">
                        <div class="card">
                            <form id="form1" runat="server">
                                <asp:Literal ID="ltAlert" runat="server"></asp:Literal>
							    <div class="card-block">
                                    <h2 class="card-title">Tingkat Dokumen</h2>
                                    <h6 class="card-subtitle italic"><asp:Label ID="lblHeader" runat="server" 
                                            Text="Daftar Tingkat Dokumen"></asp:Label></h6> 
							    </div>

                                <div class="card-block">
                                    <asp:HiddenField ID="hfIdData" runat="server" />
                                    <asp:Label ID="lblErrMessage" runat="server" ForeColor="Red"></asp:Label>
                                    
                                    <asp:panel id="pnlInputData" runat="server" visible="false" >
                                        <div class="form-material m-t-0 row">
							                <div class="form-group required col-md-6 m-t-30">
                                                <label>Tingkat</label>
                                                <asp:TextBox ID="txtTingkat" runat="server" required=""  class="form-control form-control-line" ></asp:TextBox>
                                            </div>
								            <div class="form-group required col-md-6 m-t-30">
									            <label>Status</label>
                                                <asp:DropDownList ID="ddlStatus" required="" runat="server" class="form-control form-control-line" >
                                                    <asp:ListItem Text="Aktif" Value="1"></asp:ListItem> 
                                                    <asp:ListItem Text="Tidak Aktif" Value="2"></asp:ListItem> 
                                                </asp:DropDownList>								            
                                            </div>

                                            <div class="form-group col-md-12 m-t-30 m-b-30">
                                                <asp:Button ID="btnSave" runat="server" Text="Simpan" class="btn btn-danger" type="submit" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Batal" class="btn btn-danger" CausesValidation ="false" formnovalidate="formnovalidate"  />
                                            </div>
                                        </div>
                                    </asp:panel>

                                    <asp:panel id="pnlGridData" runat="server" >
                                        <div>
                                            <asp:LinkButton ID="lnkAddNew" runat="server" class="btn btn-danger waves-effect waves-dark" aria-expanded="false"><i class="mdi mdi-open-in-new"></i><span class="hide-menu"> Tambah Baru</span></asp:LinkButton>
                                        </div>

                                        <div class="table-responsive">
                                            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" OnSorting="gv_Sorting"
                                                AllowPaging="True" GridLines="None" 
                                                CssClass="table table-bordered m-t-15 table-hover contact-list nomargin" PageSize="500" 
                                                AllowSorting="true" >                
                                                <AlternatingRowStyle CssClass="alt" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="BtnEdit" runat="server" CausesValidation="False"  
                                                                CommandArgument='<%#Eval("TingkatID")%>' CommandName="Ubah"   
                                                                ImageUrl="images/icon/edit.png" ToolTip="Edit Data" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNomor" runat="Server" 
                                                                Text="<%# Container.DataItemIndex + 1 %>" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25px" />
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="Tingkat" SortExpression="Tingkat" HeaderText="Tingkat Dokumen" />
                                                    <asp:BoundField DataField="Status" SortExpression="Status" HeaderText="Status"  />
                                                </Columns>
                                                <PagerSettings Position="Top" />
                                                <PagerStyle CssClass="pgr" />
                                            </asp:GridView>
									    </div>
                                    </asp:panel>


							    </div>
                            </form>
						</div>
					</div>
			    </div>
		    </div>

            <footer class="footer">
                © 2019 Kelompok IV
            </footer>
        </div>
    </div>

    <script type="text/javascript" src="assets/plugins/jquery/jquery.min.js"></script>
    <!-- Bootstrap tether Core JavaScript -->
	<script type="text/javascript" src="js/download/popper.min.js"></script>	
    <script type="text/javascript" src="assets/plugins/bootstrap/js/tether.min.js"></script>
    <script type="text/javascript" src="assets/plugins/bootstrap/js/bootstrap.min.js"></script>
    <!-- slimscrollbar scrollbar JavaScript -->
    <script type="text/javascript" src="js/jquery.slimscroll.js"></script>
    <!-- Wave Effects -->
    <script type="text/javascript" src="js/waves.js"></script>
    <!-- Menu sidebar -->
    <script type="text/javascript" src="js/sidebarmenu.js"></script>
    <!--stickey kit -->
    <script type="text/javascript" src="assets/plugins/sticky-kit-master/dist/sticky-kit.min.js"></script>
	<script type="text/javascript" src="js/download/jquery.sparkline.min.js"></script>	
    <!-- Custom JavaScript -->
    <script type="text/javascript" src="js/custom.min.js"></script>
    <script type="text/javascript" src="js/download/jquery.toast.js"></script>
	<script type="text/javascript" src="js/download/toastr.js"></script>
    <script type="text/javascript" src="assets/plugins/inputmask/dist/min/jquery.inputmask.bundle.min.js"></script>
    <script type="text/javascript" src="js/mask.init.js"></script>
	<!-- Style switcher -->
    <script type="text/javascript" src="js/download/jQuery.style.switcher.js"></script>
	
    <!-- Plugin JavaScript -->
    <script type="text/javascript" src="assets/plugins/moment/moment.js"></script>
    <script type="text/javascript" src="assets/plugins/bootstrap-material-datetimepicker/js/bootstrap-material-datetimepicker.js"></script>
    <!-- Clock Plugin JavaScript -->
    <script type="text/javascript" src="assets/plugins/clockpicker/dist/jquery-clockpicker.min.js"></script>
    <!-- Color Picker Plugin JavaScript -->
    <script type="text/javascript" src="assets/plugins/jquery-asColor/dist/jquery-asColor.js"></script>
    <script type="text/javascript" src="assets/plugins/jquery-asGradient/dist/jquery-asGradient.js"></script>
    <script type="text/javascript" src="assets/plugins/jquery-asColorPicker-master/dist/jquery-asColorPicker.min.js"></script>
    <!-- Date Picker Plugin JavaScript -->
    <script type="text/javascript" src="assets/plugins/bootstrap-datepicker/bootstrap-datepicker.min.js"></script>
    <!-- Date range Plugin JavaScript -->
    <script type="text/javascript" src="assets/plugins/timepicker/bootstrap-timepicker.min.js"></script>
    <script type="text/javascript" src="assets/plugins/daterangepicker/daterangepicker.js"></script>
    <script type="text/javascript" src="assets/plugins/moment/moment.js"></script>

	<script type="text/javascript">
	    focusMethod = function getFocus() {
	        document.getElementById("txtLainnya").focus();
	    }

	    focusMethod1 = function getFocus() {
	        document.getElementById("rbTingkat9").checked = true;
	    }
	    // MAterial Date picker    
	    $('#txtTanggal').bootstrapMaterialDatePicker({ format: 'DD/MM/YYYY', weekStart: 0, time: false });
	    $('#txtTglSurat').bootstrapMaterialDatePicker({ format: 'DD/MM/YYYY', weekStart: 0, time: false });
	    $('#txtTglPeringatan').bootstrapMaterialDatePicker({ format: 'DD/MM/YYYY', weekStart: 0, time: false });

	</script>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".alert").fadeOut(5000);
        })
    </script>

</body>
</html>
