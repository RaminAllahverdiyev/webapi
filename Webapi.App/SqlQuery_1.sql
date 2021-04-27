CREATE DATABASE WebApiDb;

USE [WebApiDb]
GO
CREATE TABLE Users (
    id int  PRIMARY KEY Identity,
    "name" nvarchar (25) not null,
    "surname" nvarchar (25) not null,
    age int CHECK (Age>=0),
);
CREATE TABLE Employers (
    id int  PRIMARY KEY Identity,
    "name" nvarchar (30) not null ,
    UserId int FOREIGN KEY REFERENCES  Users(id) ON DELETE CASCADE
);
Insert into Users ("name","surname",age) values('Ramin','Allahverdiyev',31);
Insert into Users ("name","surname",age) values('Orxan','Allahverdiyev',32);
Insert into Users ("name","surname",age) values('Mustafa','Allahverdiyev',34);
Insert into Users ("name","surname",age) values('Mehemmed','Allahverdiyev',21);


Insert into Employers ("name",UserId) values('Mark',1);
Insert into Employers ("name",UserId) values('Jack',2);
Insert into Employers ("name",UserId) values('Musk',3);
Insert into Employers ("name",UserId) values('Joe',4);

commit;
