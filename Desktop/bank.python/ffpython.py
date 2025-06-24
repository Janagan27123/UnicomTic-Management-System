import json
import os
from datetime import datetime

DATA_FILE = "bank_data.json"
ADMIN_CREDENTIALS = {"admin": "admin123"}

# Load data from file
def load_data():
    if not os.path.exists(DATA_FILE):
        return {}
    with open(DATA_FILE, 'r') as f:
        return json.load(f)

# Save data to file
def save_data(data):
    with open(DATA_FILE, 'w') as f:
        json.dump(data, f, indent=4)

# Admin login
def admin_login():
    username = input("Enter admin username: ")
    password = input("Enter admin password: ")
    return ADMIN_CREDENTIALS.get(username) == password

# Customer login
def customer_login(data):
    acc_num = input("Enter account number: ")
    pin = input("Enter PIN: ")
    customer = data.get(acc_num)
    if customer and customer["pin"] == pin:
        return acc_num
    print("Invalid login credentials.")
    return None

# Create new account
def create_account(data):
    acc_num = input("Enter new account number: ")
    if acc_num in data:
        print("Account already exists.")
        return
    name = input("Enter customer name: ")
    pin = input("Set a 4-digit PIN: ")
    if not pin.isdigit() or len(pin) != 4:
        print("Invalid PIN.")
        return
    data[acc_num] = {
        "name": name,
        "pin": pin,
        "balance": 0,
        "history": []
    }
    print("Account created successfully.")

# Deposit money
def deposit(data, acc_num):
    amount = input("Enter amount to deposit: ")
    if not amount.isdigit():
        print("Invalid amount.")
        return
    amount = float(amount)
    data[acc_num]["balance"] += amount
    data[acc_num]["history"].append(f"{datetime.now()}: Deposited ${amount}")
    print("Deposit successful.")

# Withdraw money
def withdraw(data, acc_num):
    amount = input("Enter amount to withdraw: ")
    if not amount.isdigit():
        print("Invalid amount.")
        return
    amount = float(amount)
    if data[acc_num]["balance"] < amount:
        print("Insufficient balance.")
        return
    data[acc_num]["balance"] -= amount
    data[acc_num]["history"].append(f"{datetime.now()}: Withdrew ${amount}")
    print("Withdrawal successful.")

# Check balance
def check_balance(data, acc_num):
    print(f"Current balance: ${data[acc_num]['balance']}")

# Transaction history
def transaction_history(data, acc_num):
    print("Transaction History:")
    for item in data[acc_num]["history"]:
        print(item)

# Main app loop
def main():
    data = load_data()

    while True:
        print("\n--- Mini Bank App ---")
        print("1. Admin Login")
        print("2. Customer Login")
        print("3. Exit")
        choice = input("Enter choice: ")

        if choice == "1":
            if admin_login():
                print("Admin logged in.")
                while True:
                    print("\n1. Create Account\n2. Logout")
                    admin_choice = input("Enter choice: ")
                    if admin_choice == "1":
                        create_account(data)
                        save_data(data)
                    elif admin_choice == "2":
                        break
                    else:
                        print("Invalid choice.")
            else:
                print("Admin login failed.")

        elif choice == "2":
            acc_num = customer_login(data)
            if acc_num:
                while True:
                    print("\n1. Deposit\n2. Withdraw\n3. Check Balance\n4. Transaction History\n5. Logout")
                    cust_choice = input("Enter choice: ")
                    if cust_choice == "1":
                        deposit(data, acc_num)
                        save_data(data)
                    elif cust_choice == "2":
                        withdraw(data, acc_num)
                        save_data(data)
                    elif cust_choice == "3":
                        check_balance(data, acc_num)
                    elif cust_choice == "4":
                        transaction_history(data, acc_num)
                    elif cust_choice == "5":
                        break
                    else:
                        print("Invalid choice.")

        elif choice == "3":
            print("Exiting...")
            break
        else:
            print("Invalid choice.")

if __name__ == "__main__":
    main()