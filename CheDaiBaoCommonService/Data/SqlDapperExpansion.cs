using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CheDaiBaoCommonService.Data
{
    public static class SqlDapperExpansion
    {
        public static List<T> GetAll<T>(this SqlConnection sqlConnection,
            SqlTransaction sqlTransation = null) where T : BaseModel
        {
            return sqlConnection.Query<T>(
                string.Format("select * from {0} where  IsValid=@IsValid", typeof(T).Name),
                new { IsValid = true }, sqlTransation).ToList();
        }

        public static T GetById<T>(this SqlConnection sqlConnection, int id,
            SqlTransaction sqlTransation = null) where T : BaseModel
        {
            if (id == 0)
            {
                throw new Exception("Id为0");
            }

            return sqlConnection.Query<T>(
                    string.Format("select * from {0} where id=@Id and IsValid=@IsValid", typeof(T).Name),
                    new { ID = id, IsValid = true }, sqlTransation).SingleOrDefault();
        }

        public static int Insert<T>(this SqlConnection sqlConnection, T t,
            SqlTransaction sqlTransation = null) where T : BaseModel
        {
            t.CreateTime = DateTime.Now;
            t.UpdateTime = DateTime.Now;
            t.IsValid = true;

            return sqlConnection.Query<int>(
            string.Format(
            "insert into {0} ({1}) values ({2})  SELECT CAST(SCOPE_IDENTITY() as int)",
            typeof(T).Name.ToLower(),
            string.Join(",", t.GetFileList().Select(s => "[" + s + "]")),
            string.Join(",", t.GetFileList().Select(s => "@" + s)))
            , t, sqlTransation).SingleOrDefault();
        }

        public static void Insert<T>(this SqlConnection sqlConnection, List<T> list, SqlTransaction sqlTransation = null) where T : BaseModel
        {
            SqlConnection temp_sqlConnection = sqlTransation == null ? SqlConnections.GetOpenConnection() : sqlTransation.Connection;
            foreach (var item in list)
            {
                temp_sqlConnection.Insert<T>(item, sqlTransation);
            }
        }

        public static void Update<T>(this SqlConnection sqlConnection, T t,
            SqlTransaction sqlTransation = null) where T : BaseModel
        {
            if (t.Id == 0)
            {
                throw new Exception("Id为0");
            }

            t.UpdateTime = DateTime.Now;
            bool result = sqlConnection.Execute(
            string.Format(
            "update {0} set {1} where id=@Id",
            typeof(T).Name.ToLower(),
            string.Join(",", t.GetFileList().Select(s => "[" + s + "]=@" + s))
             ), t, sqlTransation) > 0;
        }

        public static void Update<T>(this SqlConnection sqlConnection, List<T> list,
            SqlTransaction sqlTransation = null) where T : BaseModel
        {
            SqlConnection temp_sqlConnection = sqlTransation == null ? SqlConnections.GetOpenConnection() : sqlTransation.Connection;
            foreach (var item in list)
            {
                temp_sqlConnection.Update<T>(item, sqlTransation);
            }
        }

        public static bool Delete<T>(this SqlConnection sqlConnection, int id,
            SqlTransaction sqlTransation = null, bool isDelete = false) where T : BaseModel
        {
            if (isDelete)
            {
                return sqlConnection.Execute(
                    string.Format(
                    "delete {0} where id=@Id",
                    typeof(T).Name.ToLower()
                    ), new { Id = id }, sqlTransation) > 0;
            }
            else
            {
                return sqlConnection.Execute(
                string.Format("update {0} set IsValid=@IsValid,UpdateTime=@UpdateTime where id=@Id", typeof(T).Name),
                new { Id = id, IsValid = 0, UpdateTime = DateTime.Now }, sqlTransation) > 0;
            }
        }

        public static List<T> Search<T>(this SqlConnection sqlConnection, T t,
            SqlTransaction sqlTransation = null) where T : BaseModel
        {
            t.IsValid = true;
            return sqlConnection.Query<T>(
                string.Format(
                "select * from {0} where 1=1 {1}",
                typeof(T).Name.ToLower(),
                string.Join(" ", GetFileList(t).Select(s => " and [" + s + "]=@" + s))
                ), t, transaction: sqlTransation).ToList();
        }

        public static List<string> GetFileList(this BaseModel baseEntity)
        {
            List<string> list = new List<string>();
            foreach (var item in baseEntity.GetType().GetProperties())
            {
                object obj = item.GetValue(baseEntity, null);
                if (obj != null && !item.Name.Equals("Id", StringComparison.OrdinalIgnoreCase)
                    && item.GetCustomAttributes(typeof(OriginalFieldAttribute), true).Length > 0)
                {
                    if (obj is int)
                    {
                        if (Convert.ToInt32(obj) != 0)
                        {
                            list.Add(item.Name);
                        }
                    }
                    else
                    {
                        list.Add(item.Name);
                    }
                }
            }
            return list;
        }

        public static void DeleteBySearch<T>(this SqlConnection sqlConnection, T t,
            SqlTransaction sqlTransation = null, bool isDelete = false) where T : BaseModel
        {
            if (isDelete)
            {
                sqlConnection.Execute(
                   string.Format(
                   "delete from {0} where 1=1 {1}",
                  typeof(T).Name.ToLower(),
               string.Join(" ", GetFileList(t).Select(s => " and [" + s + "]=@" + s))
               ), t, transaction: sqlTransation);
            }
            else
            {
                sqlConnection.Execute(
                string.Format("update {0} set IsValid=0,UpdateTime='{1}' where 1=1 {2}", typeof(T).Name.ToLower(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                string.Join(" ", GetFileList(t).Select(s => " and [" + s + "]=@" + s))),
                t, sqlTransation);
            }
        }

    }
}
