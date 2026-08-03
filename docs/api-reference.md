# API endpoint reference

This inventory is generated from the current controller attributes. It records existing public spellings exactly; misspellings are retained because the static frontends consume them.

- Local origin: `https://localhost:7124`
- Base path: `/api`
- Swagger UI in Development: `/swagger`
- Authentication scheme: ASP.NET Core session cookie for customer profile state; no endpoint uses `[Authorize]`

## Banks — `/api/Bank`

| Method | Route | Request or behavior |
|---|---|---|
| POST | `/api/Bank/CreateBank` | Multipart `Bank`, optional `ImageFile`; creates a bank. |
| GET | `/api/Bank/GetAllBanks` | Returns all banks. |
| GET | `/api/Bank/LoadBankById/{id}` | Returns one bank or `404`. |
| PUT | `/api/Bank/UpdateBankById` | Multipart `Bank`; the ID is read from the form body. |
| DELETE | `/api/Bank/DeleteBank?id={id}` | Deletes by query-string ID. |

## Cities — `/api/City`

| Method | Route | Request or behavior |
|---|---|---|
| POST | `/api/City` | JSON string body containing the city name. |
| GET | `/api/City/loadcity?id={id}` | Returns one city and stores its ID in session. |
| PUT | `/api/City/UpdateCity` | JSON `City` body. |
| DELETE | `/api/City/DeleteCity/{id}` | Deletes a city unless a relationship prevents it. |
| GET | `/api/City/GetCity` | Returns all cities. |

## City discovery — `/api/Distnation`

| Method | Route | Behavior |
|---|---|---|
| GET | `/api/Distnation/Tack%20one%20city%20from%20list` | Returns all cities. The literal route contains spaces. |
| GET | `/api/Distnation/TopReatingBank/{id}` | Banks in a city, descending by rating. |
| GET | `/api/Distnation/TopHotel/{id}` | Hotels in a city, descending by rating. |
| GET | `/api/Distnation/TopRestaurant/{id}` | Restaurants in a city, descending by rating. |
| GET | `/api/Distnation/TopTourismt_Place/{id}` | Tourism places in a city, descending by rating. |
| GET | `/api/Distnation/TopEmbassies/{id}` | Embassies in a city. |
| GET | `/api/Distnation/TopEntertainmentPlace/{id}` | Entertainment places in a city. |

## Embassies — `/api/Embassies`

| Method | Route | Request or behavior |
|---|---|---|
| POST | `/api/Embassies/CreateEmbasses` | Multipart `Embasse`, optional `ImageFile`. |
| GET | `/api/Embassies/LoadEmbasseById/{id}` | Returns one embassy or `404`. |
| PUT | `/api/Embassies/UpdateEmbasseById` | Multipart `Embasse`. |
| DELETE | `/api/Embassies/DeleteEmbasse/{id}` | Deletes by path ID. |
| GET | `/api/Embassies/GetAllEmbasse` | Returns all embassies. |

## Entertainment places — `/api/EntertainmentPlace`

| Method | Route | Request or behavior |
|---|---|---|
| POST | `/api/EntertainmentPlace/CreateEntertainmentPlace` | Multipart `EntertainmentPlace`, optional `ImageFile`. |
| GET | `/api/EntertainmentPlace/LoadEntertainmentPlaceById/{id}` | Returns one entertainment place or `404`. |
| PUT | `/api/EntertainmentPlace/UpdateEntertainmentPlaceById` | Multipart `EntertainmentPlace`. |
| DELETE | `/api/EntertainmentPlace/DeleteEntertainmentPlace?id={id}` | Deletes by query-string ID. |
| GET | `/api/EntertainmentPlace/GetAllEmbasse` | Returns all entertainment places; the legacy action route is intentionally unchanged. |

## Hotels — `/api/Hotels`

| Method | Route | Request or behavior |
|---|---|---|
| POST | `/api/Hotels/CreateHotel` | Multipart `Hotel`, optional `ImageFile`. |
| GET | `/api/Hotels/LoadHotelById/{id}` | Returns one hotel or `404`. |
| PUT | `/api/Hotels/UpdateHotelById` | Multipart `Hotel`. |
| DELETE | `/api/Hotels/DeleteHotel?id={id}` | Deletes by query-string ID. |
| GET | `/api/Hotels/GetAllHotel` | Returns all hotels. |

## Restaurants — `/api/Resturant`

