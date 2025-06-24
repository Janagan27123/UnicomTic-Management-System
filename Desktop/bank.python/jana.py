ADMIN_CREDENTIALS={"ut010657","Admin@123"}

def admin_login():
    username = input("Enter admin username: ")
    password = input("Enter admin password: ")
    #return ADMIN_CREDENTIALS.get(username) == password

# Customer login
'''def customer_login(data):
    acc_num = input("Enter account number: ")
    pin = input("Enter PIN: ")
    customer = data.get(acc_num)
    if customer and customer["pin"] == pin:
        return acc_num
    print("Invalid login credentials.")
    return None'''

admin_login()