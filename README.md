# IsItConvergent School Edition

IsItConvergent is a web application for rating and reviewing Linux applications across multiple device form factors (Desktop, Tablet, Mobile, etc.). It allows users to submit usability ratings, view community feedback, and manage app data in a modern, responsive interface.

## Features

- **Linux App Catalog:** Browse a grid of Linux applications, each with details, icons, and version info.
- **Multi-Form Factor Ratings:** Users can rate apps for different device types (e.g., Desktop, TV, Tablet, Mobile).
- **Community Reviews:** See average ratings and latest user reviews for each form factor.
- **User Authentication:** ASP.NET Core Identity for user registration, login, and role management.
- **Search & Sort:** Quickly search apps by name and sort them alphabetically or by version.
- **Responsive Design:** Built with Bootstrap for usability on all screen sizes.

## Technologies Used

- **ASP.NET Core MVC** (C#)
- **Entity Framework Core** (with SQLite or other DB)
- **Bootstrap 5**
- **jQuery**
- **Identity Framework** for authentication and roles

## Project Structure

- `/Models/DbModels/` — Entity Framework Core models (e.g., `LinuxApp`, `AppUsabilityRating`)
- `/Models/ViewModels/` — View models for passing data to views
- `/Models/DataModels/` — Methods for converting between DbModels and ViewModels
- `/Controllers/` — MVC controllers (e.g., `HomeController`)
- `/Views/Home/` — Razor views and partials (e.g., `_LinuxAppCard.cshtml`, `ProductPage.cshtml`)
- `/wwwroot/js/` — JavaScript files (e.g., `site.js` for AJAX and UI logic)
- `/Migrations/` — EF Core migration files

## Getting Started

1. **Clone the repository:**
   ```sh
   git clone https://github.com/YOUR-USERNAME/IsItConvergent_SchoolEdition.git
   cd IsItConvergent_SchoolEdition/IsItConvergent
   ```

2. **Set up the database:**
   ```sh
   dotnet ef database update
   ```

3. **Run the application:**
   ```sh
   dotnet run
   ```

4. **Open in your browser:**  
   Visit `http://localhost:5000` (or the port shown in your terminal).

## Usage

- **Browse Apps:** The homepage displays a searchable, sortable grid of Linux apps.
- **View Details:** Click an app card to see detailed info and community ratings.
- **Submit Ratings:** Logged-in users can submit usability ratings and comments for each form factor.
- **Admin Features:** (**Coming Soon**) Admins can manage apps and user access levels.

## Contributing

Pull requests are welcome! Please open an issue first to discuss major changes.

## License

None at this time. 

---

**IsItConvergent School Edition** — Rate, review, and discover Linux apps for every device!
