# Currency-Converter
This repository helps currency converter test cases with Verifications in detail

Convert the amount from one currency to another currency.

10 EUR => 11.33 USD

This project is a skeleton with dependencies to Cucumber (for java) and Specflow (for csharp). It can be used to apply the 
BDD (Behavior Driven Development) method.

**Converter Object
**
Without API KEY

Requests goes to https://free.currencyconverterapi.com/api/v6/ base url without api key;

var converter = new Converter();

**Synchronous Functions
**
Basic Conversion

var result = converter.Convert(1, CurrencyType.USD, CurrencyType.EUR);
