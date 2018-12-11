using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;
using System.Collections.Generic;
using System;

namespace ToDoList.Tests
{
  [TestClass]
  public class CategoryTest : IDisposable
  {

    public void Dispose()
    {
      Category.ClearAll();
      Item.ClearAll();
    }

    public CategoryTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=todolist_test;";
    }

  [TestMethod]
  public void CategoryConstructor_CreatesInstanceOfCategory_Category()
  {
    Category newCategory = new Category("test category");
    Assert.AreEqual(typeof(Category), newCategory.GetType());
  }

    [TestMethod]
    public void GetName_ReturnsName_String()
    {
      //Arrange
      string name = "Test Category";
      Category newCategory = new Category(name);

      //Act
      string result = newCategory.GetName();

      //Assert
      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void GetItems_RetrievesAllItemsWithCategory_ItemList()
    {
      //Arrange
      Category newCategory = new Category("Household chores");
      newCategory.Save();
      Item firstItem = new Item("Mow the lawn", newCategory.GetId());
      firstItem.Save();
      Item secondItem = new Item("Do the dishes", newCategory.GetId());
      secondItem.Save();
      List<Item> testItemList = new List<Item> { firstItem, secondItem};

      //Act
      List<Item> resultItemList = newCategory.GetItems();

      //Assert
      CollectionAssert.AreEqual(testItemList, resultItemList);
    }


    [TestMethod]
    public void GetAll_CategoriesEmptyAtFirst_List()
    {
      //Arrange, Act
      int result = Category.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void GetAll_ReturnsAllCategoryObjects_CategoryList()
    {
      //Arrange
      string name01 = "Work";
      string name02 = "School";
      Category newCategory1 = new Category(name01);
      newCategory1.Save();
      Category newCategory2 = new Category(name02);
      newCategory2.Save();
      List<Category> newList = new List<Category> { newCategory1, newCategory2 };

      //Act
      List<Category> result = Category.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Find_ReturnsCategoryInDatabase_Category()
    {
      //Arrange
      string name01 = "Work";
      Category testCategory = new Category(name01);
      testCategory.Save();

      //Act
      Category foundCategory = Category.Find(testCategory.GetId());

      //Assert
      Assert.AreEqual(testCategory, foundCategory);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_Category()
    {
      //Arrange, Act
      Category firstCategory = new Category("Household chores");
      Category secondCategory = new Category("Household chores");

      //Assert
      Assert.AreEqual(firstCategory, secondCategory);
    }

    [TestMethod]
    public void Save_SavesCategoryToDatabase_CategoryList()
    {
      //Arrange
      Category testCategory = new Category("Household chores");
      testCategory.Save();

      //Act
      List<Category> result = Category.GetAll();
      List<Category> testList = new List<Category>{testCategory};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
   public void Delete_DeletesCategoryAssociationsFromDatabase_CategoryList()
   {
     //Arrange
     Item testItem = new Item("Mow the lawn");
     testItem.Save();
     string testName = "Home stuff";
     Category testCategory = new Category(testName);
     testCategory.Save();

     //Act
     testCategory.AddItem(testItem);
     testCategory.Delete();
     List<Category> resultItemCategories = testItem.GetCategories();
     List<Category> testItemCategories = new List<Category> {};

     //Assert
     CollectionAssert.AreEqual(testItemCategories, resultItemCategories);
   }

   [TestMethod]
    public void Test_AddItem_AddsItemToCategory()
    {
      //Arrange
      Category testCategory = new Category("Household chores");
      testCategory.Save();
      Item testItem = new Item("Mow the lawn");
      testItem.Save();
      Item testItem2 = new Item("Water the garden");
      testItem2.Save();

      //Act
      testCategory.AddItem(testItem);
      testCategory.AddItem(testItem2);
      List<Item> result = testCategory.GetItems();
      List<Item> testList = new List<Item>{testItem, testItem2};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetItems_ReturnsAllCategoryItems_ItemList()
    {
      //Arrange
      Category testCategory = new Category("Household chores");
      testCategory.Save();
      Item testItem1 = new Item("Mow the lawn");
      testItem1.Save();
      Item testItem2 = new Item("Buy plane ticket");
      testItem2.Save();

      //Act
      testCategory.AddItem(testItem1);
      List<Item> savedItems = testCategory.GetItems();
      List<Item> testList = new List<Item> {testItem1};

      //Assert
      CollectionAssert.AreEqual(testList, savedItems);
    }
  }
}
