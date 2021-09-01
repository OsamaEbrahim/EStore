# EStore
ASP.NET Core - Estore Application 

To run the project you need to:

1-Create a database in sql server.

2-Update the connection string in the appsettings.json file.

3-Install the required packages(Right-Click on Dependencies/Packages and click update).

4-Run the Add-Migration "CreateSchema" command.

5-Run Update-Database command.

Notes:

-Some dummy data will be seeded to the database after your first update.

-An admin account will be seeded to the database with the following credentials: UserName: admin@admin.com, Password: Admin_123.

-If you want to use the email sender service add a real email and the password to that email in the appsettings.json file (the smtp client is using gmail configuration), else comment the code that uses the email sender in the Orders controller (line# 204) and Cart controller(line# 192). 


![EmailProvidersPNG](https://user-images.githubusercontent.com/73915466/131718035-32d6dbb8-10f0-481e-9b45-1a13c7f4a9b4.PNG)
