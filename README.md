# SQLCryptExt

Additional encryption options for SQL Server

## Encryption options

### Hashing

- PBKDF2
  - SELECT * FROM dbo.PBKDF2('text', 'iterations.salt')

## Dependencies

- [SimpleCrypto.net](https://github.com/martinisaksen/SimpleCrypto.net)

## Compatability

This code has been tested against the following versions:

- 2016
- 2017

### Recognition

This repo is heavily influenced by the work of Solomon Rutzky. I recommend you check [his article series](https://sqlquantumleap.com/2017/08/07/sqlclr-vs-sql-server-2017-part-1-clr-strict-security/) that helped start this project.

The hashing algorithim comes from Shawn Mclean and his [SimpleCrypto.net](https://github.com/shawnmclean/SimpleCrypto.net) project.