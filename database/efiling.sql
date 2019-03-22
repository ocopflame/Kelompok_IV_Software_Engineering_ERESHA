-- phpMyAdmin SQL Dump
-- version 4.8.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Waktu pembuatan: 22 Mar 2019 pada 15.15
-- Versi server: 10.1.37-MariaDB
-- Versi PHP: 5.6.40

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `efiling`
--

-- --------------------------------------------------------

--
-- Struktur dari  tabel `bentuk`
--

CREATE TABLE `bentuk` (
  `BentukID` int(11) NOT NULL,
  `Bentuk` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Struktur dari tabel `incoming`
--

CREATE TABLE `incoming` (
  `IncomingID` int(11) NOT NULL,
  `TanggalTerima` datetime DEFAULT NULL,
  `TanggalSurat` datetime DEFAULT NULL,
  `BentukID` int(11) DEFAULT NULL,
  `Dari` varchar(100) DEFAULT NULL,
  `NoSurat` varchar(100) NOT NULL,
  `NoAgenda` varchar(100) DEFAULT NULL,
  `Perihal` varchar(255) DEFAULT NULL,
  `Ringkasan` varchar(500) DEFAULT NULL,
  `DisposisiKe` varchar(500) DEFAULT NULL,
  `IsiDisposisi` varchar(500) DEFAULT NULL,
  `EmailPeringatan` varchar(100) DEFAULT NULL,
  `TanggalPeringatan` datetime DEFAULT NULL,
  `IsiPeringatan` varchar(255) DEFAULT NULL,
  `lampiran1` varchar(255) DEFAULT NULL,
  `lampiran2` varchar(255) DEFAULT NULL,
  `lampiran3` varchar(255) DEFAULT NULL,
  `UserID` int(11) NOT NULL,
  `UserName` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Struktur dari tabel `user`
--

CREATE TABLE `user` (
  `UserID` int(11) NOT NULL,
  `UserName` varchar(50) DEFAULT NULL,
  `password` varchar(50) DEFAULT NULL,
  `NamaUser` varchar(100) DEFAULT NULL,
  `Otorisasi` smallint(6) DEFAULT NULL,
  `Status` smallint(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Indexes for dumped tables
--

--
-- Indeks untuk tabel `bentuk`
--
ALTER TABLE `bentuk`
  ADD PRIMARY KEY (`BentukID`),
  ADD UNIQUE KEY `Bentuk` (`Bentuk`);

--
-- Indeks untuk tabel `incoming`
--
ALTER TABLE `incoming`
  ADD PRIMARY KEY (`IncomingID`),
  ADD UNIQUE KEY `NoSurat` (`NoSurat`),
  ADD KEY `UserID` (`UserID`),
  ADD KEY `BentukID` (`BentukID`);

--
-- Indeks untuk tabel `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`UserID`),
  ADD UNIQUE KEY `UserName` (`UserName`);

--
-- AUTO_INCREMENT untuk tabel yang dibuang
--

--
-- AUTO_INCREMENT untuk tabel `bentuk`
--
ALTER TABLE `bentuk`
  MODIFY `BentukID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT untuk tabel `incoming`
--
ALTER TABLE `incoming`
  MODIFY `IncomingID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT untuk tabel `user`
--
ALTER TABLE `user`
  MODIFY `UserID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- Ketidakleluasaan untuk tabel pelimpahan (Dumped Tables)
--

--
-- Ketidakleluasaan untuk tabel `incoming`
--
ALTER TABLE `incoming`
  ADD CONSTRAINT `incoming_ibfk_1` FOREIGN KEY (`UserID`) REFERENCES `user` (`UserID`),
  ADD CONSTRAINT `incoming_ibfk_2` FOREIGN KEY (`BentukID`) REFERENCES `bentuk` (`BentukID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
