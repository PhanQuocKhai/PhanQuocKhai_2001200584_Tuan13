CREATE DATABASE USERS
go

USE USERS
go

CREATE TABLE TK
(
	idTK int identity(1,1),
	TenTK nvarchar(50),
	matKhau varchar(MAX),
	CONSTRAINT PK_ten primary key (TenTK) 
)