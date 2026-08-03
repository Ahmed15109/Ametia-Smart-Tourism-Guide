# Repository audit and cleanup record

Audit date: 2026-08-03

This document records how the original collaborative repository was classified and normalized. It does not contain credential values.

## Audit scope

The audit covered the root Git index and history metadata, the nested backend repository, all four discovered .NET projects, solution membership, controllers, DTOs, entities, repositories, migrations, configuration, publish artifacts, both static frontends, API calls, local links, external libraries, build outputs, and secret-shaped content.

## Canonical components

| Component | Evidence | Final location |
|---|---|---|
| Web API | The only project listed in the original `Grad.sln`; consumed by both root frontends. | `backend/Ametia.Api/` |
| Customer frontend | Root `UserInterface` pages consume the API and contain the complete customer workflow. | `frontend/user/` |
| Admin dashboard | Root `DashBordWeb` is newer than the imported MVC prototypes and consumes the same Web API. | `frontend/admin/` |

## Original Git representation

- The parent repository stored `Api/grad` as a `160000` gitlink at an older nested commit.
- The working directory contained a newer checkout at `Api/Grad` with its own `.git` directory, two modified backend files, and two untracked empty migrations.
- No parent `.gitmodules` file was present.
- The canonical backend working tree was flattened into normal parent-repository files.
- The nested `.git` directory and copied repository metadata were removed. Parent history was not rewritten.

## File and directory classification

The following rules cover every original path and explain the disposition of each category.

| Classification | Original paths or patterns | Disposition |
|---|---|---|
| Required production source | `Api/Grad/Grad/Controllers/**/*.cs`, `DTO/**/*.cs`, `Models/**/*.cs`, `Repo/**/*.cs`, `Program.cs` | Retained under the canonical backend, with folder-only naming cleanup. |
| Required build/runtime configuration | Canonical `.sln`, `.csproj`, tool manifest, launch settings, `appsettings*.json` | Retained, renamed, sanitized, and documented. |
| Required database source | Initial migration, image-byte migration, embassy image-column migration, model snapshot | Retained without schema edits. |
| Required customer source | Referenced pages in `UserInterface/` and `config.js` | Retained under `frontend/user`; links and verified API mismatches corrected. |
| Required admin source | CRUD/list pages in `DashBordWeb/` | Retained under `frontend/admin`; API URL centralized. |
| Required external assets | Bootstrap, Font Awesome, Leaflet, OpenStreetMap, Google Fonts, Dialogflow, and referenced hosted images | Retained as external references after URL inventory. |
| Duplicate implementation | `Api/Grad/DashBord/`, `Api/Grad/DashbordGrad/`, `Api/Grad/GarduationDashbord/` | Removed after solution/dependency/history comparison. |
| Generated output | Every `.vs/`, `bin/`, `obj/`, publish/PubTmp output, DLL, PDB, cache, generated executable | Removed; covered by root `.gitignore`. |
| IDE/local-only artifact | `*.csproj.user`, `*.pubxml.user`, `UserInterface/.vscode/`, root/nested Visual Studio state | Removed; covered by root `.gitignore`. |
| Secret/environment-specific | Configured canonical connection string, local-machine connection strings, Web Deploy profiles | Credential-bearing canonical JSON was sanitized; environment-specific profiles were removed. |
| Obsolete experiment/documentation | Nested README with unimplemented Flutter/JWT claims, weather-forecast HTTP sample | README replaced; HTTP sample replaced with real Ametia routes. |
| Empty migration experiment | `up303`, `dto`, `as`, and `asdx` migration pairs | Removed after verifying empty `Up` and `Down` bodies. |
| Unsafe abandoned page | Unreferenced `forgot-password.html` | Removed because the API action is not a recovery flow and can establish session state by email lookup. |
| Unknown or contract-sensitive | Unused DTOs, transport-provider model/read route, legacy spellings and image-path properties still represented in schema | Retained to avoid inventing intent, breaking routes, or changing the database schema. |

Cleanup targets were sent to the Windows Recycle Bin during this working session, making the deletion locally recoverable. They are no longer part of the repository tree.

