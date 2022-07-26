create database CMS

use CMS

create table UserAccounts(
username varchar(15) Not Null Unique  CHECK( username NOT LIKE '%[^A-Z0-9]%'),
firstname varchar(25),
lastname varchar(25),
pwd varchar(15) Not Null
);

ALTER TABLE UserAccounts
ADD PRIMARY KEY (username);

select * from UserAccounts

Create proc Insert_Accounts(@uname varchar(30),@fname varchar(15), @lname varchar(15),@pwd varchar(15))
as
begin
Insert into UserAccounts values(@uname,@fname,@lname,@pwd)
end

exec Insert_Accounts 'ninja20','Samar','Kumar','admin'

select * from UserAccounts where username='ninjaq20' and pwd='admin'


Create table Doctors(
DoctorId int Not Null Identity(100,1),
firstname varchar(15) Check(firstname  NOT LIKE '%[^A-Z]%' ),
lastname varchar(15) Check(lastname NOT LIKE '%[^A-Z]%' ),
sex VARCHAR(6) NOT NULL CHECK (sex IN ('Male', 'Female','Others')),
specialization varchar(20) Not Null Check(specialization IN ('General', 'Internal Medicine', 'Pediatrics', 'Orthopedics', 'Ophthalmology')),
fromdate datetime ,
todate datetime,
slotavailabel int
);



ALTER TABLE Doctors
ADD PRIMARY KEY (DoctorId);

select * from Doctors



Create proc Insert_Doc_details(@firstname varchar(15),@lastname varchar(15),@sex varchar(6),@specialization varchar(20),@fromdate datetime , @todate datetime)
as 
begin
Insert into Doctors(firstname,lastname,sex,specialization,fromdate,todate ,slotavailabel) values(@firstname,@lastname,@sex,@specialization,@fromdate,@todate, DATEDIFF(hour,@fromdate,@todate))
end

exec Insert_Doc_details 'Ramesh','Pandey','Male','General','20220601 10:00:00 AM' ,'20220601 11:00:00 AM' 
exec Insert_Doc_details 'Anshu','Iyer','Male','Orthopedics','20220601 09:00:00 AM' ,'20220601 01:00:00 PM' 
exec Insert_Doc_details 'Surabhi','Mishra','Female','Ophthalmology','20220601 11:00:00 AM' ,'20220601 06:00:00 PM' 
exec Insert_Doc_details 'Rashmi','Sharma','Female','General','20220601 11:00:00 AM' ,'20220601 01:00:00 PM' 

create table Patients(
patientId int Not Null Identity(1,1),
firstname varchar(15) Check(firstname  NOT LIKE '%[^A-Z]%' ),
lastname varchar(15) Check(lastname NOT LIKE '%[^A-Z]%' ),
sex VARCHAR(6) NOT NULL CHECK (sex IN ('Male', 'Female','Others')),
age int check(age>0 and age<120),
dob datetime
);


ALTER TABLE Patients
ADD PRIMARY KEY (patientId);

select * from Patients


alter proc insert_patients(@firstname varchar(15),@lastname varchar(15),@sex varchar(6),@age int ,@dob varchar(10))
as
begin
SET DATEFORMAT DMY
Insert into Patients values(@firstname,@lastname,@sex,@age,@dob)
end

---exec insert_patients 'Rohan','Gupta','Male',19,'20/03/1998'

Create table Appoitment(
appointmentId int Not Null Identity(1000,1),
Patientid int Not Null,
doctorfname varchar(20),
doctorlname varchar(20),
doctorid int not null,
visitdate datetime,
apttime int 
);

alter table Appoitment
Add Primary key(appointmentId)

alter table Appoitment
Add Foreign Key(Patientid) references Patients(patientId)

alter table Appoitment
Add Foreign Key(doctorid) references Doctors(DoctorId)

alter table Appoitment 
Add aptvisittime datetime


select * from Appoitment
select * from Doctors
select * from Patients

alter table Appoitment drop column aptvisittime 

alter proc record_appoitment(@patientid int ,@doctorfname varchar(20), @doctorlname varchar(15),@doctorid int,@visitdate varchar(20) ,@apttime int)
as
begin
SET DATEFORMAT DMY
Insert into Appoitment values(@patientid,@doctorfname,@doctorlname,@doctorid,@visitdate,@apttime)
end


---select * from Appoitment where PatientId=1 and visitdate='06/01/2022'


create proc ext_lname(@dcid int)
as
begin 
select lastname from Doctors where DoctorId=@dcid
end

--exec ext_lname 103


---delete from Appoitment where doctorid=103