# Employees API

The Employees API is an HTTP API used to access data about employees.

## GET `/employees`

This route returns a list of all employees

```json
{
  "employees": [
    {
      "id": "c04ab9a0-e398-471d-8cfe-95317ccf0a20",
      "firstName": "John",
      "lastName": "Doe",
      "facility": {
        "id": "1e95757d-066d-41a6-b5d2-5e34b71d6051",
        "alias": "BDU5",
        "type": 0,
        "location": {
          "id": "f103279-fe1c-4d68-8043-09ad13f13207",
          "address": "8350 Quintero St.",
          "city": "Commerce City",
          "stateCode": "US",
          "postalCode": "80022",
          "countryCode": "CO"
        }
      }
    }
  ],
  "count": 1
}
```
