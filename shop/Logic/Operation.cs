using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Shop.Logic.Data.Tables;

namespace Shop.Logic
{
    public class Operation
    {

        public void Buy(Account buyer, Product product)
        {
            AccountTable at = new AccountTable();
            Account seller = at.Get(product.SellerId);
            Transaction transaction = Transaction.newTransaction(product.Price, seller, buyer, $"For {product.Id} {product.Name}");
            transaction.Execute();
            at.Update(buyer);
            at.Update(seller);
            new ProductTable().Remove(product.Id);
        }

        public void Replenishment(Account receiver, long sum)
        {
            AccountTable at = new AccountTable();
            AccountDTO account = new AccountDTO();
            account.Id = -666;
            account.Name = "bank";
            account.Email = "****";
            account.Password = "****";
            account.Access = true;
            account.Balance = sum;
            account.History = new List<string>();
            Account bank = new Account(account);
            Transaction transaction = Transaction.newTransaction(sum, receiver, bank, "Account replenishment");
            transaction.Execute();
            at.Update(receiver);
        }


    }
}
