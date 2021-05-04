# Checkout.PaymentGateway

## What is PaymentGateway?

PaymentGateway is a free source code gateway that allows us to make payment transactions between the shopper and the bank.
The developing of the product is at the MVP stage now.

  - [Getting started](#Getting-started)
  - [How can I use it?](#How-can-I-use-it)
  - [Building](#Building)
  - [Deploying](#Deploying)
  - [Features](#Features)
  - [Configuration](#Configuration)
  - [Contributing](#Contributing)
  - [Links](#Links)
  - [License](#License)

### Getting started
Out-of-box this gateway includes a simulation of interaction with a merchant and an a acquiring bank.
The following explanation shows us the payment process from the shoppers purchase decision till acquiring bank confirmation:

![paymentProcess](https://github.com/elenapakumirzakova/Checkout.PaymentGateway/blob/master/paymentProcess.JPG)

After the shopper transmit his Bankdata to the merchant, the following actions will be performed:
1. Merchant sends the POST request to our paymentGateway to initiate the payment process;
2. paymentGateway based on merchant request, asks the bank to conduct a transaction;
3. Bank, depending on the result, responds to the paymentGateway request;
4. paymentGateway responds about the result of the transaction in response to the initial merchant's POST request;

## How can I use it?
After applications started (see [Building](#Building)), you can use Swagger to make a payment. Every application has its own Swagger page:

`MerchantApi`

`PaymentGateway`

`BankApi`

#### To send shoppers Bankdata to the merchant:
Make POST request in `MerchantApi` using Body:

URL: `/Payments`
```
{
  "cardHolderName": "John Doe",
  "cardNumber": "1111-1111-1111-1111",
  "cvc": "111",
  "expirationDate": "2022-01-01",
  "amount": 11
}
```
Response Body:
```
{
  "operationId": "7c0d6f2b-3899-485d-ad6b-6c22d6ee6750",
  "status": "Paid",
  "result": "Success"
}
```
Using "operationId" you can make a GET request in MerchantApi to get the transaction details:

URL: `/Payments/id:guid?id=7c0d6f2b-3899-485d-ad6b-6c22d6ee6750`

## Developing
Clone this repository:
```
$ git clone https://github.com/elenapakumirzakova/Checkout.PaymentGateway.git
```

## Building
Requirements:
  - .NET 5.0
  - MS SQL Server 2019;
  - Visual Studio IDE

To run the project locally:
  - import cloned project to your Visual Studio IDE
  - replace in `appsettings.json` with your database configuration:
 
```
 "System": {
 "Sql": {
 "Host": "your hostname",
 "Database": "your database name",
 "UserId": "encrypted UserId",
 "Password": "encrypted Password",
 "ConnectionStringTemplate": "Server={0};Database={1};User Id={2};Password={3};"
 }}
 ```
 **NOTE:** "UserId" and "Password" supposed to be encrypted.
Please find the encryption method in:
```Checkout.PaymentGateaway\Checkout.PaymentGateaway.Shared\Extensions\Extensions.cs```

  - To create new database use script in PM console as follows:
  ```
  Add-Migration "your migrationname"
  Update-Database
  ```
  - In Solution set multiple startup Projects:
 
  `Checkout.Bank, Checkout.Merchant, Checkout.PaymentGateaway`
  - Start application  
 
## Configuration
To switch Bank integration, please update `appsettings.json`:
```
"Bank": {
    "Endpoint": "your hostname",
    "PaymentProviderUniqueToken": "your unique token"
    }
 ```

## Features
  - Allows the merchant to retrieve payment operation by Id from gateway;  
  - All data is stored in the database;
  - `cardNumber` is now masked
  - Logging with Serilog in `Checkout.PaymentGateaway;`
  - `cardNumber`, `Password`, `UserId` are now encrypted;
  - Validation of the shoppers `cardNumber`;
 
## ToDo List
What supposed to be done next:
  - Authorization and authentication
  - Errorhandler
  - Unit Tests
  - CI Deploy (Cloud preffered)

## Contributing
If you'd like to contribute, please fork the repository and use a feature branch.
Pull requests are warmly welcome :-)

## Links
Author: https://www.linkedin.com/in/yelena-pak-umirzakova

## License
Its use is governed by MIT License (X11 License).
