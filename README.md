# Metro Template

Do you want to quickly make a great looking modern app with database support? Are you frustrated by example templates that are too code/dependancy heavy? Do you want to waste less time on theme and design? If you have said yes to any of these questions, read on!

MetroTemplate is a project file designed to quick-start WPF app creation with a clean Metro style UI and SQLite database! The project files are purposefully kept simple for ease of use and to keep the learning curve low. Sometimes less is more ;)

This template has code that covers:

* Pop ups
* Menu with controls
* Database class with basic functionality
* Custom sub form popup
* USER CONTROLLED PERSISTENT theme (seriously, if you let the user decide how it looks, it will always look right!)
* DataGrid binding
* Database selector using Windows openFileDialog
* Persists user settings even after app closes
* Basic logging

## Getting Started

* Clone the repo
* Double click the sln file
* Start making it your own!

### Database Functionality

You must have an SQLite Database (.db) file with a single table defined in CREATESQL.txt for the app to run properly with a database. Alternatively, you can comment or remove the database calls.

One of the many ways to set up the SQLite database:

* Download DB Browser for SQLite [here](https://sqlitebrowser.org/dl/)
* Click "New Database"
* Create an SQLite database with the .db extension
* Click (navigate) the "Execute SQL" tab
* Copy and paste CREATESQL.txt (in MetroTemplate project folder) into the SQL dialog box and run

The app uses a ConnectionString setting to load the database. Either hardcode it to the setting, or go to Settings > Connect Database... in the app and load your database that way. It will remember your selection!

### Prerequisites

Nothing yet

## Deployment

Use Visual Studio to publish the app (right click solution > Publish, follow directions for your use case)

IMPORTANT
The folders x64 and x86 found in the root of the project files MUST CONTAIN Sqlite.Interop.dll! Doing this will ensure the Application Files in the build will include the library in the setup manifest. Visual Studio fails to automagically do this for some reason. The project is already set up for this.

## Built With

* DB Browser for SQLite [here](https://sqlitebrowser.org/dl/)
* Visual Studio 2017

## Contributing

This repository is closed for contribution, as it is intended to be a base template to expand on and make your own :)

## Authors

* **X3r0byte** - *Initial work* - [X3r0byte](https://github.com/X3r0byte)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* MahApps and its contributors for Metro UI!
* Good directions on how to set up a visual query editor for SQLite [here](https://kenfallon.com/adding-sqlite-as-a-datasource-to-sqleo/)
* SQLeo Download [here](https://sourceforge.net/projects/sqleo/)
* SQLite JDBC Driver for SQLeo [here](https://mvnrepository.com/artifact/org.xerial/sqlite-jdbc)
