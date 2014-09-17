using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace NorthwindConsole
{
    class Program
    {
        //This is your Data Context.  It is your gateway to your database.  
        static public NORTHWNDEntities dc = new NORTHWNDEntities();

        static void Main(string[] args)
        {
            //Take a look at these examples that use a live data source.
            //RunLambdaExamples();

            //Fill in these functions
            //RunWhereExpressions();

            //RunSelectExpressions();

            RunRelatedDataExpressions();

            RunAggregateFunctions();

            Console.ReadKey();
        }

        static void RunLambdaExamples()
        {
            //Lets get all products.  dc.Products means -> on our data context called "dc", let's get the entire table called Products
            var products = dc.Products;

            Console.WriteLine();
            Console.WriteLine("START of writing products sorted by name");
            Console.WriteLine();

            //create a loop to get all products that are not discontinued and order them by the product name
            foreach (var p in products.Where(x => !x.Discontinued).OrderBy(x => x.ProductName))
            {
                //write the product name and price to the console.
                //UnitPrice is can be null, so we have to use .Value to get the real value.
                //  We then use .ToString("C") to convert the decimal value to the currency format so it displays like $2.99 instead of 2.990000.
                Console.WriteLine(p.ProductName + " - " + p.UnitPrice.Value.ToString("C"));
            }

            Console.WriteLine();
            Console.WriteLine("END of writing products sorted by name");
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("START of writing products where not discontinued sorted by price then name");
            Console.WriteLine();

            //NOTICE ON THIS ONE WE ARE USING dc.Products TO SELECT OUR DATA INSTEAD OF JUST PRODUCTS.  YOU CAN DECLARE A VARIABLE TO 
            //HOLD YOUR DATA TABLE, OR JUST CALL THE DATACONTEXT.  

            //create a loop to get all products that are under $17, not discontinued.  order by price, then by product name
            foreach (var p in dc.Products.Where(x => !x.Discontinued && x.UnitPrice < 17).OrderBy(x => x.UnitPrice).ThenBy(x => x.ProductName))
            {
                //write the product name and price to the console.
                Console.WriteLine(p.ProductName + " - " + p.UnitPrice.Value.ToString("C"));
            }

            Console.WriteLine();
            Console.WriteLine("END of writing products where not discontinued sorted by price then name");
            Console.WriteLine();
        }

        static void RunSelectExpressions()
        {
            Console.WriteLine();
            Console.WriteLine("START of distinct countries sorted by name");
            Console.WriteLine();

            //this loop selects a single property from our Customer object, country, this would return a list of strings.
            //the .Distinct() function filters that list to unique values (countries).  This means that USA will only appear once 
            //even though there is more than one customer from the USA
            foreach (var country in dc.Customers.Select(x => x.Country).OrderBy(x => x))
            {
                //write the name of the country to the console.
                Console.WriteLine(country);
            }

            Console.WriteLine();
            Console.WriteLine("END of writing products sorted by name");
            Console.WriteLine();

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 35-37 

            Console.WriteLine();
            Console.WriteLine("START of writing products sorted by city backwards");
            Console.WriteLine();

            //create a loop to get all customers and select the unique cities that they live in order in reverse alphabetical order.  
            //print to console: <city>

            foreach (var c in dc.Customers.Select(x => x.City).OrderByDescending(x => x))
            {
                Console.WriteLine(c); 
            }

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 48-50

            Console.WriteLine();
            Console.WriteLine("END of writing products sorted by city backwards");
            Console.WriteLine();

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 35-37 

            Console.WriteLine();
            Console.WriteLine("START of writing products sorted by unique shipping postal code");
            Console.WriteLine();

            //create a loop to get all orders and select the unique shipping postal code.  
            //print to console: <shipPostalCode>

            foreach (var code in dc.Customers.Select(x => x.PostalCode).OrderBy(x => x.Length))
            {
                Console.WriteLine(code);
            }

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 48-50

            Console.WriteLine();
            Console.WriteLine("END of writing products sorted by unique shipping postal code");
            Console.WriteLine();

           
        }

        static void RunWhereExpressions()
        {

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 35-37 

            Console.WriteLine();
            Console.WriteLine("START of writing products over $50 sorted by price");
            Console.WriteLine();

            //create a loop to get all products that are over $50, not discontinuted.  order by price.  
            //print to console: <productName> - <unitPrice>

            foreach (var price in dc.Products.Where(x => !x.Discontinued).OrderBy(x => x.UnitPrice))
            {
                Console.WriteLine(price.ProductName + " - " + price.UnitPrice.Value.ToString("C"));
            }

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 48-50

            Console.WriteLine();
            Console.WriteLine("END of writing products over $50 sorted by price");
            Console.WriteLine();

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 35-37 

            Console.WriteLine();
            Console.WriteLine("START of writing employees who live in london, by last name");
            Console.WriteLine();
            
            //create a loop to get all employees that live in London, order by last name.  
            //print to console: <firstname> <lastname> lives in <city>

            foreach (var name in dc.Employees.Where(x => x.City == "London").OrderBy(x => x.LastName))
            {
                Console.WriteLine(name.FirstName + " " + name.LastName + " lives in " + name.City);
            }

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 48-50

            Console.WriteLine();
            Console.WriteLine("END of writing employees who live in london, by last name");
            Console.WriteLine();

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 35-37 

            Console.WriteLine();
            Console.WriteLine("START of writing employees whos title is not Sales Rep");
            Console.WriteLine();

            //create a loop to get all employees whose title is not Sales Representative, order by last name.  
            //print to console: <firstname> <lastname> is a <title>

            foreach (var employ in dc.Employees.Where(x => x.Title != "Sales Representative").OrderBy(x => x.LastName))
            {
                Console.WriteLine(employ.FirstName + " " + employ.LastName +  " is a " + employ.Title);
            }

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 48-50

            Console.WriteLine();
            Console.WriteLine("END of writing employees whos title is not Sales Rep");
            Console.WriteLine();

        }

        static void RunRelatedDataExpressions()
        {
            //Using Entity Framework (our database connection), we are also able to select related data as well.

            Console.WriteLine();
            Console.WriteLine("START of orderID and related CompanyName sorted by CompanyName");
            Console.WriteLine();

            // This loop selects orders, but are sorting them based on the customer's company name.
            // You might also notice the .Take(10), this simply limits the amount of data returned to 10.
            // If we wanted to turn 20 records, we would use .Take(20) instead.
            // x.Customer means, for this Order (x), get the associated Customer
            foreach (var o in dc.Orders.OrderBy(x => x.Customer.CompanyName).Take(10))
            {
                //print to console: <orderDate> - <companyName>
                Console.WriteLine(o.OrderDate + " - " + o.Customer.CompanyName);
            }

            Console.WriteLine();
            Console.WriteLine("END of orderID and related CompanyName sorted by CompanyName");
            Console.WriteLine();

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 35-37 

            Console.WriteLine();
            Console.WriteLine("START of employees title is Inside Sales Coordinator");
            Console.WriteLine();

            //create a loop to get 10 orders where the employee's title is Inside Sales Coordinator, order by the most recent OrderDate.  
            //print to console: <orderID> <orderDate> was handled by <employee firstName> <employee lastName>, an <title>

            foreach (var employeryyeryer in dc.Orders.Where(x => x.Employee.Title == "Inside Sales Coordinator").OrderByDescending(x => x.OrderDate).Take(10))
            {
                Console.WriteLine(employeryyeryer.OrderID + " " + employeryyeryer.OrderDate + " was handled by " + employeryyeryer.Employee.FirstName + " " + employeryyeryer.Employee.LastName + ", a " + employeryyeryer.Employee.Title);
            }

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 48-50

            Console.WriteLine();
            Console.WriteLine("END of employees title is Inside Sales Coordinator");
            Console.WriteLine();

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 35-37 

            Console.WriteLine();
            Console.WriteLine("START of get 5 orders where the order was handled by the employee Andrew Fuller order by the most recent OrderDate");
            Console.WriteLine();

            //create a loop to get 5 orders where the order was handled by the employee Andrew Fuller order by the most recent OrderDate.  
            //print to console: <orderID> <orderDate> was handled by <employee firstName> <employee lastName> <title>

            foreach (var order in dc.Orders.Where(x => x.Employee.FirstName == "Andrew" && x.Employee.LastName == "Fuller").OrderByDescending(x => x.OrderDate).Take(5))
            {
                Console.WriteLine(order.OrderID + " " + order.OrderDate + " was handled by " + order.Employee.FirstName + " " + order.Employee.LastName + " " + order.Employee.Title);
            }

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 48-50

            Console.WriteLine();
            Console.WriteLine("END of get 5 orders where the order was handled by the employee Andrew Fuller order by the most recent OrderDate");
            Console.WriteLine();

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 35-37 

            Console.WriteLine();
            Console.WriteLine("START 5 Suppliers and display the suppliers name and the number of products we stock");
            Console.WriteLine();

            //create a loop to get 5 Suppliers and display the suppliers name and the number of products we stock. order by companyName.  
            //print to console: <companyName> supplies us with <number of products> products

            foreach (var suppliers in dc.Products.Select(x => x.Supplier).OrderBy(x => x.CompanyName).Take(5))
            {
                Console.WriteLine(suppliers.CompanyName + " supplies us with " + suppliers.Products.Count + " products");
            }

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 48-50

            Console.WriteLine();
            Console.WriteLine("END 5 Suppliers and display the suppliers name and the number of products we stock");
            Console.WriteLine();

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 35-37 

            Console.WriteLine();
            Console.WriteLine("START get 5 employees and display the employees first and last name and the number of orders they have handled");
            Console.WriteLine();

            //create a loop to get 5 employees and display the employees first and last name and the number of orders they have handled. order by last name.  
            //print to console: <firstname> <lastname> has handled <number of orders> orders

            foreach (var employ in dc.Employees.Select(x => x.Orders.Count).OrderBy(x => x))
            {
                Console.WriteLine();
            }

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 48-50

            Console.WriteLine();
            Console.WriteLine("END get 5 employees and display the employees first and last name and the number of orders they have handled");
            Console.WriteLine();

        }

        static void RunAggregateFunctions()
        {
            //We can also perform math functions as well like .Sum()
            Console.WriteLine();
            Console.WriteLine("START of orderID and Total Cost of the order");
            Console.WriteLine();

            // This loop selects orders, but are sorting them based on the customer's company name.
            // Inside the loop where we write out the orderID, we then select all of the order_details records, 
            // and sum together the quantity of items purchases
            foreach (var o in dc.Orders.OrderBy(x => x.Customer.CompanyName).Take(10))
            {
                //print to console: <orderID> - <totalQuantityOrdered>
                Console.WriteLine(o.OrderID + " - " + o.Order_Details.Sum(x=> x.Quantity));
            }

            Console.WriteLine();
            Console.WriteLine("END of orderID and related CompanyName sorted by CompanyName");
            Console.WriteLine();

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 35-37 

            Console.WriteLine();
            Console.WriteLine("START of orderID and total price");
            Console.WriteLine();

            //create a loop to get 5 orders and display the order ID and the total price of the order (quantity times unit price). 
            //print to console: <orderID> was sold at <totalPrice>

            foreach (var order in dc.Orders.Take(5))
            {
                Console.WriteLine("Order " + order.OrderID + " was sold at " + order.Order_Details.Sum(x => x.Quantity * x.UnitPrice));
            }

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 48-50

            Console.WriteLine();
            Console.WriteLine("END of orderID and total price");
            Console.WriteLine();

            //You can even do nested lambda expressions as well (notice its not very fast, but they work in a pinch) 
            //Uncomment the lines below to check it out
            //foreach (var e in dc.Employees) { 
            //The nested lambda expressions are saying we want to sum the total of order detail's sum of unitPrice times quanity
            //    Console.WriteLine(e.FirstName + " " + e.LastName + " sold a total of " + e.Orders.Sum(y => y.Order_Details.Sum(z => z.UnitPrice * z.Quantity)).ToString("C")); 
            //}

        }

    }
}
