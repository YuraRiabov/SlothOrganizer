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
    public static class SQLiteConnector
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

        public static void DisconnectSubTasks(Assignment task)
        {
            using (SQLiteConnection connection = new SQLiteConnection(GetConnectionString()))
            {
                ExecuteQuery($"update Tasks set ParentId=NULL, IsSubTask=0 where ParentId={task.Id}");
            }
        }

        public static void DeleteSubTasks(Assignment task)
        {
            foreach (Assignment st in task.SubTasks)
            {
                Assignment subTask = st;
                if(subTask.SubTasks.Count != 0)
                {
                    DeleteSubTasks(subTask);
                }
                using(SQLiteConnection connection = new SQLiteConnection(GetConnectionString()))
                {
                    ExecuteQuery($"delete from Tasks where ParentId={task.Id}");
                }
            }
        }

        public static List<Assignment> GetAllTasks()
        {
            List<Assignment> tasks = new List<Assignment>();
            using(SQLiteConnection connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();
                string query = "select * from Tasks";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                SQLiteDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    Assignment assignment = new Assignment((int)reader.GetInt64(0), reader.GetString(1), DateTime.Parse(reader.GetString(5)), DateTime.Parse(reader.GetString(6)), reader.GetInt32(4));
                    tasks.Add(assignment);
                }
            }
            return tasks;
        }

        public static List<Assignment> GetSubTasks(Assignment task)
        {
            List<Assignment> subTasks = new List<Assignment>();
            using (SQLiteConnection connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();

                string query = $"select * from Tasks where ParentId={(Int64)task.Id}";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Assignment subTask = new Assignment((int)reader.GetInt64(0), reader.GetString(1), DateTime.Parse(reader.GetString(5)), DateTime.Parse(reader.GetString(6)), reader.GetInt32(4));
                    subTasks.Add(subTask);
                }
            }
            return subTasks;
        }

        public static void UpdateTask(Assignment task, DateTime newStart, DateTime newEnd)
        {
            ExecuteQuery($"update Tasks set Start='{newStart}', End='{newEnd}' where id={task.Id}");
        }

        public static void UpdateTask(Assignment task, TaskState newState)
        {
            ExecuteQuery($"update Tasks set State={(int)newState} where id={task.Id}");
        }

        public static List<TimePeriod> GetAllTimePeriods()
        {
            List<TimePeriod> periods = new List<TimePeriod>();
            using (SQLiteConnection connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();
                string query = "select * from TimePeriods";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                SQLiteDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    TimePeriod timePeriod = new TimePeriod((int)reader.GetInt64(0), DateTime.Parse(reader.GetString(3)), reader.GetInt32(2), 
                                                            reader.GetInt32(5),reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8));
                    periods.Add(timePeriod);
                }
            }
            return periods;
        }

        public static void UpdateTimePeriod(TimePeriod timePeriod, int newActive, int newCompleted, int newPartiallyCompleted, int newFailed)
        {
            ExecuteQuery($"update TimePeriods set ActiveNumber={newActive}, CompletedNumber={newCompleted}," +
                         $" PartiallyCompletedNumer={newPartiallyCompleted}, FailedNumber={newFailed} where id={timePeriod.Id}");
        }

        public static TimePeriod CreateTimePeriod(TimePeriod timePeriod, int parentId = 0)
        {
            using (SQLiteConnection connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();
                SQLiteCommand command;
                if (timePeriod.Length < 365)
                {
                    string query = "insert into TimePeriods (ParentId, Length, Start, End, ActiveNumber, CompletedNumber, PartiallyCompletedNumber, FailedNumber) " +
                                   $"values ({parentId}, {timePeriod.Length}, '{timePeriod.Start.Date}', '{timePeriod.End.Date}', {timePeriod.ActiveNumber}," +
                                   $" {timePeriod.CompletedNumber}, {timePeriod.PartiallyCompletedNumber}, {timePeriod.FailedNumber})";
                    command = new SQLiteCommand(query, connection);
                    command.ExecuteNonQuery();

                }
                else
                {
                    string query = "insert into TimePeriods (ParentId, Length, Start, End, ActiveNumber, CompletedNumber, PartiallyCompletedNumber, FailedNumber) " +
                                   $"values (NULL, {timePeriod.Length}, '{timePeriod.Start.Date}', '{timePeriod.End.Date}', {timePeriod.ActiveNumber}," +
                                   $" {timePeriod.CompletedNumber}, {timePeriod.PartiallyCompletedNumber}, {timePeriod.FailedNumber})";
                    command = new SQLiteCommand(query, connection);
                    command.ExecuteNonQuery();
                }
                command.CommandText = "select last_insert_rowid()";
                Int64 id = (Int64)command.ExecuteScalar();
                timePeriod.Id = (int)id;
            }
            return timePeriod;
        }

        public static TimePeriod GetYear(DateTime yearBeginning)
        {
            TimePeriod year = new TimePeriod();
            using (SQLiteConnection connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();
                string query = $"select * from TimePeriods where Start='{yearBeginning}' and Length > 364";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    year = new TimePeriod((int)reader.GetInt64(0), DateTime.Parse(reader.GetString(3)), reader.GetInt32(2),
                                              reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8));
                }
            }
            return year;
        }

        public static List<TimePeriod> GetChildrenTimePeriods(TimePeriod timePeriod)
        {
            List<TimePeriod> children = new List<TimePeriod>();
            using (SQLiteConnection connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();
                string query = $"select * from TimePeriods where ParentId={timePeriod.Id}";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TimePeriod child = new TimePeriod((int)reader.GetInt64(0), DateTime.Parse(reader.GetString(3)), reader.GetInt32(2),
                                      reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8));
                    children.Add(child);
                }
            }
            return children;
        }

        public static void UpdateTimePeriod(TimePeriod newTimePeriod)
        {
            ExecuteQuery($"update TimePeriods set ActiveNumber={newTimePeriod.ActiveNumber}, CompletedNumber={newTimePeriod.CompletedNumber}," +
                         $"PartiallyCompletedNumber={newTimePeriod.PartiallyCompletedNumber}, FailedNumber={newTimePeriod.FailedNumber} " +
                         $"where id={newTimePeriod.Id}");
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
