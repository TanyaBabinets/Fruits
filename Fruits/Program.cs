using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Создайте приложение, которую позволит пользователю подключиться и отключиться от базы данных «Овощи
//и фрукты». В случае успешного подключения выводите
//информационное сообщение. Если подключение было
//неуспешным, сообщите об ошибке. 
//Задание 3
//Добавьте к приложению из второго задания следующую функциональность:
//■ Отображение всей информации из таблицы с овощами и фруктами;
//■ Отображение всех названий овощей и фруктов;
//■ Отображение всех цветов;
//■ Показать максимальную калорийность;
//■ Показать минимальную калорийность;
//■ Показать среднюю калорийность.
//Задание 4
//Добавьте к приложению из второго задания следующую функциональность:
//■ Показать количество овощей;
//■ Показать количество фруктов;
//■ Показать количество овощей и фруктов заданного //цвета;
//■ Показать количество овощей фруктов каждого цвета;
//■ Показать овощи и фрукты с калорийностью ниже //указанной;
//■ Показать овощи и фрукты с калорийностью выше //указанной;
//■ Показать овощи и фрукты с калорийностью в указанном диапазоне;
//■ Показать все овощи и фрукты, у которых цвет желтый или красный.
namespace Fruits
{
    internal class Program
    {
        static SqlConnection connection;
        static void Main(string[] args)
        {
            connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=TANYA06\SQLEXPRESS;
                                           Initial Catalog=Fruits;
                                           Integrated Security=true;";

            try
            {
                
                connection.Open();
                Console.WriteLine("Connection openned");

                GetAllFruits(connection);
                Console.WriteLine();
                GetName(connection);
                Console.WriteLine();
                GetColor(connection);
                Console.WriteLine();
                MaxC(connection);

                Console.WriteLine();
                CountF(connection);
                Console.WriteLine();
                Count1Color(connection);
                Console.WriteLine();

                CountEachColor(connection);
                Console.WriteLine();
                GetRed(connection);
                Console.WriteLine();
                GetCallory1(connection);
                Console.WriteLine();
                GetCallory2(connection);
                Console.WriteLine();
                GetCallory3(connection);

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Connectin Error " + ex.Message);
            }
            finally
            {
                //закрываем соединение
                connection.Close();
                Console.WriteLine("Connection closed");
            }
            
        }
        static void GetAllFruits(SqlConnection connection)
        {
            
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT Name, Type, Color, Callory from Fruktiki";

            SqlDataReader reader = command.ExecuteReader();     
            while (reader.Read())
            {
                Console.WriteLine(
                    $"Info: {reader["Name"]} {reader["Type"]} {reader["Color"]} {reader["Callory"]}");
            }
           
            reader.Close();
        }
        static void GetColor(SqlConnection connection)
        {
           
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT Name, Color from Fruktiki";
            
            SqlDataReader reader = command.ExecuteReader();          
            while (reader.Read())
            {
                Console.WriteLine(
                    $"Color: {reader["Name"]}  {reader["Color"]} ");

            }          
            reader.Close();
        }
        static void GetName(SqlConnection connection)
        {
            
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT Name, Color from Fruktiki";

            
            SqlDataReader reader = command.ExecuteReader();

            
            while (reader.Read())
            {
                Console.WriteLine(
                    $"Product list: {reader["Name"]}   ");

            }
           
            reader.Close();
        }
        static void MaxC(SqlConnection connection)
        {
            string sqlExpression = "SELECT Max(Callory) FROM Fruktiki";

            SqlCommand command = new SqlCommand(sqlExpression, connection);
            object maxC = command.ExecuteScalar();

            command.CommandText = "SELECT Min(Callory) FROM Fruktiki";
            object minC = command.ExecuteScalar();
            command.CommandText = "SELECT AVG(Callory) FROM Fruktiki";
            object avgC = command.ExecuteScalar();
        
            Console.WriteLine("Max callories: {0}", maxC);
            Console.WriteLine("Min callories: {0}", minC);
            Console.WriteLine("Average callories: {0}", avgC);

        }
        static void CountF(SqlConnection connection)
        {
            string sqlExpression = "SELECT COUNT(type) FROM Fruktiki where type='fruit'";

            SqlCommand command = new SqlCommand(sqlExpression, connection);
            object count = command.ExecuteScalar();
            command.CommandText = "SELECT COUNT(type) FROM Fruktiki where type='veg'";
            object count1 = command.ExecuteScalar();

            Console.WriteLine("we have {0} fruits", count);
            Console.WriteLine("we have {0} vegetables", count1);

        }

        static void Count1Color(SqlConnection connection)
        {
            string sqlExpression = "SELECT COUNT(color) FROM Fruktiki where color='white'";

            SqlCommand command = new SqlCommand(sqlExpression, connection);
            object count = command.ExecuteScalar();
           
            Console.WriteLine("we have {0} products of white color", count);


        }
        
        
        
        //■ Показать количество овощей фруктов каждого цвета;
        static void CountEachColor(SqlConnection connection)
        {

            
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT  Color, COUNT(*) FROM Fruktiki GROUP BY  Color";

          
          SqlDataReader reader = command.ExecuteReader();
           
            while (reader.Read())
                  {
                
            Console.Write($"{reader["Color"]} \t {reader[1]} \n" );
            
                
            }
            reader.Close();

        }

    

    //■ Показать все овощи и фрукты, у которых цвет желтый или красный.
    static void GetRed(SqlConnection connection)
    {
        //создаем команду для выборки данных
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT Name, Color from Fruktiki where Color='yellow' OR Color= 'red'";

        //выполняем команду
        SqlDataReader reader = command.ExecuteReader();

        //считываем результат
        while (reader.Read())
        {
            Console.Write(reader["Name"]);
            Console.Write(" ");
            Console.WriteLine(reader["Color"]);
        }
        //освобождаем память 
        reader.Close();
    }


    //■ Показать овощи и фрукты с калорийностью ниже //указанной;
    //■ Показать овощи и фрукты с калорийностью выше //указанной;
    //■ Показать овощи и фрукты с калорийностью в указанном диапазоне;
    static void GetCallory1(SqlConnection connection)
    {
        
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT Name, Callory from Fruktiki where Callory > 120 ";



        
        SqlDataReader reader = command.ExecuteReader();
        
        Console.WriteLine("Callory > 120");

        while (reader.Read())
        {

            Console.Write(reader["Name"]);
            Console.Write(" ");
            Console.WriteLine(reader["Callory"]);
        }
        
        reader.Close();
    }
    static void GetCallory2(SqlConnection connection)
    {
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT Name, Callory from Fruktiki where Callory < 120 ";
        SqlDataReader reader = command.ExecuteReader();
       
        Console.WriteLine("Callory < 120");
        while (reader.Read())
        {

            Console.Write(reader["Name"]);
            Console.Write(" ");
            Console.WriteLine(reader["Callory"]);
        }
       
        reader.Close();
    }
    static void GetCallory3(SqlConnection connection)
    {
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT Name, Callory from Fruktiki where Callory BETWEEN 120 AND 150 ";
        SqlDataReader reader = command.ExecuteReader();
        
        Console.WriteLine("Callory between 120 and 150");
        while (reader.Read())
        {

            Console.Write(reader["Name"]);
            Console.Write(" ");
            Console.WriteLine(reader["Callory"]);
        }
        
        reader.Close();
    }

}

}

