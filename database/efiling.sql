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

-- membuang struktur untuk table efiling.divisi
CREATE TABLE IF NOT EXISTS `divisi` (
  `DivisiID` smallint(6) NOT NULL AUTO_INCREMENT,
  `Divisi` varchar(50) DEFAULT NULL,
  `StatusID` smallint(6) DEFAULT NULL,
  `CreateBy` varchar(50) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `UpdateBy` varchar(50) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  PRIMARY KEY (`DivisiID`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;

-- Pengeluaran data tidak dipilih.
-- membuang struktur untuk table efiling.incoming
CREATE TABLE IF NOT EXISTS `incoming` (
  `IncomingID` int(11) NOT NULL AUTO_INCREMENT,
  `DivisiID` smallint(6) DEFAULT NULL,
  `Tanggal` datetime DEFAULT NULL,
  `TanggalSurat` datetime DEFAULT NULL,
  `Dari` varchar(100) DEFAULT NULL,
  `NoSurat` varchar(100) NOT NULL,
  `NoAgenda` varchar(100) DEFAULT NULL,
  `Perihal` varchar(255) DEFAULT NULL,
  `Ringkasan` varchar(500) DEFAULT NULL,
  `DisposisiKe` varchar(500) DEFAULT NULL,
  `IsiDisposisi` varchar(500) DEFAULT NULL,
  `JenisID` smallint(6) DEFAULT NULL,
  `TingkatID` smallint(6) DEFAULT NULL,
  `lampiran1` varchar(255) DEFAULT NULL,
  `lampiran2` varchar(255) DEFAULT NULL,
  `lampiran3` varchar(255) DEFAULT NULL,
  `UserName` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`IncomingID`),
  UNIQUE KEY `DivisiID_NoSurat` (`DivisiID`,`NoSurat`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;

-- Pengeluaran data tidak dipilih.
-- membuang struktur untuk table efiling.jenis
CREATE TABLE IF NOT EXISTS `jenis` (
  `JenisID` int(11) NOT NULL AUTO_INCREMENT,
  `Jenis` varchar(100) DEFAULT NULL,
  `StatusID` smallint(6) DEFAULT NULL,
  `CreateBy` varchar(50) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `UpdateBy` varchar(50) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  PRIMARY KEY (`JenisID`),
  UNIQUE KEY `Bentuk` (`Jenis`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;

-- Pengeluaran data tidak dipilih.
-- membuang struktur untuk table efiling.log
CREATE TABLE IF NOT EXISTS `log` (
  `LogID` int(11) NOT NULL AUTO_INCREMENT,
  `Modul` varchar(50) DEFAULT NULL,
  `Deskripsi` varchar(255) DEFAULT NULL,
  `UserName` varchar(50) DEFAULT NULL,
  `Tanggal` datetime DEFAULT NULL,
  PRIMARY KEY (`LogID`)
) ENGINE=InnoDB AUTO_INCREMENT=39 DEFAULT CHARSET=latin1;

-- Pengeluaran data tidak dipilih.
-- membuang struktur untuk table efiling.outgoing
CREATE TABLE IF NOT EXISTS `outgoing` (
  `OutgoingID` int(11) NOT NULL AUTO_INCREMENT,
  `DivisiID` smallint(6) DEFAULT NULL,
  `Tanggal` datetime DEFAULT NULL,
  `TanggalSurat` datetime DEFAULT NULL,
  `Kepada` varchar(100) DEFAULT NULL,
  `NoSurat` varchar(100) NOT NULL,
  `NoAgenda` varchar(100) DEFAULT NULL,
  `Perihal` varchar(255) DEFAULT NULL,
  `Ringkasan` varchar(500) DEFAULT NULL,
  `JenisID` smallint(6) DEFAULT NULL,
  `TingkatID` smallint(6) DEFAULT NULL,
  `lampiran1` varchar(255) DEFAULT NULL,
  `lampiran2` varchar(255) DEFAULT NULL,
  `lampiran3` varchar(255) DEFAULT NULL,
  `UserName` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`OutgoingID`),
  UNIQUE KEY `DivisiID_NoSurat` (`DivisiID`,`NoSurat`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=latin1;

-- Pengeluaran data tidak dipilih.
-- membuang struktur untuk table efiling.tingkat
CREATE TABLE IF NOT EXISTS `tingkat` (
  `TingkatID` smallint(6) NOT NULL AUTO_INCREMENT,
  `Tingkat` varchar(50) DEFAULT NULL,
  `StatusID` smallint(6) DEFAULT NULL,
  `CreateBy` varchar(50) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `UpdateBy` varchar(50) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  PRIMARY KEY (`TingkatID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

-- Pengeluaran data tidak dipilih.
-- membuang struktur untuk table efiling.user
CREATE TABLE IF NOT EXISTS `user` (
  `UserID` int(11) NOT NULL AUTO_INCREMENT,
  `DivisiID` smallint(6) DEFAULT NULL,
  `UserName` varchar(50) DEFAULT NULL,
  `password` varchar(50) DEFAULT NULL,
  `NamaUser` varchar(100) DEFAULT NULL,
  `OtorisasiID` smallint(6) DEFAULT NULL,
  `StatusID` smallint(6) DEFAULT NULL,
  `CreateBy` varchar(50) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `UpdateBy` varchar(50) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  PRIMARY KEY (`UserID`),
  UNIQUE KEY `UserName` (`UserName`),
  KEY `FK_user_divisi` (`DivisiID`),
  CONSTRAINT `FK_user_divisi` FOREIGN KEY (`DivisiID`) REFERENCES `divisi` (`DivisiID`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=latin1;

-- Pengeluaran data tidak dipilih.
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
