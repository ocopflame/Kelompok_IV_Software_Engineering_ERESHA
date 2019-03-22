-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Versi server:                 10.1.16-MariaDB - mariadb.org binary distribution
-- OS Server:                    Win32
-- HeidiSQL Versi:               9.5.0.5196
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Membuang struktur basisdata untuk efiling
CREATE DATABASE IF NOT EXISTS `efiling` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `efiling`;

-- membuang struktur untuk table efiling.bentuk
CREATE TABLE IF NOT EXISTS `bentuk` (
  `BentukID` int(11) NOT NULL AUTO_INCREMENT,
  `Bentuk` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`BentukID`),
  UNIQUE KEY `Bentuk` (`Bentuk`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

-- Pengeluaran data tidak dipilih.
-- membuang struktur untuk table efiling.incoming
CREATE TABLE IF NOT EXISTS `incoming` (
  `IncomingID` int(11) NOT NULL AUTO_INCREMENT,
  `TanggalTerima` datetime DEFAULT NULL,
  `TanggalSurat` datetime DEFAULT NULL,
  `BentukID` smallint(6) DEFAULT NULL,
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
  `UserName` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`IncomingID`),
  UNIQUE KEY `NoSurat` (`NoSurat`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;

-- Pengeluaran data tidak dipilih.
-- membuang struktur untuk table efiling.user
CREATE TABLE IF NOT EXISTS `user` (
  `UserID` int(11) NOT NULL AUTO_INCREMENT,
  `UserName` varchar(50) DEFAULT NULL,
  `password` varchar(50) DEFAULT NULL,
  `NamaUser` varchar(100) DEFAULT NULL,
  `Otorisasi` smallint(6) DEFAULT NULL,
  `Status` smallint(6) DEFAULT NULL,
  PRIMARY KEY (`UserID`),
  UNIQUE KEY `UserName` (`UserName`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;

-- Pengeluaran data tidak dipilih.
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
