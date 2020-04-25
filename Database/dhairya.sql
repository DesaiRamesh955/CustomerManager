-- phpMyAdmin SQL Dump
-- version 4.8.5
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 25, 2020 at 03:57 PM
-- Server version: 10.1.38-MariaDB
-- PHP Version: 7.1.28

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `dhairya`
--

-- --------------------------------------------------------

--
-- Table structure for table `customer`
--

CREATE TABLE `customer` (
  `id` int(11) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Device` varchar(50) NOT NULL,
  `Note` varchar(50) NOT NULL,
  `Fault` varchar(50) NOT NULL,
  `RDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Paid` int(11) NOT NULL DEFAULT '0',
  `Number` bigint(20) NOT NULL,
  `User` varchar(50) NOT NULL,
  `Close_date` varchar(50) NOT NULL DEFAULT '-',
  `receipt_no` int(10) NOT NULL,
  `status` int(1) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `from_tbl`
--

CREATE TABLE `from_tbl` (
  `id` int(11) NOT NULL,
  `from_name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `from_tbl`
--

INSERT INTO `from_tbl` (`id`, `from_name`) VALUES
(1, 'Dhairya Infocom');

-- --------------------------------------------------------

--
-- Table structure for table `replacement`
--

CREATE TABLE `replacement` (
  `id` int(11) NOT NULL,
  `cust_name` varchar(100) NOT NULL,
  `device` varchar(50) NOT NULL,
  `serial_no` varchar(50) NOT NULL,
  `courier_name` varchar(50) NOT NULL,
  `where_from` varchar(50) NOT NULL,
  `where_to` varchar(50) NOT NULL,
  `date` varchar(50) NOT NULL,
  `note` varchar(50) NOT NULL,
  `fault` varchar(50) NOT NULL,
  `number` bigint(12) NOT NULL,
  `status_replace` tinyint(1) NOT NULL DEFAULT '0',
  `status_cust` tinyint(1) NOT NULL DEFAULT '0',
  `user` varchar(50) NOT NULL,
  `docket` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `replacement`
--

INSERT INTO `replacement` (`id`, `cust_name`, `device`, `serial_no`, `courier_name`, `where_from`, `where_to`, `date`, `note`, `fault`, `number`, `status_replace`, `status_cust`, `user`, `docket`) VALUES
(1, 'dasdsad', 'dasdasdasd', '0000', 'asdasd', 'Dhairya Infocom', 'sagar', '11-04-2020', 'dsadsadasd', 'sadsadasd', 6666666666, 0, 0, 'ramesh', 'No number');

-- --------------------------------------------------------

--
-- Table structure for table `to_tbl`
--

CREATE TABLE `to_tbl` (
  `id` int(11) NOT NULL,
  `to_name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `id` int(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(250) NOT NULL,
  `role` enum('admin','superadmin','','') NOT NULL,
  `last_login` varchar(50) NOT NULL DEFAULT '-',
  `block` int(1) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`id`, `username`, `password`, `role`, `last_login`, `block`) VALUES
(1, 'admin', 'admin@123', 'superadmin', '25-04-2020 07:24', 0);

-- --------------------------------------------------------

--
-- Table structure for table `user_work`
--

CREATE TABLE `user_work` (
  `id` int(11) NOT NULL,
  `user` int(2) NOT NULL,
  `user_work` varchar(50) NOT NULL,
  `date` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `user_work`
--

INSERT INTO `user_work` (`id`, `user`, `user_work`, `date`) VALUES
(1, 2, 'canon printer OK', '07-04-2020 05:50'),
(2, 3, 'I am new', '07-04-2020 06:23');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `customer`
--
ALTER TABLE `customer`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `from_tbl`
--
ALTER TABLE `from_tbl`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `replacement`
--
ALTER TABLE `replacement`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `to_tbl`
--
ALTER TABLE `to_tbl`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `user_work`
--
ALTER TABLE `user_work`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `customer`
--
ALTER TABLE `customer`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `from_tbl`
--
ALTER TABLE `from_tbl`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `replacement`
--
ALTER TABLE `replacement`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `to_tbl`
--
ALTER TABLE `to_tbl`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `user_work`
--
ALTER TABLE `user_work`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
