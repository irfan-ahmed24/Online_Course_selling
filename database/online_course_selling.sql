-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Aug 19, 2026 at 12:48 PM
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
(51, ' web design front end developer Bangla Tutorial', 'https://youtu.be/FwmuhNTrJO4?si=hVk1g1xWX8jK3Hl2', 13),
(52, 'html bangla tutorial 0 : Guiding Video | playlist details', 'https://youtu.be/J5nGBcgTHz8?si=vMtRzE3w-L6-sCzG', 13),
(53, 'html bangla tutorial 1 : Introduction to HTML', 'https://youtu.be/d35dfSwBTNY?si=VC6kMzh1EAdxCJdx', 13),
(54, 'html bangla tutorial 2: Tag, element & attribute', 'https://youtu.be/SEZ7YCF141I?si=bKtHElPpFOdWo5pk', 13),
(55, ' html bangla tutorial 3 : basic structure | HTML এর সাধারণ গঠন', 'https://youtu.be/t9FkGMxsz_g?si=k1DRroJtVPwcVwGo', 13),
(56, 'html bangla tutorial 4 : first html webpage', 'https://youtu.be/j4jh3iZ6t-M?si=VYcOn0An8BEGW422', 13);

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
  `VideoCount` int(11) NOT NULL DEFAULT 0,
  `TeacherId` int(11) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `courses`
--

INSERT INTO `courses` (`Id`, `Title`, `Category`, `Price`, `Description`, `ThumbnailUrl`, `CreatedAt`, `VideoCount`, `TeacherId`) VALUES
(13, 'HTML tutorial for basic', 'Web Development', 10.00, 'this is for begineer HTML learning. You can learn from basic of HTML as a begineer.', '/uploads/thumbnails/41a01ff8-3e0d-432a-bd27-569ab990d3fb_images (9).jpg', '2026-08-19 16:46:23.032029', 6, 3);

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
(3, 'Irfan ahmed', 'irfanahmed99989@gmail.com', 'AQAAAAIAAYagAAAAEBnJnyr3nZvBA+QSejpWHaOY0cSX2LClk6WhhH77NDPnIBCCo0oysTfvXkiGSJgZvg==', 'Teacher'),
(4, 'Forhad islam', 'forhadislam@gmail.com', 'AQAAAAIAAYagAAAAEI80VIz0YArdTf+0Nd39FLh5NXIt4Dvl2nAjmz2v8LrIbP4/D2l5j/rOq/iRED6hYA==', 'Teacher');

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
('20260819060307_AddTeacherIdToCourse', '9.0.0');

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
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=57;

--
-- AUTO_INCREMENT for table `courses`
--
ALTER TABLE `courses`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

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
