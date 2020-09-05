﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class BisnesLogic
    {
        HBEntities1 db;

        public BisnesLogic()
        {
            db = new HBEntities1();
        }
        // DB for TEST
#if DEBUG
        public BisnesLogic(HBEntities1 entity)
        {
            db = entity;
        }
#endif


        public List<Users> getAllUsers()
        {
            var users = db.Users.Select(user => user);

            return users.ToList();
        }

        public List<Categories> getAllCategory()
        {
            //var result = from i in db.Categories
            //           select i;


            var result = db.Categories.Select(c => c);

            return result.ToList();
        }

        public List<OrdersView> getAllOrders()
        {

            var result = (from order in db.Orders
                          join category in db.Categories on order.Category_Id equals category.Id
                          join user in db.Users on order.User_Id equals user.Id
                          select new OrdersView
                          {
                              Id = order.Id,
                              CategoryName = category.Name,
                              UserName = user.Name,
                              DateOrder = order.Date,
                              Price = order.Price,
                              Description = order.Description
                          }).ToList();


            return result;
        }


        public string getCategoryName(int index)
        {
            var categoryName = db.Categories.FirstOrDefault(categ => categ.Id == index).Name;

            return categoryName;
        }

        public bool userIdentification(string login, string pass)
        {
            var user = db.Users.Where(u => u.Name == login && u.Password == pass).FirstOrDefault();
            return true;
        }

        public FamilyMembers getFamilyName(int id)
        {
            var family = db.FamilyMembers.Where(f => f.Id == id).FirstOrDefault();

            return family;
        }


        public void addOrder(string categoryName, string userName, DateTime dateOrder, decimal price, string description)
        {
            var categoryId = db.Categories.Where(cat => cat.Name == categoryName)
                                          .Select(x => x.Id).FirstOrDefault();

            var userId = db.Users.Where(user => user.Name == userName)
                                          .Select(u => u.Id).FirstOrDefault();

            var newOrder = new Orders() { Category_Id = categoryId, User_Id = userId, Date = dateOrder, Price = price, Description = description };
            db.Orders.Add(newOrder);
            db.SaveChanges();
        }

        //DELETE ORDER
        public void deleteOrder(int id)
        {
            var orderToDelete = db.Orders.Where(o => o.Id == id).FirstOrDefault();

            if (orderToDelete != null)
            {
                db.Orders.Remove(orderToDelete);
                db.SaveChanges();
            }
        }



        public void changeOrder(int id, string categoryName, string userName, DateTime dateOrder, decimal price, string description)
        {
            var orderToChange = db.Orders.Where(o => o.Id == id).FirstOrDefault();


            var categoryId = db.Categories.Where(cat => cat.Name == categoryName)
                                          .Select(x => x.Id).FirstOrDefault();

            var userId = db.Users.Where(user => user.Name == userName)
                                          .Select(u => u.Id).FirstOrDefault();
            orderToChange.Category_Id = categoryId;
            orderToChange.User_Id = userId;
            orderToChange.Date = dateOrder;
            orderToChange.Price = price;
            orderToChange.Description = description;

            db.SaveChanges();

        }

        // Add Category
        public void addCategory(string categoryName, bool type)
        {
            var newCategory = new Categories()
            {
                Name = categoryName,
                Type = type
            };

            db.Categories.Add(newCategory);
            db.SaveChanges();
        }

        // Delete category
        public void delateCategory(int id)
        {
            var categoryToDelete = db.Categories.Where(cat => cat.Id == id).FirstOrDefault();

            var useOrders = db.Orders.Where(ord => ord.Category_Id == id);

            if (useOrders.Count() > 0)
            {
                throw new Exception("Category is used in orders!!!");
            }

            if (categoryToDelete != null)
            {
                db.Categories.Remove(categoryToDelete);
                db.SaveChanges();
            }
        }

        // Change category
        public void changeCategory(int id, string newName, bool newType)
        {
            var categoryToChange = db.Categories.Where(c => c.Id == id).FirstOrDefault();
            if (categoryToChange != null)
            {
                categoryToChange.Name = newName;
                categoryToChange.Type = newType;

                db.SaveChanges();
            }
        }

        // Add newUser
        public void addNewUser(string email, string userName, string pass, int familyId)
        {

            var family = db.FamilyMembers.Where(f => f.Id == familyId).FirstOrDefault();

            var newUser = new Users()
            {
                Email = email,
                Name = userName,
                Password = pass,
                FamilyMembers = family
            };

            db.Users.Add(newUser);
            db.SaveChanges();
        }


        // Delete user
        public void deleteUser(int id)
        {
            var userToDelete = db.Users.Where(o => o.Id == id).FirstOrDefault();

            var useOrders = db.Orders.Where(ord => ord.User_Id == id);
            if (useOrders.Count() > 0)
            {
                throw new Exception("User is used in orders!!!");
            }

            if (userToDelete != null)
            {
                db.Users.Remove(userToDelete);
                db.SaveChanges();
            }
        }

        // Change my data
        public void changeCurentUser(int id, string email, string name, string pass/*, int familyId*/)
        {
            var userToChange = db.Users.Where(c => c.Id == id).FirstOrDefault();

            userToChange.Email = email;
            userToChange.Name = name;
            userToChange.Password = pass;
            //userToChange.Family_Id = familyId;

            db.SaveChanges();
        }

    }
}