| Method | Route | Request or behavior |
|---|---|---|
| POST | `/api/Resturant/CreateRestaurant` | Multipart `Restaurant`, optional `ImageFile`. |
| GET | `/api/Resturant/LoadRestaurantById/{id}` | Returns one restaurant or `404`. |
| PUT | `/api/Resturant/UpdateRestaurantById` | Multipart `Restaurant`. |
| DELETE | `/api/Resturant/DeleteRestaurant?id={id}` | Deletes by query-string ID. |
| GET | `/api/Resturant/GetAllRestaurant` | Returns all restaurants. |

## Read-only service aggregation — `/api/Services`

| Method | Route | Behavior |
|---|---|---|
| GET | `/api/Services/GetCity` | Returns all cities. |
| GET | `/api/Services/GetBank` | Returns all banks. |
| GET | `/api/Services/GetEmbasses` | Returns all embassies. |
| GET | `/api/Services/GetEntartinmentPlace` | Returns all entertainment places. |
| GET | `/api/Services/GetHotel` | Returns all hotels. |
| GET | `/api/Services/GetRestuarant` | Returns all restaurants. |
| GET | `/api/Services/GetToursimPlaces` | Returns all tourism places. |
| GET | `/api/Services/Get%20All%20TransPort%20Provider` | Returns all transport providers. The literal route contains spaces. |

## Tourism places — `/api/TourismPlace`

| Method | Route | Request or behavior |
|---|---|---|
| POST | `/api/TourismPlace/CreateTourismPlace` | Multipart `Tourismt_Place`, optional `ImageFile`. |
| GET | `/api/TourismPlace/LoadTourismt_PlaceById/{id}` | Returns one tourism place or `404`. |
| PUT | `/api/TourismPlace/UpdateTourismt_PlaceById` | Multipart `Tourismt_Place`. |
| DELETE | `/api/TourismPlace/DeleteTourismt_Place?id={id}` | Deletes by query-string ID. |
| GET | `/api/TourismPlace/GetAllTourismt_Place` | Returns all tourism places. |

## Place types — `/api/TypePlaces`

| Method | Route | Request or behavior |
|---|---|---|
| POST | `/api/TypePlaces/AddNewType_place` | JSON `Type_place` body. |
| GET | `/api/TypePlaces/loadType_place/{id}` | Returns one place type or `404`. |
| PUT | `/api/TypePlaces/UpdateType_place` | JSON `Type_place` body. |
| DELETE | `/api/TypePlaces/DeleteType_place?city={id}` | Deletes a place type; the query parameter is named `city`. |
| GET | `/api/TypePlaces/GetType_place` | Returns all place types. |

## Users and session — `/api/User`

| Method | Route | Request or behavior |
|---|---|---|
| POST | `/api/User/Register` | Multipart `User`, optional `ImageFile`; hashes the submitted password before storage. |
| POST | `/api/User/Login/{login}/{Password}` | Validates credentials and creates a session. Credentials are in route segments; see security warning below. |
| GET | `/api/User/LoadUserById/{id}` | Returns one complete user entity or `404`. |
| PUT | `/api/User/Update` | Multipart `User`; looks up the existing user by submitted email. |
| POST | `/api/User/ForgotPassword/{Email}` | Account lookup that stores the email in session; this is not a password-reset flow. |
| GET | `/api/User/Users` | Returns all complete user entities. |
| DELETE | `/api/User/DeleteUser?id={id}` | Deletes by query-string ID. |
| GET | `/api/User/Profile` | Returns the user identified by session email; returns `401` when the session is absent. |
| POST | `/api/User/Logout` | Clears the session. |

## Data and persistence notes

- Controllers currently bind and return entity models directly in most actions. The four types in `DTOs/` are not used by the current actions.
- Images are accepted as `IFormFile` values and persisted as nullable `varbinary(max)` columns.
- `CityId` relationships connect the location/service entities to cities. Tourism places also require a place type; transport providers also reference a place type.
- The database enforces unique indexes for user email, city name, and place-type name.
- The generic repository uses async EF Core calls for list, lookup, create, update, and delete operations.

## Security and compatibility warning

None of the CRUD or user-management endpoints has `[Authorize]`. The profile action performs its own session check, but object lookups and all admin writes remain publicly callable when the API is reachable. User entity responses include the stored password hash. The login URL can also leak credentials through URL logging. Treat the API as an academic/local implementation until authentication, authorization, DTO projection, CSRF protection, and password handling are redesigned.
