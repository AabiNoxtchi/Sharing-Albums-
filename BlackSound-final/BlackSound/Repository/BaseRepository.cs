using BlackSound.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Reflection;

namespace BlackSound.Repository
{
    public abstract class BaseRepository<T> where T : BaseEntity, new()
    {
        protected readonly string connectionString;

        public BaseRepository()
        {
            connectionString = ConfigurationManager.AppSettings["connectionString"];
        }

        public T GetById(int id)
        {
            T item = null;

            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = String.Format("SELECT * FROM [{0}] WHERE Id = {1}", GetTableName(), id);

            SqlDataReader reader = null;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    item = new T();
                    PopulateFromReader(item, reader);
                }

            }
            finally
            {
                conn.Close();
                reader.Close();
            }

            AfterSelect(item);

            return item;
        }

        public List<T> GetAll(Func<T, bool> filter)
        {
            List<T> items = new List<T>();

            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = String.Format("SELECT * FROM [{0}]", GetTableName());

            SqlDataReader reader = null;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    T item = new T();
                    PopulateFromReader(item, reader);

                    AfterSelect(item);

                    if (filter == null || filter(item))
                    {
                        items.Add(item);
                    }
                }
            }
            finally
            {
                conn.Close();
                reader.Close();
            }

            return items;
        }

        public void Save(T item)
        {
            if (item.Id > 0)
                Update(item);
            else
                Insert(item);
        }
        
        public void Delete(T item)
        {

            BeforeDelete(item,true);

            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = String.Format("DELETE FROM [{0}] WHERE Id = {1}", GetTableName(), item.Id);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        protected virtual void AfterSelect(T item) { }
        protected virtual void AfterInsert(T item) { }
        protected virtual void BeforeDelete(T item,bool all) { }
        protected virtual void BeforeUpdate(T item) { }

        private void Insert(T item)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = String.Format("INSERT INTO [{0}] ({1}) VALUES ({2})", GetTableName(),
                                                    String.Join(", ", GetColumnsList()),
                                                    String.Join(", ", GetEntityValues(item))
                                           );

            //This command gets the last generated ID by an IDENTITY column within the current db session
            //A db session starts when you open a connection object and ends when you close it
            SqlCommand scopeIdentity = new SqlCommand();
            scopeIdentity.Connection = conn;
            scopeIdentity.CommandText = String.Format("SELECT SCOPE_IDENTITY()");

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                object o = scopeIdentity.ExecuteScalar();
                item.Id = Convert.ToInt32(o);
            }
            finally
            {
                conn.Close();
            }

            AfterInsert(item);
        }
        private void Update(T item)
        {
            BeforeUpdate(item);

            List<string> updateColumns = new List<string>();
            List<string> columns = GetColumnsList();
            List<string> values = GetEntityValues(item);

            for(int i=0;i<columns.Count;i++)
            {
                updateColumns.Add(String.Format("{0} = {1}", columns[i], values[i]));
            }

            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = String.Format("UPDATE [{0}] SET {1} WHERE Id = {2}", GetTableName(), String.Join(", ", updateColumns), item.Id);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        private void PopulateFromReader(T item, SqlDataReader reader)
        {
            Type type = typeof(T);
            foreach (PropertyInfo pi in type.GetProperties())
            {
                if (!typeof(ValueType).IsAssignableFrom(pi.PropertyType) && !pi.PropertyType.IsAssignableFrom(typeof(string)))
                    continue;

                pi.SetValue(item, reader[pi.Name]);
            }
        }
        private string GetTableName()
        {
            return typeof(T).Name + "s";
        }
        private List<string> GetColumnsList()
        {
            List<string> columns = new List<string>();

            Type type = typeof(T);
            foreach (PropertyInfo pi in type.GetProperties())
            {
                if (pi.Name == "Id")
                    continue;

                if (!typeof(ValueType).IsAssignableFrom(pi.PropertyType) && !pi.PropertyType.IsAssignableFrom(typeof(string)))
                    continue;

                columns.Add(pi.Name);
            }

            return columns;
        }
        private List<string> GetEntityValues(T item)
        {
            List<string> values = new List<string>();

            Type type = typeof(T);
            foreach (PropertyInfo pi in type.GetProperties())
            {
                if (pi.Name == "Id")
                    continue;

                if (!typeof(ValueType).IsAssignableFrom(pi.PropertyType) && !pi.PropertyType.IsAssignableFrom(typeof(string)))
                    continue;

                object value = pi.GetValue(item);

                if (value is string)
                    values.Add("'" + value + "'");
                else if (value is bool)
                    values.Add(((bool)value) == true ? "1" : "0");
                else
                    values.Add(value.ToString());
            }

            return values;
        }
    }
}
