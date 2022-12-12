

namespace Shop.Logic
{
    public class AccountDTO 
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long Balance { get; set; }
        public List<string> History { get; set; }
        public bool Access { get; set; }
    }

    public class Account
    {
        public int Id { get; }
        public string Name
        {
            get { return _name; }
            set
            {
                if (_access) _name = value;
                else throw AccessError();
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                if (_access) _email = value;
                else throw AccessError();
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                if (_access) _password = value;
                else throw AccessError();
            }
        }
        public long Balance { get { return _balance; } }
        public List<string> History { get { return _history; } }
        public bool IsAccess { get {
                Console.WriteLine(_access);
                return _access; } }
        public List<Product> Basket { get; set; }

        private bool _access;
        private long _balance;
        private string _name;
        private string _email;
        private string _password;
        private readonly List<string> _history;

        public Account(AccountDTO account)
        {
            Id = account.Id;
            _name = account.Name;
            _email = account.Email;
            _password = account.Password;
            _balance = account.Balance;
            _history = account.History;
            _access = account.Access;
            Basket = new List<Product>();
        }

        public void ExecuteTransaction(Transaction transaction)
        {
            if (transaction.IsReceiver(this))
            {
                _balance += transaction.Sum;
                History.Add(transaction.ToString());
            }
            else if (transaction.IsSender(this))
            {
                if (!_access)
                    throw AccessError();
                if (_balance < transaction.Sum)
                    throw new ArgumentOutOfRangeException("The balance is not enough money for this operation");
                _balance -= transaction.Sum;
                History.Add(transaction.ToString());
            }
        }

        public bool Open(string password)
        {
            if (_access) return true;
            if (!new Hash(password).GetHash().Equals(Password)) return false;
            _access = true;
            Password = password;
            return _access;
        }

        public bool Close()
        {
            if (!_access) return true;
            Password = new Hash(Password).GetHash();
            _access = false;
            return true;
        }

        public override string ToString()
        {
            if (_access)
            {
                return $"{Id}, {Name}, {Email}, {Balance}, {string.Join("; ", History)}";
            }
            else
            {
                return $"{Id}, {Name}";
            }
        }

        public override bool Equals(object? obj)
        {
            var o = obj as Account;
            if (o == null)
                return false;
            return Id == o.Id;
        }


        private AccessViolationException AccessError()
        {
            return new AccessViolationException("Firstly you have to get access for this account");
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
