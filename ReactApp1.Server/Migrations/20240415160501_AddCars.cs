using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddCars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"INSERT INTO Car(CategoryId, Brand, Model, DailyPrice) VALUES " +
                $"(1, 'Toyota', 'Carmy', 50), " +
                $"(1, 'Honda', 'Civic', 45), " +
                $"(2, 'Toyota', 'Yaris', 50), " +
                $"(3, 'Ford', 'Fiesta', 40)"
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
