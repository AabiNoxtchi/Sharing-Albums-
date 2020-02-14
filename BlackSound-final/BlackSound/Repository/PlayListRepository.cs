using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BlackSound.Entity;

namespace BlackSound.Repository
{
    public class PlayListRepository : BaseRepository<PlayList>
    {
        protected override void AfterSelect(PlayList item)
        {
            UserRepository usersRepo = new UserRepository();

            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = String.Format("SELECT [UserId] FROM [UserToPlayList] WHERE PlayListId = @PlayListId");
            cmd.Parameters.AddWithValue("@PlayListId", item.Id);

            SqlDataReader reader = null;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int userId = Convert.ToInt32(reader["UserId"]);
                    item.Shares.Add(usersRepo.GetById(userId));
                }
            }
            finally
            {
                conn.Close();
                reader.Close();
            }
        }

        protected override void BeforeDelete(PlayList item,bool all)
        {
            if (all == true)
            {
                SongRepository songRepo = new SongRepository();
                songRepo.GetAll(x => x.ParentID == item.Id).ForEach(x=>songRepo.Delete(x));
            }
            SqlConnection conn = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = String.Format("DELETE FROM [UserToPlayList] WHERE  [PlayListId] = @PlayListId");
                //cmd.Parameters.AddWithValue("UserId", user.Id);
                cmd.Parameters.AddWithValue("PlayListId", item.Id);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            //}
        }

        protected override void AfterInsert(PlayList item)
        {
            foreach (User user in item.Shares)
            {
                SqlConnection conn = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = String.Format("INSERT INTO [UserToPlayList] ([UserId], [PlayListId]) VALUES (@UserId, @PlayListId)");
                cmd.Parameters.AddWithValue("UserId", user.Id);
                cmd.Parameters.AddWithValue("PlayListId", item.Id);

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
        }

        protected override void BeforeUpdate(PlayList item)
        {
            BeforeDelete(item,false);
            AfterInsert(item);
        }
    }
}
