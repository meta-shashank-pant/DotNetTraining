
begin
	--print 'Hello World';
	
	--declare @var int;
	--set @var = 100;

	--declare @var1 int = 200;
	
	declare @Name varchar(20);
	declare @Marks int;
	declare @Grade char(1);
	select @Name = name, @Marks = Marks 
	from student 
	where id = 101;

	/*if @Marks >= 85
	begin
		set @Grade = 'A'
	end
	else if @Marks >= 75
	begin
		set @Grade = 'B'
	end 
	else if @Marks >= 65
	begin
		set @Grade = 'C'
	end
	else
	begin
		set @Grade = 'D'
	end*/

	print @Name + ' -- ' + @Grade; 
end

--ERROR HANDLING

begin try
	select 1/0;
end try
begin catch
	select
		@@ERROR as Error, 
		ERROR_NUMBER() as ErrorNumber, 
		ERROR_LINE() as ErrorLine, 
		ERROR_PROCEDURE() as ErrorProcedure, 
		ERROR_SEVERITY() as ErrorSeverity, 
		ERROR_MESSAGE() as ErrorMessage;
end catch

/**
	TRANSACTIONS:
	1. Is a set of T-SQL statements which are executed together as a unit.
	2. COMMIT statement is used to save the changes.
	3. ROLLBACK is used to undo the changes before saving.
	4. SAVEPOINT splits the complete transaction in smaller parts for ROLLBACK
	5. @@TRANCOUNT counts the number of transaction.
*/

begin transaction
	save transaction s1
	update student set Marks = 100 where id = 104;

	save transaction s2
	update student set Marks = 50 where id = 103;

	print @@TRANCOUNT;

	commit

	print @@TRANCOUNT;


/**
	CURSORS
	1. Used to retrieve data from result set one row at a time.
	2. Is used when we need to update records in a database table row by row.
	3. @@FETCH_STATUS returns the status of last cursor fetch.
	4. Stages of cursor:
		- Declare Cursor
		- Open
		- Fetch
		- Close
		- Deallocate
*/

begin
	declare @id int;
	declare @name varchar(15);
	declare @marks int;
	declare cur cursor for
	select id, name, Marks from student;
	open cur;
	fetch next from cur into @id, @name, @marks;

	while @@FETCH_STATUS = 0
	begin
		if @marks >= 95
		begin
			set @marks = @marks - 5;
			update student set Marks = @marks where id = @id;
		end
		print @name + ' ' + cast(@marks as varchar);
		fetch next from cur into @id, @name, @marks;
	end
	close cur;
	deallocate cur;
end

/**
	STORED PROCEDURE
	1. Is a database object which contains set of logical T-SQL statement.
	2. Increase the performance of database.
	3. Compilation takes place during the creation of procedure.
	4. Provide reuseability
	5. Access can be controlled
	6. Is invoked whenever required usinf EXEC or EXECUTE keywords.
*/

create procedure SetStudentMarks as
begin
	declare @id int;
	declare @name varchar(15);
	declare @marks int;
	declare cur cursor for
	select id, name, Marks from student;
	open cur;
	fetch next from cur into @id, @name, @marks;

	while @@FETCH_STATUS = 0
	begin
		if @marks >= 95
		begin
			set @marks = @marks - 5;
			update student set Marks = @marks where id = @id;
		end
		print @name + ' ' + cast(@marks as varchar);
		fetch next from cur into @id, @name, @marks;
	end
	close cur;
	deallocate cur;
end

exec SetStudentMarks;


alter procedure SetStudentMarks @NewMarks as int, @StudentId as int, @Remark as char(1) output  as
begin
	declare @id int;
	declare @name varchar(15);
	declare @marks int;
	declare cur cursor for
	select id, name, Marks from student;
	open cur;
	fetch next from cur into @id, @name, @marks;

	while @@FETCH_STATUS = 0
	begin
		if @id = @StudentId
		begin			
			set @marks = @NewMarks;
			update student set Marks = @marks where id = @id;
		end
		if @marks >= 90 and @id = @StudentId
		begin
			set @Remark = 'A';
		end
		else
		begin
			set @Remark = 'B';
		end
		print @name + ' ' + cast(@marks as varchar);
		fetch next from cur into @id, @name, @marks;
	end
	close cur;
	deallocate cur;
end

begin
	declare @Remark char(1);
	exec SetStudentMarks 75,102,@Remark output;
	print @Remark;
end


/**
	FUNCTIONS
	1. Is a database object that contains set of logical statements which must return a value
	2. Can only have input parameters
	3. Can be used in select, where, having clause
	4. Can't use exception handling
*/

alter function StudentPercentage(@marks as int) returns numeric(5,2)
begin
	return @marks / 1.05;
end

select id, name, Marks, dbo.StudentPercentage(Marks) as [Percentage out of 105] from student;

/**
	TRIGGERS
	1. Is a special kind of procedure designed for a particular event.
	2. On that event it is executed implicitly.
	3. Can be planned for DDL, DML or Login events

	Classification
	1. After Trigger
	2. Instead of trigger

	Magic Table
	1. Inserted
	2. Deleted
*/

create trigger SetMarks on student for update
as
begin
	declare @oldMark int;
	declare @newMark int;
	select @oldMark = Marks from deleted;
	select @newMark = Marks from inserted;

	if(@newMark > 100 or @newMark < 0 or @newMark = @oldMark)
	begin
		print 'Marks should between 1 and 100 and should differ old value.';
		rollback;
	end
end

update student set Marks = -2 where id = 101

-- For views we have special statement "instead of" example:
--View
create view GetInfo as
select id, name, Marks / 1.05 as FinalPercentage from student;

--Trigger
create trigger ViewTrigger on student
instead of insert as
begin

end