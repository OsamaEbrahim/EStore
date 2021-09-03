# Project Objective:
The objective of this project is to create a set of reuseable services to be used in future projects.
# Project Functionalities:
There are two types of users in this project:
- Admins.
- Customers.

Each type of users can perform a set of functions:
- Common Functionalities:
  - View list of categories and products.
  - View product details.
- Admin:
  - Can manage list of categories ( add, edit, delete).
  - Can manage list of products ( add, edit, delete).
  - Can update the status of customers orders.
  - Can view analytics of products sales and revenue.
- Customer:
  - Can add products to cart.
  - Can update the quantity of products in cart. 
  - Can remove products from the cart.
  - Can checkout.
  - Can view a list of previous orders.

Note:
Unauthenticated users can view categories and products but can't add items to cart unless they sign up first.

# Sample Screens:
![Dashboard](https://user-images.githubusercontent.com/73915466/132071027-96053198-29d3-4961-821f-1827b1465b0e.PNG)
![CategoriesManagement](https://user-images.githubusercontent.com/73915466/132071047-2468d9d8-d6dd-40b4-ae07-98c76035b362.PNG)
![ProductsManagement](https://user-images.githubusercontent.com/73915466/132071104-f27d3321-758e-43b5-9a8b-93ab4d1b70f1.PNG)
![ManageOrders](https://user-images.githubusercontent.com/73915466/132071110-2b559765-1da6-47b3-aedd-aea5a12082ff.PNG)
![CreateCategory](https://user-images.githubusercontent.com/73915466/132071124-2cde5140-cbda-4a76-be34-135f46acd303.PNG)
![CreateProduct](https://user-images.githubusercontent.com/73915466/132071127-31593db8-f815-4f0b-9cfb-89e36e4c48e1.PNG)
![Home](https://user-images.githubusercontent.com/73915466/132071147-c57d752c-d467-486f-8f66-47478db74034.PNG)
![Categories](https://user-images.githubusercontent.com/73915466/132071152-354357f5-5ccf-4b6c-b43e-a6bc57b4f7a6.PNG)
![Products](https://user-images.githubusercontent.com/73915466/132071159-b6365005-7441-4f12-bd0d-512c7bb3ec3f.PNG)
![Cart](https://user-images.githubusercontent.com/73915466/132071170-fab65031-b9de-4d78-9095-cf58ed38551d.PNG)
![Checkout](https://user-images.githubusercontent.com/73915466/132071175-2fda8f1b-de59-42fe-a56c-9af1455d807c.PNG)
![MyOrders](https://user-images.githubusercontent.com/73915466/132071178-2884075a-e438-437e-8af9-1e00cc232b49.PNG)



# To run the project you need to:

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