## Duplicate dashboard comparison

### `DashBord`

- ASP.NET Core MVC scaffold with only a Home controller and default views.
- Referenced the API project but was not a solution member.
- Last path update predated the root static admin dashboard.

### `DashbordGrad`

- Another MVC scaffold with one partial city controller, duplicated models, and reverse-engineering configuration.
- Referenced the API project but was not a solution member.
- Did not provide a complete admin workflow.

### `GarduationDashbord`

- More complete direct-to-database MVC prototype with CRUD controllers/views, search actions, a chart-data action, and transport-provider management.
- Used its own duplicated DbContext/models and a separate migration rather than the shipped Web API.
- Was not included in `Grad.sln`, was not referenced by either root frontend, and predated the root static dashboard.
- Its unique chart and transport-provider screens were prototype-only and were never integrated into the canonical API/frontend path.

The root static dashboard was therefore selected as the canonical admin application. Keeping the three MVC trees would leave multiple competing persistence paths and four dashboard projects in a portfolio repository.

## Backend findings

- Target framework: .NET 8 / ASP.NET Core 8.
- EF Core and SQL Server provider: 9.0.0.
- Swagger/OpenAPI: Swashbuckle.AspNetCore 6.6.2, Development only.
- Persistence: ten SQL Server tables with EF Core migrations and a generic repository.
- Authentication: in-memory ASP.NET Core session, not JWT.
- CORS: credentialed allow-list for customer ports 5500 and admin ports 5501 on localhost/127.0.0.1.
- No automated tests, CI, container, or production deployment configuration existed.

Low-risk backend fixes removed duplicate cache registration and duplicate routing middleware, put routing/CORS/session/authorization in a single clear order, added HTTPS redirection, made CORS/session settings configurable, shortened the default session idle timeout, awaited two dropped update tasks, and added missing not-found checks. Routes, namespaces, database entities, and schema were not renamed.

## Frontend findings

- Both applications are plain static HTML/CSS/JavaScript; neither has npm dependencies or a build pipeline.
- The old customer config used only the retired hosted API, while every admin page embedded that URL independently.
- Customer category/detail pages use base64 image bytes returned by the API.
- Leaflet/OpenStreetMap maps are implemented on generic and embassy detail pages.
- Dialogflow Messenger is embedded on the customer home page.
- Search/filter behavior is client-side; city labels now come from the actual `/api/City/GetCity` response instead of nonexistent `cityName` DTO properties.
- Case-sensitive filename mismatches and a nonexistent embassy city route were corrected.
- Fabricated dashboard statistics/example personal records and fabricated customer rating defaults were removed.

## Secret and personal-data review

- One configured remote SQL Server credential was found in the canonical working configuration and was replaced with an empty committed value.
- Three legacy local-machine connection strings were confined to removed duplicate projects.
- Three Web Deploy profiles and their user-specific companions were removed.
- No API key, JWT secret, SMTP credential, private key, or bearer token was required by the retained source.
- Example names/emails presented as recent admin users were removed from the dashboard.
- The Dialogflow agent identifier remains because it is a public client-side integration identifier, not a server credential.

No secret values are reproduced in this document.

## Deliberately retained suspicious names

- `Grad` namespaces: changing them would create broad source and migration churn without user-facing value.
- `Distnation`, `Resturant`, `Embasse`, `Tourismt_Place`, `Type_place`: these names participate in public routes, serialized contracts, or EF metadata.
- `SacondName`, `ScialMedia`, `OpiningHour`, `Discription`: existing API/database property spellings are retained for compatibility.
- `TransportProvider`: no current frontend renders it, but the schema and `/api/Services/Get All TransPort Provider` read route are real.
- Composite DTO files: currently unused by actions, but retained because their intended future role cannot be disproved safely.
- Remaining nullable `Image` properties on entities: retained because removing mapped properties could change the database model.

## Final canonical structure

Only one backend solution/project, one customer frontend, and one admin dashboard remain. See the root README for the current tree and verified local run commands.
