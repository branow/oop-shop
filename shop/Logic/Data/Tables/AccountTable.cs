using MySql.Data.MySqlClient;
using Shop.Logic;
using Shop.Logic.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shop.Logic.Data.Tables
{
    public class AccountTable : Table
    {

        private static readonly string s_table = "accounts", s_id = "account_id", s_name = "account_name",
            s_email = "email", s_balance = "balance", s_password = "account_password", s_history = "account_history";
        public AccountTable() : base(s_table, s_id)
        {
        }

        public Account Create(string name, string email, string password)
        {
            AccountDTO account = new AccountDTO();
            account.Id = -1;
            account.Name = name;
            account.Email = email;
            account.Password = password;
            account.Access = true;
            account.Balance = 0;
            account.History = new List<string>();
            Account temp = new Account(account);
            temp.Close();
            int id = Insert(name, temp.Email, temp.Password, temp.Balance, temp.History);
            account.Id = id;
            account.Access = false;
            Account ac = new Account(account);
            ac.Open(password);
            return ac;
        }

        public List<Account> Get()
        {
            List<Account> list = new List<Account>();
            MySqlDataReader reader = Select();
            while (reader.Read())
            {
                AccountDTO account = new AccountDTO();
                account.Id = reader.GetInt32(s_id);
                account.Name = reader.GetString(s_name);
                account.Email = reader.GetString(s_email);
                account.Password = reader.GetString(s_password);
                account.Access = false;
                account.Balance = reader.GetInt64(s_balance);
                account.History = StringToList(reader.GetString(s_history));
                list.Add(new Account(account));
            }
            reader.Close();
            return list;
        }

        public Account Get(int id)
        {
            MySqlDataReader reader = Select(new Condition(new DataField(s_id, id)));
            reader.Read();
            AccountDTO account = new AccountDTO();
            account.Id = reader.GetInt32(s_id);
            account.Name = reader.GetString(s_name);
            account.Email = reader.GetString(s_email);
            account.Password = reader.GetString(s_password);
            account.Access = false;
            account.Balance = reader.GetInt64(s_balance);
            account.History = StringToList(reader.GetString(s_history));
            Account ac = new Account(account);
            reader.Close();
            return ac;
        }

        public Account Get(string name)
        {
            MySqlDataReader reader = Select(new Condition(new DataField(s_name, name)));
            try
            {
                if (!reader.HasRows)
                {
                    return null;
                };
                reader.Read();
                AccountDTO account = new AccountDTO();
                account.Id = reader.GetInt32(s_id);
                account.Name = reader.GetString(s_name);
                account.Email = reader.GetString(s_email);
                account.Password = reader.GetString(s_password);
                account.Access = false;
                account.Balance = reader.GetInt64(s_balance);
                account.History = StringToList(reader.GetString(s_history));
                Account ac = new Account(account);
                return ac;
            }
            catch (MySqlException e)
            {
                return null;
            }
            finally
            {
                reader.Close();
            }
            return null;
        }

        public void Update(Account account)
        {
            account.Close();
            Condition con = new Condition(new DataField(s_id, account.Id));
            Update(new DataField(s_name, account.Name), con);
            Update(new DataField(s_email, account.Email), con);
            Update(new DataField(s_password, account.Password), con);
            Update(new DataField(s_balance, account.Balance), con);
            Update(new DataField(s_history, ListToString(account.History)), con);
        }

        public void Remove(int id)
        {
            Delete(new Condition(new DataField(s_id, id)));
        }

        public bool ExistName(string name)
        {
            MySqlDataReader reader = Select(new Condition(new DataField(s_name, name)));
            bool exist = reader.HasRows;
            reader.Close();
            return exist;
        }

        private string ListToString(List<string> list)
        {
            string his = "";
            list.ForEach(e => his += e + ";");
            return his;
        }

        private List<string> StringToList(string str)
        {
            if (str.Equals(""))
                return new List<string>();
            string[] strings = str.Split(";");
            return new List<string>(strings);
        }

    }
}
