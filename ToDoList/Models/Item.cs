using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace ToDoList.Models
{
  public class Item
  {
    private string _description;
    private int _id;

    public Item (string description, int id = 0)
    {
      _description = description;
      _id = id;
    }

    public string GetDescription()
    {
      return _description;
    }

    public void SetDescription(string newDescription)
    {
      _description = newDescription;
    }

    public override bool Equals(System.Object otherItem)
    {
      if (!(otherItem is Item))
      {
        return false;
      }
      else
      {
        Item newItem = (Item) otherItem;
        bool idEquality = (this.GetId() == newItem.GetId());
        bool descriptionEquality = (this.GetDescription() == newItem.GetDescription());
        return (idEquality && descriptionEquality);
        //fail the Equals test by not adding the Equals method
      }
    }

    public override int GetHashCode()
    {
      return this.GetDescription().GetHashCode();
    } // add this method to get rid of the hashcode warning



    public static List<Item> GetAll()
    {
      List<Item> allItems = new List<Item> { };
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM items;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int itemId = rdr.GetInt32(1);
        string itemDescription = rdr.GetString(0);
        // Line below now only provides one argument!
        Item newItem = new Item(itemDescription, itemId);
        allItems.Add(newItem);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return allItems;

      //To fail Get All Empty List method use this code
      // Item dummyItem = new Item("dummy item");
      // List<Item> allItems = new List<Item> { dummyItem };
      // return allItems;

      //Get All Returns Items will fail until Save is running on objects.
      //Add object.Save() after Save method code is added to make it pass.
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO items (description) VALUES (@ItemDescription);";
      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@ItemDescription";
      description.Value = this._description;
      cmd.Parameters.Add(description);
      cmd.ExecuteNonQuery();    // This line is new!
      _id = (int) cmd.LastInsertedId;
      // One more line of logic will go here in the next lesson.

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      //To fail Save method - declare method and keep it empty
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM items;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }


    public static Item Find(int id)
    {

      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `items` WHERE id = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      int itemId = 0;
      string itemDescription = "";
      while (rdr.Read())
      {
        itemId = rdr.GetInt32(1);
        itemDescription = rdr.GetString(0);
      }
       Item foundItem = new Item(itemDescription, itemId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundItem;

      //To fail Find use below code:
      //Item dummyItem = new Item("dummy item");
      //return dummyItem;
    }

    public int GetId()
    {
      // Temporarily returning dummy id to get beyond compiler errors, until we refactor to work with database.
      return _id;
      //To fail GetId - add return 0; and comment out the private id property and id prperty in the item constructor.
    }
  }
}
