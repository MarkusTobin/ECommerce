## Configuring `appsettings.json` for the Application

In order to make this work, you'll need to add a `appsettings.json` file in the `ECommerce.Api` project with the appropriate configuration settings.

### Steps:

1. **Create the `appsettings.json` file** in the `ECommerce.Api` project.
2. **Add the following configuration sections**:
    - **ConnectionString**: Required for connecting to MongoDB.
    - **Jwt**: Required for JWT token configuration.

### Example of `appsettings.json`:

```json
{
  "MongoDBSettings": {
    "ConnectionString": "your_mongo_connection_string_here",
    "DatabaseName": "your_database_name_here"
  },
  "Jwt": {
    "Key": "your_secret_key_here",
    "Issuer": "your_issuer_here",
    "Audience": "your_audience_here",
    "ExpiryMinutes": 60
  }
}
