﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;
namespace Model.Dao
{
    public class UserDao
    {
        OnlineShopDbContext db = null;
        public UserDao() {
            db = new OnlineShopDbContext();
        }

        public long Insert(User entity) {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(User entity) {
            try
            {
                var user = db.Users.Find(entity.ID);
                user.Name = entity.Name;
                if (!string.IsNullOrEmpty(entity.Password)) {
                    user.Password = entity.Password;
                }
                user.Address = entity.Address;
                user.Email = entity.Email;
                user.ModifiedBy = entity.ModifiedBy;
                user.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

        public IEnumerable<User> ListAllPaging(int page, int PageSize) {
            return db.Users.OrderByDescending(x => x.CreatedDate).ToPagedList(page, PageSize);
        }

        public User GetById(string userName) {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }

        public User ViewDetail(int id) {
            return db.Users.Find(id);
        }

        public int Login(string userName, string password) {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
            if (result == null) {
                return 0;
            }
            else {
                if (result.Status == false) {
                    return -1;
                }
                else {
                    if (result.Password == password)
                        return 1;
                    else 
                        return -2;
                }
            }
        }

        public bool Delete(int id) {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch(Exception) {
                return false;
            }
        }

    }
}
