[![CircleCI](https://circleci.com/gh/pakosel/transactions-api.svg?style=svg)](https://circleci.com/gh/pakosel/transactions-api)

# transactions-api

Storing and aggregating operations of the Stock market Wallet (buy, sell, dividend, etc.). Ability to view current Position size for the given stock and whole Wallet value

## Building and Running
```
sudo docker-compose build
sudo docker-compose up -d
```

After running API endpoint *Swagger* documentation available at:
```
http://localhost:5000/swagger/
```

PostgreSQL pgAdmin available at:
```
http://localhost:54888/browser/
```
