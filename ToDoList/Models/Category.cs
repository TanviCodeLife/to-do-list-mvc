using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace ToDoList.Models
{
  public class Category
  {
    private string _name;
    private int _id;
    private List<Item> _items;

    public Category(string categoryName, int id = 0)
    {
      _name = categoryName;
      _id = id;
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
        Category newCategory = new Category(CategoryName, CategoryId);
        allCategories.Add(newCategory);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCategories;

      //To fail Get All Empty List method use this code
      // Category dummyCategory = new Category("dummy category");
      // List<Category> allCategories = new List<Category> { dummyCategory };
      // return allCategories;

      //Get All Returns Categories will fail until Save is running on objects in the test method.
      //Add object.Save() to the GetAll_ReturnsAllCategoryObjects test after Save method code is added to make it pass.
    }


    public static Category Find(int id)
    {

      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM categories WHERE id = (@searchId);";
      // MySqlParameter idParameter = new MySqlParameter();
      // idParameter.ParameterName = "@searchId";
      // idParameter.ParameterValue = id;
      cmd.Parameters.AddWithValue("@searchId", id);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      int CategoryId = 0;
      string CategoryName = "";

      while(rdr.Read())
      {
        CategoryId = rdr.GetInt32(0);
        CategoryName = rdr.GetString(1);
      }

      Category newCategory = new Category(CategoryName, CategoryId);
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }

      return newCategory;

      //To fail Find add the following code and write test
      //Category dummyCategory = new Category("dummy category");
      //return dummyCategory;
    }


    public List<Item> GetItems()
    {
      List<Item> allCategoryItems = new List<Item> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM items WHERE category_id = @category_id;";
      MySqlParameter categoryId = new MySqlParameter();
      // categoryId.ParameterName = "@categoryId";
      // categoryName.ParameterValue = this._id;
      // cmd.Parameters.Add(categoryId);
      cmd.Parameters.AddWithValue("@category_id", this._id);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemDescription = rdr.GetString(1);
        int itemCategoryId = rdr.GetInt32(2);
        Item newItem = new Item(itemDescription, itemCategoryId, itemId);
        allCategoryItems.Add(newItem);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCategoryItems;
      //Add following code to return dummy list to fail test method GetItems_RetrievesAllItemsWithCategory
      //List<Item> allCategoryItems = new List<Item> {};
      //return allCategoryItems;
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
        bool idEquality = this.GetId().Equals(newCategory.GetId());
        return (idEquality && nameEquality);
        //fail the Equals test by not adding the Equals method
      }
    }

    public void Save()
    {

      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO categories (name) VALUES (@name);";
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId; //fetch database-assigned primary key

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      //To fail the SavesCategoryToDatabase don't fill in the save.
    }


  }
}
