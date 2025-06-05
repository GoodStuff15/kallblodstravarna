# Kallblodstravarna API Documentation



## Type: Authentication

```
POST
Register
https://localhost:7064/api/auth/register
﻿

Body
raw (json)
json
{
  "username": "Peterr",
  "password": "Parker123#",
  "role": "Admin"
}
```
```
POST
Login
https://localhost:7064/api/auth/login
﻿

Body
raw (json)
json
{
  "username": "Peterr",
  "password": "Parker123#",
  "role": null
}
```
```
POST
Refresh Token
https://localhost:7064/api/auth/refresh-token
﻿

Body
raw (json)
json
{
  "userId": 3,
  "refreshToken": "xWs9wdVmphT/ifS4X0H4nxEn++dYr8ZWIUG1tcC3EdE="
}
```
```
GET
Test Auth (Auth)
https://localhost:7064/api/auth
﻿

Authorization
Bearer Token
Token
<token>
```
```
POST
Logout (Auth)
https://localhost:7064/api/auth/logout
﻿

Authorization
Bearer Token
Token
<token>
```
```
GET
Test Admin (Admin)
https://localhost:7064/api/auth/admin-only
﻿

Authorization
Bearer Token
Token
<token>
```
```
GET
Get All Users (Admin)
https://localhost:7064/api/auth/GetAllUsers
﻿

Authorization
Bearer Token
Token
<token>
```
```
DELETE
Delete User (Admin)
https://localhost:7064/api/auth/1
﻿

Authorization
Bearer Token
Token
<token>
```

## Type: Accessibility

```
GET
Get All
https://localhost:7064/api/Accessibility/Get all accessibilities
```
```
GET
Get By Id
https://localhost:7064/api/Accessibility/3
```
```
PUT
Modify By Id
https://localhost:7064/api/Accessibility/2
﻿

Body
raw (json)
json
{
    "name": "Hörselhjälpmedel",
    "description": "Utrustning för hörselskadade"
}
```
```
DELETE
Delete By Id
https://localhost:7064/api/Accessibility/2
```
```
POST
Add
https://localhost:7064/api/Accessibility/

Body
raw (json)
json
{
    "name": "Hörselhjälpmedel",
    "description": "Utrustning för hörselskadade"
}
```

## Type: Accomodation
﻿
```
GET
Available Receptionist (incl. noOfGuests)
https://localhost:7064/api/Accomodation/availableReceptionist
﻿

Authorization
Bearer Token
Token
<token>
Body
raw (json)
json
{
    "start": "2025-06-10T00:00:00",
    "end": "2025-06-12T00:00:00",
    "noOfGuests": 2
}
```
```
GET
Available Guest (excl. noOfGuests)
https://localhost:7064/api/Accomodation/availableGuest
﻿

Authorization
Bearer Token
Token
<token>
Body
raw (json)
json
{
  "start": "2025-06-10T00:00:00",
  "end": "2025-06-12T00:00:00"
}
```
```
GET
Get All
https://localhost:7064/api/Accomodation/Get all Accomodations
﻿```
```
GET
Get By Id
https://localhost:7064/api/Accomodation/2
﻿```
```
PUT
Modify By Id
https://localhost:7064/api/Accomodation/3


Body
raw (json)
json
{
    "name": "300A",
    "maxOccupancy": 2,
    "accomodationTypeId": 1,
    "accessibilityIds": [1]
}
```
```
DELETE
Delete By Id
https://localhost:7064/api/Accomodation/2
﻿```
```
POST
Add
https://localhost:7064/api/Accomodation


Body
raw (json)
json
{
    "name": "300A",
    "maxOccupancy": 2,
    "accomodationTypeId": 1,
    "accessibilityIds": [1]
}
```

