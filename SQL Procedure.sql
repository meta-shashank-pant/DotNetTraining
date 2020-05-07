
alter procedure AddEmployeeToDB @fullName varchar(30), @gender varchar(10), @emailId varchar(50), @empPassword varchar(20)
	, @contactNumber varchar(20), @organisation varchar(30), @empId int 
	as
	begin
		insert into employee values (@fullName, @gender, @emailId, @empPassword, @contactNumber, @organisation, @empId)
	end


create procedure AddVehicleToDB @empId int, @vehicleName varchar(30), @vehicleNumber varchar(20), @vehicleType varchar(20), @vehicleId int
as
begin
	insert into vehicle values (@empId, @vehicleName, @vehicleNumber, @vehicleType, @vehicleId);
end

create procedure AddPassToDB @vehicleId int, @vehicleType varchar(20), @planType varchar(20), @amount int
as
begin
	insert into pass values(@vehicleId, @vehicleType, @planType, @amount)
end


alter procedure DeleteEmployeeFromDB @empId int
as
begin
	declare @vehicleId int;
	select @vehicleId = vehicleId from vehicle where empId = @empId;
	delete from employee where empId = @empId;
	delete from vehicle where empId = @empId;
	delete from pass where vehicleId = @vehicleId;

end

create procedure DisplayDataFromDB as
begin
	select e.empId, e.fullName, e.gender, e.emailId, e.contactNumber,
				e.organisation, v.vehicleName, v.vehicleNumber, v.vehicleType, p.planType, p.amount 
				from employee as e join vehicle as v on e.empId = v.empId 
				join pass as p on p.vehicleId = v.vehicleId
end

create procedure GetVehicleId as
begin
	select vehicleId from vehicle where vehicleId = (select max(vehicleId) from vehicle);
end

create procedure GetEmployee @empId int as
begin
	select * from employee where empId = @empId;
end

create procedure GetVehicle @empId int as
begin
	select * from vehicle where empId = @empId;
end

create procedure GetPass @vehicleId int as
begin
	select * from pass where vehicleId = @vehicleId;
end
