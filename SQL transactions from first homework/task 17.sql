-- BACK UPING, DROPING AND RESTORING THE NEEDED DATABASE FROM SoftUni DATABASE, task 17
USE [master]

BACKUP DATABASE SoftUni
TO DISK = 'D:\softuni-backup.bak'

DROP DATABASE Softuni

RESTORE DATABASE SoftUni 
FROM DISK = 'D:\softuni-backup.bak'