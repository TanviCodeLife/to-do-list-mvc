using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDoList.Controllers;
using ToDoList.Models;
using System;

namespace ToDoList.Tests
{
    [TestClass]
    public class HomeControllerTest
    {
      [TestMethod]
      public void Index_ReturnsCorrectView_True()
      {
        //Arrange
        HomeController controller = new HomeController();
        //Act
        var indexView = controller.Index();
        //Assert
        Assert.IsInstanceOfType(indexView, typeof(ViewResult));
        Console.WriteLine(typeof(ViewResult));
      }

      [TestMethod]
      public void Index_HasCorrectedModelType_ItemList()
      {
        //Arrange
        HomeController controller = new HomeController();
        var indexView = controller.Index() as ViewResult;
        //Act
        var result = indexView.ViewData.Model;
        //Assert
        Assert.IsInstanceOfType(result, typeof(List<Item>));
      }

    }
}
