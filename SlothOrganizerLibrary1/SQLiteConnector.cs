using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace SlothOrganizerLibrary
{
    public class SQLiteConnector
    {
        public static Assignment CreateTask(Assignment task, int parentId = 0)
        {
            
            using (SQLiteConnection connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();
                SQLiteCommand command;
                if (task.IsSubTask)
                {
                    string query = "insert into Tasks (Text, IsSubTask, ParentId, State, Start, End) " +
                                   $"values ('{task.Text}', {task.IsSubTask}, {parentId}, {(int)task.State}, '{task.TimeLimits.Start.Date}', '{task.TimeLimits.End.Date}')";
                    command = new SQLiteCommand(query, connection);
                    command.ExecuteNonQuery();

                }
                else
                {
                    string query = "insert into Tasks (Text, IsSubTask, ParentId, State, Start, End) " +
                                   $"values ('{task.Text}', {task.IsSubTask}, NULL, {(int)task.State}, '{task.TimeLimits.Start.Date}', '{task.TimeLimits.End.Date}')";
                    command = new SQLiteCommand(query, connection);
                    command.ExecuteNonQuery();
                }
                command.CommandText = "select last_insert_rowid()";
                Int64 id = (Int64)command.ExecuteScalar();
                task.Id = (int)id;
            }
            return task;
        }

        public static void DeleteTask(Assignment task)
        {
            using(SQLiteConnection connection = new SQLiteConnection(GetConnectionString()))
            {
                ExecuteQuery($"delete from Tasks where id={task.Id}");
            }
        }
        public static List<Assignment> GetSubTasks(Assignment task)
        {
            List<Assignment> subTasks = new List<Assignment>();
            using (SQLiteConnection connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();

                string query = $"select from Tasks where ParentId={task.Id}";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Assignment subTask = new Assignment(reader.GetString(1), reader.GetDateTime(5),
                                            reader.GetDateTime(6), reader.GetBoolean(2));
                    subTasks.Add(subTask);
                }
            }
            return subTasks;
        }
        private static string GetConnectionString(string name = "SlothOrganizerDB")
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        private static void ExecuteQuery(string query)
        {
            using(SQLiteConnection connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
