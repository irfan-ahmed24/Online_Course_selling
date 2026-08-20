-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Aug 20, 2026 at 10:24 PM
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
-- Table structure for table `courselectures`
--

CREATE TABLE `courselectures` (
  `Id` int(11) NOT NULL,
  `LectureTitle` longtext NOT NULL,
  `VideoUrl` longtext NOT NULL,
  `CourseId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `courselectures`
--

INSERT INTO `courselectures` (`Id`, `LectureTitle`, `VideoUrl`, `CourseId`) VALUES
(13, 'CSS Tutorial #1: Overview & Structure | Web Development | Filipino | Tagalog', 'https://youtu.be/RU-R2BXSCVw?si=XXlw69JlY9ozN8KR', 3);

-- --------------------------------------------------------

--
-- Table structure for table `courses`
--

CREATE TABLE `courses` (
  `Id` int(11) NOT NULL,
  `Title` longtext NOT NULL,
  `Category` longtext NOT NULL,
  `Price` decimal(18,2) NOT NULL,
  `Description` longtext NOT NULL,
  `ThumbnailUrl` longtext DEFAULT NULL,
  `CreatedAt` datetime(6) NOT NULL,
  `IsCourseApproved` tinyint(1) NOT NULL DEFAULT 0,
  `VideoCount` int(11) NOT NULL DEFAULT 0,
  `TeacherId` int(11) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `courses`
--

INSERT INTO `courses` (`Id`, `Title`, `Category`, `Price`, `Description`, `ThumbnailUrl`, `CreatedAt`, `IsCourseApproved`, `VideoCount`, `TeacherId`) VALUES
(3, 'learn CSS', 'Web Development', 200.00, 'this is css basic course', '/uploads/thumbnails/70bc6b10-5eba-480c-96ad-47692bb60bee_Screenshot 2025-10-30 205640.png', '2026-08-21 01:58:03.951254', 1, 1, 4);

-- --------------------------------------------------------

--
-- Table structure for table `enrollments`
--

CREATE TABLE `enrollments` (
  `Id` int(11) NOT NULL,
  `StudentId` int(11) NOT NULL,
  `CourseId` int(11) NOT NULL,
  `EnrollmentDate` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `teacherwallets`
--

CREATE TABLE `teacherwallets` (
  `Id` int(11) NOT NULL,
  `TeacherId` int(11) NOT NULL,
  `CurrentBalance` decimal(18,2) NOT NULL,
  `TotalWithdrawn` decimal(18,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `transactions`
--

CREATE TABLE `transactions` (
  `Id` int(11) NOT NULL,
  `CourseId` int(11) NOT NULL,
  `StudentId` int(11) NOT NULL,
  `TeacherId` int(11) NOT NULL,
  `TotalAmount` decimal(18,2) NOT NULL,
  `TeacherAmount` decimal(18,2) NOT NULL,
  `AdminCommission` decimal(18,2) NOT NULL,
  `TransactionDate` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `Id` int(11) NOT NULL,
  `FullName` longtext NOT NULL,
  `Email` longtext NOT NULL,
  `Password` longtext NOT NULL,
  `Role` longtext NOT NULL,
  `IsApproved` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`Id`, `FullName`, `Email`, `Password`, `Role`, `IsApproved`) VALUES
(1, 'System Admin', 'admin@gmail.com', 'AQAAAAIAAYagAAAAENfvPYdtjgq9SoU/GTIRFgX+L0ufGVMGhkAXRSri0YfkjXhMwbyr01/acC3Q3n9X0g==', 'Admin', 1),
(4, 'Irfan ahmed', 'irfanahmed99989@gmail.com', 'AQAAAAIAAYagAAAAECB5NAJ0myiLHvpWXAhowRJsYWJ6vAkNhQ1JYcY563VsKPpTQTGrjhGYkACNRp/msQ==', 'Teacher', 1),
(6, 'Irfan ahmed', 'irfanahmed89@gmail.com', 'AQAAAAIAAYagAAAAEEC0FpP9kOTODBLyVJJUsSBXKfIpkH2Kcl8J1DfcDVsw3m+6RE4wXvQZYz3H9z4RVw==', 'Student', 1);

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
('20260817171746_addUserAndCourseTable', '9.0.0'),
('20260817174144_AddCourseLectures', '9.0.0'),
('20260817181341_RemoveVideoUrlAndAddVideoCount', '9.0.0'),
('20260819060307_AddTeacherIdToCourse', '9.0.0'),
('20260819202519_AddTeacherForeignKeyToCourse', '9.0.0'),
('20260820191513_AddEnrollmentsTable', '9.0.0'),
('20260820191724_AddTransactionsTable', '9.0.0'),
('20260820191909_AddTeacherWalletsTable', '9.0.0'),
('20260820192153_AddTeacherWalletsTable', '9.0.0');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `courselectures`
--
ALTER TABLE `courselectures`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_CourseLectures_CourseId` (`CourseId`);

--
-- Indexes for table `courses`
--
ALTER TABLE `courses`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Courses_TeacherId` (`TeacherId`);

--
-- Indexes for table `enrollments`
--
ALTER TABLE `enrollments`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Enrollments_CourseId` (`CourseId`),
  ADD KEY `IX_Enrollments_StudentId` (`StudentId`);

--
-- Indexes for table `teacherwallets`
--
ALTER TABLE `teacherwallets`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_TeacherWallets_TeacherId` (`TeacherId`);

--
-- Indexes for table `transactions`
--
ALTER TABLE `transactions`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Transactions_CourseId` (`CourseId`),
  ADD KEY `IX_Transactions_StudentId` (`StudentId`),
  ADD KEY `IX_Transactions_TeacherId` (`TeacherId`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `courselectures`
--
ALTER TABLE `courselectures`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT for table `courses`
--
ALTER TABLE `courses`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `enrollments`
--
ALTER TABLE `enrollments`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `teacherwallets`
--
ALTER TABLE `teacherwallets`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `transactions`
--
ALTER TABLE `transactions`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `courselectures`
--
ALTER TABLE `courselectures`
  ADD CONSTRAINT `FK_CourseLectures_Courses_CourseId` FOREIGN KEY (`CourseId`) REFERENCES `courses` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `courses`
--
ALTER TABLE `courses`
  ADD CONSTRAINT `FK_Courses_Users_TeacherId` FOREIGN KEY (`TeacherId`) REFERENCES `users` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `enrollments`
--
ALTER TABLE `enrollments`
  ADD CONSTRAINT `FK_Enrollments_Courses_CourseId` FOREIGN KEY (`CourseId`) REFERENCES `courses` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Enrollments_Users_StudentId` FOREIGN KEY (`StudentId`) REFERENCES `users` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `teacherwallets`
--
ALTER TABLE `teacherwallets`
  ADD CONSTRAINT `FK_TeacherWallets_Users_TeacherId` FOREIGN KEY (`TeacherId`) REFERENCES `users` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `transactions`
--
ALTER TABLE `transactions`
  ADD CONSTRAINT `FK_Transactions_Courses_CourseId` FOREIGN KEY (`CourseId`) REFERENCES `courses` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Transactions_Users_StudentId` FOREIGN KEY (`StudentId`) REFERENCES `users` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Transactions_Users_TeacherId` FOREIGN KEY (`TeacherId`) REFERENCES `users` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
