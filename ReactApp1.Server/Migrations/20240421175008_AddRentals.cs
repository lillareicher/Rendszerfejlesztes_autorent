using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddRentals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"INSERT INTO Rental(UserId, CarId, FromDate, ToDate, Created) VALUES " +
                $"(1, 1, '2024-05-06', '2024-05-10', '2024-04-25 20:05:45'), " +
                $"(1, 2, '2024-06-10', '2024-06-15', '2024-05-28 15:10:02'), " +
                $"(1, 1, '2024-05-12', '2024-05-23', '2024-05-03 13:50:57'), " +
                $"(1, 3, '2024-07-20', '2024-07-26', '2024-05-10 08:02:10') "
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