## Type:  AccomodationType
﻿
```
GET
Get All
https://localhost:7064/api/AccomodationType/Get all accomodationType
﻿```
```
GET
Get By Id
https://localhost:7064/api/AccomodationType/1
﻿```
```
PUT
Modify By Id
https://localhost:7064/api/AccomodationType/1


Body
raw (json)
json
{
    "id": 1,
    "name": "Enkelrum",
    "description": "Ett rum med en säng",
    "basePrice": 850.00
}
```
```
DELETE
Delete By Id
https://localhost:7064/api/AccomodationType/1
﻿```
```
POST
Add
https://localhost:7064/api/AccomodationType/
﻿

Body
raw (json)
json
{
  "name": "Enkelrum",
  "description": "Ett enkelt rum",
  "basePrice": 1000
}
```
## Type: AdditionalOption
﻿
```
GET
Get All
https://localhost:7064/api/AdditionalOption
﻿```
```
GET
Get By Id
https://localhost:7064/api/AdditionalOption/1
﻿```
```
PUT
Modify By Id
https://localhost:7064/api/AdditionalOption/4


Body
raw (json)
json
{
    "name": "Frukost",
    "description": "Bufféfrukost varje morgon",
    "price": 120.00,
    "perGuest": true,
    "perNight": true
}
```
```
DELETE
Delete By Id
https://localhost:7064/api/AdditionalOption/1
﻿```
```
POST
Add
https://localhost:7064/api/AdditionalOption
﻿

Body
raw (json)
json
{
    "name": "Frukost",
    "description": "Bufféfrukost varje morgon",
    "price": 120.00,
    "perGuest": true,
    "perNight": false
}
```

## Type: Booking
﻿
```
GET
Get All Bookings Overview
https://localhost:7064/api/booking/overview
﻿

Authorization
Bearer Token
Token
<token>
```
```
GET
Get All Bookings Detailed
https://localhost:7064/api/booking/Detailed overview
﻿

Authorization
Bearer Token
Token
<token>
```
```
GET
Get Customers Bookings By Id
https://localhost:7064/api/Booking/customer/17
﻿```
```
GET
Get By Customer Id And Customer Email
https://localhost:7064/api/booking/customersearch?customerId=1&email=anna@example.com
﻿

Query Params
customerId
email
```
```
GET
Get By Id
https://localhost:7064/api/Booking/21
﻿

Authorization
Bearer Token
Token
<token>
```
```
PUT
Cancel By Id
https://localhost:7064/api/booking/11
﻿```
```
PUT
Modify By Id
https://localhost:7064/api/booking/modify/21
﻿

Body
raw (json)
View More
json
    {
        "bookingId": 21,
        "checkIn": "2025-06-12T00:00:00",
        "checkOut": "2025-06-14T00:00:00",
        "accomodationId": 2,
        "cost": 1000,
        "guests": [
            {
                "firstName": "Adaxcvxcm",
                "lastName": "Evass",
                "age": 12
            },
                        {
                "firstName": "Alkexander",
                "lastName": "Evass",
                "age": 12
            }
        ],
        "additionalOptionsId": [2,3,4]
    }
```
```
DELETE
Delete By Id
https://localhost:7064/api/booking/11
﻿

Authorization
Bearer Token
Token
<token>
```
```
POST
Add
https://localhost:7064/api/booking/
﻿

Authorization
Bearer Token
Token
<token>
Body
raw (json)
View More
json
{
  "checkIn": "2025-06-19",
  "checkOut": "2025-06-20",
  "accomodationId": 2,
  "customerId": 1,
  "cost": 1000,
  "guests": [
    {
      "firstName": "Adaxcvxcm",
      "lastName": "Eva",
      "age": 12
    }
  ],
  "additionalOptionIds": [
    1,2,3
  ]
}
```


## Type: PriceRequest
https://localhost:7064/api/booking/Pricerequest
﻿```
POST
Body
raw (json)
json
{
  "AccomodationId": 2,
  "GuestCount": 2,
  "Duration": 2,
  "AdditonalOptionIds": [1, 2]
}
```
## Type: Customer
﻿
```
GET
Get By Id
https://localhost:7064/api/Customer/1
﻿```
```
POST
New Customer
https://localhost:7064/api/Customer
﻿

Authorization
Bearer Token
Token
<token>
Body
raw (json)
json
{
  "type": "Kung",
  "firstName": "David",
  "lastName": "Filip",
  "email": "asdassd@asdasd.com",
  "phoneNumber": "0700001234",
  "paymentMethod": "Cash"
}
```
## Type: PriceChanges
﻿
```
GET
Get All
https://localhost:7064/api/PriceChanges/Get all price changes
﻿```
```
GET
Get By Id
https://localhost:7064/api/PriceChanges/2
﻿```
```
PUT
Modify By Id
https://localhost:7064/api/PriceChanges/2
﻿

Body
raw (json)
json
{
    "priceChange": 0.8,
    "type": "Kompis till chefen"
}
```
```
DELETE
Delete By Id
https://localhost:7064/api/PriceChanges/2
﻿```
```
POST
Add
https://localhost:7064/api/PriceChanges/
﻿

Body
raw (json)
json
{
    "priceChange": 0.8,
    "type": "Kompis till chefen"
}
```
