-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Aug 15, 2026 at 11:17 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `online_course_selling`
--

-- --------------------------------------------------------

--
-- Table structure for table `courses`
--

CREATE TABLE `courses` (
  `Id` int(11) NOT NULL,
  `Title` varchar(150) NOT NULL,
  `Description` varchar(2000) NOT NULL,
  `Price` decimal(65,30) NOT NULL,
  `DurationInHours` int(11) NOT NULL,
  `Category` varchar(100) NOT NULL,
  `CreatedAtUtc` datetime(6) NOT NULL,
  `TeacherId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `Id` int(11) NOT NULL,
  `FullName` longtext NOT NULL,
  `Email` varchar(255) NOT NULL,
  `Password` longtext NOT NULL,
  `IsTeacherApproved` tinyint(1) NOT NULL DEFAULT 0,
  `IsTeacherRequestPending` tinyint(1) NOT NULL DEFAULT 0,
  `Role` varchar(20) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`Id`, `FullName`, `Email`, `Password`, `IsTeacherApproved`, `IsTeacherRequestPending`, `Role`) VALUES
(7, 'Admin', 'admin@gmail.com', 'AQAAAAIAAYagAAAAEGJFBoTxb5X/6X5VHl5NpSwPxbCbGlG5ax4JE1xMYWRg0cfZKqDZWBa8kt7Z9nsLew==', 0, 0, 'Admin'),
(8, 'Irfan ahmed', 'irfanahmed99989@gmail.com', 'AQAAAAIAAYagAAAAEId3Zukf2iXDD3N6TpAcmZQfgQN5PZyOoJEt0uIYS1L6Zjabmt85My6XsVun6eZVjA==', 0, 0, 'Teacher');

-- --------------------------------------------------------

--
-- Table structure for table `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20260810163731_Users', '9.0.0'),
('20260811183139_RoleBasedAuthAndCourses', '9.0.0');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `courses`
--
ALTER TABLE `courses`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Courses_TeacherId` (`TeacherId`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_Users_Email` (`Email`);

--
-- Indexes for table `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `courses`
--
ALTER TABLE `courses`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `courses`
--
ALTER TABLE `courses`
  ADD CONSTRAINT `FK_Courses_Users_TeacherId` FOREIGN KEY (`TeacherId`) REFERENCES `users` (`Id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
