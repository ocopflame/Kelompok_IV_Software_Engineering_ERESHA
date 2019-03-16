===========================================
= Nama User       : RaRizal123             =
= Nama Mahasiswa  : Rachmad Rizal          =
= NIM             : 181022000003           =
===========================================

<?php
$host       = "localhost";
$user       = "root";
$password   = "";
$database   = "source_artikel";
$connect    = mysqli_connect($host, $user, $password, $database)
?>


<form action="login_proses.php" method="post">
    <input type="text" name="username" placeholder="username">
    <input type="password" name="password" placeholder="password">
    <button type="submit" name="submit">Login</button>
</form>


<?php
include '../koneksi.php';
$username = $_POST['username'];
$password = md5($_POST['password']);
$login    = mysqli_query($connect, "select * from users where username='$username' and password='$password'");
$result   = mysqli_num_rows($login);
if($result>0){
    $user = mysqli_fetch_array($login);
    session_start();
    $_SESSION['username'] = $user['username'];
    header("location:welcome.php");
}else{
    header("location:login.php");
}
