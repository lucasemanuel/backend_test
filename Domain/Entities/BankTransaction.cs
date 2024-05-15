using Domain.Enums;
using Domain.Exceptions;
using MongoDB.Bson;

namespace Domain.Entities;

public class BankTransaction
{
    public ObjectId _id { get; set; }
    public decimal amount { get; set; }
    public TypeBankTransaction? typeBankTransaction { get; set; }
    public BankAccount BankAccount { get; set; }
    public DateTime createdAt { get; set; }
    public ObjectId BankAccount_id { get; set; }

    public BankTransaction Deposit(BankAccount bankAccount, decimal value)
    {
        if (bankAccount.isInactived) {
            throw new DomainException("it is not possible to carry out operations on a deactivated account.");
        }

        if (value <= 0)
        {
            throw new DomainException("Deposit amount must be greater than zero.");
        }

        typeBankTransaction = TypeBankTransaction.Deposit;
        BankAccount = bankAccount;
        BankAccount.balance += value;
        amount = value;
        createdAt = DateTime.Now;

        return this;
    }

    public BankTransaction Withdraw(BankAccount bankAccount, decimal value)
    {
        if (bankAccount.isInactived) {
            throw new DomainException("it is not possible to carry out operations on a deactivated account.");
        }

        if (value <= 0)
        {
            throw new DomainException("Withdrawal amount must be greater than zero.");
        }

        if (value > bankAccount.balance)
        {
            throw new DomainException("Insufficient balance to make the withdrawal.");
        }

        typeBankTransaction = TypeBankTransaction.Withdraw;
        BankAccount = bankAccount;
        BankAccount.balance -= value;
        amount = value;
        createdAt = DateTime.UtcNow;

        return this;
    }
}