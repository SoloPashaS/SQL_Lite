using System;
using Microsoft.Data.Sqlite;

struct Order
{
    private String table_name;
    private String data;

    public Order(String table_name, String data = "*")
    {
        this.data = data;
        this.table_name = table_name;
    }

    public String TABLE_NAME
    {
        get { return table_name; }
        set { table_name = value; }
    }
    public String DATA
    {
        get { return data; }
        set { data = value; }
    }
}


class Writer
{
    public int size { get; set; }

    protected SqliteConnection DB;
    public Writer(String DB_Name, int SIZE = 25)
    {
        DB = new SqliteConnection("Data Source = " + DB_Name);
        this.size = SIZE;
        DB.Open();
    }

    public void SetDB(String DB_Name)
    {
        DB.Close();
        DB = new SqliteConnection("Data Source = " + DB_Name);
        DB.Open();
    }

    public SqliteConnection GetDB()
    {
        return DB;
    }


    protected SqliteDataReader Get_SQL(String table_name, String data = "*")
    {
        SqliteCommand CMD = DB.CreateCommand();
        CMD.CommandText = "select " + data + " from " + table_name;
        return CMD.ExecuteReader();
    }
    protected SqliteDataReader Get_SQL(Order order)
    {
        SqliteCommand CMD = DB.CreateCommand();
        CMD.CommandText = "select " + order.DATA + " from " + order.TABLE_NAME;
        return CMD.ExecuteReader();
    }

    protected void print_title(String table_name, String data = "*")
    {
        var SQL = Get_SQL(table_name, data);

        Console.WriteLine(new String('=', (size + 1) * SQL.FieldCount + 1));
        Console.Write("|");
        for (var f = 0; f < SQL.FieldCount; f++)
        {
            Console.Write("{0, " + size +"}|", "|" + Convert.ToString(SQL.GetName(f)) + "|" + ": " + Convert.ToString(SQL.GetFieldType(f)));
        }
        Console.WriteLine();
        Console.WriteLine(new String('=', (size + 1) * SQL.FieldCount + 1));
    }
    protected void print_title(Order order)
    {
        var SQL = Get_SQL(order.TABLE_NAME, order.DATA);

        Console.WriteLine(new String('=', (size + 1) * SQL.FieldCount + 1));
        Console.Write("|");
        for (var f = 0; f < SQL.FieldCount; f++)
        {
            Console.Write("{0, " + size + "}|", "|" + Convert.ToString(SQL.GetName(f)) + "|" + ": " + Convert.ToString(SQL.GetFieldType(f)));
        }
        Console.WriteLine();
        Console.WriteLine(new String('=', (size + 1) * SQL.FieldCount + 1));
    }
    protected void print_title(SqliteDataReader SQL)
    {
        Console.WriteLine(new String('=', (size + 1) * SQL.FieldCount + 1));
        Console.Write("|");
        for (var f = 0; f < SQL.FieldCount; f++)
        {
            Console.Write("{0, " + size + "}|", "|" + Convert.ToString(SQL.GetName(f)) + "|" + ": " + Convert.ToString(SQL.GetFieldType(f)));
        }
        Console.WriteLine();
        Console.WriteLine(new String('=', (size + 1) * SQL.FieldCount + 1));
    }



    public void print_data(String table_name, String data ="*" )
    {
        SqliteDataReader SQL = Get_SQL(table_name, data);

        if (SQL.HasRows)
        {
            print_title(table_name, data);

            while (SQL.Read())
            {
                Console.Write("|");
                for (var f = 0; f < SQL.FieldCount; f++)
                {
                    Console.Write("{0," + size + "}|", SQL[f]);
                }
                Console.WriteLine();
                Console.WriteLine(new String('=', (size + 1) * SQL.FieldCount + 1));
            }
        }
    }
    public void print_data(Order order)
    {
        SqliteDataReader SQL = Get_SQL(order.TABLE_NAME, order.DATA);

        if (SQL.HasRows)
        {
            print_title(order.TABLE_NAME, order.DATA);

            while (SQL.Read())
            {
                Console.Write("|");
                for (var f = 0; f < SQL.FieldCount; f++)
                {
                    Console.Write("{0," + size + "}|", SQL[f]);
                }
                Console.WriteLine();
                Console.WriteLine(new String('=', (size + 1) * SQL.FieldCount + 1));
            }
        }
    }
    public void print_data(SqliteDataReader SQL)
    {
        if (SQL.HasRows)
        {
            print_title(SQL);

            while (SQL.Read())
            {
                Console.Write("|");
                for (var f = 0; f < SQL.FieldCount; f++)
                {
                    Console.Write("{0," + size + "}|", SQL[f]);
                }
                Console.WriteLine();
                Console.WriteLine(new String('=', (size + 1) * SQL.FieldCount + 1));
            }
        }
    }

    public SqliteDataReader find(string field_name, string target, String table_name, String data = "*")
    {
        SqliteCommand CMD = DB.CreateCommand();
        CMD.CommandText = "select " + data + " from " + table_name + " where " + field_name + " like " + target;
        return CMD.ExecuteReader();
    }
    public SqliteDataReader find(string field_name, string target, Order order)
    {
        SqliteCommand CMD = DB.CreateCommand();
        CMD.CommandText = "select " + order.DATA + " from " + order.TABLE_NAME + " where " + field_name + " like " + target;
        return CMD.ExecuteReader();
    }


    ~Writer() {
        DB.Close();
    }
}


namespace hi
{


    class Program
    {
        static void Main(string[] args)
        {

            Writer HELLO = new Writer("NeT.db");
            HELLO.print_data("yyy");
            Console.WriteLine("Hello World!");


            Order O = new Order("yyy");
            Writer HELL = new Writer("NeT.db");
            //HELLO.print_data(O);
            HELL.print_data(HELL.find("F1", "2", "yyy"));
            Console.WriteLine("Hello World!");
        }
    }
}

