using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ds.Tutorial.Model.Repositories
{
    public class TutorialRepository
    {
        private TutorialDbContext _dbContext;

        public TutorialRepository(TutorialDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 新規作成
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Create(UserEntity user)
        {
            using (_dbContext)
            {
                _dbContext.Users.Add(user);
                return _dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Update(UserEntity user)
        {
            using (_dbContext)
            {
                var userForm = _dbContext.Users.FirstOrDefault(x => x.Id == user.Id);
                userForm.Name = user.Name;
                userForm.Age = user.Age;
                userForm.Hobby = user.Hobby;
                return _dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            using (_dbContext)
            {
                var userForm = _dbContext.Users.FirstOrDefault(x => x.Id == id);
                _dbContext.Users.Remove(userForm);
                return _dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// IdからUserを取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserEntity GetOneById(int id)
        {
            using (_dbContext)
            {
                return _dbContext.Users.FirstOrDefault(x => x.Id == id);
            }
        }

        /// <summary>
        /// 年齢からUser一覧を取得する
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        public List<UserEntity> GetAllByAge(int age)
        {
            using (_dbContext)
            {
                return _dbContext.Users.Where(x => x.Age == age).ToList();
            }
        }

        /// <summary>
        /// 年齢から名前一覧を取得する
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        public List<string> GetNamesByAge(int age)
        {
            using (_dbContext)
            {
                return _dbContext.Users.Where(x => x.Age == age).Select(x => x.Name).ToList();
            }
        }

        /// <summary>
        /// ページング
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<UserEntity> GetAllPaging(int pageSize, int page)
        {
            using (_dbContext)
            {
                return _dbContext.Users.Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
        }

        /// <summary>
        /// トランザクション処理 0歳以下のUserの年齢を0に変更する
        /// </summary>
        /// <returns></returns>
        public int FixAge()
        {
            using (_dbContext)
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var userListForm = _dbContext.Users.Where(x => x.Age < 0);
                        foreach (UserEntity user in userListForm)
                        {
                            user.Age = 0;
                        }
                        var count = _dbContext.SaveChanges();
                        transaction.Commit();
                        return count;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return 0;
                    }
                }
            }
        }
    }
}
