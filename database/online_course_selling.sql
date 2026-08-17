-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Aug 17, 2026 at 08:26 PM
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
(1, 'Atif Aslam song', 'https://youtu.be/XvLJYyBTQKA?si=Z3Jt6hl0D7po3Y5e', 1),
(2, 'atif aslam song2', 'https://youtu.be/SxTYjptEzZs?si=uGubPgBLD44y9CyB', 1),
(3, 'Atif Aslam song', 'https://youtu.be/XvLJYyBTQKA?si=Z3Jt6hl0D7po3Y5e', 2),
(4, 'Atif Aslam song', 'https://youtu.be/XvLJYyBTQKA?si=Z3Jt6hl0D7po3Y5e', 2),
(5, 'Atif Aslam song', 'https://youtu.be/XvLJYyBTQKA?si=Z3Jt6hl0D7po3Y5e', 3),
(6, 'Atif Aslam song', 'https://youtu.be/XvLJYyBTQKA?si=Z3Jt6hl0D7po3Y5e', 3),
(7, 'Atif Aslam song', 'https://youtu.be/XvLJYyBTQKA?si=Z3Jt6hl0D7po3Y5e', 3),
(8, 'Atif Aslam song', 'https://youtu.be/XvLJYyBTQKA?si=Z3Jt6hl0D7po3Y5e', 4),
(9, 'Atif Aslam song', 'https://youtu.be/XvLJYyBTQKA?si=Z3Jt6hl0D7po3Y5e', 4),
(10, 'Atif Aslam song', 'https://youtu.be/XvLJYyBTQKA?si=Z3Jt6hl0D7po3Y5e', 4),
(11, 'Atif Aslam song', 'https://youtu.be/XvLJYyBTQKA?si=Z3Jt6hl0D7po3Y5e', 5),
(12, 'Atif Aslam song', 'https://youtu.be/XvLJYyBTQKA?si=Z3Jt6hl0D7po3Y5e', 5);

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
  `VideoCount` int(11) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `courses`
--

INSERT INTO `courses` (`Id`, `Title`, `Category`, `Price`, `Description`, `ThumbnailUrl`, `CreatedAt`, `VideoCount`) VALUES
(1, 'song', 'Web Development', 200.00, 'this is song for test', NULL, '2026-08-17 23:55:43.209065', 0),
(2, 'songs', 'Mobile App Development', 300.00, 'this is also for test', '/uploads/thumbnails/e1c63afb-125a-4fac-9cf2-9fd3eafaa128_Screenshot 2025-10-30 205640.png', '2026-08-18 00:01:41.358278', 0),
(3, 'songs 3', 'Web Development', 200.00, 'this is also for test', '/uploads/thumbnails/4d369edc-a0b5-404e-863c-92e2c92cec1e_Screenshot 2026-07-17 020125.png', '2026-08-18 00:19:20.028546', 3),
(4, 'jhlkjh', 'Mobile App Development', 200.00, 'hgljhgbljk hjhkujhljkgbljglhg jigb hg ', '/uploads/thumbnails/c5fe652b-a46e-4ab0-8463-f0dd633c4cfd_Screenshot 2026-04-01 010135.png', '2026-08-18 00:21:09.166325', 3),
(5, 'hjgbj', 'Mobile App Development', 200.00, 'jsahdjfa aj shf jajkh jhajdsf', '/uploads/thumbnails/43e46054-24fe-472a-8a88-478eb298fabb_Screenshot 2026-05-15 164242.png', '2026-08-18 00:24:32.443980', 2);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `Id` int(11) NOT NULL,
  `FullName` longtext NOT NULL,
  `Email` longtext NOT NULL,
  `Password` longtext NOT NULL,
  `Role` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`Id`, `FullName`, `Email`, `Password`, `Role`) VALUES
(1, 'System Admin', 'admin@gmail.com', 'Admin123', 'Admin'),
(2, 'Irfan ahmed', 'irfanahmed89@gmail.com', 'AQAAAAIAAYagAAAAENQpO73XMqoXwSBu7Aob9Zd3GMi/IP3grdHB9lmtm4M2nm/ULzVhjTR9teNeUHwJBg==', 'Student'),
(3, 'Irfan ahmed', 'irfanahmed99989@gmail.com', 'AQAAAAIAAYagAAAAEBnJnyr3nZvBA+QSejpWHaOY0cSX2LClk6WhhH77NDPnIBCCo0oysTfvXkiGSJgZvg==', 'Teacher');

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
('20260817181341_RemoveVideoUrlAndAddVideoCount', '9.0.0');

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
  ADD PRIMARY KEY (`Id`);

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
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `courses`
--
ALTER TABLE `courses`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `courselectures`
--
ALTER TABLE `courselectures`
  ADD CONSTRAINT `FK_CourseLectures_Courses_CourseId` FOREIGN KEY (`CourseId`) REFERENCES `courses` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
