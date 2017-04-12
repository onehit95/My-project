using System;
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

        public IEnumerable<User> ListAllPaging(int page, int PageSize) {
            return db.Users.OrderByDescending(x => x.CreatedDate).ToPagedList(page, PageSize);
        }

        public User GetById(string userName) {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
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

    }
}
