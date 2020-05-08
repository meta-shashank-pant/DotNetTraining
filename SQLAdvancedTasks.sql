-- Task 1 (Function and Procedure) and Task 4 (Cursor)
	-- 5% Discount by the Jaipur sellers
	alter proc DiscountOnSelectedItems as
	begin

		declare @amount int;
		declare @id int;

		declare cur cursor for
			select p.id, p.Price from 
			Product as p join Buys as b on p.Id = b.ProdId 
			join Seller as s on s.Id = b.SellerId 
			where s.CityId = (select Id from City where CityName = 'Jaipur');

		-- Open cursor
		open cur;

		-- Fetch the record from cursor
		fetch next from cur into @id, @amount;

		while @@FETCH_STATUS = 0
		begin
			set @amount = @amount * 0.95;
			update Product set Price = @amount where Id = @id;
			fetch next from cur into @id, @amount;
		end

		-- Close curser
		close cur;

		-- Finally, deallocate cursor
		deallocate cur;
	end

	select * from Product;
	exec DiscountOnSelectedItems;

	-- Fuction
	alter function MaximumDiscount(@price as int) returns numeric(10,2)
	begin
		return @price * 0.95;
	end

	select id, ProdName,dbo.MaximumDiscount(Price) BestPrice from Product

-- Task 2 (Temp Tables)
	/*
		Temporary tables are tables that exist temporarily on the SQL Server.
		The temporary tables are useful for storing the immediate result sets that are accessed multiple times.
		SQL Server provided two ways to create temporary tables via SELECT INTO and CREATE TABLE statements.
		The name of the temporary table starts with a hash symbol (#).
		Once you execute the statement, you can find the temporary table name created in the system database named tempdb, 
		which can be accessed via the SQL Server Management Studio using the following path System Databases > tempdb > Temporary Tables.
		The temporary table also consists of a sequence of numbers as a postfix.
		This is a unique identifier for the temporary table. Because multiple database connections can create 
		temporary tables with the same name, SQL Server automatically appends this unique number at the end 
		of the temporary table name to differentiate between the temporary tables.
	*/

	-- Creation using select operator.
	select Id, CustName, ContactNumber into #trek_customer from Customer;

	select * from #trek_customer;

	-- Creation using create operator.
	create table #trek_customer2(id int, custNumber varchar(20), contact varchar(12));

	insert into #trek_customer2
		select Id, CustName, ContactNumber from Customer;

	-- GLOBAL TEMP TABLE
	/*
		When you want table to be accessible across connections.In this case, you can use global temporary tables.
		Unlike a temporary table, the name of a global temporary table starts with a double hash symbol (##).
		Both types of temporary tables get dropped automatically
	*/
		
		select Id, CustName, ContactNumber into #global_customer from Customer;

-- Task 3 ( CTE )
	/*
		CTE stands for common table expression.
		A CTE is atemporary result set, that can be referenced within the seletc, insert, update or delete statement,
		that immediately follows the CTE.
		If there is any select, update, insert, delete statement before the statement that use CTE, then there will be an error.
		We can also create multiple CTE by separating with ',' and using WITH only once.
	*/

	with CustomerJoinBuys(CustomerName, ProductId)
	as
	(
		select c.CustName as CustomerName, b.ProdId as ProductId
		from Customer as c join Buys as b 
		on c.Id = b.CustId
	)
	select cb.CustomerName, p.ProdName 
	from CustomerJoinBuys as cb join Product as p 
	on cb.ProductId = p.Id

-- Task 5 (Ranking Function)
	/*
		Rank returns a rank starting at 1 based on the ordering of rows imposed by the order by clause.
		ORDER BY clause is required.
		PARTITION BY clause is optional.
		When data is partioned rank is reseted to 1.
		There are RANK and DENSE_RANK function, the common difference between them is RANK function skips ranking
		if there is a tie where as DENSE_RANK will not. 
		Use:
		Both of these functions can be used to find the n th rank of item, how ever usage depends upon the requirement

	*/

	Select Id, ProdName, Price, Rank() over (order by Price desc) as [Rank],
	DENSE_RANK() over (order by Price desc) as DenseRank
	from Product

	Select Id, ProdName, Category, Price, Rank() over (partition by Category order by Price desc) as [Rank],
	DENSE_RANK() over (partition by Category order by Price desc) as DenseRank
	from Product

-- Task 7 (Aggregate Functions)
	/*
		Aggregate Function are max, min, count, avg, sum. These function can directly be applied to the table
		or it can be applied with the GROUP BY clause.
	*/										     
	
	select max(Price) as [Max Price] from Product;

	select min(Price) as [Min Price] from Product;

	select avg(Price) as [Avg Price] from Product;

	select sum(Price) as [Total Price] from Product;

	select count(Price) as [Count] from Product;
