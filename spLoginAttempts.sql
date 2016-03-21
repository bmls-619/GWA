CREATE PROCEDURE spLoginAttempts

@Username nvarchar(200),
@Password nvarchar(200)

as

		begin

			declare @BlockAccount bit
			declare @Count int
			declare @LoginAttempt int
			
			select @BlockAccount = IsLocked
			from dbo.Users
			where Username = @Username
			
			if(@BlockAccount = 1)
			
		begin
			  
			   select 1 as BlockAccount, 0 as authenticated, 0 as LoginAttempt
			  
		end
			
			else
			 
			   select @Count = count(Username) from dbo.Users
			   where Username = @Username and Password = @Password
			   
		   if(@Count = 1)
		   
		begin
		     
			   update dbo.Users set LoginAttempt = 0
			   where Username = @Username
		      
			  select 0 as BlockAccount, 1 as authenticated, 0 as LoginAttempt
		      
		end
		   
		   else
		   
		begin
		   
			  select @LoginAttempt = isNull(LoginAttempt,0)
			  from dbo.Users
			  where Username = @Username
		      
			  set @LoginAttempt = @LoginAttempt + 1
		      
			  if(@LoginAttempt <= 3)
		      
		begin
		      
			   update dbo.Users
			   set LoginAttempt =  @LoginAttempt
			   where Username = @Username 
		          
			   select 0 as BlockAccount, 0 as authenticated, @LoginAttempt as LoginAttempt
		      
		end
		      
			  else
		      
		begin
				Update dbo.Users set LoginAttempt = @LoginAttempt,
				IsLocked = 1, LockedDate = GETDATE()
				where Username = @Username
				
				select 1 as BlockAccount, 0 as authenticated, 0 as LoginAttempt
		 
		end

    end
  end