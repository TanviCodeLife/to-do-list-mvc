using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace ToDoList.Models
{
  public class Category
  {
    private static List<Category> _instances = new List<Category> {};
    private string _name;
    private int _id;
    private List<Item> _items;

    public Category(string categoryName)
    {
      _name = categoryName;
      _instances.Add(this);
      _id = _instances.Count;
      _items = new List<Item>{};
    }

    public string GetName()
    {
      return _name;
    }

    public int GetId()
    {
      return _id;
    }

    public void AddItem(Item item)
    {
      _items.Add(item);
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM categories;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

    }

    public static List<Category> GetAll()
    {
      List<Category> allCategories = new List<Category> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM categories;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int CategoryId = rdr.GetInt32(0);
        string CategoryName = rdr.GetString(1);
        Category newCategory = new Category(CategoryName);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCategories;
      //To fail Get All Empty List method use this code
      // Category dummyCategory = new Category("dummy category");
      // List<Category> allCategorys = new List<Category> { dummyCategory };
      // return allCategorys;
    }


    public static Category Find(int searchId)
    {
      return _instances[searchId-1];
    }

    public List<Item> GetItems()
    {
      return _items;
    }

    public override bool Equals(System.Object otherCategory)
    {
      if(!(otherCategory is Category))
      {
        return false;
      }
      else
      {
        Category newCategory = (Category) otherCategory;
        bool nameEquality = this.GetName().Equals(newCategory.GetName());
        return nameEquality;
        //fail the Equals test by not adding the Equals method
      }
    }

    public void Save()
    {
      //To fail the SavesCategoryToDatabase don't fill in the save.
      MySqlConnection conn = DB.Connection();
   conn.Open();
   var cmd = conn.CreateCommand() as MySqlCommand;
   cmd.CommandText = @"INSERT INTO categories (name) VALUES (@name);";
   MySqlParameter name = new MySqlParameter();
   name.ParameterName = "@name";
   name.Value = this._name;
   cmd.Parameters.Add(name);
   cmd.ExecuteNonQuery();
   conn.Close();
   if (conn != null)
   {
     conn.Dispose();
   }
 }


  }
}
